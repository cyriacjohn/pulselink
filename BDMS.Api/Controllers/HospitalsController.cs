using BDMS.Application.Services;
using BDMS.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BDMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BDMS.Infrastructure.Services;
using BDMS.Domain.Enums;

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

        [HttpPost("create-hospital")]
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
        [HttpPost("hospital-request")]
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

        [Authorize(Roles = "Hospital")]
        [HttpGet("bloodgroups")]
        public async Task <IActionResult> BloodGroups()
        {
            var bloodGroups = Enum.GetNames(typeof(BloodGroup));
            return Ok(bloodGroups);
        }

        [Authorize(Roles = "Hospital")]
        [HttpGet("open-requests")]
        public async Task<IActionResult> GetOpenBloodRequests()
        {
            var requests = await _service.GetOpenRequestsAsync();
            return Ok(requests);
        }

        [Authorize(Roles ="Hospital")]
        [HttpPost("mark-as-completed")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var status = _service.MarkAsCompleted(id);
            return NoContent();
        }
    }
}
