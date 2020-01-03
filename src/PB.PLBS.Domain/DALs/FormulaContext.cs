using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PB.PLBS.Domain.Task;

namespace PB.PLBS.Domain
{
    public class FormulaContext : DbContext
    {
        public FormulaContext(DbContextOptions<FormulaContext> options)
            :base(options)
        {

        }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<FormulaItem> FormulaItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormulaItem>().HasOne(f => f.Formula)
                .WithMany(p=>p.FormulaItms);
            //modelBuilder.Entity<Formula>().HasMany(f => f.FormulaItms).WithOne();
            //base.OnModelCreating(modelBuilder);
        }
    }
}
