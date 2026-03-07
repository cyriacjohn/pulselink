using BDMS.Domain.Entities;
using BDMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BDMS.Application.Interfaces;
using BDMS.Domain.Enums;

namespace BDMS.Infrastructure.Repositories
{
    public class DonationRepository: IDonationRepository
    {
        public readonly BDMSDbContext _dbContext;
        public DonationRepository(BDMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Donation donation)
        {
            await _dbContext.AddAsync(donation);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Donation>> GetAllAsync()
        {
            return await _dbContext.Donations
                .Include(d => d.Donor)
                .Include(d => d.Hospital)
                .ToListAsync();
        }

        public async Task<Donation> GetByIdWithDetailsAsync(int id)
        {
            return await _dbContext.Donations.Include(d => d.Donor)
                                             .Include(d => d.Hospital)
                                             .FirstOrDefaultAsync(d => d.Id == id);
        }

        public IQueryable<Donation> QueryWithIncludes()
        {
            return _dbContext.Donations.Include(d => d.Donor).Include(d => d.Hospital);
        }
    }
}
