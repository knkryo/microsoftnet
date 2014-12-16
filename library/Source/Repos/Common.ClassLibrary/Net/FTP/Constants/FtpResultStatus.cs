using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Common.ClassLibrary.Net.Ftp
{

	public static class FtpResultStatus
	{

		/// <summary>
		/// System.Net.FtpStatusCodeの独自版
		/// </summary>
		/// <remarks>
		/// 開発にあたり、System.Net.FtpStatusCodeだけではコードを見つけるのが辛いので、別出ししています。
		/// 
		/// </remarks>
		public enum FtpResultStatusCode
		{

			///<summary>
			///    Restart marker reply
			///    リスタートマーカー応答
			///</summary>
			///<remarks>
			///    Restart marker reply
			///    リスタートマーカー応答
			///    この場合、文章は正確でなくてはならず、以前の実装が残されていてはならない。
			///    これはMARK yyyy = mmmmyyyyは、ユーザープロセスのデータ列中の目印であり、mmmmはサーバー側で対応する目印である。
			///    マーカーと「＝」の間には空白があることに注意
			///</remarks>
			_110_Restart_marker_reply = 110,


			///<summary>
			///    Service ready in nnn minutes
			///    サービスはnnn分で準備できる
			///</summary>
			///<remarks>
			///    Service ready in nnn minutes
			///    サービスはnnn分で準備できる
			///</remarks>
			_120_Service_ready_in_nnn_minutes = 120,


			///<summary>
			///    Data connection already open transfer starting
			///    データ接続はすでにオープンしている。転送を開始する
			///</summary>
			///<remarks>
			///    Data connection already open transfer starting
			///    データ接続はすでにオープンしている。転送を開始する
			///</remarks>
			_125_Data_connection_already_open_transfer_starting = 125,


			///<summary>
			///    File status okay about to open data connection
			///    ファイル状態は問題ない。データ接続をオープンしようとしている
			///</summary>
			///<remarks>
			///    File status okay about to open data connection
			///    ファイル状態は問題ない。データ接続をオープンしようとしている
			///</remarks>
			_150_File_status_okay_about_to_open_data_connection = 150,


			///<summary>
			///    Command okay
			///    コマンドは成功した
			///</summary>
			///<remarks>
			///    Command okay
			///    コマンドは成功した
			///</remarks>
			_200_Command_okay = 200,


			///<summary>
			///    Command not implemented superfluous at this site
			///    コマンドは実装されていない、このサイトでは不要。
			///</summary>
			///<remarks>
			///    Command not implemented superfluous at this site
			///    コマンドは実装されていない、このサイトでは不要。
			///</remarks>
			_202_Command_not_implemented_superfluous_at_this_site = 202,


			///<summary>
			///    System status or system help reply
			///    システム状態、ヘルプの応答
			///</summary>
			///<remarks>
			///    System status or system help reply
			///    システム状態、ヘルプの応答
			///</remarks>
			_211_System_status_or_system_help_reply = 211,


			///<summary>
			///    Directory status
			///    ディレクトリ状態
			///</summary>
			///<remarks>
			///    Directory status
			///    ディレクトリ状態
			///</remarks>
			_212_Directory_status = 212,


			///<summary>
			///    File status
			///    ファイル状態
			///</summary>
			///<remarks>
			///    File status
			///    ファイル状態
			///</remarks>
			_213_File_status = 213,


			///<summary>
			///    Help message
			///    ヘルプメッセージサーバーの使い方やコマンド、特に標準でないものの意味。この応答は人間のユーザーにとってのみ便利である。
			///</summary>
			///<remarks>
			///    Help message
			///    ヘルプメッセージサーバーの使い方やコマンド、特に標準でないものの意味。この応答は人間のユーザーにとってのみ便利である。
			///</remarks>
			_214_Help_message = 214,


			///<summary>
			///    NAME system type
			///    NAMEは、Assigned Numbers documentによる公式なシステム名称。
			///</summary>
			///<remarks>
			///    NAME system type
			///    NAMEは、Assigned Numbers documentによる公式なシステム名称。
			///</remarks>
			_215_NAME_system_type = 215,


			///<summary>
			///    Service ready for new user
			///    新規ユーザーへの準備が完了した
			///</summary>
			///<remarks>
			///    Service ready for new user
			///    新規ユーザーへの準備が完了した
			///</remarks>
			_220_Service_ready_for_new_user = 220,


			///<summary>
			///    Service closing control connection
			///    サービスはコントロール接続をクローズしているもし、適切ならログアウトである。
			///</summary>
			///<remarks>
			///    Service closing control connection
			///    サービスはコントロール接続をクローズしているもし、適切ならログアウトである。
			///</remarks>
			_221_Service_closing_control_connection = 221,


			///<summary>
			///    Data connection open no transfer in progress
			///    データ接続はオープンした。転送は行われていない
			///</summary>
			///<remarks>
			///    Data connection open no transfer in progress
			///    データ接続はオープンした。転送は行われていない
			///</remarks>
			_225_Data_connection_open_no_transfer_in_progress = 225,


			///<summary>
			///    Closing data connection
			///    データ接続をクローズしている要求されたファイル処理は成功した例えば、ファイル転送やファイルアボート？？など
			///</summary>
			///<remarks>
			///    Closing data connection
			///    データ接続をクローズしている要求されたファイル処理は成功した例えば、ファイル転送やファイルアボート？？など
			///</remarks>
			_226_Closing_data_connection = 226,


			///<summary>
			///    Entering Passive Mode (h1h2h3h4p1p2)
			///    パッシブモードに入る(h1h2h3h4p1p2)
			///</summary>
			///<remarks>
			///    Entering Passive Mode (h1h2h3h4p1p2)
			///    パッシブモードに入る(h1h2h3h4p1p2)
			///</remarks>
			_227_Entering_Passive_Mode = 227,


			///<summary>
			///    User logged in proceed
			///    ユーザーはログインする。先に進む。
			///</summary>
			///<remarks>
			///    User logged in proceed
			///    ユーザーはログインする。先に進む。
			///</remarks>
			_230_User_logged_in_proceed = 230,


			///<summary>
			///    Requested file action okay completed
			///    要求されたファイル操作は問題なく終了した
			///</summary>
			///<remarks>
			///    Requested file action okay completed
			///    要求されたファイル操作は問題なく終了した
			///</remarks>
			_250_Requested_file_action_okay_completed = 250,


			///<summary>
			///    PATHNAME created
			///    「PATHNAME」が作成された
			///</summary>
			///<remarks>
			///    PATHNAME created
			///    「PATHNAME」が作成された
			///</remarks>
			_257_PATHNAME_created = 257,


			///<summary>
			///    User name okay need password
			///    ユーザー名は問題無い。パスワードを必要とする
			///</summary>
			///<remarks>
			///    User name okay need password
			///    ユーザー名は問題無い。パスワードを必要とする
			///</remarks>
			_331_User_name_okay_need_password = 331,


			///<summary>
			///    Need account for login
			///    ログインには、課金情報が必要
			///</summary>
			///<remarks>
			///    Need account for login
			///    ログインには、課金情報が必要
			///</remarks>
			_332_Need_account_for_login = 332,


			///<summary>
			///    Requested file action pending further information
			///    要求されたファイル操作は他の情報を待っている
			///</summary>
			///<remarks>
			///    Requested file action pending further information
			///    要求されたファイル操作は他の情報を待っている
			///</remarks>
			_350_Requested_file_action_pending_further_information = 350,


			///<summary>
			///    Service not available closing control connection
			///    サービスは有効でない。コントロール接続をクローズするこのコマンドは、サーバーがシャットダウンしなければならないとき、どのコマンドの応答ともなりうる。
			///</summary>
			///<remarks>
			///    Service not available closing control connection
			///    サービスは有効でない。コントロール接続をクローズするこのコマンドは、サーバーがシャットダウンしなければならないとき、どのコマンドの応答ともなりうる。
			///</remarks>
			_421_Service_not_available_closing_control_connection = 421,


			///<summary>
			///    Can't open data connection
			///    データ接続をオープンできない
			///</summary>
			///<remarks>
			///    Can't open data connection
			///    データ接続をオープンできない
			///</remarks>
			_425_Cant_open_data_connection = 425,


			///<summary>
			///    Connection closed transfer aborted
			///    接続はクローズした。転送は中断した。
			///</summary>
			///<remarks>
			///    Connection closed transfer aborted
			///    接続はクローズした。転送は中断した。
			///</remarks>
			_426_Connection_closed_transfer_aborted = 426,


			///<summary>
			///    Requested file action not taken
			///    要求されたファイル操作は行われなかったファイルが使用不能例：ファイルビジー
			///</summary>
			///<remarks>
			///    Requested file action not taken
			///    要求されたファイル操作は行われなかったファイルが使用不能例：ファイルビジー
			///</remarks>
			_450_Requested_file_action_not_taken = 450,


			///<summary>
			///    Requested action aborted Local error in processing
			///    要求された操作は中断した。処理の際にローカルエラーが起こった
			///</summary>
			///<remarks>
			///    Requested action aborted Local error in processing
			///    要求された操作は中断した。処理の際にローカルエラーが起こった
			///</remarks>
			_451_Requested_action_aborted_Local_error_in_processing = 451,


			///<summary>
			///    Requested action not taken
			///    要求された操作は行われなかったシステムに十分な記憶容量がない。
			///</summary>
			///<remarks>
			///    Requested action not taken
			///    要求された操作は行われなかったシステムに十分な記憶容量がない。
			///</remarks>
			_452_Requested_action_not_taken_Disk_insufficiency = 452,


			///<summary>
			///    Syntax error command unrecognized
			///    文法エラー、コマンドは認識出来なかった。これには、コマンド行が長すぎる等も含まれるだろう。
			///</summary>
			///<remarks>
			///    Syntax error command unrecognized
			///    文法エラー、コマンドは認識出来なかった。これには、コマンド行が長すぎる等も含まれるだろう。
			///</remarks>
			_500_Syntax_error_command_unrecognized = 500,


			///<summary>
			///    Syntax error in parameters or arguments
			///    引数やパラメータに文法エラーがある
			///</summary>
			///<remarks>
			///    Syntax error in parameters or arguments
			///    引数やパラメータに文法エラーがある
			///</remarks>
			_501_Syntax_error_in_parameters_or_arguments = 501,


			///<summary>
			///    Command not implemented
			///    コマンドは実装されていない
			///</summary>
			///<remarks>
			///    Command not implemented
			///    コマンドは実装されていない
			///</remarks>
			_502_Command_not_implemented = 502,


			///<summary>
			///    Bad sequence of commands
			///    コマンドの順序が不正
			///</summary>
			///<remarks>
			///    Bad sequence of commands
			///    コマンドの順序が不正
			///</remarks>
			_503_Bad_sequence_of_commands = 503,


			///<summary>
			///    Command not implemented for that parameter
			///    コマンドのパラメータが実装されていない
			///</summary>
			///<remarks>
			///    Command not implemented for that parameter
			///    コマンドのパラメータが実装されていない
			///</remarks>
			_504_Command_not_implemented_for_that_parameter = 504,


			///<summary>
			///    Not logged in
			///    ログインしていない
			///</summary>
			///<remarks>
			///    Not logged in
			///    ログインしていない
			///</remarks>
			_530_Not_logged_in = 530,


			///<summary>
			///    Need account for storing files
			///    ファイル蓄積には課金情報が必要
			///</summary>
			///<remarks>
			///    Need account for storing files
			///    ファイル蓄積には課金情報が必要
			///</remarks>
			_532_Need_account_for_storing_files = 532,


			///<summary>
			///    Requested action not taken
			///    要求されたファイル操作は行われなかったファイルが使用不能例：ファイルが存在しない、アクセス不能
			///</summary>
			///<remarks>
			///    Requested action not taken
			///    要求されたファイル操作は行われなかったファイルが使用不能例：ファイルが存在しない、アクセス不能
			///</remarks>
			_550_Requested_action_not_taken_access_denied = 550,


			///<summary>
			///    Requested action aborted Page type unknown
			///    要求された操作は中断した。ページタイプが不明
			///</summary>
			///<remarks>
			///    Requested action aborted Page type unknown
			///    要求された操作は中断した。ページタイプが不明
			///</remarks>
			_551_Requested_action_aborted_Page_type_unknown = 551,


			///<summary>
			///    Requested file action aborted
			///    要求されたファイル操作は中断した現在のディレクトリや、データセットに確保された記憶容量を超えた。
			///</summary>
			///<remarks>
			///    Requested file action aborted
			///    要求されたファイル操作は中断した現在のディレクトリや、データセットに確保された記憶容量を超えた。
			///</remarks>
			_552_Requested_file_action_aborted = 552,


			///<summary>
			///    Requested action not taken
			///    要求された処理は行われなかったファイル名が許されていない。
			///</summary>
			///<remarks>
			///    Requested action not taken
			///    要求された処理は行われなかったファイル名が許されていない。
			///</remarks>

			_553_Requested_action_not_taken_invalid_filename = 553



		}


	}

	//110		Restart_marker_reply	110	Restart marker reply	（リスタートマーカー応答）この場合、文章は正確でなくてはならず、以前の実装が残されていてはならない。これはMARK yyyy = mmmmyyyyは、ユーザープロセスのデータ列中の目印であり、mmmmはサーバー側で対応する目印である。（マーカーと「＝」の間には空白があることに注意）
	//120		Service_ready_in_nnn_minutes	120	Service ready in nnn minutes	（サービスはnnn分で準備できる？？）
	//125		Data_connection_already_open_transfer_starting	125	Data connection already open transfer starting	（データ接続はすでにオープンしている。転送を開始する）
	//150		File_status_okay_about_to_open_data_connection	150	File status okay about to open data connection	（ファイル状態は問題ない。データ接続をオープンしようとしている）
	//200		Command_okay	200	Command okay	（コマンドは成功した）
	//202		Command_not_implemented_superfluous_at_this_site	202	Command not implemented superfluous at this site	（コマンドは実装されていない、このサイトでは不要。）
	//211		System_status_or_system_help_reply	211	System status or system help reply	（システム状態、ヘルプの応答）
	//212		Directory_status	212	Directory status	（ディレクトリ状態）
	//213		File_status	213	File status	（ファイル状態）
	//214		Help_message	214	Help message	（ヘルプメッセージ）サーバーの使い方やコマンド、特に標準でないものの意味。この応答は人間のユーザーにとってのみ便利である。
	//215		NAME_system_type	215	NAME system type	NAMEは、Assigned Numbers documentによる公式なシステム名称。
	//220		Service_ready_for_new_user	220	Service ready for new user	（新規ユーザーへの準備が完了した）
	//221		Service_closing_control_connection	221	Service closing control connection	（サービスはコントロール接続をクローズしている）もし、適切ならログアウトである。
	//225		Data_connection_open_no_transfer_in_progress	225	Data connection open no transfer in progress	（データ接続はオープンした。転送は行われていない）
	//226		Closing_data_connection	226	Closing data connection	（データ接続をクローズしている）要求されたファイル処理は成功した（例えば、ファイル転送やファイルアボート？？など）
	//227		Entering_Passive_Mode	227	Entering Passive Mode (h1h2h3h4p1p2)	（パッシブモードに入る(h1h2h3h4p1p2)）
	//230		User_logged_in_proceed	230	User logged in proceed	（ユーザーはログインする。先に進む。）
	//250		Requested_file_action_okay_completed	250	Requested file action okay completed	（要求されたファイル操作は問題なく終了した）
	//257		PATHNAME_created	257	PATHNAME created	（「PATHNAME」が作成された）
	//331		User_name_okay_need_password	331	User name okay need password	（ユーザー名は問題無い。パスワードを必要とする）
	//332		Need_account_for_login	332	Need account for login	（ログインには、課金情報が必要）
	//350		Requested_file_action_pending_further_information	350	Requested file action pending further information	（要求されたファイル操作は他の情報を待っている）
	//421		Service_not_available_closing_control_connection	421	Service not available closing control connection	（サービスは有効でない。コントロール接続をクローズする）このコマンドは、サーバーがシャットダウンしなければならないとき、どのコマンドの応答ともなりうる。
	//425		Can't_open_data_connection	425	Can't open data connection	（データ接続をオープンできない）
	//426		Connection_closed_transfer_aborted	426	Connection closed transfer aborted	（接続はクローズした。転送は中断した。）
	//450		Requested_file_action_not_taken	450	Requested file action not taken	（要求されたファイル操作は行われなかった）ファイルが使用不能（例：ファイルビジー）
	//451		Requested_action_aborted_Local_error_in_processing	451	Requested action aborted Local error in processing	（要求された操作は中断した。処理の際にローカルエラーが起こった）
	//452		Requested_action_not_taken_Disk_insufficiency	452	Requested action not taken	（要求された操作は行われなかった）システムに十分な記憶容量がない。
	//500		Syntax_error_command_unrecognized	500	Syntax error command unrecognized	（文法エラー、コマンドは認識出来なかった。）これには、コマンド行が長すぎる等も含まれるだろう。
	//501		Syntax_error_in_parameters_or_arguments	501	Syntax error in parameters or arguments	（引数やパラメータに文法エラーがある）
	//502		Command_not_implemented	502	Command not implemented	（コマンドは実装されていない）
	//503		Bad_sequence_of_commands	503	Bad sequence of commands	（コマンドの順序が不正）
	//504		Command_not_implemented_for_that_parameter	504	Command not implemented for that parameter	（コマンドのパラメータが実装されていない）
	//530		Not_logged_in	530	Not logged in	（ログインしていない）
	//532		Need_account_for_storing_files	532	Need account for storing files	（ファイル蓄積には課金情報が必要）
	//550		Requested_action_not_taken_access_denied	550	Requested action not taken	（要求されたファイル操作は行われなかった）ファイルが使用不能（例：ファイルが存在しない、アクセス不能）
	//551		Requested_action_aborted_Page_type_unknown	551	Requested action aborted Page type unknown	（要求された操作は中断した。ページタイプが不明）
	//552		Requested_file_action_aborted	552	Requested file action aborted	（要求されたファイル操作は中断した）（現在のディレクトリや、データセットに）確保された記憶容量を超えた。
	//553		Requested_action_not_taken_invalid_filename	553	Requested action not taken	（要求された処理は行われなかった）ファイル名が許されていない。
	//
}
