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
        private Location m_ConstructionLocation = Location.Empty;
        private Formula m_TargetFormula = null;

        public ProductiveTask()
        {
        }

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
        public Location ConstructionLocation { get => m_ConstructionLocation; set => m_ConstructionLocation = value; }

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
            string code = string.Format("PK-{0}", ID);
            return ProductiveTaskPackage.Create(code, this);
        }
        #endregion

        #region Factory Methods.
        public static ProductiveTask Create(string code)
        {
            if(string.IsNullOrEmpty(code))
            {
                throw new DomainExcetption(NamedObjectDTO.Empty, "不允许使用空的编号创建生产任务");
            }
            ProductiveTask result = new ProductiveTask(code);
            return result;
        }
        #endregion
    }
}
