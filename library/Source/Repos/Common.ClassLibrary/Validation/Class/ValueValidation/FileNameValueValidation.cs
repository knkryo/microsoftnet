
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// ファイル名の検証を行う
	/// </summary>
	/// <remarks></remarks>
	public class FileNameValueValidation : AbstructValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------


		public FileNameValueValidation(string validationValue)
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

			//InvalidFileNameCharsが見つかった場合にはFalse
			if (!this._ValidationValue.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()).Equals(-1)) {
				base.SetInvalidReason(new ValidationReason(true));
				return false;
			} else {
				base.SetInvalidReason(new ValidationReason(false, "", ""));
				return true;
			}

		}

	}

}
