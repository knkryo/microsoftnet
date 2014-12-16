using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Common.ClassLibrary.Net.Ftp
{

	/// ------------------------------------------------------------------------------------------
	/// <summary>
	/// Ftpクライアントを実装するインターフェース
	/// </summary>
	/// <remarks></remarks>
	/// <history>
	///     2008/02/20 R.Kaneko Created
	///     2008/04/07 R.Kaneko isFileExist , isDirectoryExistsを追加
	///     2008/04/28 R.Kaneko getFileListメソッドに、引数2(サブフォルダ検索有無)の
	///                         Overridesメソッドを追加
	/// </history>
	/// ------------------------------------------------------------------------------------------
	public interface IFtpClient
	{

		#region "Properties"

		#region "RemoteHost"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// リモートホストを指定します
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		string RemoteHost { get; set; }
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

		Int32 RemotePort { get; set; }
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

		string RemotePath { get; set; }
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

		string RemotePassword { get; set; }
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

		string RemoteUser { get; set; }
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

		string MessageString { get; set; }
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

		System.Text.Encoding ServerEncoding { get; set; }
		#endregion


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
		bool ChangeDirectory(string directoryName);

		#endregion

		#region "CloseConnection"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 接続を切断します
		/// </summary>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------

		void CloseConnection();
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
		bool CreateDirectory(string directoryName);

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
		bool DownloadFile(string targetFileName);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのダウンロードを行います
		/// </summary>
		/// <param name="targetFileName">ダウンロードするファイル名</param>
		/// <param name="isResume">Resumeを行いますかどうかを指定します</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		bool DownloadFile(string targetFileName, bool isResume);

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
		bool DownloadFile(string targetFileName, string localFileName);

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
		bool DownloadFile(string targetFileName, string localFileName, bool isResume);

		#endregion

		#region "FileExists"

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
		bool FileExist(string targetFileName);

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
		bool DirectoryExists(string targetDirectoryName);

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
		string[] GetFileList(string maskString);


		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 現在のディレクトリのファイル一覧を返却します
		/// </summary>
		/// <param name="maskString">マスクしたい文字列</param>
		/// <param name="subFolderSearch">サブフォルダも検索するかどうか</param>
		/// <returns>String配列</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		string[] GetFileList(string maskString, bool subFolderSearch);

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
		long GetFileSize(string targetFileName);

		#endregion

		#region "isConnect"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 接続されているかどうかを返します
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		bool isConnect();

		#endregion

		#region "Login"

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// 指定されたHost情報を元にログインする
		/// </summary>
		/// <returns></returns>
		/// <remarks>コンストラクタ、またはプロパティで設定されたログイン情報を使用してログインを行います</remarks>
		/// ------------------------------------------------------------------------------------------
		bool Login();

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

		void SetBinaryMode(bool bMode);
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
		bool UploadFile(string uploadFileName);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルのアップロードを行います
		/// </summary>
		/// <param name="uploadFileName">アップロードしたいローカルファイルのフルパス</param>
		/// <param name="isResume">Resumeを行うかどうか</param>
		/// <returns></returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		bool UploadFile(string uploadFileName, bool isResume);

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
		bool UploadFile(string uploadFileName, string transferFileName, bool isResume);

		/// ------------------------------------------------------------------------------------------
		/// <summary>
		/// ファイルの削除を行います
		/// </summary>
		/// <param name="targetFileName">削除したいファイル名</param>
		/// <returns>成功した場合 = True , 失敗の場合 = False</returns>
		/// <remarks></remarks>
		/// ------------------------------------------------------------------------------------------
		bool DeleteFile(string targetFileName);

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
		bool RenameFile(string oldFileName, string newFileName);

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
		bool RemoveDirectory(string directoryName);

		#endregion

		#endregion

	}

}
