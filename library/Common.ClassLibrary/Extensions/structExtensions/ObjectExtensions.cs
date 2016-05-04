using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary.Extensions
{
    public static class ObjectExtensions
    {

        //値変換

        #region ConvertNullToEmpty

        /// <summary>
        /// 【拡張メソッド】nullの場合、String.Emptyを返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConvertNullToEmpty(this object target)
        {
            return (target == null ? string.Empty : target.ToString());
        }

        #endregion

        //型変換

        #region ToBoolean

        /// <summary>
        /// 【拡張メソッド】空白ならFalse,そうでない場合にはBoolean変換をした結果を返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object target)
        {
            if (Convert.ToString(target) == string.Empty)
            {
                return false;
            }
            else
            {
                return TryParseExtensions.ToSafeParseBoolean(Convert.ToString(target));
            }

        }

        #endregion

        //型変換 (他関数I/Fのみ

        #region ToSafeInt32

        /// <summary>
        /// 【拡張メソッド】安全なInt32型に変換する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Int32 ToSafeInt32(this object target)
        {
            return TryParseExtensions.ToSafeParseInt32(Convert.ToString(target));
        }

        #endregion

        #region ToSafeInt64

        /// <summary>
        /// 【拡張メソッド】安全なInt64型に変換する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Int64 ToSafeInt64(this object target)
        {
            return TryParseExtensions.ToSafeParseInt64(Convert.ToString(target));
        }

        #endregion

        #region ToSafeLong

        /// <summary>
        /// 【拡張メソッド】安全なLong型に変換する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static long ToSafeLong(this object target)
        {
            return TryParseExtensions.ToSafeParseInt64(Convert.ToString(target));
        }

        #endregion

        #region ToSafeDecimal

        /// <summary>
        /// 【拡張メソッド】安全なDecimal型に変換する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static System.Decimal ToSafeDecimal(this object target)
        {
            return TryParseExtensions.ToSafeParseDecimal(Convert.ToString(target));
        }

        #endregion

        #region ToTruncateValue

        /// <summary>
        /// 【拡張メソッド】Truncateした値に変換する
        /// </summary>
        /// <param name="target">Nullの場合、Null、数値評価できる場合、Truncate、そうでない場合、入力値のまま</param>
        /// <returns></returns>
        public static string ToTruncateValue(this object target, int length)
        {
            return Utils.MathUtils.TruncateValue(target, length);
        }

        #endregion

    }
}
