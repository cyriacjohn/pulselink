using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Application.Interfaces;
using BDMS.Domain.Entities;
using BDMS.Infrastructure;
using BDMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BDMS.Infrastructure.Repositories;

namespace BDMS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly BDMSDbContext _db;
        public UserRepository(BDMSDbContext db)
        {
            _db = db; 
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
