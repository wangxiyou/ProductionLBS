using PB.PLBS.Domain.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain.Machines
{
    /// <summary>
    /// 为混凝土的生产线建模
    /// </summary>
    [Serializable]
    public class ProductionLine : NamedObject
    {
        private IList<StorageBin> _storageBinItems = null;

        public ProductionLine()
        {

        }
        public ProductionLine(string code)
        {

        }

        #region Public Query APIs.
        /// <summary>
        /// 获取包含的所有存储仓的集合
        /// </summary>
        public List<StorageBin> StorageBinItms
        {
            get { return ArrayListHelper.ConvertListToList<StorageBin>(GetmStorageBinItems()); }
            set
            {
                if (value != null)
                {
                    GetmStorageBinItems().Clear();
                    foreach (StorageBin item in value)
                    {
                        GetmStorageBinItems().Add(item);
                    }
                }
            }
        }
        public Material[] GetSupportMaterials()
        {
            List<Material> result = new List<Material>();
            Dictionary<string, Material> materialMap = new Dictionary<string, Material>();
            foreach (StorageBin bin in GetmStorageBinItems())
            {
                if (bin == null) continue;
                if (bin.StorageMaterial == null || !bin.StorageMaterial.ValidateObject()) continue;
                if(materialMap.ContainsKey(bin.StorageMaterial.BomCode))
                {
                    materialMap[bin.StorageMaterial.BomCode] = bin.StorageMaterial.Combine(bin.StorageMaterial);
                }
                else
                {
                    materialMap.Add(bin.StorageMaterial.BomCode, bin.StorageMaterial.PrototypeCopy() as Material);
                }

            }
            return ArrayListHelper.ConvertListToArray<Material>(materialMap.Values);
        }
        #endregion

        #region Internal Members.
        /// <summary>
        /// 获取包含的所有存储仓的集合
        /// </summary>
        /// <returns></returns>
        protected virtual IList<StorageBin> GetmStorageBinItems()
        {
            if (_storageBinItems == null)
            {
                _storageBinItems = new List<StorageBin>();
            }
            return _storageBinItems;
        }
        #endregion
    }
}
