using PB.PLBS.Domain.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain.Machines
{
    /// <summary>
    /// 为混凝土生产线的物料存储仓建模
    /// </summary>
    [Serializable]
    public class StorageBin : NamedObject
    {
        private Material _storageMaterial = null;

        public StorageBin()
        {

        }

        public StorageBin(string code)
            :base(code)
        {

        }

        #region Public Query APIs.
        /// <summary>
        /// 获取存储的物料
        /// </summary>
        public Material StorageMaterial { get => _storageMaterial; }
        #endregion

        #region Public Operator APIs.
        /// <summary>
        /// 初始化存储的物料
        /// </summary>
        /// <param name="material"></param>
        public void InitStorage(Material material)
        {
            _storageMaterial = material;
        }
        #endregion
    }
}
