using Aveneo.TestExcercise.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aveneo.TestExcercise.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<DataObject> DataObjects { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<DataObjectFeature> DateObjectFeatures { get; set; }

        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DataObject>()
                .OwnsOne(e => e.Location);

            builder.Entity<DataObjectFeature>()
                .HasOne(e => e.DataObject)
                .WithMany(e => e.Features)
                .OnDelete(DeleteBehavior.Cascade);

            builder.SeedFeatures();
        }
    }
}
