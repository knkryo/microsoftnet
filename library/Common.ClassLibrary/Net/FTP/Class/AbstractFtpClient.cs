using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Common.ClassLibrary.Net.Ftp
{

	/// ------------------------------------------------------------------------------------------
	/// <summary>
	/// IFtpClientを実装したSuperクラス
	/// </summary>
	/// <remarks>
	/// IFtpClientを実装するには、本クラスを継承することを推奨します
	/// </remarks>
	/// <history>
	///     2008/02/20 R.Kaneko Created
	///     2008/04/07 R.Kaneko isFileExist , isDirectoryExistsを追加
	/// </history>
	/// ------------------------------------------------------------------------------------------
	public abstract class AbstractFtpClient : IFtpClient
	{

		#region "Private Section"

		#region "Properties"

			//リモートホスト
		private string _RemoteHost;
			//リモートパス
		private string _RemotePath;
			//リモートユーザー
		private string _RemoteUser;
			//リモートパスワード
		private string _RemotePassword;
			//リモートポート
		private int _RemotePort;
			//メッセージ文字列
		private string _MessageString;

			//Encoding
		private System.Text.Encoding _ServerEncoding = System.Text.Encoding.GetEncoding("SJIS");

		#endregion

		#region "Class Variable Declarations"

		#endregion

		#endregion

		#region "Public Events"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 接続が切断された際に発生します
		/// </summary>
		/// <param name="sender">自Object</param>
		/// <param name="e">空のEventArgsオブジェクト</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public event CloseConnectionCompleteEventHandler CloseConnectionComplete;
		public delegate void CloseConnectionCompleteEventHandler(object sender, EventArgs e);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ダウンロードが完了した際に発生します
		/// </summary>
		/// <param name="sender">自Object</param>
		/// <param name="e">DownloadFileEventArgs</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public event DownloadCompleteEventHandler DownloadComplete;
		public delegate void DownloadCompleteEventHandler(object sender, FtpFileOperationEventArgs e);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 削除が完了した際に発生します
		/// </summary>
		/// <param name="sender">自Object</param>
		/// <param name="e">DownloadFileEventArgs</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public event DeleteCompleteEventHandler DeleteComplete;
		public delegate void DeleteCompleteEventHandler(object sender, FtpFileOperationEventArgs e);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ログインが完了した際に発生します
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">空のEventArgsオブジェクト</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public event LoginCompleteEventHandler LoginComplete;
		public delegate void LoginCompleteEventHandler(object sender, EventArgs e);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// アップロードが完了した際に発生します
		/// </summary>
		/// <param name="sender">自Object</param>
		/// <param name="e">UploadFileEventArgs</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public event UploadCompleteEventHandler UploadComplete;
		public delegate void UploadCompleteEventHandler(object sender, FtpFileOperationEventArgs e);

		#endregion

		#region "Protected OnEvents Method"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ログインが完了した際に発生するイベントハンドラ
		/// </summary>
		/// <param name="e"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		protected void OnLoginComplete(EventArgs e)
		{
			if (LoginComplete != null) {
				LoginComplete(this, e);
			}

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ダウンロードが完了した際に発生するイベントハンドラ
		/// </summary>
		/// <param name="e"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		protected void OnDownloadComplete(FtpFileOperationEventArgs e)
		{
			if (DownloadComplete != null) {
				DownloadComplete(this, e);
			}

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 削除が完了した際に発生するイベントハンドラ
		/// </summary>
		/// <param name="e"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		protected void OnDeleteComplete(FtpFileOperationEventArgs e)
		{
			if (DeleteComplete != null) {
				DeleteComplete(this, e);
			}

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// アップロードが完了した際に発生するイベントハンドラ
		/// </summary>
		/// <param name="e"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		protected void OnUploadComplete(FtpFileOperationEventArgs e)
		{
			if (UploadComplete != null) {
				UploadComplete(this, e);
			}

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 切断がが完了した際に発生するイベントハンドラ
		/// </summary>
		/// <param name="e"></param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		protected void OnCloseConnectionComplete(EventArgs e)
		{
			if (CloseConnectionComplete != null) {
				CloseConnectionComplete(this, e);
			}

		}

		#endregion

		#region "Public Property"

		#region "RemoteHostFTPServer"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// リモートホストを指定します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public string RemoteHost {
			get { return _RemoteHost; }
			set { _RemoteHost = value; }
		}

		#endregion

		#region "RemotePort"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// リモートポートを指定します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public int RemotePort {
			get { return _RemotePort; }

			set { _RemotePort = value; }
		}


		#endregion

		#region "RemotePath"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// リモートパスを指定します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public string RemotePath {
			get { return _RemotePath; }
			set { _RemotePath = value; }
		}

		#endregion

		#region "RemotePassword"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// パスワードを指定します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public string RemotePassword {
			get { return _RemotePassword; }
			set { _RemotePassword = value; }
		}

		#endregion

		#region "RemoteUser"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ログインユーザーを指定します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public string RemoteUser {
			get { return _RemoteUser; }
			set { _RemoteUser = value; }
		}

		#endregion

		#region "MessageString"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// リモートホストからのメッセージを返却する
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public string MessageString {
			get { return _MessageString; }
			set { _MessageString = value; }
		}

		#endregion

		#region "ServerEncoding"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// サーバーサイドのEncodingを指定します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public System.Text.Encoding ServerEncoding {
			get { return this._ServerEncoding; }
			set { this._ServerEncoding = value; }
		}

		#endregion

		#endregion

		#region "Mustoverride Functions"

		#region "ChangeDirectory"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// カレントディレクトリを変更します
		/// </summary>
		/// <param name="directoryName">変更したいディレクトリ名</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool ChangeDirectory(string directoryName);

		#endregion

		#region "CloseConnection"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 接続を切断します
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract void CloseConnection();

		#endregion

		#region "CreateDirectory"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ディレクトリを作成します
		/// </summary>
		/// <param name="directoryName">現在のディレクトリを基準として、参照可能なディレクトリパス</param>
		/// <returns>成功した場合 = True , 失敗の場合 = False</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool CreateDirectory(string directoryName);

		#endregion

		#region "DownloadFile"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行います
		/// </summary>
		/// <param name="targetFileName">ダウンロードするファイル名</param>
		/// <returns></returns>
		/// <remarks>
		/// ダウンロードを行うファイル名のみを指定してダウンロードを行います
		/// ローカルへの保存先は相対パスのサーバーと同名ファイルとなります
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool DownloadFile(string targetFileName);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行います
		/// </summary>
		/// <param name="targetFileName">ダウンロードするファイル名</param>
		/// <param name="isResume">Resumeを行いますかどうかを指定します</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool DownloadFile(string targetFileName, bool isResume);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行います
		/// </summary>
		/// <param name="targetFileName">ダウンロードするファイル名</param>
		/// <param name="localFileName">ダウンロードしたいローカルのファイルパス</param>
		/// <returns></returns>
		/// <remarks>
		/// 取得ファイル名と保存先を指定してダウンロードを行います
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool DownloadFile(string targetFileName, string localFileName);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行います
		/// </summary>
		/// <param name="targetFileName">ダウンロードするファイル名</param>
		/// <param name="localFileName">ダウンロードしたいローカルのファイルパス</param>
		/// <param name="isResume">Resumeを行いますかどうかを指定します</param>
		/// <returns></returns>
		/// <remarks>
		/// 取得ファイル名と保存先を指定してダウンロードを行います
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool DownloadFile(string targetFileName, string localFileName, bool isResume);

		#endregion

		#region "GetFileList"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 現在のディレクトリのファイル一覧を返却します
		/// </summary>
		/// <param name="maskString">マスクしたい文字列</param>
		/// <returns>String配列</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract string[] GetFileList(string maskString);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 現在のディレクトリのファイル一覧を返却します
		/// </summary>
		/// <param name="maskString">マスクしたい文字列</param>
		/// <param name="subFolderSearch">サブフォルダも検索するかどうか</param>
		/// <returns>String配列</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract string[] GetFileList(string maskString, bool subFolderSearch);

		#endregion

		#region "GetFileSize"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 指定したファイルのサイズを取得する
		/// </summary>
		/// <param name="targetFileName">取得するファイル名</param>
		/// <returns>ファイルサイズ</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract long GetFileSize(string targetFileName);

		#endregion

		#region "isConnect"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 接続されているかどうかを返します
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool isConnect();

		#endregion

		#region "Login"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 指定されたHost情報を元にログインする
		/// </summary>
		/// <returns></returns>
		/// <remarks>コンストラクタ、またはプロパティで設定されたログイン情報を使用してログインを行います</remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool Login();

		#endregion

		#region "SetBinaryMode"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// バイナリモードへの設定を行います
		/// </summary>
		/// <param name="bMode">Trueの場合 = BinaryMode , Falseの場合 = AsciiMode</param>
		/// <remarks>
		/// ダウンロードを行うファイル名のみを指定してダウンロードを行います
		/// ローカルへの保存先は相対パスのサーバーと同名ファイルとなります
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract void SetBinaryMode(bool bMode);

		#endregion

		#region "UploadFile"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのアップロードを行います
		/// </summary>
		/// <param name="uploadFileName">アップロードしたいローカルファイルのフルパス</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool UploadFile(string uploadFileName);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのアップロードを行います
		/// </summary>
		/// <param name="uploadFileName">アップロードしたいローカルファイルのフルパス</param>
		/// <param name="isResume">Resumeを行うかどうか</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool UploadFile(string uploadFileName, bool isResume);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのアップロードを行います
		/// </summary>
		/// <param name="uploadFileName">アップロードしたいローカルファイルのフルパス</param>
		/// <param name="transferFileName">アップロード先のファイル名</param>
		/// <param name="isResume">Resumeを行うかどうか</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool UploadFile(string uploadFileName, string transferFileName, bool isResume);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルの削除を行います
		/// </summary>
		/// <param name="targetFileName">削除したいファイル名</param>
		/// <returns>成功した場合 = True , 失敗の場合 = False</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool DeleteFile(string targetFileName);

		#endregion

		#region "RenameFile"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイル名を変更します
		/// </summary>
		/// <param name="oldFileName">変更前のファイル名</param>
		/// <param name="newFileName">変更後のファイル名</param>
		/// <returns>成功の場合 = True , 失敗の場合 = False</returns>
		/// <exception cref="System.IO.IOException">ファイルが見つからないときに発生するException</exception>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool RenameFile(string oldFileName, string newFileName);

		#endregion

		#region "RemoveDirectory"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ディレクトリの削除を行います
		/// </summary>
		/// <param name="directoryName">現在のディレクトリを基準として、参照可能なディレクトリパス</param>
		/// <returns>成功した場合 = True , 失敗の場合 = False</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public abstract bool RemoveDirectory(string directoryName);

		#endregion

		#region "ファイルの存在チェック (FileExist)"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルの存在チェックを行い、結果を返す
		/// </summary>
		/// <param name="targetFileName">検索したいファイル名</param>
		/// <returns>存在する場合 = True , 存在しない場合 = False</returns>
		/// <remarks>
		/// 内部的には論理名のみで検索をしているため、ディレクトリか、
		/// ファイルかの厳密的な判断はしていません。
		/// 厳密な判断が必要な場合には、プログラムを改修してください
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public virtual bool FileExist(string targetFileName)
		{

			string[] fileList = null;

			fileList = this.GetFileList("");

			//取得できたなら、一致するまでループして結果を返す

			if (fileList.GetUpperBound(0) > 0) {

				foreach (string s in fileList) {

					if (s.Equals(targetFileName) == true) {
						return true;

					}

				}

			}

            return false;
		}

		#endregion

		#region "DirectoryExists"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ディレクトリの存在チェックを行い、結果を返す
		/// </summary>
		/// <param name="targetDirectoryName">検索したいディレクトリ名</param>
		/// <returns>存在する場合 = True , 存在しない場合 = False</returns>
		/// <remarks>
		/// 内部的には論理名のみで検索をしているため、ディレクトリか、
		/// ファイルかの厳密的な判断はしていません。
		/// 厳密な判断が必要な場合には、プログラムを改修してください
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public virtual bool DirectoryExists(string targetDirectoryName)
		{

			return this.FileExist(targetDirectoryName);

		}

		#endregion

		#endregion

	}

}
