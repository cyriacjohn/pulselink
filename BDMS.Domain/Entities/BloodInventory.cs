using BDMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Domain.Entities
{
    public class BloodInventory
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public int UnitsAvailable { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
