using BDMS.Application.DTOs;
using BDMS.Application.Interfaces;
using BDMS.Domain.Entities;
using BDMS.Domain.Enums;
using BDMS.Domain.Logic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BDMS.Application.Services
{
    public class SmartMatchingService
    {
        private readonly IDonorRepository _repository;
        private readonly IBloodRequestRepository _bloodRequestRepository;
        private readonly IGoogleMapsService _googleMapsService;
        private readonly ICacheService _cache;
        private readonly IDonationRepository _donationRepository;
        public SmartMatchingService(IDonorRepository repository, IBloodRequestRepository bloodRequestRepository, IGoogleMapsService googleMapsService, ICacheService cache, IDonationRepository donationRepository)
        {
            _repository = repository;
            _bloodRequestRepository = bloodRequestRepository;
            _googleMapsService = googleMapsService;
            _cache = cache;
            _donationRepository = donationRepository;
        }

        public async Task<List<SmartMatchingDTO>> FindMatchingDonors(int requestId)
        {
            var cacheKey = $"smart_match_{requestId}";
            try
            {
                var cacheData = await _cache.GetAsync(cacheKey);
                if (cacheData != null)
                {
                    return JsonSerializer.Deserialize<List<SmartMatchingDTO>>(cacheData);
                }
                var request = await _bloodRequestRepository.GetByIdAsync(requestId);
                if (request == null)
                {
                    throw new Exception("Blood request not found");
                }
                var donors = await _repository.GetAllAsync();
                var compatibleGroups = BloodGroupCompatibility.GetCompatibleBloodGoups(request.bloodGroup);
                var filteredDonors = donors.Where(d => compatibleGroups.Contains(d.BloodGroup) && (d.LastDonatedDate == null || (DateTime.UtcNow - d.LastDonatedDate.Value).TotalDays >= 90));
                var topDonors = filteredDonors.Take(10).ToList();
                var result = new List<SmartMatchingDTO>();
                foreach (var donor in topDonors)
                {
                    var distance = await _googleMapsService.GetDistanceInKm(request.Hospital.Latitude, request.Hospital.Longitude, donor.Latitude, donor.Longitude);

                    var score = DonorScore.CalculateScore(donor, request, distance);
                    result.Add(new SmartMatchingDTO
                    {
                        Id = donor.Id,
                        Name = donor.Name,
                        BloodGroup = donor.BloodGroup,
                        LastDonatedDate = donor.LastDonatedDate,
                        Score = score,
                        Latitude = donor.Latitude,
                        Longitude = donor.Longitude,
                        HospitalLatitude = request.Hospital.Latitude,
                        HospitalLongtitude = request.Hospital.Longitude,
                        Distance = distance
                    });
                }
                await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(result), TimeSpan.FromMinutes(15));
                return result.OrderByDescending(x => x.Score).Take(5).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                throw;
            }
        }

        public async Task<Donation> CreateDonationRequest(NotifyDonorsDTO dto)
        {
            var request = await _bloodRequestRepository.GetByIdAsync(dto.RequestId);
            var donation = new Donation
            {
                HospitalId = request.HospitalId,
                DonorId = dto.DonorId,
                DonationDate = DateTime.UtcNow,
                Status = DonationStatus.Pending
            };
            await _donationRepository.AddAsync(donation);
            await _donationRepository.SaveChangesAsync();
            return donation;

        }
    }
}
