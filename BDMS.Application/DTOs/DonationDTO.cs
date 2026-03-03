using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.DTOs
{
    public class DonationDTO
    {
        public int Id { get; set; }
        public string DonorName { get; set; }
        public string HospitalName { get; set; }
        public string BloodGroup { get; set; }
        public DateTime DonationDate { get; set; }
        public int Status{ get; set; }

    }
}
