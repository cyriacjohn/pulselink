using BDMS.Application.DTOs;
using BDMS.Application.Interfaces;
using BDMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDMS.Application.Services
{
    public class DonorService
    {
        private readonly IDonorRepository _repository;

        public DonorService(IDonorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Donor> RegisterAsync(CreateDonorDTO dto)
        {
            var donor = new Donor(
                dto.Name,
                dto.Address,
                dto.Phone,
                dto.Email,
                dto.Age,
                dto.BloodGroup
                );
            await _repository.AddAsync(donor);
            await _repository.SaveChangesAsync();
            return donor;
        }
        public async Task<List<Donor>> GetAllAsync()
        {
            return await _repository.GetAllAsync();

        }

        public async Task<Donor> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var donor = await _repository.GetByIdAsync(id);
            if (donor == null)
            {
                return false;
            }
            _repository.Remove(donor);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, UpdateDonorDTO dto)
        {
            var donor = await _repository.GetByIdAsync(id);
            if (donor == null)
            {
                return false;
            }

            donor.UpdateDetails(
            dto.Name,
            dto.Email,
            dto.Age,
            dto.BloodGroup,
            dto.Address,
            dto.Phone);

            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
