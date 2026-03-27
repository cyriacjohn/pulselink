using BDMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Domain.Logic
{
    public static class BloodGroupCompatibility
    {
        public static List<BloodGroup> GetCompatibleBloodGoups(BloodGroup requested)
        {
            return requested switch
            {
                BloodGroup.APositive => new()
                {
                    BloodGroup.ABPositive,
                    BloodGroup.ABNegative,
                    BloodGroup.OPositive,
                    BloodGroup.ONegative
                },
                BloodGroup.ANegative => new()
                {
                    BloodGroup.OPositive,
                    BloodGroup.ONegative
                },
                BloodGroup.BPositive => new()
                {
                    BloodGroup.BPositive,
                    BloodGroup.BNegative,
                    BloodGroup.OPositive,
                    BloodGroup.ONegative
                },
                BloodGroup.BNegative => new()
                {
                    BloodGroup.BNegative,
                    BloodGroup.ONegative
                },
                BloodGroup.ABPositive => new()
                {
                    BloodGroup.APositive,
                    BloodGroup.ANegative,
                    BloodGroup.BPositive,
                    BloodGroup.BNegative,
                    BloodGroup.ABPositive,
                    BloodGroup.ABNegative,
                    BloodGroup.OPositive,
                    BloodGroup.ONegative
                },
                BloodGroup.ABNegative => new()
                {
                    BloodGroup.ABNegative,
                    BloodGroup.ANegative,
                    BloodGroup.BNegative,
                    BloodGroup.ONegative
                },
                BloodGroup.OPositive => new()
                {
                    BloodGroup.OPositive,
                    BloodGroup.ONegative
                },
                BloodGroup.ONegative => new()
                {
                    BloodGroup.ONegative
                },
                _ => new List<BloodGroup>()
            };
        }
    }
}
