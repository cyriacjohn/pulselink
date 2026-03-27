using BDMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Application.Interfaces;
using BDMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BDMS.Infrastructure.Repositories
{
    public class BloodRequestRepository : IBloodRequestRepository
    {
        private readonly BDMSDbContext _context;
        public BloodRequestRepository(BDMSDbContext context)
        {
            _context = context;
        }

        public async Task<BloodRequest?> GetByIdAsync(int requestId)
        {
            var all = _context.BloodRequests.ToList();
            return await _context.BloodRequests.Include(d => d.Hospital).FirstOrDefaultAsync(d => d.Id == requestId);
        }
    }
}
