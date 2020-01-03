using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace PB.PLBS.Domain.Task
{
    public class ProductiveTaskContextTest
    {
        [Fact]
        public void BaseSaveAndUpdateTest()
        {
            DbContextOptions<ProductiveTaskContext> options = InMemoryDbContextFactory.CreateOptions<ProductiveTaskContext>("ProductiveTask-Base");
            using (ProductiveTaskContext context = new ProductiveTaskContext(options))
            {

            }
        }
    }
}
