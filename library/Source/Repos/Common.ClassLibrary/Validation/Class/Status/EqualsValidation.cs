
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 文字の一致チェックを行うValidation
	/// </summary>
	/// <remarks></remarks>
	public class EqualsValidation : AbstructValidation
	{


		private string _EqualsValue = string.Empty;

        /// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <param name="equalCheckValue">一致しているかをチェックする文字列</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public EqualsValidation(string validationValue, string equalCheckValue)
		{
			this._ValidationValue = validationValue;
			this._EqualsValue = equalCheckValue;

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

			return this._ValidationValue.Equals(this._EqualsValue);

		}

	}

}
