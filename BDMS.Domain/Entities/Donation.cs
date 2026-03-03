using BDMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Domain.Entities
{
    public class Donation
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int HospitalId { get; set; }

        public DateTime DonationDate { get; set; }
        public string CertificateNumber { get; set; } = string.Empty;
        public Donor Donor { get; set; }
        public Hospital Hospital { get; set; }
        public DonationStatus Status { get; set; }

    }
}
