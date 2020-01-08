using System;
using System.Collections.Generic;
using System.Text;
using PB.PLBS.Domain.Materials;

namespace PB.PLBS.Domain.Task
{
    /// <summary>
    /// 定义配方项的领域模型
    /// 配方的领域模型在生产任务上线文中，用于描述生产计划的物料需求计划
    /// </summary>
    [Serializable]
    public class Formula : NamedObject
    {
        private string _concreteTokenString = string.Empty;
        private IList<FormulaItem> _formulaItms;

        public Formula()
        {

        }

        private Formula(string code)
            :base(code)
        {

        }

        #region Public Query APIs.
        /// <summary>
        /// 获取或设置配方的混凝土标记
        /// </summary>
        public string ConcreteTokenString { get => _concreteTokenString; set => _concreteTokenString = value; }
        /// <summary>
        /// 获取包含的所有配方项的集合
        /// </summary>
        public List<FormulaItem> FormulaItms
        {
            get { return ArrayListHelper.ConvertListToList<FormulaItem>(GetFormulaItms()); }
            set
            {
                if(value != null)
                {
                    GetFormulaItms().Clear();
                    foreach(FormulaItem item in value)
                    {
                        GetFormulaItms().Add(item);
                    }
                }
            }
        }
        public int FormulaItemCount
        {
            get => FormulaItms == null ? 0 : FormulaItms.Count;
        }
        public Material[] GetMaterialRequirements(double yield)
        {
            List<Material> result = new List<Material>();
            foreach (FormulaItem item in GetFormulaItms())
            {
                if (item == null) continue;
                Material mat = item.GetMaterialRequirement(yield);
                if (mat != null) result.Add(mat);
            }
            return result.ToArray();
        }
        #endregion

        #region Public Operate APIs.
        public void AddFormulaItem(FormulaItem item)
        {
            if (item == null) return;
            foreach(FormulaItem obj in GetFormulaItms())
            {
                if (obj == null) continue;
                if(obj.CanCombine(item))
                {
                    obj.Combine(item);
                    return;
                }
            }
            GetFormulaItms().Add(item);
        }
        public void RemoveFormulaItem(FormulaItem item)
        {
            GetFormulaItms().Remove(item);
        }
        #endregion

        #region Internal Members.
        /// <summary>
        /// 获取包含的所有配方项的集合
        /// </summary>
        /// <returns></returns>
        protected virtual IList<FormulaItem> GetFormulaItms()
        {
            if (_formulaItms == null)
            {
                _formulaItms = new List<FormulaItem>();
            }
            return _formulaItms;
        }
        #endregion

        #region Factory Methods.
        public static Formula Builder(string code, string concreteToken, IEnumerable<FormulaItem> items)
        {
            Formula result = new Formula(code);
            result.ConcreteTokenString = concreteToken;
            if (items != null)
            {
                foreach (FormulaItem item in items)
                {
                    result.AddFormulaItem(item);
                }
            }
            return result;
        }
        public static Formula Create(string code,string concreteToken,IEnumerable<FormulaItem> items)
        {
            Formula result = new Formula(code);
            result.ConcreteTokenString = concreteToken;
            if(items!= null)
            {
                foreach(FormulaItem  item in items)
                {
                    result.AddFormulaItem(item);
                }
            }
            return result;
        }
        #endregion
    }
}
