using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PB.PLBS.Domain.DALs
{
    public class FormulaDesignTimeDbContextFactory : IDesignTimeDbContextFactory<FormulaContext>
    {
        public FormulaContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FormulaContext>();
            builder.UseSqlServer("Data Source=B-0052\\SQL2012;Initial Catalog=PLBSDB;Integrated Security=True");
            return new FormulaContext(builder.Options);
        }
    }
}
