using BDMS.Application.Services;
using BDMS.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BDMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class DonorsController : ControllerBase
    {
        private readonly DonorService _service;

        public DonorsController(DonorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var donors = await _service.GetAllAsync();
            return Ok(donors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonorDTO dto)
        {
            var donor = await _service.RegisterAsync(dto);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return CreatedAtAction(nameof(GetAll), new { id = donor.Id }, donor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var donor = await _service.GetById(id);
            if(donor == null)
            {
                return NotFound();
            }
            return Ok(donor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var donor = await _service.DeleteByIdAsync(id);
            if(!donor)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDonorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UpdateAsync(id, dto);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
