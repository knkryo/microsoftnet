
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 指定された値の中に含まれているかどうかをチェックするValidation
    /// 含まれていればTrue , 含まれていなければFalse
    /// </summary>
	/// <remarks></remarks>
	public class CodeListExistsValidation : AbstructValidation
	{

		private List<string> _CodeList;


		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">開始値</param>
		/// <param name="CodeList">対象値を持つStringList</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public CodeListExistsValidation(string validationValue, List<string> codeList)
		{
			this._ValidationValue = validationValue;
			this._CodeList = codeList;

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

			//同じ値が存在すれば正
			foreach (string s in _CodeList) {
				if (s.Equals(this._ValidationValue) == true) {
					return true;
				}
			}
            return false;

		}

		#endregion

	}

}
