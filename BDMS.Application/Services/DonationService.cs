using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Application.DTOs;
using BDMS.Application.Interfaces;
using BDMS.Domain.Entities;



namespace BDMS.Application.Services
{
    public class DonationService
    {
        private readonly IDonationRepository _repository;
        private readonly IDonorRepository _donorRepository;
        private readonly IBloodInventoryRepository _bloodInventoryRepository;
        public DonationService(IDonationRepository repository, IDonorRepository donorRepository, IBloodInventoryRepository bloodInventoryRepository)
        {
            _repository = repository;
            _donorRepository = donorRepository;
            _bloodInventoryRepository = bloodInventoryRepository;
        }

        public async Task<Donation> CreateAsync(CreateDonationDTO dto)
        {
            var donor = await _donorRepository.GetByIdAsync(dto.DonorId);
            if(donor == null)
            {
                throw new Exception("Donor not found");
            }
            var donation = new Donation
            {
                DonorId = dto.DonorId,
                HospitalId = dto.HospitalId,
                DonationDate = DateTime.UtcNow,
                CertificateNumber = GenerateCertificateNumber()
            };

            await _repository.AddAsync(donation);
            //await _bloodInventoryRepository.GetByHospitalAndBloodGroup(dto.HospitalId, Donor.BloodGroup);

await _repository.SaveChangesAsync();

            var fullDonation = await _repository.GetByIdWithDetailsAsync(donation.Id);
            return fullDonation;
        }

        private string GenerateCertificateNumber()
        {
            return String.Concat("CERT-", Guid.NewGuid().ToString()[..8].ToUpper());
        }

        public async Task<List<Donation>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
