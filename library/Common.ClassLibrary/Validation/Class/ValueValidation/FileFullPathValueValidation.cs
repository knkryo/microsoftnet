using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// ファイルのフルパスとして妥当かどうかの検証を行う
	/// </summary>
	/// <remarks>
	/// ファイルパスが活性であるかどうかは本クラスではチェックしません
	/// </remarks>
	public class FileFullPathValueValidation : AbstructValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="validationValue">検証対象の値</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------


		public FileFullPathValueValidation(string validationValue)
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

			string fileName = string.Empty;
			string filePath = string.Empty;



			try {
				if ((_ValidationValue.ToString().Length == 0)) {
					base.SetInvalidReason(new FileFullPathValueValidationReason(FileFullPathValueValidationReason.FilePathValidationReason.UnKnownPath));
					return false;
				}

				fileName = System.IO.Path.GetFileName(base._ValidationValue);

				//ディレクトリで取れない場合には、ルートの可能性あり
				filePath = System.IO.Path.GetDirectoryName(base._ValidationValue);
                if (filePath == null)
                {
                    filePath = string.Empty;
                }
				if ((filePath.ToString().Length == 0)) {
					if (System.IO.Path.IsPathRooted(base._ValidationValue) == true) {
						filePath = System.IO.Path.GetPathRoot(filePath);
					}
				}

				//ファイル名に不正な文字が含まれる場合にはFalse
				if (new Validation.FileNameValueValidation(fileName).IsValid() == false) {
					base.SetInvalidReason(new FileFullPathValueValidationReason(FileFullPathValueValidationReason.FilePathValidationReason.InvalidFileNameChar));
					return false;
				}

				//ファイルパスに不正な文字が含まれる場合にはFalse
				if (new Validation.FilePathValueValidation(filePath).IsValid() == false) {
					base.SetInvalidReason(new FileFullPathValueValidationReason(FileFullPathValueValidationReason.FilePathValidationReason.InvalidFilePathChar));
					return false;
				}

				//ファイルパスが相対パスまたはUnKnownの場合にはFalse
                switch (IO.Path.FileOperationUtils.GetFilePathType(this._ValidationValue))
                {
                    case IO.Path.FileOperationUtils.FilePathType.UnKnown:

						base.SetInvalidReason(new FileFullPathValueValidationReason(FileFullPathValueValidationReason.FilePathValidationReason.UnKnownPath));

						return false;
                    case IO.Path.FileOperationUtils.FilePathType.RelativePath:

						base.SetInvalidReason(new FileFullPathValueValidationReason(FileFullPathValueValidationReason.FilePathValidationReason.RelativePath));

						return false;
                    case IO.Path.FileOperationUtils.FilePathType.AbsolutePath:
                    case IO.Path.FileOperationUtils.FilePathType.PathNameString:
                    case IO.Path.FileOperationUtils.FilePathType.UniversalNamingConvention:

						base.SetInvalidReason(new FileFullPathValueValidationReason(FileFullPathValueValidationReason.FilePathValidationReason.Success));


						return true;
				}
				return true;


			} catch (System.Exception) {
				//不明エラー
				base.SetInvalidReason(new FileFullPathValueValidationReason(FileFullPathValueValidationReason.FilePathValidationReason.UnKnownPath));
				return false;


			} finally {
			}


		}

	}

}
