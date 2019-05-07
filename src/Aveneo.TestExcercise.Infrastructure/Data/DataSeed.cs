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
                new Feature { IconName = "fas-fa-mountain" },
                new Feature { IconName = "fas-fa-camera" },
                new Feature { IconName = "fas fa-fish" }
            };

            context.Set<Feature>().AddRange(features);
            context.SaveChanges();
        }
    }
}
