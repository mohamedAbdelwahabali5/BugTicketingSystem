using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
namespace DataAccessLayer.Repositories
{
    public interface IGenaricRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate,
                 params Expression<Func<T, object>>[] includes);
        Task Add(T entity);
        Task Update(T entity);
        void Delete(T entity);  
        Task<int> SaveChangesAsync();
    }
}
