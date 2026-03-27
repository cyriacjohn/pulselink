using BDMS.Application.Services;
using BDMS.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BDMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmartMatchingController : ControllerBase
    {
        public readonly SmartMatchingService _service;
        public SmartMatchingController(SmartMatchingService service)
        {
            _service = service;
        }

        [HttpGet("smart-match/{requestId}")]
        public async Task<IActionResult> SmartMatch(int requestId)
        {
            var result = await _service.FindMatchingDonors(requestId);
            return Ok(result);
        }
    }
}
