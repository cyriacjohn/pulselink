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
            var existingEmail = await _user.GetByEmailAsync(dto.Email);
            if (existingEmail != null)
            {
                return false;
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User(dto.Username, passwordHash, dto.Email, "User");
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

        public async Task<bool> UpdateUserRoleAsync(int userId, string newRole)
        {
            var user = await _user.GetByIdAsync(userId);
            if(user == null)
            {
                return false;
            }
            user.UpdateRole(newRole);
            await _user.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _user.GetAllAsyc(pageNumber, pageSize);
            return users.Select(u => new UserResponseDTO
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _user.GetByIdAsync(userId);
            if(user == null)
            {
                return false;
            }
            _user.Remove(user);
            await _user.SaveChangesAsync();
            return true;
        }
    }
}
