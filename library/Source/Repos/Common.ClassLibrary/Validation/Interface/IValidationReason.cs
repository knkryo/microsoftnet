
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// ------------------------------------------------------------------------------------------
	/// <summary>
	/// Validメソッドの詳細な理由を保持するインターフェース
	/// </summary>
	/// <remarks></remarks>
	/// ------------------------------------------------------------------------------------------
	public interface IValidationReason
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// メッセージ文字列を取得設定します
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		string MessageCode { get; }

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// メッセージコードを取得設定します
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		string Message { get; }
		/// <summary>
		/// 検証が成功したかどうかを設定します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>

		bool Success { get; }
	}

}
