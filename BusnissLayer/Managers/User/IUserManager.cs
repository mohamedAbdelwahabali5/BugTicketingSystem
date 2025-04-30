using BusnissLayer.DTOs.UserDtos;
using SchoolApp.BL.Dtos.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusnissLayer.Managers
{
    public interface IUserManager
    {
        Task<GeneralResult<ICollection<UserDto>>> GetAllUsers();
        Task<GeneralResult<UserRegDto>> RegisterUser(UserRegDto userDto);
        Task<GeneralResult<TokenDto>> LoginUser(UserLogDto userLogDto);
    }
}