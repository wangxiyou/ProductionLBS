using System;
using System.Collections.Generic;
using System.Text;
using PB.PLBS.Domain.Materials;

namespace PB.PLBS.Domain.Task
{
    /// <summary>
    /// 定义生产任务包领域模型
    /// </summary>
    [Serializable]
    public class ProductiveTaskPackage : NamedObject
    {
        private IList<ProductiveTask> m_TaskItems = null;

        public ProductiveTaskPackage()
        {
        }
        public ProductiveTaskPackage(string code)
            :base(code)
        {
        }

        #region Public Query APIs.
        /// <summary>
        /// 获取包含的所有生产任务的集合
        /// </summary>
        public List<ProductiveTask> TaskItms
        {
            get { return ArrayListHelper.ConvertListToList<ProductiveTask>(GetTaskItms()); }
            set
            {
                if(value != null)
                {
                    foreach(ProductiveTask item in value)
                    {
                        GetTaskItms().Add(item);
                    }
                }
            }
        }
        public Material[] GetMaterialRequirements()
        {
            List<Material> result = new List<Material>();
            foreach(ProductiveTask item in GetTaskItms())
            {
                if (item == null) continue;
                Material[] items = item.GetMaterialRequirements();
                foreach(Material obj in items)
                {
                    AddMaterialInArray(result, obj);
                }
            }
            return result.ToArray();
        }
        #endregion

        #region Public Operate APIs.
        public void AddTaskItem(ProductiveTask item)
        {
            if (item == null) return;
            if(GetTaskItms().Contains(item))
            {
                throw new DomainExcetption(CreateDTO(), "已经存现相同的任务项");
            }
            GetTaskItms().Add(item);
        }
        public void RemoveTaskItem(ProductiveTask item)
        {
            GetTaskItms().Remove(item);
        }
        #endregion

        #region Internal Members.
        /// <summary>
        /// 获取包含的所有生产任务的集合
        /// </summary>
        /// <returns></returns>
        protected virtual IList<ProductiveTask> GetTaskItms()
        {
            if (m_TaskItems == null)
            {
                m_TaskItems = new List<ProductiveTask>();
            }
            return m_TaskItems;
        }
        private void AddMaterialInArray(List<Material> materials,Material material)
        {
            if (material == null || materials == null) return;
            bool add = false;
            for(int i=0;i<materials.Count;i++)
            {
                if (materials[i] == null) continue;
                if(materials[i].CanCombine(material))
                {
                    materials[i] = materials[i].Combine(material);
                    add = true;
                    break;
                }
            }
            if (!add) materials.Add(material);
        }
        #endregion

        #region Factory Methods.
        public static ProductiveTaskPackage Create(string code, ProductiveTask task)
        {
            if(string.IsNullOrEmpty(code))
            {
                throw new DomainExcetption(NamedObjectDTO.Empty, "不能使用空的编号创建任务包");
            }
            ProductiveTaskPackage result = new ProductiveTaskPackage(code);
            result.AddTaskItem(task);
            return result;
        }
        public static ProductiveTaskPackage Create(string code,ProductiveTask master,ProductiveTask mortar)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new DomainExcetption(NamedObjectDTO.Empty, "不能使用空的编号创建任务包");
            }
            if(master == null)
            {
                throw new DomainExcetption(NamedObjectDTO.Empty, "不能创建主任务为空的任务包");
            }
            ProductiveTaskPackage result = new ProductiveTaskPackage(code);
            result.AddTaskItem(master);
            result.AddTaskItem(mortar);
            return result;
        }
        #endregion
    }
}
