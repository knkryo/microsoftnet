namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 英字の入力判定を行うValidation
	/// </summary>
	/// <remarks>1文字単位の検証を行う</remarks>
	public class BritishCharValidation : AbstructRegexCharValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public BritishCharValidation(string validationValue) : base(validationValue)
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
			get { return ValidationConstants.BritishCharRegexString; }
		}

	}

}

