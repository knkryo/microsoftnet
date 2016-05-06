using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary.Extensions
{
    public static class DateExtensions
    {
        /// <summary>
        /// 一日を返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime GetFirstDay(this DateTime target)
        {
            return DateCore.GetFirstDay(target);
        }
        /// <summary>
        /// 末日を返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime GetLastDay(this DateTime target)
        {
            return DateCore.GetLastDay(target);
        }
    }
}
