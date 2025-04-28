using BusnissLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.Managers
{
    public interface IUserManager
    {
        Task<bool> RegisterUser(UserRegDto userDto);
        Task<bool> LoginUser(UserLogDto userLogDto);
    }
}
