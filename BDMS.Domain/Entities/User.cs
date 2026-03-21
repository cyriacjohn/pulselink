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
        public string Role { get; set; }

        public User() { }
            public User(string username, string passwordHash, string email, string role)
        {
            UserName = username;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
        }

        public  void UpdateRole(string newRole)
        {
            if(newRole != "Admin" && newRole != "User")
            {
                throw new ArgumentException("Invalid role specified.");
            }
            Role = newRole; 
        }
    }
}
