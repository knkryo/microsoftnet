
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using Common.ClassLibrary.Extensions;

namespace Common.ClassLibrary.Validation
{

    /// <summary>
    /// Aの年月とBの年月が指定月数の範囲かどうかをチェックする
    /// 範囲内であればTrue,範囲外の場合はFalse
    /// </summary>
    /// <remarks></remarks>
    public class MonthsDistanceValidation : AbstructValidation
    {

        private string _validationValue2;
        private int _distanceMonth;
        #region IsValid

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="validationValue">検証対象の値</param>
        /// <param name="minValue">許可する最小値</param>
        /// <param name="maxValue">許可する最大値</param>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------

        public MonthsDistanceValidation(string validationValue, string validationValue2, Int32 distanceMonth)
        {
            base._ValidationValue = validationValue;
            this._validationValue2 = validationValue2;
            this._distanceMonth = distanceMonth;

        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 検証を実行する
        /// </summary>
        /// <returns>True = 検証は正常 , False = 検証にて異常</returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        public override bool IsValid()
        {

             string fromValue = this._ValidationValue.ToString().Replace("/", "");
             string toValue = this._validationValue2.ToString().Replace("/", "");

            // パラメータのチェック処理
            if (fromValue.Trim() == string.Empty){ return true; }
            if (toValue.Trim() == string.Empty) { return true; }

            if (fromValue.Length == 6) { fromValue = fromValue + "01"; }
            if (toValue.Length == 6) { toValue = toValue + "01"; }

            if (fromValue.Length != 8) { return true; }
            if (toValue.Length != 8) { return true; }

            // 日付カウント
            DateTime targetFrom = fromValue.ToDateTime();
            DateTime targetTo = toValue.ToDateTime();

            if (targetFrom.AddMonths(this._distanceMonth) > targetTo)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

    }

}
