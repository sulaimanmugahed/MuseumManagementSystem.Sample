
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Application.Contracts.Persistence.Base
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(Guid id);
        Task<T?> GetAsync(Expression<Func<T, bool>> criteria);
        Task<T?> GetAsync(Guid id, params Expression<Func<T, object>>[] includes);
        Task<T?> GetAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>?> GetAllAsync();
        Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> criteria);
        Task<IEnumerable<T>?> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);
        Task<int> CountAsync();
        Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> criteria, string[] includes);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
