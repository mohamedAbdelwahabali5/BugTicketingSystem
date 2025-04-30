using BusnissLayer.DTOs.BugDtos;
using SchoolApp.BL.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusnissLayer.Managers;
using DataAccessLayer;
using DataAccessLayer.Models;
namespace BusnissLayer.Managers
{
    public class BugManager : IBugManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public BugManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GeneralResult<AddBugDto>?> CreateBug(AddBugDto bugDto)
        {
            try
            {
                 if (bugDto == null){
                    return new GeneralResult<AddBugDto>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "INVALID_BUG", Message = "Bug data is invalid" }]
                    };
                 }

                 // check if bug is already exist
                 var existingBug = await _unitOfWork.BugRepository.AnyAsync(b => b.Title == bugDto.Title);

                 if (existingBug)
                 {
                     return new GeneralResult<AddBugDto>()
                     {
                         IsValid = false,
                         Errors = [new ResultError { Code = "EXISTING_BUG", Message = "Bug already exist" }]
                     };
                 }

                var bug = new Bug
                {
                    Title = bugDto.Title,
                    Description = bugDto.Description,
                    Status = bugDto.Status,
                    Priority = bugDto.Priority,
                    CreatedAt = DateTime.Now,
                    ProjectId = bugDto.ProjectId,
                };

                await _unitOfWork.BugRepository.Add(bug);
                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult<AddBugDto>()
                {
                    IsValid = true,
                    Data = bugDto
                };
            }catch (Exception ex) {
                return new GeneralResult<AddBugDto>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<bool>> DeleteBug(int id)
        {
            try
            {
                var bug = await _unitOfWork.BugRepository.GetByIdAsync(b=> b.Id == id);
                if (bug == null)
                {
                    return new GeneralResult<bool>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "BUG_NOT_FOUND", Message = "Bug not found" }]
                    };
                }

                _unitOfWork.BugRepository.Delete(bug);
                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult<bool>()
                {
                    IsValid = true,
                    Data = true
                };
            }catch(Exception ex) {
                return new GeneralResult<bool>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<ICollection<BugDto>?>> GetAllBugs()
        {
            try
            {
                var bugs = await _unitOfWork.BugRepository.GetAllAsync(b => b.Attachments! );
                var bugDtos = bugs.Select(b => new BugDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Status = b.Status,
                    Priority = b.Priority,
                    CreatedAt = b.CreatedAt,
                    Attachments = b.Attachments!.Select(a => new BAttachmentDto
                    {
                        FileName = a.FileName,
                        ContentType = a.ContentType,
                        FileType = a.FileType,
                        FilePath = a.FilePath,
                    }).ToList(),
                }).ToList();
                
                return new GeneralResult<ICollection<BugDto>?>()
                {
                    IsValid = true,
                    Data = bugDtos
                };

            }catch (Exception ex) {
                return new GeneralResult<ICollection<BugDto>?>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<BugDto>?> GetBugById(int id)
        {
            try
            {
                var bug = await _unitOfWork.BugRepository.GetByIdAsync(b => b.Id == id, b => b.Attachments!);
                if (bug == null)
                {
                    return new GeneralResult<BugDto>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "BUG_NOT_FOUND", Message = "Bug not found" }]
                    };
                }

                return new GeneralResult<BugDto>()
                {
                    IsValid = true,
                    Data = new BugDto
                    {
                        Id = bug.Id,
                        Title = bug.Title,
                        Description = bug.Description,
                        Status = bug.Status,
                        Priority = bug.Priority,
                        CreatedAt = bug.CreatedAt,
                        Attachments = bug.Attachments!.Select(a => new BAttachmentDto
                        {
                            FileName = a.FileName,
                            ContentType = a.ContentType,
                            FileType = a.FileType,
                            FilePath = a.FilePath,
                        }).ToList(),
                    }
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult<BugDto>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }

        public async Task<GeneralResult<UpdateBugDto>?> UpdateBug(int id, UpdateBugDto bugDto)
        {
            try
            {
                var bug = await _unitOfWork.BugRepository.GetByIdAsync(b => b.Id == id);
                if (bug == null)
                {
                    return new GeneralResult<UpdateBugDto>()
                    {
                        IsValid = false,
                        Errors = [new ResultError { Code = "BUG_NOT_FOUND", Message = "Bug not found" }]
                    };
                }

                bug.Title = bugDto.Title!;
                bug.Description = bugDto.Description;
                bug.Status = bugDto.Status;
                bug.Priority = bugDto.Priority;

                await _unitOfWork.SaveChangesAsync();

                return new GeneralResult<UpdateBugDto>()
                {
                    IsValid = true,
                    Data = bugDto
                };
            } catch (Exception ex) {
                return new GeneralResult<UpdateBugDto>()
                {
                    IsValid = false,
                    Errors = [new ResultError { Code = "SERVER_ERROR", Message = ex.Message }]
                };
            }
        }
    }
}
