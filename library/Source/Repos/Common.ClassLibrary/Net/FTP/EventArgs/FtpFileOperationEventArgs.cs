using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Common.ClassLibrary.Net.Ftp
{

	/// ------------------------------------------------------------------------------------------
	/// <summary>
	/// アップロード完了時に取得可能な情報を持つEventArgsオブジェクト
	/// </summary>
	/// <remarks>
	/// </remarks>
	/// ------------------------------------------------------------------------------------------
	public class FtpFileOperationEventArgs : System.EventArgs
	{

		#region "Private Section"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 対象のファイル名
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		private string _TargetFileName;
		#endregion

		#region "Constructor"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="targetFileName"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public FtpFileOperationEventArgs(string targetFileName)
		{
			this._TargetFileName = targetFileName;

		}

		#endregion

		#region "Property"

		#region "TargetFileName"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 処理されたされたファイル名を返却します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public string TargetFileName {



			get { return this._TargetFileName; }
		}


		#endregion

		#endregion

	}

}
