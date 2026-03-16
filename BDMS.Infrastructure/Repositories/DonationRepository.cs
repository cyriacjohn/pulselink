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
using BDMS.Infrastructure.Services;

namespace BDMS.Infrastructure.Repositories
{
    public class DonationRepository: IDonationRepository
    {
        public readonly BDMSDbContext _dbContext;
        public readonly CertificateGenerator _certificateGenerator;
        public DonationRepository(BDMSDbContext dbContext, CertificateGenerator certificateGenerator)
        {
            _dbContext = dbContext;
            _certificateGenerator = certificateGenerator;
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

        public async Task<List<Donation>> GetByDonorIdAsync(int id)
        {
            return await _dbContext.Donations.Where(d => d.DonorId == id)
                                             .Include(d => d.Hospital)
                                             .ToListAsync();
        }  
        
        public async Task<byte[]> GenerateCertificateAsync(int donationId)
        {
            var donation = await _dbContext.Donations.Include(d => d.Donor)
                                             .Include(d => d.Hospital)
                                             .FirstOrDefaultAsync(d => d.Id == donationId);
            if (donation == null)
            {
                throw new Exception("Donation not found");
            }
            return  _certificateGenerator.Generate(donation.Donor.Name, donation.Hospital.Name, donation.CertificateNumber);
        }

        public async Task<Dictionary<string, int>> GetBloodGroupStatsAsync()
        {
            return await _dbContext.Donations.GroupBy(d => d.Donor.BloodGroup)
                                             .Select(g => new { BloodGroup = g.Key, Count = g.Count() })
                                             .ToDictionaryAsync(x => Convert.ToString(x.BloodGroup), x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetBloodGroupStatsByUserAsync(int id)
        {
            return await _dbContext.Donations.Where(d => d.DonorId == id)
                                             .GroupBy(d => d.Status)
                                             .Select(g => new { Status = g.Key, Count = g.Count() })
                                             .ToDictionaryAsync(x => Convert.ToString(x.Status), x => x.Count);
        }
    }
}
