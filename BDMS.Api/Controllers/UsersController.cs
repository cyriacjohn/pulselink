using BDMS.Application.Services;
using BDMS.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BDMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly AuthService _service;

        public UsersController(AuthService service)
        {
            _service = service;
        }

        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateRole(int id, UpdateUserRoleDTO dto)
        {
            try
            {
                var result = await _service.UpdateUserRoleAsync(id, dto.Role);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _service.GetAllUsersAsync(pageNumber, pageSize);
            return Ok(users);
        }
    }
}
