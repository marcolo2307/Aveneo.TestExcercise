using Aveneo.TestExcercise.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aveneo.TestExcercise.Infrastructure.Data
{
    public static class DataSeed
    {
        public static void SeedFeatures(this ModelBuilder builder)
        {
            builder.Entity<Feature>().HasData(
                new Feature { Id = -1, IconName = "fas-fa-mountain" },
                new Feature { Id = -2, IconName = "fas-fa-bike" },
                new Feature { Id = -3, IconName = "fas fa-swimmer" },
                new Feature { Id = -4, IconName = "fas fa-trees" });
        }
    }
}
