using BDMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Domain.Entities;

namespace BDMS.Application.Interfaces
{
    public interface IDonationRepository
    {
        Task AddAsync(Donation donation);
        Task SaveChangesAsync();
        Task<List<Donation>> GetAllAsync();
        Task<Donation> GetByIdWithDetailsAsync(int id);
        IQueryable<Donation> QueryWithIncludes();
        Task<List<Donation>> GetByDonorIdAsync(int id);
        Task<byte[]> GenerateCertificateAsync(int donationId);
        Task<Dictionary<string, int>> GetBloodGroupStatsAsync();
        Task<Dictionary<string, int>> GetBloodGroupStatsByUserAsync(int id);
    }
}
