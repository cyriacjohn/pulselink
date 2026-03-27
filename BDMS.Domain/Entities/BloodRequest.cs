using BDMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Domain.Entities
{
    public class BloodRequest 
    {
        public int Id { get; set; }
        public BloodGroup bloodGroup { get; set; }
        public int UnitsRequired { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public DateTime RequestedAt { get; set; }
        public bool IsFulfilled { get; set; }
        public Priority Priority { get; set; }
    }
}
