using BusnissLayer.DTOs.UserDtos;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolApp.BL.Dtos.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserManager(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<GeneralResult<ICollection<UserDto>>> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllAsync(u => u.Roles!);

                var result = new GeneralResult<ICollection<UserDto>>
                {
                    IsValid = true,
                    Data = users.Select(user => new UserDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        CreatedAt = user.CreatedAt,
                        Roles = user.Roles?.Select(r => new RoleDto { Id = r.Id, Name = r.Name }).ToList()
                                ?? new List<RoleDto>()
                    }).ToList()
                };

                return result;
            }
            catch (Exception ex)
            {
                return new GeneralResult<ICollection<UserDto>>
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "DATABASE_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<TokenDto>> LoginUser(UserLogDto userLogDto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByEmailAsync(userLogDto.Email);
                if (user == null)
                {
                    return new GeneralResult<TokenDto>
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "USER_NOT_FOUND", Message = "User not found" }]
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(userLogDto.Password, user.Password))
                {
                    return new GeneralResult<TokenDto>
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "INVALID_CREDENTIALS", Message = "Invalid password" }]
                    };
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                if (user.Roles != null)
                {
                    foreach (var role in user.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                var token = GenerateToken(claims);
                return new GeneralResult<TokenDto>
                {
                    IsValid = true,
                    Data = token
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<TokenDto>
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "LOGIN_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<TokenDto>> RegisterUser(UserRegDto userDto)
        {
            try
            {
                var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(userDto.Email);
                if (existingUser != null)
                {
                    return new GeneralResult<TokenDto>
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "USER_EXISTS", Message = "User already exists" }]
                    };
                }

                var user = new User
                {
                    UserName = userDto.UserName,
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    Roles = new List<Role> { new Role { Name = "User" } },
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "User")
                };

                var token = GenerateToken(claims);
                return new GeneralResult<TokenDto>
                {
                    IsValid = true,
                    Data = token
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<TokenDto>
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "REGISTRATION_ERROR", Message = ex.Message }]
                };
            }
        }

        private TokenDto GenerateToken(List<Claim> claims)
        {
            var secretKey = _configuration["Jwt:SecretKey"] ??
                          throw new ArgumentNullException("Jwt:SecretKey is missing in configuration");
            var issuer = _configuration["Jwt:Issuer"] ?? "BugTicketingSystem";
            var audience = _configuration["Jwt:Audience"] ?? "BugTicketingSystemClient";

            // Fixed GetValue issue by using TryGetValue pattern
            if (!int.TryParse(_configuration["Jwt:ExpiryInMinutes"], out var expiryInMinutes))
            {
                expiryInMinutes = 60; // Default value
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiryInMinutes),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDto
            {
                Token = tokenString,
                Expiration = token.ValidTo
            };
        }
    }
}