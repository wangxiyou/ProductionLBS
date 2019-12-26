using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain
{
    public static class ArrayListHelper
    {
        static ArrayListHelper()
        {

        }

        #region Public APIs.
        /// <summary>
        /// 把列表集合转换成数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T[] ConvertListToArray<T>(IEnumerable<T> collection)
        {
            if (collection == null) return new T[] { };
            List<T> result = new List<T>();
            result.AddRange(collection);
            return result.ToArray();
        }
        #endregion
    }
}
