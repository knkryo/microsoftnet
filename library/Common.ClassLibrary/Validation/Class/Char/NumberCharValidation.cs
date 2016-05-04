namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 数字の入力判定を行うValidation
	/// </summary>
	/// <remarks>1文字単位の検証を行う</remarks>
	public class NumberCharValidation : AbstructRegexCharValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public NumberCharValidation(string validationValue) : base(validationValue)
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
			get { return ValidationConstants.NumberCharRegexString; }
		}

	}

}

