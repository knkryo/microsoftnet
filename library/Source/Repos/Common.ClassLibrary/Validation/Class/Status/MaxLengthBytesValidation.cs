
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

    /// <summary>
    /// 文字列長をバイト数でチェックするValidation
    /// </summary>
    /// <remarks></remarks>
    public class MaxLengthBytesValidation : AbstructValidation
    {

        private int _Length = 0;

        private System.Text.Encoding _Encoding;

        #region IsValid

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="validationValue">検証対象の値</param>
        /// <param name="Length">最大で許可する文字数</param>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------
        public MaxLengthBytesValidation(string validationValue, int length)
            : this(validationValue, length, System.Text.Encoding.GetEncoding("Shift-JIS"))
        {
        }

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="validationValue">検証対象の値</param>
        /// <param name="Length">最大で許可する文字数</param>
        /// <param name="encoding">バイト計算を行うエンコーディング</param>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------

        public MaxLengthBytesValidation(string validationValue, int Length, System.Text.Encoding encoding)
        {
            this._ValidationValue = validationValue;
            this._Length = Length;
            this._Encoding = encoding;

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

            return this._Encoding.GetByteCount(this._ValidationValue) <= this._Length;

        }

        #endregion

    }

}
