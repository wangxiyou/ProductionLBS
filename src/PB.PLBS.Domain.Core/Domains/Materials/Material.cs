using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain.Materials
{
    /// <summary>
    /// 描述物料对象
    /// </summary>
    [Serializable]
    public class Material : IValueObject
    {
        private string m_BomCode = string.Empty;
        private string m_BomName = string.Empty;
        private double m_Quantity = 0;

        public Material()
        {

        }

        public Material(string bomCode,string bomName,double quantity)
        {
            m_BomCode =  bomCode;
            m_BomName =  bomName;
            m_Quantity = quantity <= 0 ? 0 : quantity;
        }

        #region Public Query APIs.
        public string BomCode
        {
            get => string.IsNullOrEmpty(m_BomCode) ? string.Empty : m_BomCode;
            set => m_BomCode = string.IsNullOrEmpty(value) ? string.Empty : value;
        }
        public string BomName
        {
            get => string.IsNullOrEmpty(m_BomName) ? string.Empty : m_BomName;
            set => m_BomName = string.IsNullOrEmpty(value) ? string.Empty : value;
        }
        public double Quantity
        {
            get => m_Quantity<0 ? 0 : m_Quantity;
            set => m_Quantity = value < 0 ? 0 : value;
        }
        public bool EqualBom(Material obj)
        {
            if (obj == null) return false;
            if (string.IsNullOrEmpty(BomCode) || string.IsNullOrEmpty(obj.BomCode)) return false;
            return BomCode.Equals(obj.BomCode);
        }
        public bool CanCombine(Material obj)
        {
            if (obj == null) return true;
            if (string.IsNullOrEmpty(BomCode) || string.IsNullOrEmpty(obj.BomCode)) return false;
            return BomCode.Equals(obj.BomCode);
        }
        public bool ValidateObject()
        {
            if (string.IsNullOrEmpty(BomCode)) return false;
            return true;
        }
        #endregion

        #region Public Operator APIs.
        public void InitQuantity(double quantity)
        {
            m_Quantity = quantity < 0 ? 0 : quantity;
        }
        /// <summary>
        /// 把当前物料和指定的物料合并，返回一个新的物料，并把当前物料和指定物料清空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Material Combine(Material obj)
        {
            if(!CanCombine(obj))
            {
                throw new DomainExcetption(NamedObjectDTO.Empty, "当前物料不支持和指定的物料合并，可能原因是两种物料的物料清单不一致");
            }
            if(obj == null)
            {
                Material objCopy = PrototypeCopy() as Material;
                InitQuantity(0);
                return objCopy;
            }
            Material result = Create(BomCode, BomName);
            result.InitQuantity(Quantity + obj.Quantity);
            InitQuantity(0);
            obj.InitQuantity(0);
            return result;
        }
        public Material ScaleUpQuantity(double scale)
        {
            if (string.IsNullOrEmpty(BomCode) || Quantity <= 0)
            {
                return PrototypeCopy() as Material;
            }
            Material result = Create(BomCode, BomName);
            result.InitQuantity(scale <= 0 ? 0 : scale * Quantity);
            return result;
        }
        #endregion

        #region Members Of IValueObject.
        /// <summary>
        /// 使用当前对象为原型,创建一个副本
        /// </summary>
        /// <returns></returns>
        public IValueObject PrototypeCopy()
        {
            Material obj = new Material(m_BomCode, m_BomName, m_Quantity);
            obj.CopyProperty(this);
            return obj;
        }
        /// <summary>
        /// 从另一个对象复制属性
        /// </summary>
        /// <param name="obj"></param>
        public void CopyProperty(object obj)
        {
            Material component = obj as Material;
            if (component == null) return;
            m_BomCode = component.m_BomCode;
            m_BomName = component.m_BomName;
            m_Quantity = component.m_Quantity;
        }
        #endregion

        #region Override Members.
        public override int GetHashCode()
        {
            return BomCode.GetHashCode();
        }
        #endregion

        #region Factory Methods.
        /// <summary>
        /// 使用数据构建一个 <see cref="Material"/> 对象
        /// </summary>
        /// <param name="bomCode"></param>
        /// <param name="bomName"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static Material Builder(string bomCode, string bomName, double quantity)
        {
            Material obj = new Material(bomCode, bomName, quantity);
            return obj;
        }
        /// <summary>
        /// 使用物料编号和名称，创建一个 <see cref="Material"/> 对象
        /// </summary>
        /// <param name="bomCode"></param>
        /// <param name="bomName"></param>
        /// <returns></returns>
        public static Material Create(string bomCode,string bomName)
        {
            if(string.IsNullOrEmpty(bomCode))
            {
                throw new DomainExcetption(NamedObjectDTO.Empty, "不能使用空的物料清单编号创建物料");
            }
            Material obj = new Material(bomCode, string.IsNullOrEmpty(bomName) ? string.Empty : bomName, 0);
            return obj;
        }
        #endregion
    }
}
