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
    public class UserRepository : GeneraicRepository<User>, IUserRepository 
    {
        private readonly BTSDbContext _context;
        public UserRepository(BTSDbContext context):base(context) 
        {
            _context = context;
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
