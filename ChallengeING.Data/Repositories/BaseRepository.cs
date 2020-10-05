using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ChallengeING.Models.Interfaces;

namespace ChallengeING.Data
{
    public class BaseRepository<T> : IRepository<T>
        where T: class
    {
        private readonly SqlDBContext dbContext;

        public BaseRepository(SqlDBContext context)
        {
            this.dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entry = await this.dbContext.AddAsync(entity);

            await this.SaveChanges();

            return entry.Entity;
        }

        public async Task<IEnumerable<T>> AddManyAsync(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            entities = entities
                .Select(x => this.dbContext.Add(x))
                .Select(x => x.Entity)
                .ToArray();

            await this.SaveChanges();

            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this.dbContext
                .Remove(entity);
            
            await this.SaveChanges();
        }

        public async Task DeleteManyAsync(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            this.dbContext
                .RemoveRange(entities);

            await this.SaveChanges();
        }

        public void Dispose()
        {
            this.dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<T> GetAsync(Guid key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return await this.dbContext.Set<T>().FindAsync(key);
        }

        /// <summary>
        /// Returns all instances that match the predicate. Useful for lambda.
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="predicate">Linq expression</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return await this.dbContext.Set<T>()
                .Where(predicate)
                .ToArrayAsync();
        }

        public async Task SaveChanges()
        {
            await this.dbContext
                .SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entry = this.dbContext
                .Update(entity);

            await this.SaveChanges();

            return entry.Entity;
        }

        public IQueryable<T> GetAll()
        {
            return this.dbContext.Set<T>();
        }
    }
}
