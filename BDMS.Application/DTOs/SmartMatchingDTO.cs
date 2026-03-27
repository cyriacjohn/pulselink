using BDMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.DTOs
{
    public class SmartMatchingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public DateTime? LastDonatedDate { get; set; }
        public int Score { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
