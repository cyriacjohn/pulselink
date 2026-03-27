using BDMS.Domain.Entities;
using BDMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Domain.Logic
{
    public class DonorScore
    {
        public static int CalculateScore(Donor d, BloodRequest request, double distance)
        {
            int score = 0;
            score += 50;
            if (d.LastDonatedDate == null)
            {
                score += 40;
            }
            else
            {
                var days = (DateTime.UtcNow - d.LastDonatedDate.Value).TotalDays;
                
                if (days >= 365)
                {
                    score += 30;
                }
                else if (days >= 180)
                {
                    score += 20;
                }
                else if (days >= 90)
                {
                    score += 10;
                }
                else
                {
                    score += 0;
                }
            }

            if (distance <= 5)
            {
                score += 40;
            }
            else if (distance <= 10)
            {
                score += 30;
            }
            else if (distance <= 20)
            {
                score += 20;
            }
            else
            {
                score += 10;
            }

            score += request.Priority switch
            {
                Priority.Critical => 50,
                Priority.High => 40,
                Priority.Medium => 25,
                Priority.Low => 10,
                _ => 0
            };
            return score;
        }
    }
}
