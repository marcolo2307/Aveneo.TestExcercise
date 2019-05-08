using Aveneo.TestExcercise.ApplicationCore;
using Aveneo.TestExcercise.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.Infrastructure.Data
{
    public class EfcRepository<TEntity, TContext> 
        : IRepository<TEntity> 
        where TEntity : EntityBase
        where TContext : DbContext
    {
        protected DbContext _context { get; }
        private Func<DbSet<TEntity>, IQueryable<TEntity>> _includies { get; }

        public EfcRepository(TContext context)
        {
            _context = context;
            _includies = set => set.AsQueryable();
        }

        public EfcRepository(TContext context, Func<DbSet<TEntity>, IQueryable<TEntity>> includies)
        {
            _context = context;
            _includies = includies;
        }

        protected IQueryable<TEntity> GetEntities() => _includies(_context.Set<TEntity>());

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public Task<TEntity> FindByIdAsync(int id)
        {
            return FirstAsync(e => e.Id == id);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetEntities().FirstOrDefaultAsync(predicate);
        }

        public async Task<ICollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetEntities().Where(predicate).ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(IEnumerable<TEntity> entites)
        {
            _context.Set<TEntity>().AddRange(entites);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entites)
        {
            _context.Set<TEntity>().UpdateRange(entites);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entites)
        {
            _context.Set<TEntity>().RemoveRange(entites);
            await _context.SaveChangesAsync();
        }
    }
}
