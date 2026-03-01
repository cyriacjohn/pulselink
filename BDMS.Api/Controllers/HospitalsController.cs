using BDMS.Application.Services;
using BDMS.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BDMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BDMS.Infrastructure.Services;

namespace BDMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class HospitalsController : ControllerBase
    {
        private readonly HospitalService _service;

        public HospitalsController(HospitalService service) 
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hospitals = await _service.GetAllAsync();
            return Ok(hospitals);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHospitalDTO dto)
        {
            var hospital = await _service.CreateAsync(dto);
            return Ok(hospital);
        }
    }
}
