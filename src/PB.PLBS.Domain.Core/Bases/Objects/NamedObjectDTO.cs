using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain
{
    /// <summary>
    /// 提供 <see cref="INamedObject"/> 对象的数据迁移对象
    /// </summary>
    public struct NamedObjectDTO
    {
        private string m_Code;
        private string m_Name;
        private int m_SortOrder;
        private bool m_Enable;
        private string m_ObjetToken;
        private bool m_IsEmpty;

        /// <summary>
        /// 描述一个空对象
        /// </summary>
        public static NamedObjectDTO Empty = CreateEmpty();

        private NamedObjectDTO(string code,string name,int sortOrder,bool enable,string token,bool empty)
        {
            m_Code = code;
            m_Name = name;
            m_SortOrder = sortOrder;
            m_Enable = enable;
            m_ObjetToken = token;
            m_IsEmpty  = empty;
        }

        /// <summary>
        /// 获取对象的编号
        /// </summary>
        public string Code { get => m_Code; }
        /// <summary>
        /// 获取对象的名称
        /// </summary>
        public string Name { get => m_Name;}
        /// <summary>
        /// 获取对象的排列序号
        /// </summary>
        public int SortOrder { get => m_SortOrder; }
        /// <summary>
        /// 获取一个值，指示对象是否有效
        /// </summary>
        public bool Enable { get => m_Enable; }
        /// <summary>
        /// 获取对象类型标识
        /// </summary>
        public string ObjetToken { get => m_ObjetToken;}
        /// <summary>
        /// 获取一个值，指示结构是否描述一个空对象
        /// </summary>
        public bool IsEmpty { get => m_IsEmpty;}

        public static NamedObjectDTO CreateEmpty()
        {
            NamedObjectDTO result = new NamedObjectDTO(string.Empty, string.Empty, 0, false, string.Empty, true);
            return result;
        }

        public static NamedObjectDTO CreateIdenty(string code)
        {
            NamedObjectDTO result = new NamedObjectDTO(code, string.Empty, 0, false, string.Empty, false);
            return result;
        }
        public static NamedObjectDTO CreateIdenty<T>(string code)
        {
            string token = typeof(T).Name;
            NamedObjectDTO result = new NamedObjectDTO(code, string.Empty, 0, false, token, false);
            return result;
        }

        public static NamedObjectDTO CreateTemporary(string code,string name)
        {
            NamedObjectDTO result = new NamedObjectDTO(code, name, 0, false, string.Empty, false);
            return result;
        }
        public static NamedObjectDTO CreateTemporary<T>(string code, string name)
        {
            string token = typeof(T).Name;
            NamedObjectDTO result = new NamedObjectDTO(code, name, 0, false, token, false);
            return result;
        }

        public static NamedObjectDTO Create(INamedObject obj)
        {
            if (obj == null) return NamedObjectDTO.Empty;
            NamedObjectDTO result = new NamedObjectDTO(obj.Code, obj.Name, obj.SortOrder, obj.Enable, obj.GetObjectToken(), false);
            return result;
        }
    }
}
