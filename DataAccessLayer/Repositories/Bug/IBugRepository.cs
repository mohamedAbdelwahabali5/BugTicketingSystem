using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Repositories.Genaric;
using DataAccessLayer.Models;

namespace SchoolApp.DAL.Repositories
{
    public interface IBugRepository : IGenaricRepository<Bug>
    {
        //public Task<Bug?> GetTeacherWithCoursesAsync(int id);

    }
}
