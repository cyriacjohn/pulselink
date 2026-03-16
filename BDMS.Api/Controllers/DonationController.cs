using BDMS.Application.DTOs;
using BDMS.Application.Services;
using BDMS.Domain.Entities;
using BDMS.Domain.Enums;
using BDMS.Infrastructure.Data;
using BDMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BDMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User,Admin")]
    public class DonationController : ControllerBase
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
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            dto.DonorId = userId;
            var donation = await _service.CreateAsync(dto);

            var pdf = _certificateGenerator.Generate(donation.Donor.Name, donation.Hospital.Name, donation.CertificateNumber);
            return File(pdf, "application/pdf", $"Certificate-{donation.CertificateNumber}.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DonationStatus? status)
        {
            var donation = await _service.GetAllAsync(status);
            return Ok(donation);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveAsync(int id)
        {
            await _service.ApproveAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectAsync(int id)
        {
            await _service.RejectAsync(id);
            return NoContent();
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _service.GetStatsAsync();
            return Ok(stats);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyDonations()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var donations = await _service.GetByDonorIdAsync(userId);
            return Ok(donations);
        }

        [HttpGet("{id}/certificate")]
        public async Task<IActionResult> GetCertificate(int id)
        {
            var pdf = await _service.GenerateCertificateAsync(id);
            return File(pdf, "application/pdf", $"certificate-{id}.pdf");
        }

        [HttpGet("stats/bloodgroups")]
        public async Task<IActionResult> GetBloodGroupStats()
        {
            var stats = await _service.GetBloodGroupStatsAsync();
            return Ok(stats);
        }

        [HttpGet("stats/my")]
        public async Task<IActionResult> GetBloodGroupStatsByUser()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var stats = await _service.GetBloodGroupStatsByUserAsync(userId);
            return Ok(stats);
        }
    }
}
