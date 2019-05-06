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
        private DbContext _context { get; }

        public EfcRepository(TContext context)
        {
            _context = context;
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<ICollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(TEntity[] entites)
        {
            _context.Set<TEntity>().AddRange(entites);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity[] entites)
        {
            _context.Set<TEntity>().UpdateRange(entites);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity[] entites)
        {
            _context.Set<TEntity>().RemoveRange(entites);
            await _context.SaveChangesAsync();
        }
    }
}
