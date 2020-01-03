using System;
using System.Collections.Generic;
using System.Text;
using PB.PLBS.Domain.Materials;

namespace PB.PLBS.Domain.Task
{
    /// <summary>
    /// 定义配方项的领域模型
    /// 配方的领域模型在生产任务上线文中，用于获取单条物料需求计划
    /// </summary>
    [Serializable]
    public class FormulaItem : NamedObject
    {
        private Formula m_Formula = null;
        private Material m_ExpectMaterial = null;

        public FormulaItem()
        {

        }

        private FormulaItem(string code,Material material)
            :base(code)
        {
            m_ExpectMaterial = material;
        }

        #region Public Query APIs.
        public Material ExpectMaterial { get => m_ExpectMaterial;}
        public Formula Formula { get => m_Formula; set => m_Formula = value; }

        public Material GetMaterialRequirement(double yield)
        {
            if (ExpectMaterial == null) return null;
            return ExpectMaterial.ScaleUpQuantity(yield);
        }
        public bool CanCombine(FormulaItem item)
        {
            if (ExpectMaterial == null || !ExpectMaterial.ValidateObject())
            {
                return false;
            }
            return ExpectMaterial.CanCombine(item?.ExpectMaterial);
        }
        public void Combine(FormulaItem item)
        {
            if(!CanCombine(item))
            {
                throw new DomainExcetption(CreateDTO(), "不支持与指定物料合并");
            }
            m_ExpectMaterial = ExpectMaterial.Combine(item?.ExpectMaterial);
        }
        #endregion


        #region Factory Methods.
        /// <summary>
        /// 使用现有数据构建一个 <see cref="FormulaItem"/> 对象
        /// </summary>
        /// <param name="code"></param>
        /// <param name="bomCode"></param>
        /// <param name="bomName"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static FormulaItem Builder(string code, string bomCode,string bomName,double quantity)
        {
            Material material = Material.Builder(bomCode, bomName, quantity);
            FormulaItem result = new FormulaItem(code,material);
            return result;
        }
        /// <summary>
        /// 创建一个 <see cref="FormulaItem"/> 对象
        /// </summary>
        /// <param name="code"></param>
        /// <param name="bomCode"></param>
        /// <param name="bomName"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static FormulaItem Create(string code,string bomCode,string bomName,double quantity)
        {
            Material material = Material.Create(bomCode, bomName);
            material.InitQuantity(quantity);
            string formulaItemCode = string.IsNullOrEmpty(code) ? Guid.NewGuid().ToString() : code;
            FormulaItem result = new FormulaItem(formulaItemCode, material);
            return result;
        }
        #endregion
    }
}
