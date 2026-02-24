using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Application.Interfaces;
using BDMS.Domain.Entities;
using BDMS.Infrastructure;
using BDMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BDMS.Infrastructure.Repositories
{
    public class DonorRepository : IDonorRepository
    {
        private readonly BDMSDbContext _db;
        public DonorRepository(BDMSDbContext db)
        {
            _db = db;
        }
        public async Task<Donor?> GetByIdAsync(int id)
        {
            return await _db.Donors.FindAsync(id);
        }
        public async Task<List<Donor>> GetAllAsync()
        {
            return await _db.Donors.ToListAsync();
        }
        public async Task AddAsync(Donor donor)
        {
            await _db.Donors.AddAsync(donor);
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
        public void Remove(Donor donor)
        {
            _db.Donors.Remove(donor);
        }
    }
}
