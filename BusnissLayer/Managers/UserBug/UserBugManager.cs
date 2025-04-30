using BusnissLayer.DTOs.UserBugDtos;
using BusnissLayer.Managers;
using DataAccessLayer.Models;
using DataAccessLayer;
using SchoolApp.BL.Dtos.Common;

namespace BusnissLayer.Managers
{
    public class UserBugManager : IUserBugManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserBugManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResult<UserBugDto>> AssignUserToBug(int bugId, int userId)
        {
            try
            {
                var existingAssignment = await _unitOfWork.UserBugRepository
                    .GetByIdAsync(userId, bugId);

                if (existingAssignment != null)
                {
                    return new GeneralResult<UserBugDto>
                    {
                        IsValid = false,
                        Errors = [new() { Code = "EXISTS", Message = "User is already assigned to this bug" }]
                    };
                }

                var userBug = new User_Bug { BugId = bugId, UserId = userId };
                _unitOfWork.UserBugRepository.Add(userBug);
                await _unitOfWork.UserBugRepository.SaveChangesAsync();

                return new GeneralResult<UserBugDto>
                {
                    IsValid = true,
                    Data = new UserBugDto { BugId = bugId, UserId = userId }
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<UserBugDto>
                {
                    IsValid = false,
                    Errors = [new() { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<bool>> RemoveUserFromBug(int bugId, int userId)
        {
            try
            {
                var userBug = await _unitOfWork.UserBugRepository
                    .GetByIdAsync(userId, bugId);

                if (userBug == null)
                {
                    return new GeneralResult<bool>
                    {
                        IsValid = false,
                        Errors = [new() { Code = "NOT_FOUND", Message = "Assignment not found" }]
                    };
                }

                _unitOfWork.UserBugRepository.Delete(userBug);
                await _unitOfWork.UserBugRepository.SaveChangesAsync();

                return new GeneralResult<bool>
                {
                    IsValid = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<bool>
                {
                    IsValid = false,
                    Errors = [new() { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }
    }
}