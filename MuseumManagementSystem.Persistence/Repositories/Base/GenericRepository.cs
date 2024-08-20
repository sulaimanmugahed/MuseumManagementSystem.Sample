using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts.Persistence.Base;

using System.Linq.Expressions;

using System.Data;
using MuseumManagementSystem.Domain.Models.Common;

namespace MuseumManagementSystem.Persistence.Repositories.Base
{
    public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T> where T : BaseEntity
    {

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<int> CountAsync()
        {
            return await context.Set<T>().CountAsync();
        }

        public Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

      

        public async Task<IEnumerable<T>?> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> criteria)
        {
            return await context.Set<T>().Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> criteria, string[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> criteria)
        {
            return await context.Set<T>().FirstOrDefaultAsync(criteria);
        }

        public async Task<T?> GetAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(q => q.Id == id);
        }


        public async Task<T?> GetAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(criteria);
        }


        public Task<T> UpdateAsync(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);

        }
    }
}
