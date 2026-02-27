using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [PasswordPropertyText]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
