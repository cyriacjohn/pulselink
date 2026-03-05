using BDMS.Application.DTOs;
using BDMS.Application.Services;
using BDMS.Domain.Enums;
using BDMS.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BDMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using BDMS.Application.Interfaces;

namespace BDMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodInventoryController : ControllerBase
    {
        private readonly IBloodInventoryRepository _bloodInventoryRepository;
        public BloodInventoryController(IBloodInventoryRepository bloodInventoryRepository)
        {
            _bloodInventoryRepository = bloodInventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventory = await _bloodInventoryRepository.QueryWithIncludes().ToListAsync();

            var result = inventory.Select(i => new
            {
                Hospital = i.Hospital != null ? i.Hospital.Name : "Unknown",
                BloodGroup = i.BloodGroup.ToString(),
                UnitsAvailable = i.UnitsAvailable
            });

            return Ok(result);
        }     
    }
}
