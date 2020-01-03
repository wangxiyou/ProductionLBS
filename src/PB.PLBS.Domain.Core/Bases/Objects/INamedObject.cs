using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain
{
    /// <summary>
    /// 定义一个可命名对象的接口
    /// 可命名对象具有编号和名称属性，并可以用编号作为唯一索引
    /// </summary>
    public interface INamedObject
    {
        /// <summary>
        /// 获取或设置编号
        /// </summary>
        string ID { get; set; }
        /// <summary>
        /// 获取或设置名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 获取或设置排列序号
        /// </summary>
        int SortOrder { get; set; }
        /// <summary>
        /// 获取或设置一个值，指示对象是否有效
        /// </summary>
        bool Enable { get; set; }

        /// <summary>
        /// 获取对象的类型标识
        /// </summary>
        /// <returns></returns>
        string GetObjectToken();
        /// <summary>
        /// 使用当前对象为原型,创建一个副本
        /// </summary>
        /// <returns></returns>
        INamedObject PrototypeCopy();
        /// <summary>
        /// 使用当前对象为原型,创建一个副本
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        INamedObject PrototypeCopy(string code);
        /// <summary>
        /// 从另一个对象复制属性
        /// </summary>
        /// <param name="obj"></param>
        void CopyProperty(object obj);

        /// <summary>
        /// 创建当前对简单象数据迁移对象
        /// </summary>
        /// <returns></returns>
        NamedObjectDTO CreateDTO();
    }
}
