using BDMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDMS.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task SaveChangesAsync();
        Task<User> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<List<User?>> GetAllAsyc(int pageNumber, int pageSize);
        void Remove(User user);
    }
}
