using BusnissLayer.DTOs.ProjectDtos;
using SchoolApp.BL.Dtos.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusnissLayer.Managers
{
    public interface IProjectManager
    {
        Task<GeneralResult<ICollection<ProjectDto>>> GetAllProjects();
        Task<GeneralResult<ProjectDto>> GetProjectById(int id);
        Task<GeneralResult<AddProjectDto>> AddProject(AddProjectDto addProjectDto);
        Task<GeneralResult<UpdatedProjectDto>> UpdateProject(UpdatedProjectDto projectDto, int id);
        Task<GeneralResult<bool>> DeleteProject(int id);
    }
}