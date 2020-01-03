using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PB.PLBS.Domain.Materials;

namespace PB.PLBS.Domain.Materials
{
    public class MaterialContext : DbContext
    {
        public MaterialContext()
        {

        }

        public MaterialContext(DbContextOptions<MaterialContext> options)
            : base(options)
        {

        }
        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>().HasKey(f => f.BomCode);
            //base.OnModelCreating(modelBuilder);
        }

    }
}
