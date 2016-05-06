using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary
{
    public static class DateCore
    {
        /// <summary>
        /// 一日を返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime GetFirstDay(DateTime target)
        {
            DateTime tmp = new DateTime(target.Year, target.Month, 1, target.Hour, target.Minute, target.Second);
            return tmp;
        }
        /// <summary>
        /// 末日を返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime GetLastDay(DateTime target)
        {
            return DateCore.GetFirstDay(target).AddMonths(1).AddDays(-1);
        }
    }
}
