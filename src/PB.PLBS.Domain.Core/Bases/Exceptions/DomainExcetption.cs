using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain
{
    /// <summary>
    /// 用于描述领域模型层抛出的异常
    /// </summary>
    public class DomainExcetption : Exception
    {
        private NamedObjectDTO m_SourceObject = NamedObjectDTO.Empty;

        public DomainExcetption(NamedObjectDTO source)
        {
            m_SourceObject = source;
        }
        public DomainExcetption(NamedObjectDTO source, string message)
            :base(message)
        {
            m_SourceObject = source;
        }

        /// <summary>
        /// 获取引发异常的对象
        /// </summary>
        public NamedObjectDTO SourceObject { get => m_SourceObject; }
    }
}
