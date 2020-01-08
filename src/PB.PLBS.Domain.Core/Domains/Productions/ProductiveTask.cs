using System;
using System.Collections.Generic;
using System.Text;
using PB.PLBS.Domain.Materials;

namespace PB.PLBS.Domain.Task
{
    public class ProductiveTask : NamedObject
    {
        private string _purchaserSide = string.Empty;
        private string _constructionSide = string.Empty;
        private string _projectName = string.Empty;
        private double _expectQuantity = 0;
        private Location _constructionLocation = Location.Empty;
        private Formula _targetFormula = null;

        public ProductiveTask()
        {
        }

        public ProductiveTask(string code)
            : base(code)
        {
        }

        #region Public Query APIs.
        public string PurchaserSide { get => _purchaserSide; set => _purchaserSide = value; }
        public string ConstructionSide { get => _constructionSide; set => _constructionSide = value; }
        public string ProjectName { get => _projectName; set => _projectName = value; }
        public double ExpectQuantity { get => _expectQuantity; set => _expectQuantity = value; }
        public Formula TargetFormula { get => _targetFormula; set => _targetFormula = value; }
        public Location ConstructionLocation { get => _constructionLocation; set => _constructionLocation = value; }

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
