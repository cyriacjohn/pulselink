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

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User?>> GetAllAsyc(int pageNumber, int pageSize)
        {
            return await _db.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public void Remove(User user)
        {
            _db.Users.Remove(user);
        }
    }
}
