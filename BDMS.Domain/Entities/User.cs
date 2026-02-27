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
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }

        private User() { }
            public User(string username, string passwordHash, string email, string role)
        {
            UserName = username;
            PasswordHash = passwordHash;
            Email = email;
            Role = role;
        }
    }
}
