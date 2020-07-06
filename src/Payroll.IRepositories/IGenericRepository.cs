using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Payroll.IRepositories
{
    public interface IGenericRepository<T> : IBaseRepository
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync();
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
    }
}