using Common.ClassLibrary.Extensions;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// マルチバイトのみを許可するValidation
	/// </summary>
	/// <remarks>1文字単位の検証を行う</remarks>
	public class MultiByteOnlyCharValidation : AbstructValidation
	{

		#region IsValid

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public MultiByteOnlyCharValidation(string validationValue)
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

			if ((this._ValidationValue.GetByteCount()).Equals(this._ValidationValue.Length * 2) == true) {
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
