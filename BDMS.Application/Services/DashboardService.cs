using BDMS.Application.DTOs;
using BDMS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BDMS.Application.Services
{
    public class DashboardService
    {
        private readonly IDonationRepository _donationRepository;
        private readonly DonationService _donationService;

        public DashboardService(IDonationRepository donationRepository, DonationService donationService)
        {
            _donationRepository = donationRepository;
            _donationService = donationService;
        }

        public async Task<DashboardResponseDTO> GetDashboardAsync()
        {
            var stats = await _donationService.GetStatsAsync();
            var recent = await _donationRepository.QueryWithIncludes().OrderByDescending(d => d.DonationDate).Take(5).
                                                    Select(d => new DonationDTO
                                                    {
                                                        Id = d.Id,
                                                        DonorName = d.Donor.Name,
                                                        HospitalName = d.Hospital.Name,
                                                        BloodGroup = d.Donor.BloodGroup.ToString(),
                                                        DonationDate = d.DonationDate,
                                                        Status = (int)d.Status
                                                    }
                                                    ).ToListAsync();
            return new DashboardResponseDTO
            {
                Stats = stats,
                RecentDonations = recent
            };
        }
    }
}
