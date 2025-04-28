using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Repositories.Genaric;

namespace SchoolApp.DAL.Repositories
{
    public interface IUserBugRepository
    {
       Task<List<User_Bug>> GetAllAsync();
        Task<User_Bug?> GetByIdAsync(int userId, int bugId);
        void Add(User_Bug userBug);
        void Update(User_Bug userBug);
        void Delete(User_Bug userBug);
        Task<int> SaveChangesAsync();
    }
}
