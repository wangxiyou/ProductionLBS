using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PB.PLBS.Domain;
using PB.PLBS.Domain.Materials;
using Xunit;

namespace PB.PLBS.Domains.DALs.Mateials
{
    public class MaterialContextTest
    {
        [Fact]
        public void BaseOperationTest()
        {
            DbContextOptions<MaterialContext> options = InMemoryDbContextFactory.CreateOptions<MaterialContext>("Material_Base");
            using (MaterialContext content = new MaterialContext(options))
            {
                var obj = content.Materials.SingleOrDefault<Material>(f => f.BomCode == "M1");
                Assert.Null(obj);

                Material m1 = Material.Builder("M1", "N1", 30);
                Material m2 = Material.Builder("M2", "N2", 0);
                Material m3 = Material.Builder("M3", "N3", 20);

                content.Materials.Add(m1);
                content.Materials.Add(m2);
                content.Materials.Add(m3);
                content.Materials.Add(m3);
                var count = content.SaveChanges();

                Assert.Equal(3, count);

                obj = content.Materials.SingleOrDefault<Material>(f => f.BomCode == "M1");
                Assert.NotNull(obj);
                Assert.Equal("M1", obj.BomCode);
                Assert.Equal("N1", obj.BomName);
                Assert.Equal(30, obj.Quantity);

                var objs = content.Materials.Where(f => f.Quantity > 0).ToList<Material>();
                Assert.Equal(2, objs.Count());

                objs = content.Materials.Where(f => f.Quantity/2 > 10).ToList<Material>();
                Assert.Single(objs);

                objs = content.Materials.Where(f => f.BomName.Contains("N")).ToList<Material>();
                Assert.Equal(3, objs.Count());

                objs = content.Materials.Where(f => f.BomName.Contains("T")).ToList<Material>();
                Assert.Empty(objs);

                objs = content.Materials.Where(f => !f.BomName.Contains("T")).ToList<Material>();
                Assert.Equal(3, objs.Count());

                m2.BomName = "T2";
                content.Materials.Update(m2);
                content.SaveChanges();

                objs = content.Materials.Where(f => !f.BomName.Contains("T")).ToList<Material>();
                Assert.Equal(2, objs.Count());
            }
        }
    }
}
