using Aveneo.TestExcercise.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aveneo.TestExcercise.Infrastructure.Data
{
    public class DataObjectRepository
        : EfcRepository<DataObject, DataContext>
    {
        public DataObjectRepository(DataContext context) : base(context)
        {
        }

        public override async Task<ICollection<DataObject>> GetAllAsync()
        {
            return await _context.Set<DataObject>()
                .Include(e => e.Features)
                .ThenInclude(e => e.Feature)
                .ToListAsync();
        }

        public override async Task<DataObject> FirstAsync(Expression<Func<DataObject, bool>> predicate)
        {
            return await _context.Set<DataObject>()
                .Include(e => e.Features)
                .ThenInclude(e => e.Feature)
                .FirstOrDefaultAsync(predicate);
        }

        public override Task<DataObject> FindByIdAsync(int id)
        {
            return FirstAsync(e => e.Id == id);
        }

        public override async Task<ICollection<DataObject>> WhereAsync(Expression<Func<DataObject, bool>> predicate)
        {
            var entities = _context.Set<DataObject>()
                .Include(e => e.Features)
                .ThenInclude(e => e.Feature);
            return await entities.Where(predicate).ToListAsync();
        }
    }
}
