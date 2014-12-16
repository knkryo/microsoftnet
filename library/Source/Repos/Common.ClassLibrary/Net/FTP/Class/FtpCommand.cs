using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

//2008/02/20 R.Kaneko記
//<課題事項
//FtpWebClientクラスを使用するかどうか、検討が必要
//対応可能な命令セットが残っている
//IFtpAbstractFtpClientを使用する必要があるのか？

namespace Common.ClassLibrary.Net.Ftp
{

	/// ------------------------------------------------------------------------------------------
	/// <summary>
	/// Ftpコマンドを使用して接続を行うクラス
	/// FtpWebRequestクラスを使用しない場合やFrameworkが2.0未満の場合には、本クラスを使用してください。
	/// </summary>
	/// <remarks>
	/// Ftpコマンドを使用しての接続を行います。
	/// 以下のコマンドセットをサポートします
	///   - Upload a file
	///   - Download a file
	///   - Create a directory
	///   - Remove a directory
	///   - Change directory
	///   - Remove a file
	///   - Rename a file
	///   - Set the user name of the remote user
	///   - Set the password of the remote user
	/// 
	/// ＜確認できている事象＞
	/// ・GetFileListメソッドにて、サブフォルダのファイルまで検索してしまう
	/// 
	/// </remarks>
	/// <history>
	/// 2008/02/20 R.Kaneko Created (microsoftから移植)
	/// </history>
	/// ------------------------------------------------------------------------------------------
	public class FtpCommand : AbstractFtpClient, IDisposable
	{

		#region "Class Variable Declarations"

		#region "Status"

		/// <summary>
		/// ログイン状態を保持する
		/// </summary>
		/// <remarks></remarks>

		private bool _isLoggedIn;
		#endregion

		#region "Private Work"

		/// <summary>
		/// 接続するSocketオブジェクトを保持する
		/// </summary>
		/// <remarks></remarks>

		private Socket _objClientSocket;
		/// <summary>
		/// 取得したメッセージを保持する
		/// </summary>
		/// <remarks></remarks>

		private string _Mes;
		/// <summary>
		/// 応答メッセージ文字列を保持する
		/// </summary>
		/// <remarks></remarks>

		private string _Reply;
		/// <summary>
		/// 読み取りを行うブロックサイズを定義する
		/// </summary>
		/// <remarks></remarks>

		private const int BLOCK_SIZE = 512;
		#endregion

		#endregion

		#region "Class Constructors"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// Default Contructor
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public FtpCommand()
		{
			base.RemoteHost = "microsoft";
			base.RemotePath = ".";
			base.RemoteUser = "anonymous";
			base.RemotePassword = "";
			base.MessageString = "";
			base.RemotePort = 21;
			this._isLoggedIn = false;

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sRemoteHost">リモートホスト名</param>
		/// <param name="sRemotePath">リモートパス</param>
		/// <param name="sRemoteUser">ログインユーザー名</param>
		/// <param name="sRemotePassword">ログインパスワード</param>
		/// <param name="iRemotePort">接続ポート</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public FtpCommand(string sRemoteHost, string sRemotePath, string sRemoteUser, string sRemotePassword, int iRemotePort)
		{
			base.RemoteHost = sRemoteHost;
			base.RemotePath = sRemotePath;
			base.RemoteUser = sRemoteUser;
			base.RemotePassword = sRemotePassword;
			base.MessageString = "";
			base.RemotePort = iRemotePort;
			this._isLoggedIn = false;

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sRemoteHost">リモートホスト名</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public FtpCommand(string sRemoteHost)
		{
			base.RemoteHost = sRemoteHost;
			base.RemotePath = ".";
			base.RemoteUser = "anonymous";
			base.RemotePassword = "mail@anonymous";
			base.MessageString = "";
			base.RemotePort = 21;
			this._isLoggedIn = false;

		}

		#endregion

		#region "Public Method"

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

			return this._isLoggedIn;

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

			FtpResultStatus.FtpResultStatusCode returnValue = 0;
			IPAddress ipv4 = null;

			_objClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			ipv4 = getIPV4Address();

			if (ipv4 == null) {
				throw new System.Exception();
			}

			IPEndPoint ep = new IPEndPoint(ipv4, base.RemotePort);

			//' Socket接続

			try {
				_objClientSocket.Connect(ep);


			} catch (System.Exception)
            {
				//' リモートサーバーに接続できません
				MessageString = _Reply;
				throw new System.IO.IOException("Cannot connect to the remote server");

			}

			//応答を待つ
			returnValue = this.ReadReply();

			//220以外であれば接続エラー

			if ((returnValue != FtpResultStatus.FtpResultStatusCode._220_Service_ready_for_new_user)) {
				CloseConnection();
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}

			//認証を行う
			returnValue = SendCommand("USER " + base.RemoteUser);


			if ((!(returnValue == FtpResultStatus.FtpResultStatusCode._331_User_name_okay_need_password | returnValue == FtpResultStatus.FtpResultStatusCode._230_User_logged_in_proceed))) {
				Cleanup();
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}


			if ((returnValue != FtpResultStatus.FtpResultStatusCode._230_User_logged_in_proceed)) {
				returnValue = SendCommand("PASS " + base.RemotePassword);


				if ((!(returnValue == FtpResultStatus.FtpResultStatusCode._230_User_logged_in_proceed | returnValue == FtpResultStatus.FtpResultStatusCode._202_Command_not_implemented_superfluous_at_this_site))) {
					Cleanup();
					MessageString = _Reply;
					throw new System.IO.IOException(_Reply.Substring(4));

				}

			}

			this._isLoggedIn = true;

			ChangeDirectory(base.RemotePath);

			base.OnLoginComplete(EventArgs.Empty);

			return this._isLoggedIn;

		}

		#endregion

		#region "GetFileList (+2)"

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

			return this.GetFileList(maskString, false);

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

			Socket cSocket = null;
			int bytes = 0;
			string[] mess = null;
			byte[] ReadBuffer = new byte[BLOCK_SIZE + 1];

			FtpResultStatus.FtpResultStatusCode returnValue = 0;

			_Mes = "";

			if ((!(_isLoggedIn))) {
				Login();
			}

			cSocket = CreateDataSocket();
			returnValue = SendCommand("NLST " + maskString);


			if ((!(returnValue == FtpResultStatus.FtpResultStatusCode._150_File_status_okay_about_to_open_data_connection | returnValue == FtpResultStatus.FtpResultStatusCode._125_Data_connection_already_open_transfer_starting))) {
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}

			_Mes = "";

			//応答がなくなるまで繰り返し読み取りを行う

			while ((true)) {
				Array.Clear(ReadBuffer, 0, ReadBuffer.Length);
				bytes = cSocket.Receive(ReadBuffer, ReadBuffer.Length, 0);
				_Mes += base.ServerEncoding.GetString(ReadBuffer, 0, bytes);

				if ((bytes < ReadBuffer.Length)) {
					break; // TODO: might not be correct. Was : Exit Do
				}

			}

			if (_Mes.IndexOf("\r\n") > 0) {
				_Mes = _Mes.Replace("\n".ToString(),"");
				mess = _Mes.Split(Convert.ToChar("\r"));
			} else if (_Mes.IndexOf("\r") > 0) {
				mess = _Mes.Split(Convert.ToChar("\r"));
			} else {
				mess = _Mes.Split(Convert.ToChar("\n"));
			}

			cSocket.Close();

			returnValue = ReadReply();


			if ((returnValue != FtpResultStatus.FtpResultStatusCode._226_Closing_data_connection)) {
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}



			if (subFolderSearch == false) {
				string[] messSub = new string[mess.GetUpperBound(0) + 1];
				int i = 0;


				foreach (string s in mess) {
					if (s.IndexOf("/") == -1) {
						messSub[i] = s;
						i += 1;
					}

				}

				Array.Resize(ref messSub, i);

				return messSub;


			} else {
				return mess;

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

			long size = 0;
			FtpResultStatus.FtpResultStatusCode returnValue = 0;

			if (this.isConnect() == false) {
				this.Login();
			}

			returnValue = SendCommand("SIZE " + targetFileName);
			size = 0;


			if ((returnValue == FtpResultStatus.FtpResultStatusCode._213_File_status)) {
				size = Int64.Parse(_Reply.Substring(4));


			} else {
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}

			return size;

		}

		#endregion

		#region "SetBinaryMode"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// バイナリモードの変更を行う
		/// </summary>
		/// <param name="bMode">Trueの場合 = BinaryMode , Falseの場合 = AsciiMode</param>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		public override void SetBinaryMode(bool bMode)
		{
            FtpResultStatus.FtpResultStatusCode returnValue = 0;


			if ((bMode)) {
				returnValue = SendCommand("TYPE I");


			} else {
				returnValue = SendCommand("TYPE A");

			}


			if ((returnValue != FtpResultStatus.FtpResultStatusCode._200_Command_okay)) {
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}

		}

		#endregion

		#region "ファイルのダウンロードを行う (DownloadFile)(+4) "

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行う
		/// </summary>
		/// <param name="targetFileName">ダウンロードを行うファイル名</param>
		/// <returns></returns>
		/// <remarks>
		/// ダウンロードを行うファイル名のみを指定してダウンロードを行います
		/// ローカルへの保存先は相対パスのサーバーと同名ファイルとなります
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool DownloadFile(string targetFileName)
		{

			//パラメータを補完して呼び出し
			return DownloadFile(targetFileName, "", false);

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行う
		/// </summary>
		/// <param name="targetFileName">ダウンロードを行うファイル名</param>
		/// <param name="isResume">Resumeを行うかどうか</param>
		/// <returns></returns>
		/// <remarks>
		/// ダウンロードを行うファイル名のみを指定してダウンロードを行います
		/// ローカルへの保存先は相対パスのサーバーと同名ファイルとなります
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool DownloadFile(string targetFileName, bool isResume)
		{

			//パラメータを補完して呼び出し
			return DownloadFile(targetFileName, "", isResume);

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行う
		/// </summary>
		/// <param name="targetFileName">ダウンロードを行うファイル名</param>
		/// <param name="localFileName">ダウンロード先のファイルパス</param>
		/// <returns></returns>
		/// <remarks>
		/// 取得ファイル名と保存先を指定してダウンロードを行います
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool DownloadFile(string targetFileName, string localFileName)
		{

			//パラメータを補完して呼び出し
			return DownloadFile(targetFileName, localFileName, false);

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行う
		/// </summary>
		/// <param name="targetFileName">ダウンロードを行うファイル名</param>
		/// <param name="localFileName">ダウンロード先のファイルパス</param>
		/// <param name="isResume">Resumeを行うかどうか</param>
		/// <returns></returns>
		/// <remarks>
		/// 取得ファイル名と保存先を指定してダウンロードを行います
		/// </remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool DownloadFile(string targetFileName, string localFileName, bool isResume)
		{

			Stream st = null;
			FileStream outputFileStream = null;
			Socket cSocket = null;
			long offset = 0;
			long npos = 0;
			byte[] ReadBuffer = new byte[BLOCK_SIZE + 1];
			int bytes = 0;
            FtpResultStatus.FtpResultStatusCode returnValue = 0;


			try {
				if (this.isConnect() == false) {
					this.Login();
				}

				SetBinaryMode(true);

				if ((localFileName.Equals(""))) {
					localFileName = targetFileName;
				}

				if ((!(File.Exists(localFileName)))) {
					st = File.Create(localFileName);
					st.Close();
				}

				outputFileStream = new FileStream(localFileName, FileMode.Open);
				cSocket = CreateDataSocket();
				offset = 0;


				if ((isResume == true)) {
					offset = outputFileStream.Length;


					if ((offset > 0)) {
						returnValue = SendCommand("REST " + offset);


						if ((returnValue != FtpResultStatus.FtpResultStatusCode._350_Requested_file_action_pending_further_information)) {
							offset = 0;

						}

					}


					if ((offset > 0)) {
						npos = outputFileStream.Seek(offset, SeekOrigin.Begin);

					}

				}

				returnValue = SendCommand("RETR " + targetFileName);


				if ((!(returnValue == FtpResultStatus.FtpResultStatusCode._150_File_status_okay_about_to_open_data_connection | returnValue == FtpResultStatus.FtpResultStatusCode._125_Data_connection_already_open_transfer_starting))) {
					outputFileStream.Close();
					MessageString = _Reply;
					throw new System.IO.IOException(_Reply.Substring(4));

				}


				while ((true)) {
					Array.Clear(ReadBuffer, 0, ReadBuffer.Length);
					bytes = cSocket.Receive(ReadBuffer, ReadBuffer.Length, 0);
					outputFileStream.Write(ReadBuffer, 0, bytes);


					if ((bytes <= 0)) {
						break; // TODO: might not be correct. Was : Exit Do

					}

				}

				outputFileStream.Close();


				if ((cSocket.Connected)) {
					cSocket.Close();

				}

				returnValue = ReadReply();
				if ((!(returnValue == FtpResultStatus.FtpResultStatusCode._226_Closing_data_connection | returnValue == FtpResultStatus.FtpResultStatusCode._250_Requested_file_action_okay_completed))) {
					MessageString = _Reply;
					throw new System.IO.IOException(_Reply.Substring(4));
				}

                //'親にイベント発生を通知
                base.OnDownloadComplete(new FtpFileOperationEventArgs(targetFileName));

				return true;


			} catch (System.Exception ex) {
				return true;
				throw ex;


			} finally {
			}


		}

		#endregion

		#region "ファイルのアップロードを行う (UploadFile)(+3)"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのアップロードを行う
		/// </summary>
		/// <param name="uploadFileName">アップロードしたいローカルファイルのフルパス</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool UploadFile(string uploadFileName)
		{

			return UploadFile(uploadFileName, false);

		}

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのアップロードを行う
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
		/// ファイルのアップロードを行う
		/// </summary>
		/// <param name="uploadFileName">アップロードしたいローカルファイルのフルパス</param>
		/// <param name="transferFileName">アップロード先のファイル名</param>
		/// <param name="isResume">Resumeを行うかどうか</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool UploadFile(string uploadFileName, string transferFileName, bool isResume)
		{

			Socket cSocket = null;
			long offset = 0;
			FileStream input = null;
			bool bFileNotFound = false;
			byte[] ReadBuffer = new byte[BLOCK_SIZE + 1];
            FtpResultStatus.FtpResultStatusCode returnValue = 0;
			int bytes = 0;

			//接続されていなければ接続する
			if (this.isConnect() == false) {
				this.Login();
			}

			cSocket = CreateDataSocket();
			offset = 0;

			if (isResume)
            {
				try
                {
					SetBinaryMode(true);
					offset = GetFileSize(uploadFileName);
				}
                catch (System.Exception)
                {
					offset = 0;
				}
			}


			if ((offset > 0)) {
				returnValue = SendCommand("REST " + offset);


				if ((returnValue != FtpResultStatus.FtpResultStatusCode._350_Requested_file_action_pending_further_information)) {
					offset = 0;

				}

			}

			returnValue = SendCommand("STOR " + transferFileName);


			if ((!(returnValue == FtpResultStatus.FtpResultStatusCode._125_Data_connection_already_open_transfer_starting | returnValue == FtpResultStatus.FtpResultStatusCode._150_File_status_okay_about_to_open_data_connection))) {
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}

			bFileNotFound = false;


			if ((File.Exists(uploadFileName))) {
				input = new FileStream(uploadFileName, FileMode.Open);


				if ((offset != 0)) {
					input.Seek(offset, SeekOrigin.Begin);

				}

				bytes = input.Read(ReadBuffer, 0, ReadBuffer.Length);


				while ((bytes > 0)) {
					cSocket.Send(ReadBuffer, bytes, 0);
					bytes = input.Read(ReadBuffer, 0, ReadBuffer.Length);

				}

				input.Close();


			} else {
				bFileNotFound = true;

			}

			if ((cSocket.Connected)) {
				cSocket.Close();
			}

			if ((bFileNotFound)) {
				MessageString = _Reply;
				throw new System.IO.IOException("The file: " + uploadFileName + " was not found. " + "Cannot upload the file to the FTP site");
			}

			returnValue = ReadReply();


			if ((!(returnValue == FtpResultStatus.FtpResultStatusCode._226_Closing_data_connection | returnValue == FtpResultStatus.FtpResultStatusCode._250_Requested_file_action_okay_completed))) {
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}

			//'親にイベント発生を通知
			base.OnUploadComplete(new FtpFileOperationEventArgs(uploadFileName));

            return true;

		}

		#endregion

		#region "DeleteFile"

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

			bool bResult = false;
            FtpResultStatus.FtpResultStatusCode returnValue = 0;

			bResult = true;

			if (this.isConnect() == false) {
				this.Login();
			}

			returnValue = SendCommand("DELE " + targetFileName);


			if ((returnValue != FtpResultStatus.FtpResultStatusCode._250_Requested_file_action_okay_completed)) {
				bResult = false;
				MessageString = _Reply;

			}

			base.OnDeleteComplete(new FtpFileOperationEventArgs(targetFileName));

			return bResult;

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

			bool bResult = false;
            FtpResultStatus.FtpResultStatusCode returnValue = 0;

			bResult = true;

			if (this.isConnect() == false) {
				this.Login();
			}

			returnValue = SendCommand("RNFR " + oldFileName);


			if ((returnValue != FtpResultStatus.FtpResultStatusCode._350_Requested_file_action_pending_further_information)) {
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}

			returnValue = SendCommand("RNTO " + newFileName);


			if ((returnValue != FtpResultStatus.FtpResultStatusCode._250_Requested_file_action_okay_completed)) {
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}

			return bResult;

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

			bool bResult = false;
			FtpResultStatus.FtpResultStatusCode returnValue = 0;

			bResult = true;

			if (this.isConnect() == false) {
				this.Login();
			}

			returnValue = SendCommand("MKD " + directoryName);


			if ((returnValue != FtpResultStatus.FtpResultStatusCode._257_PATHNAME_created)) {
				bResult = false;
				MessageString = _Reply;

			}

			return bResult;

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

			bool bResult = false;
			FtpResultStatus.FtpResultStatusCode returnValue = 0;

			bResult = true;

			if (this.isConnect() == false) {
				this.Login();
			}

			returnValue = SendCommand("RMD " + directoryName);


            if ((returnValue != FtpResultStatus.FtpResultStatusCode._250_Requested_file_action_okay_completed))
            {
				bResult = false;
				MessageString = _Reply;

			}

			return bResult;

		}

		#endregion

		#region "ChangeDirectory"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// カレントディレクトリを変更します
		/// </summary>
		/// <param name="directoryName">変更したいディレクトリ名</param>
		/// <returns>成功した場合 = True , 失敗の場合 = False</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		public override bool ChangeDirectory(string directoryName)
		{
			bool functionReturnValue = false;

			bool bResult = false;
			FtpResultStatus.FtpResultStatusCode returnValue = 0;

			bResult = true;

			if ((directoryName.Equals("."))) {
				return functionReturnValue;
			}

			if (this.isConnect() == false) {
				this.Login();
			}

			returnValue = SendCommand("CWD " + directoryName);


			if ((returnValue != FtpResultStatus.FtpResultStatusCode._250_Requested_file_action_okay_completed)) {
				bResult = false;
				MessageString = _Reply;

			}

			base.RemotePath = directoryName;

			return bResult;

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

			if (((_objClientSocket != null))) {
				this.SendCommand("QUIT");

			}

			this.Cleanup();

			base.OnCloseConnectionComplete(EventArgs.Empty);

		}

		#endregion

		#endregion

		#region "Private Method"

		#region "ReadReply"

		/// <summary>
		/// 応答の読込を行う
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		private FtpResultStatus.FtpResultStatusCode ReadReply()
		{

			_Mes = "";
			_Reply = ReadLine(true);
			return (FtpResultStatus.FtpResultStatusCode)Int32.Parse(_Reply.Substring(0, 3));

		}

		#endregion

		#region "Cleanup"

		/// <summary>
		/// 内部変数を初期化する
		/// </summary>
		/// <remarks></remarks>

		private void Cleanup()
		{

			if ((_objClientSocket != null)) {
				_objClientSocket.Close();
				_objClientSocket = null;

			}

			this._isLoggedIn = false;

		}

		#endregion

		#region "ReadLine"

		/// <summary>
		/// 取得できるバッファの読込を行い、結果を返す
		/// </summary>
		/// <param name="bClearMes"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		private string ReadLine(bool bClearMes)
		{
			string seperator = "\n";
			string[] mess = null;
			byte[] ReadBuffer = new byte[BLOCK_SIZE + 1];
			int bytes = 0;

			if ((bClearMes)) {
				_Mes = "";
			}


			while ((true)) {
				Array.Clear(ReadBuffer, 0, BLOCK_SIZE);

				bytes = _objClientSocket.Receive(ReadBuffer, ReadBuffer.Length, 0);
				_Mes += base.ServerEncoding.GetString(ReadBuffer, 0, bytes);


				if ((bytes < ReadBuffer.Length)) {
					break; // TODO: might not be correct. Was : Exit Do

				}

			}

			mess = _Mes.Split(Convert.ToChar(seperator));


			if ((_Mes.Length > 2)) {
				_Mes = mess[mess.Length - 2];


			} else {
				_Mes = mess[0];

			}


			if ((!(_Mes.Substring(3, 1).Equals(" ")))) {
				return ReadLine(true);

			}

			return _Mes;

		}

		#endregion

		#region "SendCommand"

		/// <summary>
		/// コマンドを送信するサブルーチン
		/// </summary>
		/// <param name="command">実行するコマンド</param>
		/// <remarks>
		///     コマンドを実行するサブルーチンです。FTPにて解釈可能なコマンドが指定できます
		/// </remarks>
		private FtpResultStatus.FtpResultStatusCode SendCommand(string command)
		{

			command = command + "\r\n";

			byte[] cmdbytes = base.ServerEncoding.GetBytes(command);

			_objClientSocket.Send(cmdbytes, cmdbytes.Length, 0);

			//' 結果の読み込みを行いワークに格納する
			return this.ReadReply();

		}

		#endregion

		#region "CreateDataSocket"

		/// <summary>
		/// 接続可能なソケットを返却する
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		private Socket CreateDataSocket()
		{

			int index1 = 0;
			int index2 = 0;
			int len = 0;
			int partCount = 0;
			int i = 0;
			Int32 port = default(Int32);
			string ipData = null;
			string buf = null;
			string ipAddress = null;
			Int32[] parts = new Int32[7];
			char ch = '\0';
			Socket s = null;
			IPEndPoint ep = null;
            FtpResultStatus.FtpResultStatusCode returnValue = 0;

			returnValue = SendCommand("PASV");


			if ((returnValue != FtpResultStatus.FtpResultStatusCode._227_Entering_Passive_Mode)) {
				MessageString = _Reply;
				throw new System.IO.IOException(_Reply.Substring(4));

			}

			index1 = _Reply.IndexOf("(");
			index2 = _Reply.IndexOf(")");
			ipData = _Reply.Substring(index1 + 1, index2 - index1 - 1);

			len = ipData.Length;
			partCount = 0;
			buf = "";

			for (i = 0; i <= ((len - 1) & Convert.ToInt32((partCount <= 6))); i++) {
				ch = char.Parse(ipData.Substring(i, 1));
				if ((char.IsDigit(ch))) {
					buf += ch;
				} else if ((ch != Convert.ToChar(","))) {
					MessageString = _Reply;
					throw new System.IO.IOException("Malformed PASV reply: " + _Reply);
				}

				if (((ch == Convert.ToChar(",")) | (i + 1 == len)))
                {
					try
                    {
						parts[partCount] = Int32.Parse(buf);
						partCount += 1;
						buf = "";
					}
                    catch (System.Exception)
                    {
						MessageString = _Reply;
						throw new System.IO.IOException("Malformed PASV reply: " + _Reply);
					}
				}
			}

			ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];

			port = parts[4] << 8;

			port = port + parts[5];

			s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			ep = new IPEndPoint(getIPV4Address(), port);

			try {
				s.Connect(ep);

			} catch (System.Exception)
            {
				MessageString = _Reply;
				throw new System.IO.IOException("Cannot connect to remote server.");

			}

			return s;

		}

		#endregion

		private IPAddress getIPV4Address()
		{
			IPAddress ipV4 = null;

			foreach (IPAddress ip in Dns.GetHostEntry(base.RemoteHost).AddressList) {
				if (ip.AddressFamily == AddressFamily.InterNetwork) {
					ipV4 = ip;
					break; // TODO: might not be correct. Was : Exit For
				}
			}

			return ipV4;

		}

		#endregion

			// 重複する呼び出しを検出するには
		private bool disposedValue = false;

		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue) {
				if (disposing) {
					// TODO: 他の状態を解放します (マネージ オブジェクト)。
				}

				// TODO: ユーザー独自の状態を解放します (アンマネージ オブジェクト)。
				this.Cleanup();
				// TODO: 大きなフィールドを null に設定します。
			}
			this.disposedValue = true;
		}

		#region " IDisposable Support "
		// このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
		public void Dispose()
		{
			// このコードを変更しないでください。クリーンアップ コードを上の Dispose(ByVal disposing As Boolean) に記述します。
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

	}

}
