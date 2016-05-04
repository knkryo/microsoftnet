namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// ファイルパスの検証を行う
	/// </summary>
	/// <remarks></remarks>
	public class FilePathCharValidation : AbstructValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------


		public FilePathCharValidation(string validationValue)
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

			//InvalidFileNameChars Or GetInvalidPathCharsが見つかった場合にはFalse
			if (this._ValidationValue.IndexOfAny(System.IO.Path.GetInvalidPathChars()).Equals(-1)) {
				base.SetInvalidReason(new ValidationReason(true));
				return true;
			} else {
				base.SetInvalidReason(new ValidationReason(false, "", ""));
				return false;
			}

		}

	}

}
