using BDMS.Application.DTOs;
using BDMS.Application.Services;
using BDMS.Infrastructure.Data;
using BDMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BDMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Admin")]
    public class DonationController: ControllerBase
    {
        private readonly DonationService _service;
        private readonly CertificateGenerator _certificateGenerator;
        public DonationController(DonationService service, CertificateGenerator certificateGenerator)
        {
            _service = service;
            _certificateGenerator = certificateGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateDonationDTO dto)
        {
            var donation = await _service.CreateAsync(dto);

            var pdf = _certificateGenerator.Generate(donation.Donor.Name, donation.Hospital.Name, donation.CertificateNumber);
            return File(pdf, "application/pdf", $"Certificate-{donation.CertificateNumber}.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var donation = await _service.GetAllAsync();
            return Ok(donation);
        }
    }
}
