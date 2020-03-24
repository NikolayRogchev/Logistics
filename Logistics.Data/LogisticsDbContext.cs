using Logistics.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Logistics.Data
{
    public class LogisticsDbContext : DbContext
    {
        public LogisticsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Path> Paths { get; set; }
        public DbSet<LogisticCenter> LogisticCenters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LogisticCenter>().HasOne(lc => lc.City);

            builder.Entity<Path>().HasOne(p => p.From).WithMany().IsRequired(false);
            builder.Entity<Path>().HasOne(p => p.To).WithMany().IsRequired(false);

            base.OnModelCreating(builder);
        }
    }
}
