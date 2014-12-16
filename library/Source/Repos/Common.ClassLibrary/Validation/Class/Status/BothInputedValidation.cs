
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

    /// <summary>
    /// 両方が入力されているかどうかをチェックする
    /// 両方とも入力されているか、両方とも入力されていない場合=True,片方のみの場合=False
    /// </summary>
    /// <remarks></remarks>
    public class BothInputedValidation : AbstructValidation
    {

        private string _validationValue2;

        #region IsValid

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="validationValue">検証対象の値</param>
        /// <param name="validationValue2">検証対象の値２</param>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------

        public BothInputedValidation(string validationValue,string validationValue2)
        {
            base._ValidationValue = validationValue;
            this._validationValue2 = validationValue2;

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
            if (_ValidationValue.ToString() == string.Empty && _validationValue2.ToString() == string.Empty)
            {
                return true;
            }
            else
            {
                return _ValidationValue.ToString() != string.Empty && _validationValue2.ToString() != string.Empty;
            }
        }

        #endregion

    }

}
