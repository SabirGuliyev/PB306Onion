using Microsoft.EntityFrameworkCore;
using OnionPronia.Domain.Entities;
using OnionPronia.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace OnionPronia.Persistence.Contexts
{
    internal class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

    }
}
