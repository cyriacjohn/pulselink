using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Application.DTOs;
using BDMS.Application.Interfaces;
using BDMS.Domain.Entities;
using BCrypt.Net;

namespace BDMS.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _user;

        public AuthService(IUserRepository user)
        {
            _user = user;
        }

        public async Task<bool> RegisterAsync(RegisterDTO dto)
        {
            var existingUser = await _user.GetByUsernameAsync(dto.Username);
            if (existingUser != null)
            {
                return false;
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User(dto.Username, passwordHash, dto.Email, "Admin");
            await _user.AddAsync(user);
            await _user.SaveChangesAsync();
            return true;
        }

        public async Task<User?> ValidateUserAsync(LoginDTO dto)
        {
            var user = await _user.GetByUsernameAsync(dto.Username);
            if (user == null)
            {
                return null;
            }
            var isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            return isValid ? user : null;
        }
    }
}
