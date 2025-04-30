using BusnissLayer.DTOs.ProjectDtos;
using SchoolApp.BL.Dtos.Common;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusnissLayer.Managers
{
    public class ProjectManager : IProjectManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<GeneralResult<AddProjectDto>> AddProject(AddProjectDto addProjectDto)
        {
            try
            {
                if (addProjectDto == null)
                {
                    return new GeneralResult<AddProjectDto>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "NULL_INPUT", Message = "Project data cannot be null" }]
                    };
                }

                // Check for duplicate project name
                var existingProject = await _unitOfWork.ProjectRepository
                                .AnyAsync(p => p.Name == addProjectDto.Name);

                if (existingProject == true)
                {
                    return new GeneralResult<AddProjectDto>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "DUPLICATE_PROJECT", Message = "Project with this name already exists" }]
                    };
                }

                var project = new Project()
                {
                    //Id = new Random().Next(), // Generate a random ID
                    Name = addProjectDto.Name,
                    Description = addProjectDto.Description,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.ProjectRepository.Add(project);
                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult<AddProjectDto>()
                {
                    IsValid = true,
                    Data = addProjectDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<AddProjectDto>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "ADD_PROJECT_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<bool>> DeleteProject(int id)
        {
            try
            {
                var project = await _unitOfWork.ProjectRepository.GetByIdAsync(p => p.Id == id);
                if (project == null)
                {
                    return new GeneralResult<bool>()
                    {
                        IsValid = true,
                        Data = false
                    };
                }

                _unitOfWork.ProjectRepository.Delete(project);
                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult<bool>()
                {
                    IsValid = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<bool>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "DELETE_PROJECT_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<ICollection<ProjectDto>>> GetAllProjects()
        {
            try
            {
                var projects = await _unitOfWork.ProjectRepository.GetAllAsync(p => p.Bugs!);

                if (projects == null || !projects.Any())
                {
                    return new GeneralResult<ICollection<ProjectDto>>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "NO_PROJECTS", Message = "No projects found" }]
                    };
                }

                var projectDtos = projects.Select(p => new ProjectDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    CreatedAt = p.CreatedAt,
                    Bugs = p.Bugs?.Select(b => new PBug
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description,
                        Status = b.Status,
                        Priority = b.Priority
                    }).ToList()
                }).ToList();

                return new GeneralResult<ICollection<ProjectDto>>()
                {
                    IsValid = true,
                    Data = projectDtos
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<ICollection<ProjectDto>>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "GET_PROJECTS_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<ProjectDto>> GetProjectById(int id)
        {
            try
            {
                var project = await _unitOfWork.ProjectRepository.GetByIdAsync(p => p.Id == id , p => p.Bugs);

                if (project == null)
                {
                    return new GeneralResult<ProjectDto>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "PROJECT_NOT_FOUND", Message = "Project not found" }]
                    };
                }

                var projectDto = new ProjectDto()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    CreatedAt = project.CreatedAt,
                    Bugs = project.Bugs?.Select(b => new PBug
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description,
                        Status = b.Status,
                        Priority = b.Priority
                    }).ToList()
                };

                return new GeneralResult<ProjectDto>()
                {
                    IsValid = true,
                    Data = projectDto
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<ProjectDto>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "GET_PROJECT_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<UpdatedProjectDto>> UpdateProject(UpdatedProjectDto projectDto, int id)
        {
            try
            {
                if (projectDto == null)
                {
                    return new GeneralResult<UpdatedProjectDto>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "NULL_INPUT", Message = "Project data cannot be null" }]
                    };
                }

                var project = await _unitOfWork.ProjectRepository.GetByIdAsync(p => p.Id == id);
                if (project == null)
                {
                    return new GeneralResult<UpdatedProjectDto>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "PROJECT_NOT_FOUND", Message = "Project not found" }]
                    };
                }

                var duplicateProject = (await _unitOfWork.ProjectRepository.GetAllAsync())
                    .FirstOrDefault(p => p.Name == projectDto.Name && p.Id != id);

                if (duplicateProject != null)
                {
                    return new GeneralResult<UpdatedProjectDto>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "DUPLICATE_PROJECT", Message = "Another project with this name already exists" }]
                    };
                }

                project.Name = projectDto.Name!;
                project.Description = projectDto.Description;
                project.ManagerId = projectDto.ManagerId;

                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult<UpdatedProjectDto>()
                {
                    IsValid = true,
                    Data = new UpdatedProjectDto()
                    {
                        Name = project.Name,
                        Description = project.Description,
                        ManagerId = project.ManagerId
                    }
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<UpdatedProjectDto>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "UPDATE_PROJECT_ERROR", Message = ex.Message }]
                };
            }
        }
    }
}