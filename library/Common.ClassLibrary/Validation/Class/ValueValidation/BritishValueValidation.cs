
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 英字の入力判定を行うValidation
	/// </summary>
	/// <remarks>文字全体の検証を行う</remarks>
	public class BritishValueValidation : AbstructRegexValueValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public BritishValueValidation(string validationValue) : base(validationValue)
		{

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
			get { return ValidationConstants.BritishValueRegexString; }
		}

	}

}

