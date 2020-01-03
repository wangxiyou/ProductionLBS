using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PB.PLBS.Domain.Task
{
    public class ProductiveTaskContext : DbContext
    {
        public ProductiveTaskContext(DbContextOptions<ProductiveTaskContext> options)
            :base(options)
        {

        }
        public ProductiveTaskContext()
        {

        }

        public DbSet<ProductiveTask> Tasks { get; set; }
        public DbSet<Formula> Formulas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductiveTask>().HasOne(f => f.TargetFormula).WithOne();
        }
    }
}
