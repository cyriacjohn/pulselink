using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Domain.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace BDMS.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAdminAsync(BDMSDbContext db)
        {
            if (await db.Users.AnyAsync(u => u.Role == "Admin"))
            {
                return;
            }

            var admin = new User("admin", BCrypt.Net.BCrypt.HashPassword("Admin123"), "admin@bdms.com", "Admin");
            await db.Users.AddAsync(admin);
            await db.SaveChangesAsync();
        }
    }
}
