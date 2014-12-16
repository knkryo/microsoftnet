
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Common.ClassLibrary.Validation
{

	/// ------------------------------------------------------------------------------------------
	/// <summary>
	/// 値検証用のValidationBaseクラス
	/// </summary>
	/// <remarks></remarks>
	/// ------------------------------------------------------------------------------------------
	public abstract class AbstructRegexValueValidation : AbstructValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 検証するための正規表現を保持する
		/// </summary>
		/// <remarks>各継承先のクラスにて実装する、正規表現値保持用のクラスです</remarks>
		/// ------------------------------------------------------------------------------------------
		protected abstract string RegexString { get; }

		#region "Constructor"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public AbstructRegexValueValidation(string validationValue)
		{
			base._ValidationValue = validationValue;

		}

		#endregion

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

			if (Regex.Match(base._ValidationValue, this.RegexString).Length.Equals(base._ValidationValue.Length) == true) {
				base.SetInvalidReason(new ValidationReason(true));
				return true;
			} else {
				base.SetInvalidReason(new ValidationReason(false, "", ""));
				return false;
			}

		}

		#endregion

	}

}
