
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

    /// <summary>
    /// 指定された値の中に含まれていないかどうかをチェックするValidation
    /// 含まれていなければTrue , 含まれていればFalse
    /// </summary>
    /// <remarks></remarks>
    public class CodeListNotExistsValidation : AbstructValidation
    {

        private List<string> _CodeList;

        #region IsValid

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="validationValue">開始値</param>
        /// <param name="CodeList">対象値を持つStringList</param>
        /// <remarks></remarks>
        /// ------------------------------------------------------------------------------------------

        public CodeListNotExistsValidation(string validationValue, List<string> codeList)
        {
            this._ValidationValue = validationValue;
            this._CodeList = codeList;

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

            bool bResult = true;

            //同じ値が存在すれば正
            foreach (string s in _CodeList)
            {
                if (s.Equals(this._ValidationValue) == true)
                {
                    bResult = false;
                }
            }

            return bResult;

        }

        #endregion

    }

}
