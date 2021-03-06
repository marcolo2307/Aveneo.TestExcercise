﻿using Aveneo.TestExcercise.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aveneo.TestExcercise.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<DataObject> DataObjects { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<DataObjectFeature> DateObjectFeatures { get; set; }
        public DbSet<DataObjectGallery> DataObjectGalleries { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DataObject>()
                .OwnsOne(e => e.Location);
        }
    }
}
