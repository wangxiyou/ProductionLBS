using System;
using System.Collections.Generic;
using System.Text;
using PB.PLBS.Domain.Materials;

namespace PB.PLBS.Domain.Task
{
    public class ProductiveTask : NamedObject
    {
        private string m_PurchaserSide = string.Empty;
        private string m_ConstructionSide = string.Empty;
        private string m_ProjectName = string.Empty;
        private double m_ExpectQuantity = 0;
        private Location m_Construction = Location.Empty;
        private Formula m_TargetFormula = null;

        public ProductiveTask(string code)
            : base(code)
        {

        }

        #region Public Query APIs.
        public string PurchaserSide { get => m_PurchaserSide; set => m_PurchaserSide = value; }
        public string ConstructionSide { get => m_ConstructionSide; set => m_ConstructionSide = value; }
        public string ProjectName { get => m_ProjectName; set => m_ProjectName = value; }
        public double ExpectQuantity { get => m_ExpectQuantity; set => m_ExpectQuantity = value; }
        public Formula TargetFormula { get => m_TargetFormula; set => m_TargetFormula = value; }
        public Location Construction { get => m_Construction; set => m_Construction = value; }

        public Material[] GetMaterialRequirements()
        {
            if (TargetFormula == null) return new Material[] { };
            return TargetFormula.GetMaterialRequirements(ExpectQuantity);
        }
        /// <summary>
        /// 获取包含当前任务的任务包
        /// </summary>
        /// <returns></returns>
        public ProductiveTaskPackage GetTaskPackage()
        {
            string code = string.Format("PK-{0}", Code);
            return ProductiveTaskPackage.Create(code, this);
        }
        #endregion
    }
}
