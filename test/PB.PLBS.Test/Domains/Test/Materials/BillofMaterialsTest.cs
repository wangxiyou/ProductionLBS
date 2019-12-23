using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PB.PLBS.Domains.Test
{
    public class BillofMaterialsTest
    {
        /// <summary>
        /// BillOfMaterial 对象创建和复制测试
        /// </summary>
        [Fact]
        public void BillofMaterialsCreateAndCopyTest()
        {
            string bomCode1 = "BomCode1";

            /// 使用空字符串调用 BillofMaterial.Create 创建 BillOfMaterial ，返回 Null
            BillofMaterial bom = BillofMaterial.Create(string.Empty);
            Assert.Null(bom);
            bom = BillofMaterial.Create(null);
            Assert.Null(bom);

            /// 使用字符串创建 BillofMaterial 正常返回新创建的 BillofMaterial
            bom = BillofMaterial.Create(bomCode1);
            Assert.NotNull(bom);
            bom.Name = "物料1";

            BillofMaterial bomCopy = bom.PrototypeCopy();
            Assert.NotNull(bomCopy);
            Assert.Equal<string>(bom.Code, bom.Code);
            Assert.Equal<string>(bom.Name, bom.Name);
        }
    }
}
