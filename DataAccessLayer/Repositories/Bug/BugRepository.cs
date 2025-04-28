using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using SchoolApp.DAL.Repositories.Genaric;
using SchoolApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolApp.DAL.Repositories
{
    public class BugRepository : GeneraicRepository<Bug>, IBugRepository
    {
        private readonly BTSDbContext _context;
        public BugRepository(BTSDbContext context):base(context) 
        {
            _context = context;
        }

        

        
    }
}
