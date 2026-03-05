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
using BDMS.Application.DTOs;
using BDMS.Domain.Enums;

namespace BDMS.Infrastructure.Repositories
{
    public class BloodInventoryRepository : IBloodInventoryRepository
    {
        private readonly BDMSDbContext _dbContext;
        public BloodInventoryRepository(BDMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BloodInventory> GetByHospitalAndBloodGroup(int hospitalId, BloodGroup bloodGroup)
        {
            return await _dbContext.BloodInventory.FirstOrDefaultAsync(i => i.HospitalId == hospitalId && i.BloodGroup== bloodGroup);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<BloodInventory> QueryWithIncludes()
        {
            return _dbContext.BloodInventory.Include(i => i.Hospital);
        }

        public async Task AddAsync(BloodInventory inventory)
        {
            await _dbContext.BloodInventory.AddAsync(inventory);
        }
    }
}


