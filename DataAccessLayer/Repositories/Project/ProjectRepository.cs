using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using SchoolApp.DAL.Repositories.Genaric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Repositories
{
    public class ProjectRepository : GeneraicRepository<Project>, IProjectRepository
    {
        private readonly BTSDbContext _context;

        public ProjectRepository(BTSDbContext context) : base(context)
        {
            _context = context;
        }
      
    }
}
