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
    public class HospitalsController : ControllerBase
    {
        private readonly HospitalService _service;

        public HospitalsController(HospitalService service) 
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
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

        [Authorize(Roles = "Hospital")]
        [HttpGet("hospital-dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var hospitalId = Convert.ToInt32(User.FindFirst("HospitalId").Value);
            if (hospitalId > 0)
            {
                var hospital = await _service.FindAsync(hospitalId);
                return Ok(hospital);
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize(Roles = "Hospital")]
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] BloodRequestDTO dto)
        {
            var hospitalId = Convert.ToInt32(User.FindFirst("HospitalId").Value);
            if (hospitalId > 0)
            {
                var requestId = await _service.CreateRequestAsync(hospitalId, dto);
                return Ok(new
                {
                    RequestId = requestId
                });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
