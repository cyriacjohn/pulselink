using BDMS.Infrastructure.Data;
using BDMS.Domain.Entities;
using BDMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using BDMS.Application.DTOs;
using System.Text.Json;
using BDMS.Application.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace BDMS.Infrastructure.Services
{
    public class HospitalService
    {
        private readonly BDMSDbContext _dbContext;
        private readonly ICacheService _cache;

        public HospitalService(BDMSDbContext dbContext, ICacheService cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<Hospital> CreateAsync(CreateHospitalDTO dto)
        {
            var hospital = new Hospital
            {
                Name = dto.Name,
                City = dto.City,
                Address = dto.Address,
                ContactPhone = dto.ContactPhone
            };

            await _dbContext.AddAsync(hospital);
            await _cache.DeleteAsync("hospitals");
            await _dbContext.SaveChangesAsync();

            foreach (BloodGroup group in Enum.GetValues(typeof(BloodGroup)))
            {
                var inventory = new BloodInventory
                {
                    HospitalId = hospital.Id,
                    BloodGroup = group,
                    UnitsAvailable = 0,
                    LastUpdated = DateTime.UtcNow
                };

                await _dbContext.BloodInventory.AddAsync(inventory);
            }
            await _dbContext.SaveChangesAsync();
            return hospital;
        }

        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            var cacheKey = "hospitals";
            try
            {
                var cachedData = await _cache.GetAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    return JsonSerializer.Deserialize<List<Hospital>>(cachedData) ?? Enumerable.Empty<Hospital>();
                }

                var hospitals = await _dbContext.Hospitals.ToListAsync();
                var result = hospitals.Select(h => new Hospital 
                {
                    Id = h.Id,
                    Name = h.Name,
                    City = h.City,
                    Address = h.Address,
                    ContactPhone = h.ContactPhone
                }).ToList();

                await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(result), TimeSpan.FromMinutes(10));
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                throw;
            }
        }

        public async Task<HospitalDashboardDTO> FindAsync(int hospitalId)
        {
            var cacheKey = $"hospital_dashboard_{hospitalId}";
            var cacheData = await _cache.GetAsync(cacheKey);
            if (cacheData != null)
            {
                return JsonSerializer.Deserialize<HospitalDashboardDTO>(cacheData);
            }
            var hospital = await _dbContext.Hospitals.FirstAsync(h => h.Id == hospitalId);
            var inventoryCount = await _dbContext.BloodInventory.Where(x => x.HospitalId == hospitalId).SumAsync(x => x.UnitsAvailable);
            var activeRequests = await _dbContext.BloodRequests.CountAsync(x => x.HospitalId == hospitalId && !x.IsFulfilled);
            var completedDonations = await _dbContext.Donations.CountAsync(x => x.HospitalId == hospitalId &&  x.Status == DonationStatus.Completed);
            var hospitalDashboard = new HospitalDashboardDTO
            {
                HospitalName = hospital.Name,
                InventoryCount = inventoryCount,
                ActiveRequests = activeRequests,
                CompletedDonations = completedDonations,
            };
            await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(hospitalDashboard) , TimeSpan.FromMinutes(5));
            return hospitalDashboard;
        }

        public async Task<int> CreateRequestAsync(int hospitalId, BloodRequestDTO dto)
        {
            var request = new BloodRequest
            {
                HospitalId = hospitalId,
                bloodGroup = dto.BloodGroup,
                UnitsRequired = dto.UnitsRequired,
                Priority = dto.Priority,
                IsFulfilled = false,
                RequestedAt = DateTime.UtcNow
            };
            await _dbContext.BloodRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
            return request.Id;
        }

        public async Task<List<BloodRequest>> GetOpenRequestsAsync()
        {
            try
            {
                return await _dbContext.BloodRequests.Where(x => !x.IsFulfilled).ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                throw;
            }
        }

    }
}
