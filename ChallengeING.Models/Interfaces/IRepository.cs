using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeING.Models.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(Guid key);
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddManyAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteManyAsync(IEnumerable<T> entities);
        Task SaveChanges();
    }
}
