
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// ファイルパスとして妥当かどうかの検証を行う
	/// </summary>
	/// <remarks>
	/// ファイルパスが活性であるかどうかは本クラスではチェックしません
	/// </remarks>
	public class FilePathValueValidation : AbstructValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public FilePathValueValidation(string validationValue)
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

			//パスとして認識できない場合にはFalse

            if (Utils.FileOperationUtils.GetFilePathType(this._ValidationValue).Equals(Utils.FileOperationUtils.FilePathType.UnKnown))
            {
				base.SetInvalidReason(new ValidationReason(false, "", ""));
				return false;


			} else {
				base.SetInvalidReason(new ValidationReason(true));
				return true;

			}

		}

	}

}
