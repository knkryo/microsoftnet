using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary
{
    public static class ObjectCore
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
                return TryParseCore.ToSafeParseBoolean(Convert.ToString(target));
            }

        }

        #endregion

        public static object CastTo(object source, object dest)
        {
            ClassValueTransfer trans = new ClassValueTransfer();
            trans.Invoke(source, dest);
            return dest;
        }


    }
}
