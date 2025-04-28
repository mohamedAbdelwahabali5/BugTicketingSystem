using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Repositories.Genaric;
namespace SchoolApp.DAL.Repositories.Genaric
{
    public interface IGenaricRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task Add(T entity);
        Task Update(T entity);
        void Delete(T entity);  
        Task<int> SaveChangesAsync();
    }
}
