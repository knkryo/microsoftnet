
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Common.ClassLibrary.Validation
{

	/// <summary>
	/// 検証結果を保持するためのValueObject
	/// </summary>
	/// <remarks></remarks>
	public class ValidationReason : IValidationReason
	{

		#region "Protected Property"

		/// <summary>
		/// メッセージ文字列を設定します
		/// </summary>
		/// <remarks></remarks>

		private string _Message = string.Empty;

		private string _MessageCode = string.Empty;

		private bool _Success = false;

		private const string SuccessMessage = "検証は正常です";
		#endregion

		#region "Constructor"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public ValidationReason()
		{
		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="success">正常の場合 = True , 失敗の場合 =False</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public ValidationReason(bool success) : this(success, "", "")
		{
		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="success">正常の場合 = True , 失敗の場合 =False</param>
		/// <param name="message">メッセージ</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public ValidationReason(bool success, string message) : this(success, message, "")
		{
		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="success">正常の場合 = True , 失敗の場合 =False</param>
		/// <param name="messageCode">メッセージコード</param>
		/// <param name="message">メッセージ</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public ValidationReason(bool success, string message, string messageCode)
		{
			this.SetStatus(success, message, messageCode);
		}

		#endregion

		#region "Protected Method"

		#region "SetStatus"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 内部プロパティを更新する
		/// </summary>
		/// <param name="success"></param>
		/// <param name="message"></param>
		/// <param name="messageCode"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		protected void SetStatus(bool success, string message, string messageCode)
		{
			this._Success = success;
			this._MessageCode = messageCode;
			this._Message = message;
		}

		#endregion

		#endregion

		#region "Public Property"

		#region "Message"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// メッセージ文字列を設定します
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public virtual string Message {
			get { return this._Message; }
		}

		#endregion

		#region "MessageCode"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// メッセージコードを設定します
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public virtual string MessageCode {
			get { return this._MessageCode; }
		}

		#endregion

		#region "Success"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 検証が成功したかどうかを設定します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public bool Success {
			get { return _Success; }
		}

		#endregion

		#endregion

	}

}
