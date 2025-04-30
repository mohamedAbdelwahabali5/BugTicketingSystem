using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Repositories
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
