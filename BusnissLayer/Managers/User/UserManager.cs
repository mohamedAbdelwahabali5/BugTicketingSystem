using BusnissLayer.DTOs;
using SchoolApp.DAL.UnitOfWork;
using System;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace BusnissLayer.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> LoginUser(UserLogDto userLogDto)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(userLogDto.Email);
            if (user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(userLogDto.Password, user.Password);
        }

        public async Task<bool> RegisterUser(UserRegDto userDto)
        {
            // Check if the user already exists
            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                CreatedAt = DateTime.UtcNow 
            };

            await _unitOfWork.UserRepository.Add(user); 
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}