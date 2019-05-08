using Aveneo.TestExcercise.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Aveneo.TestExcercise.Infrastructure.Data
{
    public static class EfcRepositoryIncludies
    {
        public static IQueryable<DataObject> DataObjectIncludies(DbSet<DataObject> set)
        {
            return set.Include(e => e.Features)
                .ThenInclude(e => e.Feature);
        }
    }
}
