using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PB.PLBS.Domain
{
    public static class InMemoryDbContextFactory
    {
        public static DbContextOptions<T> CreateOptions<T>(string dataName) where T : DbContext
        {
            var optionBuilder = new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase(dataName);
            return optionBuilder.Options;
        }
    }
}
