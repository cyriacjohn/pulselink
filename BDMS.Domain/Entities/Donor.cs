using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Domain.Entities
{
    public class Donor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BloodGroup { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime? LastDonatedDate { get; set; }
        public DateTime CreatedDate { get; set; } 

        private Donor() { } // Required for EF
        public Donor(string name, string email, string phone, string bloodGroup, int age, string address)
        {
            if(age < 18)
            {
                throw new ArgumentException("Donor must be atleast 18 years old.");
            }
            Name = name;
            Email = email;
            Phone = phone;
            BloodGroup = bloodGroup;
            Age = age;
            Address = address;
            CreatedDate = DateTime.UtcNow;

        }

        public void UpdateLastDonationDate(DateTime date)
        {
            LastDonatedDate = date;
        }

        public void UpdateDetails(string name, string email, int age, string bloodGroup, string address, string phone)
        {
            if(age < 18)
            {
                throw new ArgumentException("Donor must be atleast 18 years old");
            }
            Name = name;
            Email = email;
            Age = age;
            BloodGroup = bloodGroup;
            Address = address;
            Phone = phone;
        }
    }
}
