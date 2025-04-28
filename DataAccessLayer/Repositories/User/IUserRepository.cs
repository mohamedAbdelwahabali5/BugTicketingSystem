using DataAccessLayer.Models;
using SchoolApp.DAL.Repositories.Genaric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Repositories
{
    public interface IUserRepository : IGenaricRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
