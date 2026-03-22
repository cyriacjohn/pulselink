using BDMS.Infrastructure.Data;
using BDMS.Domain.Entities;
using BDMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using BDMS.Application.DTOs;
using System.Text.Json;
using BDMS.Application.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace BDMS.Infrastructure.Services
{
    public class HospitalService
    {
        private readonly BDMSDbContext _dbContext;
        private readonly ICacheService _cache;

        public HospitalService(BDMSDbContext dbContext, ICacheService cache)
        {
            _dbContext = dbContext;
            _cache = cache;
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
            await _cache.DeleteAsync("hospitals");
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

        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            var cacheKey = "hospitals";
            try
            {
                var cachedData = await _cache.GetAsync(cacheKey);
                if (!string.IsNullOrEmpty(cachedData))
                {
                    return JsonSerializer.Deserialize<List<Hospital>>(cachedData) ?? Enumerable.Empty<Hospital>();
                }

                var hospitals = await _dbContext.Hospitals.ToListAsync();
                var result = hospitals.Select(h => new Hospital
                {
                    Id = h.Id,
                    Name = h.Name,
                    City = h.City,
                    Address = h.Address,
                    ContactPhone = h.ContactPhone
                }).ToList();

                await _cache.SetAsync(cacheKey, JsonSerializer.Serialize(result), TimeSpan.FromMinutes(10));
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
                throw;
            }
        }
    }
}
