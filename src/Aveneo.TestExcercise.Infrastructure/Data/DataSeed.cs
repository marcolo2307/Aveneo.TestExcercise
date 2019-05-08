using Aveneo.TestExcercise.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Aveneo.TestExcercise.Infrastructure.Data
{
    public static class DataSeed
    {
        public static void SeedFeatures(DataContext context)
        {
            var getData = context.Set<Feature>().ToArrayAsync();
            getData.Wait();

            if (getData.Result.Length > 0)
                return;

            var features = new List<Feature>
            {
                new Feature { IconName = "fas fa-spinner fa-spin" },
                new Feature { IconName = "fas fa-circle-notch fa-spin" },
                new Feature { IconName = "fas fa-sync-alt fa-spin" },
                new Feature { IconName = "fas fa-cog fa-spin" },
                new Feature { IconName = "fas fa-horse" },
                new Feature { IconName = "fas fa-circle" },
                new Feature { IconName = "fas fa-twitter" },
                new Feature { IconName = "fas fa-ban" }
            };

            context.Set<Feature>().AddRange(features);
            context.SaveChanges();
        }
    }
}
