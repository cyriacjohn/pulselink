using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.DTOs
{
    public class HospitalDashboardDTO
    {
        public string HospitalName { get; set; }
        public int InventoryCount { get; set; }
        public int ActiveRequests { get; set; }
        public int CompletedDonations {  get; set; }
    }
}
