using BDMS.Application.DTOs;
using BDMS.Application.Services;
using BDMS.Domain.Enums;
using BDMS.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BDMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BDMS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BloodInventoryController : ControllerBase
    {
        private readonly BloodInventoryRepository _bloodInventoryRepository;
        public BloodInventoryController(BloodInventoryRepository bloodInventoryRepository)
        {
            _bloodInventoryRepository = bloodInventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventory = await _bloodInventoryRepository.QueryWithIncludes().Select(i => new
            {
                Hospital = i.Hospital.Name,
                BloodGroup = i.BloodGroup.ToString(),
                UnitsAvailable = i.UnitsAvailable
            }).ToListAsync();

            return Ok(inventory);
        }

    }
}
