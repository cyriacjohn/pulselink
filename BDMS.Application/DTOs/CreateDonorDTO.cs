using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BDMS.Application.DTOs
{
    public class CreateDonorDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        [Range(18, 65)]
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
