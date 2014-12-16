using Common.ClassLibrary.Extensions;

namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 数値の必須チェッククラス (0 Or Emptyは未入力とみなす )
    /// 値が入っていればTrue,入っていない場合はFalse
	/// </summary>
	/// <remarks></remarks>
	public class NumericRequiredValidation : AbstructValidation
	{

		#region IsValid

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

        public NumericRequiredValidation(string validationValue)
		{
			this._ValidationValue = validationValue;

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

            return !decimal.Parse((_ValidationValue.ToString().ConvertEmptyToZero()).ToString()).Equals(0);

		}

		#endregion

	}

}
