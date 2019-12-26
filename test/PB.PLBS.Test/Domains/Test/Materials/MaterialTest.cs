using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PB.PLBS.Domain.Materials.Test
{
    public class MaterialTest
    {
        [Fact]
        public void CreateAndCopyTest()
        {
            string bomCode = "M1";
            string bomName = "N1";

            /// 使用空物料编号创建一个对象
            Material mat = null;
            try
            {
                mat = Material.Create(null, null);
            }
            catch(DomainExcetption ex)
            {
                Assert.NotNull(ex);
            }
            try
            {
                mat = Material.Create(string.Empty, null);
            }
            catch (DomainExcetption ex)
            {
                Assert.NotNull(ex);
            }

            /// 使用物料编号创建一个对象
            mat = Material.Create(bomCode, bomName);
            Assert.NotNull(mat);
            Assert.Equal(bomName, mat.BomName);
            Assert.Equal<double>(0, mat.Quantity);

            /// 初始化物料数量
            mat.InitQuantity (30);
            Assert.Equal<double>(30, mat.Quantity);

            /// 使用 PrototypeCopy 创建一个副本
            Material matCopy = mat.PrototypeCopy() as Material                                          ;
            Assert.NotNull(matCopy);
            Assert.Equal(bomCode, mat.BomCode);
            Assert.Equal(bomName, mat.BomName);
            mat.InitQuantity(30);
            Assert.Equal(30, mat.Quantity);
        }

        [Fact]
        public void MaterialCombineTest()
        {
            string bomCode1 = "M1";
            string bomName1 = "N1";
            string bomCode2 = "M2";
            double quantity1 = 30;

            /// 创建物料并初始化数量
            Material mat1 = Material.Create(bomCode1, bomName1);
            mat1.InitQuantity(quantity1);
            Assert.Equal(quantity1, mat1.Quantity);

            Material mat2 = null;


            /// 判断两个物料的物料清单是否一样
            Assert.False(mat1.EqualBom(mat2));
            Assert.True(mat1.CanCombine(mat2));

            mat1 = mat1.Combine(mat2);
            Assert.NotNull(mat1);
            Assert.Equal(quantity1, mat1.Quantity);

            /// 创建一个使用不一样的物料清单的物料
            mat2 = Material.Create(bomCode2, bomName1);
            mat2.InitQuantity(quantity1);

            /// 判断两个物料的物料清单是否一样
            Assert.False(mat1.EqualBom(mat2));
            Assert.False(mat1.CanCombine(mat2));

            /// 当尝试合并不支持的物料时，，会抛出异常
            try
            {
                mat1 = mat1.Combine(mat2);
            }
            catch (DomainExcetption ex)
            {
                Assert.NotNull(ex);
            }

            /// 创建一个使用一样的物料清单的物料
            mat2 = Material.Create(bomCode1, bomName1);
            mat2.InitQuantity(quantity1);

            /// 判断两个物料的物料清单是否一样
            Assert.True(mat1.EqualBom(mat2));
            Assert.True(mat1.CanCombine(mat2));

            /// 合并两种物料
            mat1 = mat1.Combine(mat2);
            Assert.NotNull(mat1);
            Assert.Equal(quantity1*2, mat1.Quantity);
            Assert.Equal(0, mat2.Quantity);
        }
        [Fact]
        public void ScaleUpQuantityTest()
        {
            string bomCode = "M1";
            string bomName = "N1";
            double quantity = 20;

            Material mat = Material.Create(bomCode, bomName);

            Material matScale = mat.ScaleUpQuantity(2.2);
            Assert.Equal(bomCode, matScale.BomCode);
            Assert.Equal(bomName, matScale.BomName);
            Assert.Equal(0, matScale.Quantity);

            mat.InitQuantity(quantity);
            matScale = mat.ScaleUpQuantity(2.2);
            Assert.Equal(quantity, mat.Quantity);
            Assert.Equal(quantity * 2.2, matScale.Quantity);

            matScale = mat.ScaleUpQuantity(0);
            Assert.Equal(quantity, mat.Quantity);
            Assert.Equal(0, matScale.Quantity);
        }
    }
}
