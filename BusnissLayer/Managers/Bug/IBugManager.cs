using BusnissLayer.DTOs.BugDtos;
using SchoolApp.BL.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.Managers
{
    public interface IBugManager
    {

        Task<GeneralResult<ICollection<BugDto>?>> GetAllBugs();
        Task<GeneralResult<BugDto>?> GetBugById(int id);
        Task<GeneralResult<AddBugDto>?> CreateBug(AddBugDto bugDto);
        Task<GeneralResult<UpdateBugDto>?> UpdateBug(int id, UpdateBugDto bugDto);
        Task<GeneralResult<bool>> DeleteBug(int id);
    }
}
