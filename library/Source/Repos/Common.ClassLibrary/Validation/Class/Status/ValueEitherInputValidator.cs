using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ClassLibrary.Validation
{
    /// <summary>
    /// AかBいずれかが入力されているかをチェックする
    /// </summary>
    public class ValueEitherInputValidator:AbstructValidation
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

        public ValueEitherInputValidator(string validationValue, string validationValue2):base(validationValue)
        {
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
            return !(_ValidationValue.ToString() == string.Empty && _validationValue2.ToString() == string.Empty);
        }

        #endregion
    }
}
