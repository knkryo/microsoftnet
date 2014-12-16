namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 指定されたエンコードがシステム内に存在するかどうかをチェックするValidation
	/// </summary>
	/// <remarks></remarks>
	public class EncodeIsExistsValidation : AbstructValidation
	{

		#region IsValid

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------


		public EncodeIsExistsValidation(string validationValue)
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

			System.Text.Encoding encode = null;


			try {
				encode = System.Text.Encoding.GetEncoding(this._ValidationValue);

				return (encode != null);

			} catch (System.Exception) {
				return false;
			} finally {
				encode = null;
			}

		}

		#endregion

	}

}
