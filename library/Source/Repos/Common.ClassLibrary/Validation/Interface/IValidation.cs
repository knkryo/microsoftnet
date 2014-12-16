
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// ------------------------------------------------------------------------------------------
	/// <summary>
	/// 検証を行うValidationのインターフェース
	/// 検証および検証結果の保持に使用します
	/// </summary>
	/// <remarks>
	/// </remarks>
	/// ------------------------------------------------------------------------------------------
	public interface IValidation
	{

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 検証を実行する
		/// </summary>
		/// <returns>True = 検証は正常 , False = 検証にて異常</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		bool IsValid();

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 検証を実行する
		/// </summary>
		/// <param name="validationValue">検証値</param>
		/// <returns>True = 検証は正常 , False = 検証にて異常</returns>
		/// <remarks>
		/// 通常はコンストラクタにて対象値を設定する事を想定していますが、
		/// 同一インスタンスを複数回使用する場合には本メソッドを使用します
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		bool IsValid(string validationValue);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// Validメソッド実行後の実行結果を返します
		/// </summary>
		/// <value></value>
		/// <returns>IValidationReasonインターフェース</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		IValidationReason InvalidReason { get; }
	}

}
