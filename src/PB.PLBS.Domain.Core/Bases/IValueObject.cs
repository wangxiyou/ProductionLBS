using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain
{
    /// <summary>
    /// 定义一个描述值的对象
    /// 值对象没有唯一索引
    /// </summary>
    public interface IValueObject
    {
        /// <summary>
        /// 使用当前对象为原型,创建一个副本
        /// </summary>
        /// <returns></returns>
        IValueObject PrototypeCopy();
        /// <summary>
        /// 从另一个对象复制属性
        /// </summary>
        /// <param name="obj"></param>
        void CopyProperty(object obj);
    }
}
