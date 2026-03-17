using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Application.DTOs;
using BDMS.Application.Interfaces;
using BDMS.Domain.Entities;
using BDMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Security.Claims;


namespace BDMS.Application.Services
{
    public class DonationService
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IDonorRepository _donorRepository;
        private readonly IBloodInventoryRepository _bloodInventoryRepository;
        private readonly ICacheService _cache;
        private readonly INotificationService _notification;
        public DonationService(IDonationRepository donationRepository, IDonorRepository donorRepository, IBloodInventoryRepository bloodInventoryRepository, ICacheService cache, INotificationService notification)
        {
            _donationRepository = donationRepository;
            _donorRepository = donorRepository;
            _bloodInventoryRepository = bloodInventoryRepository;
            _cache = cache;
            _notification = notification;
        }

        public async Task<Donation> CreateAsync(CreateDonationDTO dto)
        {
            // Retrieve donor from repository instead of attempting to access a Controller/HttpContext User.
            var donor = await _donorRepository.GetByIdAsync(dto.DonorId);
            if (donor == null)
            {
                throw new Exception("Donor not found");
            }

            var donation = new Donation
            {
                DonorId = dto.DonorId,
                HospitalId = dto.HospitalId,
                DonationDate = DateTime.UtcNow,
                CertificateNumber = GenerateCertificateNumber(),
                Status = DonationStatus.Pending
            };

            await _donationRepository.AddAsync(donation);

            var inventory = await _bloodInventoryRepository.GetByHospitalAndBloodGroup(dto.HospitalId, donor.BloodGroup);
            if (inventory != null)
            {
                inventory.UnitsAvailable += 1;
                inventory.LastUpdated = DateTime.UtcNow;
            }

            await _donationRepository.SaveChangesAsync();
            await _cache.DeleteAsync("dashboard_stats");
            var fullDonation = await _donationRepository.GetByIdWithDetailsAsync(donation.Id);
            return fullDonation;
        }

        private string GenerateCertificateNumber()
        {
            return String.Concat("CERT-", Guid.NewGuid().ToString()[..8].ToUpper());
        }

        public async Task<List<DonationDTO>> GetAllAsync(DonationStatus? status = null, int? donorId = null)
        {
            var query = _donationRepository.QueryWithIncludes();
            if (status.HasValue)
            {
                query = query.Where(d => d.Status == status.Value);
            }
            if(donorId.HasValue)
            {
                query = query.Where(d => d.DonorId == donorId.Value);
            }
            return await query.OrderByDescending(d => d.DonationDate).Select(d => new DonationDTO
            {
                Id = d.Id,
                DonorName = d.Donor.Name,
                HospitalName = d.Hospital.Name,
                BloodGroup = d.Donor.BloodGroup.ToString(),
                DonationDate = d.DonationDate,
                Status = (int)d.Status,
            }).ToListAsync();
        }

        public async Task ApproveAsync(int donationId)
        {
            var donation = await _donationRepository.GetByIdWithDetailsAsync(donationId);
            if (donation == null)
            {
                throw new Exception("Donation not found");
            }
            if (donation.Status != DonationStatus.Pending)
            {
                throw new Exception("Only pending donations can be approved");
            }
            donation.Status = DonationStatus.Approved;
            var inventory = await _bloodInventoryRepository.GetByHospitalAndBloodGroup(donation.HospitalId, donation.Donor.BloodGroup);
            if (inventory == null)
            {
                inventory = new BloodInventory
                {
                    HospitalId = donation.HospitalId,
                    BloodGroup = donation.Donor.BloodGroup,
                    UnitsAvailable = 1
                };
                await _bloodInventoryRepository.AddAsync(inventory);
            }
            else
            {
                inventory.UnitsAvailable += 1;
            }
            await _donationRepository.SaveChangesAsync();
            await _notification.NotifyDonationUpdated(donationId, "Donation approved");
            await _cache.DeleteAsync("dashboard_stats");
        }

        public async Task RejectAsync(int donationId)
        {
            var donation = await _donationRepository.GetByIdWithDetailsAsync(donationId);
            if (donation == null)
            {
                throw new Exception("Donation not found");
            }
            donation.Status = DonationStatus.Rejected;
            await _donationRepository.SaveChangesAsync();
            await _cache.DeleteAsync("dashboard_stats");
        }

        public async Task<DonationStatsDTO> GetStatsAsync()
        {
            var cacheKey = "dashboard_stats";
            var cacheData = await _cache.GetAsync(cacheKey);
            if (cacheData != null)
            {
                return JsonSerializer.Deserialize<DonationStatsDTO>(cacheData);
            }
            var total = await _donationRepository.QueryWithIncludes().CountAsync();
            var pending = await _donationRepository.QueryWithIncludes().Where(d => d.Status == DonationStatus.Pending).CountAsync();
            var rejected = await _donationRepository.QueryWithIncludes().Where(d => d.Status == DonationStatus.Rejected).CountAsync();
            var completed = await _donationRepository.QueryWithIncludes().Where(d => d.Status == DonationStatus.Completed).CountAsync();
            var totalUnits = await _bloodInventoryRepository.QueryWithIncludes().SumAsync(i => i.UnitsAvailable);
            var stats = new DonationStatsDTO
            {
                TotalDonations = total,
                PendingDonations = pending,
                CompletedDonations = completed,
                RejectedDonations = rejected,
                TotalUnits = totalUnits
            };
            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(stats), TimeSpan.FromMinutes(5));
            return stats;
        }

        public async Task<List<Donation>> GetByDonorIdAsync(int donorId)
        {
            return await _donationRepository.GetByDonorIdAsync(donorId);
        }

        public async Task<byte[]> GenerateCertificateAsync(int donationId)
        {
            return await _donationRepository.GenerateCertificateAsync(donationId);
        }

        public async Task<Dictionary<string, int>> GetBloodGroupStatsAsync()
        {
            return await _donationRepository.GetBloodGroupStatsAsync();
        }

        public async Task<Dictionary<string, int>> GetBloodGroupStatsByUserAsync(int id)
        {
            return await _donationRepository.GetBloodGroupStatsByUserAsync(id);
        }

        public async Task<List<Donation>> GetByDonorAsync(int donorId)
        {
            return await _donationRepository.GetByDonorAsync(donorId);
        }
    }
}
