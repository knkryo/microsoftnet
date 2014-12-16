
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

    /// <summary>
    /// 年月として妥当かどうかの判定を行う
    /// </summary>
    /// <remarks></remarks>
    public class YearMonthFormatValidator : AbstructValidation
    {

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="validationValue">検証対象の値</param>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------

        public YearMonthFormatValidator(string validationValue): base(validationValue)
        {

        }


        #region IsValid

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 検証を実行する
        /// </summary>
        /// <returns>True = 検証は正常 , False = 検証にて異常</returns>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        public override bool IsValid()
        {

            string value = string.Empty;
            DateTime resultDate = DateTime.MinValue;

            value = _ValidationValue.ToString();

            // ブランクの場合、検証しない
            if (value.Trim().Equals(string.Empty)) { return true; }

            // 日付として正しい形式かを判定
            value = value.Replace("/", "") + "01";
            if (DateTime.TryParseExact(value,
                                       "yyyyMMdd",
                                       null,
                                       System.Globalization.DateTimeStyles.AllowLeadingWhite | System.Globalization.DateTimeStyles.AllowTrailingWhite,
                                       out resultDate))
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

