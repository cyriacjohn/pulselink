using BDMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int? HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        public User() { }
            public User(string username, string passwordHash, string email, Role role)
        {
            UserName = username;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
        }

        public  void UpdateRole(Role newRole)
        {
            if(newRole != Role.Admin && newRole != Role.User)
            {
                throw new ArgumentException("Invalid role specified.");
            }
            Role = newRole; 
        }
    }
}
