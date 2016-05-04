
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 文字列長をチェックするValidation
	/// </summary>
	/// <remarks></remarks>
	public class MinLengthValidation : AbstructValidation
	{


		private int _Length = 0;
		#region IsValid

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <param name="Length">最低必要な文字数</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public MinLengthValidation(string validationValue, int Length)
		{
			this._ValidationValue = validationValue;
			this._Length = Length;

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

			return this._ValidationValue.Trim().Length >= this._Length;

		}

		#endregion

	}

}
