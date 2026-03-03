using BDMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.DTOs
{
    public class DashboardResponseDTO
    {
        public DonationStatsDTO Stats { get; set; }
            public List<DonationDTO> RecentDonations { get; set; }
    }
}
