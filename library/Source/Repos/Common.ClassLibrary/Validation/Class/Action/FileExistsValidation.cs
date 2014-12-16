
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// ファイルが存在するかどうかをチェックするValidation
	/// </summary>
	/// <remarks></remarks>
	public class FileExistsValidation : AbstructValidation
	{

		#region IsValid

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="fileFullPath">対象のパス</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public FileExistsValidation(string fileFullPath)
		{
			this._ValidationValue = fileFullPath;

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

			try {
				return System.IO.File.Exists(this._ValidationValue);
			} catch (System.Exception) {
				return false;

			} finally {
			}

		}

		#endregion

	}

}
