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

        Task<TEntity> FindByIdAsync(string id);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);

        Task CreateAsync(TEntity entity);
        Task CreateAsync(TEntity[] entities);

        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(TEntity[] entities);

        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(TEntity[] entities);
    }
}
