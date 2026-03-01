using BDMS.Infrastructure.Data;
using BDMS.Domain.Entities;
using BDMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using BDMS.Application.DTOs;

namespace BDMS.Infrastructure.Services
{
    public class HospitalService
    {
        private readonly BDMSDbContext _dbContext;

        public HospitalService(BDMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Hospital> CreateAsync(CreateHospitalDTO dto)
        {
            var hospital = new Hospital
            {
                Name = dto.Name,
                City = dto.City,
                Address = dto.Address,
                ContactPhone = dto.ContactPhone
            };

            await _dbContext.AddAsync(hospital);
            await _dbContext.SaveChangesAsync();

            foreach (BloodGroup group in Enum.GetValues(typeof(BloodGroup)))
            {
                var inventory = new BloodInventory
                {
                    HospitalId = hospital.Id,
                    BloodGroup = group,
                    UnitsAvailable = 0,
                    LastUpdated = DateTime.UtcNow
                };

                await _dbContext.BloodInventory.AddAsync(inventory);
            }
            await _dbContext.SaveChangesAsync();
            return hospital;
        }

        public async Task<List<Hospital>> GetAllAsync()
        {
            return await _dbContext.Hospitals.ToListAsync();
        }
    }
}
