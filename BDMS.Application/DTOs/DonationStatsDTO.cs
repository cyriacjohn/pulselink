using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.DTOs
{
    public class DonationStatsDTO
    {
        public int TotalDonations { get; set; }
        public int PendingDonations { get; set; }
        public int CompletedDonations { get; set; }
        public int RejectedDonations { get; set; }
        public int TotalUnits { get; set; }
    }
}
