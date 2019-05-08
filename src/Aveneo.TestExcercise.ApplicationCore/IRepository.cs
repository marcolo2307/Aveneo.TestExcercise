using Aveneo.TestExcercise.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.ApplicationCore
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<ICollection<TEntity>> GetAllAsync();

        Task<TEntity> FindByIdAsync(int id);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);

        Task CreateAsync(TEntity entity);
        Task CreateAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(IEnumerable<TEntity> entities);
    }
}
