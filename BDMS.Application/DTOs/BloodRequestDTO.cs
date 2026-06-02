using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Domain.Enums;

namespace BDMS.Application.DTOs
{
    public class BloodRequestDTO
    {
        public BloodGroup BloodGroup { get; set; }
        public int UnitsRequired { get; set; }
        public Priority Priority { get; set; }
    }
}
