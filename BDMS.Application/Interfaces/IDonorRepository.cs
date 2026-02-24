using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Domain.Entities;

namespace BDMS.Application.Interfaces
{
    public interface IDonorRepository
    {
        Task<Donor?> GetByIdAsync(int id);
        Task<List<Donor>> GetAllAsync();
        Task AddAsync(Donor donor);
        Task SaveChangesAsync();
        void Remove(Donor donor);
    }
}
