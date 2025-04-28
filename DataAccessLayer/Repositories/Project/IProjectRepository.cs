using DataAccessLayer.Models;
using SchoolApp.DAL.Repositories.Genaric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Repositories
{
    public interface IProjectRepository : IGenaricRepository<Project>
    {
        //Task<Project?> GetStudentWithCoursesAsync(int studentId);

    }
}
