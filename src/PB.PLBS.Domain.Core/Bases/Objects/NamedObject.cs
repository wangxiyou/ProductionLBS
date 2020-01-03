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
        private string m_ID = string.Empty;
        private string m_Name = string.Empty;
        private int m_SortOrder = 0;
        private bool m_Enable = true;

        public NamedObject()
            :this(string.Empty)
        {

        }
        public NamedObject(string id)
        {
            m_ID = string.IsNullOrEmpty(id) ? string.Empty : id;
        }

        #region Members Of INamedObject.
        /// <summary>
        /// 获取或设置编号
        /// </summary>
        public string ID
        {
            get
            {
                return string.IsNullOrEmpty(m_ID) ? string.Empty : m_ID;
            }
            set
            {
                m_ID = string.IsNullOrEmpty(value) ? string.Empty : value;
            }
        }
        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(m_Name) ? string.Empty : m_Name;
            }
            set
            {
                m_Name = string.IsNullOrEmpty(value) ? string.Empty : value;
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
            return PrototypeCopy(ID);
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
            return ID.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            NamedObject component = obj as NamedObject;
            if (component == null) return false;
            return ID.Equals(component.ID);
        }
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
