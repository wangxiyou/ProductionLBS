using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain
{
    /// <summary>
    /// 定义一个可命名对象的基础实现
    /// </summary>
    [Serializable]
    public class NamedObject : INamedObject, IComparable<INamedObject>
    {
        private string m_Code = string.Empty;
        private string m_Name = string.Empty;
        private int m_SortOrder = 0;
        private bool m_Enable = true;

        public NamedObject()
            :this(string.Empty)
        {

        }
        public NamedObject(string code)
        {
            m_Code = string.IsNullOrEmpty(code) ? string.Empty : code;
        }

        #region Members Of INamedObject.
        /// <summary>
        /// 获取或设置编号
        /// </summary>
        public string Code
        {
            get
            {
                return string.IsNullOrEmpty(m_Code) ? string.Empty : m_Code;
            }
            set
            {
                m_Code = string.IsNullOrEmpty(value) ? string.Empty : value;
            }
        }
        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(m_Code) ? string.Empty : m_Code;
            }
            set
            {
                m_Code = string.IsNullOrEmpty(value) ? string.Empty : value;
            }
        }
        /// <summary>
        /// 获取或设置排列序号
        /// </summary>
        public int SortOrder { get => m_SortOrder; set => m_SortOrder = value; }
        /// <summary>
        /// 获取或设置一个值，指示对象是否有效
        /// </summary>
        public bool Enable { get=> m_Enable; set=> m_Enable = value; }

        /// <summary>
        /// 获取对象的类型标识
        /// </summary>
        /// <returns></returns>
        public  virtual string GetObjectToken()
        {
            return GetType().Name;
        }
        /// <summary>
        /// 使用当前对象为原型,创建一个副本
        /// </summary>
        /// <returns></returns>
        public  INamedObject PrototypeCopy()
        {
            return PrototypeCopy(Code);
        }
        /// <summary>
        /// 使用当前对象为原型,创建一个副本
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual INamedObject PrototypeCopy(string code)
        {
            NamedObject result = new NamedObject(code);
            result.CopyProperty(this);
            return result;
        }
        /// <summary>
        /// 从另一个对象复制属性
        /// </summary>
        /// <param name="obj"></param>
        public virtual void CopyProperty(object obj)
        {
            NamedObject component = obj as NamedObject;
            if (component == null) return;
            m_Name = component.m_Name;
            m_SortOrder = component.m_SortOrder;
            m_Enable = component.m_Enable;
        }
        public virtual int CompareTo(INamedObject other)
        {
            if (other == null) return 1;
            return SortOrder.CompareTo(other.SortOrder);
        }
        /// <summary>
        /// 创建当前对简单象数据迁移对象
        /// </summary>
        /// <returns></returns>
        public NamedObjectDTO CreateDTO()
        {
            return NamedObjectDTO.Create(this);
        }
        #endregion

        #region Override Members.
        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            NamedObject component = obj as NamedObject;
            if (component == null) return false;
            return Code.Equals(component.Code);
        }
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
