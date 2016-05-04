
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 文字の不一致チェックを行うValidation
    /// 異なる場合にはTrue,同じ場合にはFalse
	/// </summary>
	/// <remarks></remarks>
	public class NotEqualsValidation : AbstructValidation
	{


		private string _NotEqualsValue = string.Empty;
		#region IsValid

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <param name="NotEqualCheckValue">一致しているかをチェックする文字列</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public NotEqualsValidation(string validationValue, string NotEqualCheckValue)
		{
			this._ValidationValue = validationValue;
			this._NotEqualsValue = NotEqualCheckValue;

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

			return !this._ValidationValue.Equals(this._NotEqualsValue);

		}

		#endregion

	}

}
