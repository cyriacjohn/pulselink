using BDMS.Application.DTOs;
using BDMS.Application.Interfaces;
using BDMS.Domain.Entities;
using BDMS.Domain.Enums;
using BDMS.Domain.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.Services
{
    public class SmartMatchingService
    {
        private readonly IDonorRepository _repository;
        private readonly IBloodRequestRepository _bloodRequestRepository;
        private readonly IGoogleMapsService _googleMapsService;
        public SmartMatchingService(IDonorRepository repository, IBloodRequestRepository bloodRequestRepository, IGoogleMapsService googleMapsService)
        {
            _repository = repository;
            _bloodRequestRepository = bloodRequestRepository;
            _googleMapsService = googleMapsService;
        }

        public async Task<List<SmartMatchingDTO>> FindMatchingDonors(int requestId)
        {
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
                    Longitude = donor.Longitude
                });
                    }
            return result.OrderByDescending(x => x.Score).Take(5).ToList();
        }
    }
}
