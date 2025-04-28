using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using SchoolApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Repositories
{
    public class UserBugRepository : IUserBugRepository
    {
        private readonly BTSDbContext _context;
        public UserBugRepository(BTSDbContext context)
        {
            _context = context;
        }

        public void Add(User_Bug entity)
        {
            _context.User_Bugs.Add(entity);
        }

        public void Delete(User_Bug entity)
        {
            _context.User_Bugs.Remove(entity);
        }

        public async Task<List<User_Bug>> GetAllAsync()
        {
            return await _context.User_Bugs.ToListAsync();
        }

        public async Task<User_Bug?> GetByIdAsync(int Uid, int Bid)
        {
            return await _context.User_Bugs.FirstOrDefaultAsync(s => s.UserId == Uid && s.BugId == Bid);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(User_Bug entity)
        {

        }
    }
}
