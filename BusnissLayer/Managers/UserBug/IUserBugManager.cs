using BusnissLayer.DTOs.UserBugDtos;
using SchoolApp.BL.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.Managers
{
    public interface IUserBugManager
    {
        Task<GeneralResult<UserBugDto>> AssignUserToBug(int bugId, int userId);
        Task<GeneralResult<bool>> RemoveUserFromBug(int bugId, int userId);
    }
}
