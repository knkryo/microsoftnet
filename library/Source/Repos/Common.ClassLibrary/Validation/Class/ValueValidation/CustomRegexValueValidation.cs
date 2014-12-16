
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 任意の正規表現を元に入力検証を行うValidation
	/// </summary>
	/// <remarks>文字全体の検証を行う</remarks>
	public class CustomRegexValueValidation : AbstructRegexValueValidation
	{


		private string _RegexString = string.Empty;
		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public CustomRegexValueValidation(string validationValue, string regexString) : base(validationValue)
		{
			this._RegexString = regexString;

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 入力可能な文字列の正規表現値を設定する
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public void SetRegexString(string regexString)
		{
			this._RegexString = regexString;
		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 入力可能な文字列の正規表現値を返却する
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		protected override string RegexString {
			get { return this._RegexString; }
		}

	}

}

