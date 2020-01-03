using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PB.PLBS.Domain.Task;
using Xunit;

namespace PB.PLBS.Domain.Tasks.Test
{
    public class FormulaContextTest
    {
        [Fact]
        public void BaseSaveAndUpdateTest()
        {
            Formula formual = Formula.Create("F1", "AC20-120-水下", null);
            formual.AddFormulaItem(FormulaItem.Create(string.Empty, "B1", "N1", 10));
            formual.AddFormulaItem(FormulaItem.Create(string.Empty, "B2", "N2", 20));
            formual.AddFormulaItem(FormulaItem.Create(string.Empty, "B3", "N3", 30));

            Assert.Equal(3, formual.FormulaItemCount);

            DbContextOptions<FormulaContext> options = InMemoryDbContextFactory.CreateOptions<FormulaContext>("Formula-Base");
            using (FormulaContext content = new FormulaContext(options))
            {
                var obj =  content.Formulas.Find("F1");
                Assert.Null(obj);

                content.Formulas.Add(formual);
                content.SaveChanges();

                var objRead = content.Formulas.Find("F1");
                Assert.NotNull(objRead);
                Assert.Equal(3, objRead.FormulaItemCount);

            }
        }
    }
}
