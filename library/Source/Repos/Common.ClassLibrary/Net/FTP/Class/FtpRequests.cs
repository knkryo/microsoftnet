using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using System.Net;
using System.IO;

using Common.ClassLibrary.Extensions;

namespace Common.ClassLibrary.Net.Ftp
{

	/// ------------------------------------------------------------------------------------------
	/// <summary>
	/// FtpRequestクラスを使用したFtpClientクラス
	/// </summary>
	/// <history>
	///     2008/04/07 Ryo Kaneko Ver.1.0.0 Created
	/// </history>
	/// <remarks>
	/// 未実装機能 ＆ 未テストなので、ftpCommandクラスの使用を推奨
	/// 
	/// ＜確認できている事象＞
	/// ・GetFileListメソッドにて、サブフォルダのファイルまで検索してしまう
	/// 
	/// </remarks>
	/// ------------------------------------------------------------------------------------------
	public class FtpRequests : AbstractFtpClient
	{

		#region "Private Section"


		private bool _isLoggedIn;

		private const int _BUFFER_SIZE = 1024;

		private string _currentDirectory = string.Empty;
		#endregion

		#region "Class Constructors"

		// Main class constructor

		public FtpRequests()
		{
			base.RemoteHost = "microsoft";
			base.RemotePath = "";
			base.RemoteUser = "anonymous";
			base.RemotePassword = "";
			base.MessageString = "";
			base.RemotePort = 21;
			this._isLoggedIn = false;
			this.ServerEncoding = System.Text.Encoding.GetEncoding("SJIS");

		}

		// Parameterized constructor

		public FtpRequests(string sRemoteHost, string sRemotePath, string sRemoteUser, string sRemotePassword, Int32 iRemotePort)
		{
            base.RemoteHost = sRemoteHost.AddTailSlush();
			base.RemotePath = sRemotePath;
			base.RemoteUser = sRemoteUser;
			base.RemotePassword = sRemotePassword;
			base.MessageString = "";
			base.RemotePort = 21;
			this._isLoggedIn = false;
			this.ServerEncoding = System.Text.Encoding.GetEncoding("SJIS");

		}



		#endregion

		#region "Function"

		#region "ChangeDirectory"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// カレントディレクトリを変更します
		/// </summary>
		/// <param name="directoryName">変更したいディレクトリ名</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool ChangeDirectory(string directoryName)
		{

			//ログインしていなければ終了

			if (this._isLoggedIn == false) {
				return false;

			}


			string tmpPath = null;

            tmpPath = this._currentDirectory + directoryName.AddTailSlush();

			WebRequest req = WebRequest.Create(tmpPath);
			FtpWebResponse ftpRes = null;


			try {
				req.Credentials = new NetworkCredential(base.RemoteUser, base.RemotePassword);

				req.Method = System.Net.WebRequestMethods.Ftp.ListDirectory;

				ftpRes = (FtpWebResponse)req.GetResponse();

				switch (ftpRes.StatusCode) {

					//応答コード未確認。125は確認済
					case FtpStatusCode.CommandOK:
					case FtpStatusCode.DataAlreadyOpen:

						_currentDirectory = tmpPath;


						return true;
					case FtpStatusCode.EnteringPassive:


						return false;
                    default:
                        return true;
				}


			} finally {
				req = null;
				ftpRes = null;

			}



		}

		#endregion

		#region "CloseConnection"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 接続を切断します
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public override void CloseConnection()
		{
		}

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
		public override bool CreateDirectory(string directoryName)
		{

			WebRequest req = WebRequest.Create(this._currentDirectory + directoryName);
			FtpWebResponse ftpRes = null;

			req.Credentials = new NetworkCredential(base.RemoteUser, base.RemotePassword);

			//MakeDirectory
			req.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;

			ftpRes = (FtpWebResponse)req.GetResponse();

			if (ftpRes.StatusCode == FtpStatusCode.FileActionOK | ftpRes.StatusCode.Equals(FtpResultStatus.FtpResultStatusCode._257_PATHNAME_created)) {
				return true;
			} else {
				base.MessageString = ftpRes.StatusDescription.ToString();
				return false;
			}

		}

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
		public override bool DownloadFile(string targetFileName)
		{

			return this.DownloadFile(targetFileName, "", false);

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行います
		/// </summary>
		/// <param name="targetFileName">ダウンロードするファイル名</param>
		/// <param name="isResume">Resumeを行いますかどうかを指定します</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool DownloadFile(string targetFileName, bool isResume)
		{

			return this.DownloadFile(targetFileName, "", isResume);

		}

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
		public override bool DownloadFile(string targetFileName, string localFileName)
		{

            return this.DownloadFile(targetFileName, localFileName, false);

		}

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
		public override bool DownloadFile(string targetFileName, string localFileName, bool isResume)
		{

			if (this.isConnect() == false) {
				this.Login();
			}

			FtpWebRequest req = (FtpWebRequest)WebRequest.Create(targetFileName);

			req.Credentials = new NetworkCredential(base.RemoteUser, base.RemotePassword);
			req.Method = WebRequestMethods.Ftp.DownloadFile;

			if ((localFileName.Equals(""))) {
				localFileName = Path.GetFileName(targetFileName);
			}


			if ((!(File.Exists(localFileName)))) {
				using (FileStream outputFileStream = new FileStream(localFileName, FileMode.Create)) {

					outputFileStream.Close();

				}

			}

			using (WebResponse res = req.GetResponse()) {

				using (Stream st = res.GetResponseStream()) {

					//ローカルファイルを開く
					using (FileStream outputFileStream = new FileStream(localFileName, FileMode.Create)) {

						if (isResume == true) {
							req.ContentOffset = outputFileStream.Length;
							outputFileStream.Seek(outputFileStream.Length, SeekOrigin.Begin);
						}

						byte[] buf = new byte[_BUFFER_SIZE + 1];
						int count = 0;


						do {
							//ストリームの読込を行い、ローカルへの出力を行う
							count = st.Read(buf, 0, buf.Length);
							outputFileStream.Write(buf, 0, count);

						} while (count != 0);

					}

				}

			}

            return true;

        }

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
		public override string[] GetFileList(string maskString)
		{

			return GetFileList(maskString, false);

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 現在のディレクトリのファイル一覧を返却します
		/// </summary>
		/// <param name="maskString">マスクしたい文字列</param>
		/// <param name="subFolderSearch">サブフォルダも検索するかどうか</param>
		/// <returns>String配列</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override string[] GetFileList(string maskString, bool subFolderSearch)
		{

			byte[] buf = new byte[_BUFFER_SIZE + 1];

			int count = 0;

			string sBuffer = string.Empty;
			string[] sArray = null;
			string[] sArraySub = null;

			if (this.isConnect() == false) {
				this.Login();
			}

			FtpWebRequest req = (FtpWebRequest)WebRequest.Create(this._currentDirectory.AddTailSlush() + maskString);

			req.Credentials = new NetworkCredential(base.RemoteUser, base.RemotePassword);
			req.Method = WebRequestMethods.Ftp.ListDirectory;

			using (WebResponse res = req.GetResponse()) {

				using (Stream st = res.GetResponseStream()) {


					do {
						count = st.Read(buf, 0, buf.Length);

						sBuffer += this.ServerEncoding.GetString(buf);

					} while (!(count != _BUFFER_SIZE));

				}

			}


			if (sBuffer.IndexOf("\r\n") > 0) {
				sBuffer = sBuffer.Replace("\n", "");
				sArray = sBuffer.Split(Convert.ToChar("\r"));


			} else if (sBuffer.IndexOf("\r") > 0) {
				sArray = sBuffer.Split(Convert.ToChar("\r"));


			} else {
				sArray = sBuffer.Split(Convert.ToChar("\n"));

			}


			if (subFolderSearch == false) {
				sArraySub = new string[sArray.GetUpperBound(0) + 1];
				count = 0;


				foreach (string s in sArray) {
					if (s.IndexOf("/") == -1) {
						sArraySub[count] = s;
						count += 1;
					}

				}

				Array.Resize(ref sArraySub, count);

				return sArraySub;


			} else {
				return sArray;

			}

		}

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
		public override long GetFileSize(string targetFileName)
		{
            return 0;
		}

		#endregion

		#region "isConnect"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 接続されているかどうかを返します
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool isConnect()
		{

            return true;
		}

		#endregion

		#region "Login"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 指定されたHost情報を元にログインする
		/// </summary>
		/// <returns></returns>
		/// <remarks>コンストラクタ、またはプロパティで設定されたログイン情報を使用してログインを行います</remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool Login()
		{

			WebRequest req = WebRequest.Create(base.RemoteHost);
			WebResponse res = null;

			req.Credentials = new NetworkCredential(base.RemoteUser, base.RemotePassword);


			try {
				//ダミーでデータを取得する
				req.Method = System.Net.WebRequestMethods.Ftp.ListDirectory;

				try {
					res = req.GetResponse();

					Console.WriteLine(res.ContentLength);

					this._isLoggedIn = true;
					this._currentDirectory = base.RemoteHost;


				} catch (System.Net.WebException ex) {
					//401 アクセス拒否

					if (ex.Status == WebExceptionStatus.ProtocolError) {
						this._isLoggedIn = false;

					}


				} catch (System.Exception ex) {
					Console.WriteLine(ex);
					this._isLoggedIn = false;

				}


			} finally {
				req = null;
				res = null;

			}

			return this._isLoggedIn;

		}

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

		public override void SetBinaryMode(bool bMode)
		{
		}

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
		public override bool UploadFile(string uploadFileName)
		{
			return this.UploadFile(uploadFileName, false);
		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのアップロードを行います
		/// </summary>
		/// <param name="uploadFileName">アップロードしたいローカルファイルのフルパス</param>
		/// <param name="isResume">Resumeを行うかどうか</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool UploadFile(string uploadFileName, bool isResume)
		{
            return this.UploadFile(uploadFileName, Path.GetFileName(uploadFileName), isResume);
		}

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
		public override bool UploadFile(string uploadFileName, string transferFileName, bool isResume)
		{

			WebRequest req = WebRequest.Create(this._currentDirectory + transferFileName);

			req.Credentials = new NetworkCredential(base.RemoteUser, base.RemotePassword);

			req.Method = WebRequestMethods.Ftp.UploadFile;

			using (Stream st = req.GetRequestStream()) {
				using (FileStream fs = new FileStream(uploadFileName, FileMode.Open)) {
					byte[] buf = new byte[1025];
					int count = 0;

					do {
						count = fs.Read(buf, 0, buf.Length);
						st.Write(buf, 0, count);
					} while (count != 0);
				}
			}
            return true;

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルの削除を行います
		/// </summary>
		/// <param name="targetFileName">削除したいファイル名</param>
		/// <returns>成功した場合 = True , 失敗の場合 = False</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool DeleteFile(string targetFileName)
		{
            return true;
		}

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
		public override bool RenameFile(string oldFileName, string newFileName)
		{
            return true;

		}

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
		public override bool RemoveDirectory(string directoryName)
		{

			WebRequest req = WebRequest.Create(this._currentDirectory.AddTailSlush() + directoryName);
			WebResponse res = null;

			req.Credentials = new NetworkCredential(base.RemoteUser, base.RemotePassword);


			try {
				req.Method = System.Net.WebRequestMethods.Ftp.RemoveDirectory;

				try {
					res = req.GetResponse();

					Console.WriteLine(res.ContentLength);

					return true;


				} catch (System.Net.WebException ex) {
					throw ex;


				} catch (System.Exception ex) {
					throw ex;

				}


			} finally {
				req = null;
				res = null;

			}

		}

		#endregion

		#endregion

	}

}
