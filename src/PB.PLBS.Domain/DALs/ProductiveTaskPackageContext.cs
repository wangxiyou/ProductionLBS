using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PB.PLBS.Domain.Task
{
    public class ProductiveTaskPackageContext : DbContext
    {
        public ProductiveTaskPackageContext(DbContextOptions<ProductiveTaskPackageContext> options)
            :base(options)
        {

        }
        public ProductiveTaskPackageContext()
        {

        }

        public DbSet<ProductiveTaskPackage> TaskPackages { get; set; }
        public DbSet<ProductiveTask> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductiveTaskPackage>().HasMany(p=>p.TaskItms).WithOne();
        }
    }
}
