using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Mail;
using System.Net.Sockets;

namespace Common.ClassLibrary.Mail
{

    /// <summary>
    /// SMTP認証クラス
    /// </summary>
    public class SMTPAuth
    {
        private string _host;
        private int _port;
        private string _userId;
        private string _password;
        private AuthTypeAttribute _authType;
        public enum AuthTypeAttribute
        {
            POP = 1,
            SMTP =2
        }

        public SMTPAuth(string host, string port, string userId, string password, AuthTypeAttribute authType)
        {
            _host = host;
            _port = int.Parse(port);
            _userId = userId;
            _password = password;
            _authType = authType;
        }
        public bool Auth()
        {
            switch (_authType)
            {
                case AuthTypeAttribute.POP:
                    return popAuth(_host,_port,_userId,_password);
                case AuthTypeAttribute.SMTP:
                    return smtpAuth(_host,_port,_userId,_password);
                default:
                    return false;
            }
        }


		#region "サーバー認証"

		/// <summary>
		///    POPサーバー認証
		/// </summary>
		/// <param name="host">サーバー名</param>
		/// <param name="port">ポート番号</param>
		/// <param name="userId">ユーザーID</param>
		/// <param name="passWord">パスワード</param>
		/// <remarks>認証結果</remarks>
		private bool popAuth(string host, int port, string userId, string passWord)
		{
			string msg = "";
			NetworkStream stream = null;
			bool auth = true;

			//TcpClientの作成
			TcpClient client = new TcpClient();
			//タイムアウトの設定

			client.ReceiveTimeout = 10000;
			client.SendTimeout = 10000;

			try {
				//サーバーに接続

				client.Connect(host, port);
				//ストリームの取得

				stream = client.GetStream();
				//受信
				msg = ReceiveData(stream);

				//USERの送信
				SendData(stream, "USER " + userId + System.Environment.NewLine);
				//受信
				msg = ReceiveData(stream);

				//PASSの送信
				SendData(stream, "PASS " + passWord + System.Environment.NewLine);
				//受信
				msg = ReceiveData(stream);

				//STATの送信
				SendData(stream, "STAT" + System.Environment.NewLine);
				//受信
				msg = ReceiveData(stream);

				//QUITの送信
				SendData(stream, "QUIT" + System.Environment.NewLine);
				//受信
				msg = ReceiveData(stream);

			} catch {
				auth = false;
			} finally {
				//切断
				if ((stream != null)) {
					stream.Close();
				}
				client.Close();
			}

			return auth;
		}

		/// <summary>
		///    SMTPサーバー認証
		/// </summary>
		/// <param name="host">サーバー名</param>
		/// <param name="port">ポート番号</param>
		/// <param name="userId">ユーザーID</param>
		/// <param name="passWord">パスワード</param>
		/// <remarks>認証結果</remarks>
		private bool smtpAuth(string host, int port, string userId, string passWord)
		{
			string msg = "";
			NetworkStream stream = null;
			bool auth = true;

			//TcpClientの作成
			TcpClient client = new TcpClient();
			//タイムアウトの設定

			client.ReceiveTimeout = 10000;
			client.SendTimeout = 10000;

			try {
				//サーバーに接続

				client.Connect(host, port);
				//ストリームの取得

				stream = client.GetStream();
				//受信
				msg = ReceiveData(stream);

				//返り値の始めが"220"ではない場合、エラー
				if (!msg.StartsWith("220")) {
					throw new Exception();
				}

				//EHLOの送信
				SendData(stream, "EHLO " + System.Environment.NewLine);
				//受信
				msg = ReceiveData(stream);

				//返り値の始めが"250"ではない場合、エラー
				if (!(msg.StartsWith("250") || msg.StartsWith("501"))) {
					throw new Exception();
				}

				SendData(stream, "AUTH LOGIN " + System.Environment.NewLine);
				//受信
				msg = ReceiveData(stream);

				if (!msg.StartsWith("334")) {
					if (msg.StartsWith("502")) {
						//認証の必要なし

						return true;
					}
					throw new Exception();
				}

				SendData(stream, GetBase64String(userId) + System.Environment.NewLine);
				//受信
				msg = ReceiveData(stream);

				//SendData(stream, "AUTH " + msg + " " + vbCrLf)
				//'受信
				//msg = ReceiveData(stream)

				SendData(stream, GetBase64String(passWord) + System.Environment.NewLine);
				//受信
				msg = ReceiveData(stream);




				//返り値の始めが"250"ではない場合、エラー
				if (!msg.StartsWith("235")) {
					throw new Exception();
				}

				//QUITの送信
				SendData(stream, "QUIT" + System.Environment.NewLine);
				//受信
				msg = ReceiveData(stream);

			}
            catch (Exception ex) {
				auth = false;
			} finally {
				//切断
				if ((stream != null)) {
					stream.Close();
				}
				client.Close();
			}

			return auth;
		}

		//データを受信する
		private static string ReceiveData(NetworkStream stream, bool multiLines, int bufferSize, Encoding enc)
		{
			byte[] data = new byte[bufferSize];
			int len = 0;
			string msg = "";
			System.IO.MemoryStream ms = new System.IO.MemoryStream();

			//すべて受信する
			//(無限ループに陥る恐れあり)
			do {
				//受信
				len = stream.Read(data, 0, data.Length);
				ms.Write(data, 0, len);
				//文字列に変換する
				msg = enc.GetString(ms.ToArray());
			} while (stream.DataAvailable || ((!multiLines || msg.StartsWith("-ERR")) && !msg.EndsWith(System.Environment.NewLine)) || (multiLines && !msg.EndsWith(System.Environment.NewLine + "." + System.Environment.NewLine)));

			ms.Close();

			//"-ERR"を受け取った時は例外をスロー
			if (msg.StartsWith("-ERR")) {
				throw new ApplicationException("Received Error");
			}
			//表示
			//Console.Write(("S: " + msg))

			return msg;
		}
		private static string ReceiveData(NetworkStream stream, bool multiLines, int bufferSize)
		{
			return ReceiveData(stream, multiLines, bufferSize, Encoding.GetEncoding(50220));
		}
		private static string ReceiveData(NetworkStream stream, bool multiLines)
		{
			return ReceiveData(stream, multiLines, 256);
		}

		private static string ReceiveData(NetworkStream stream)
		{
			return ReceiveData(stream, false);
		}

		//データを送信する
		private static void SendData(NetworkStream stream, string msg, Encoding enc)
		{
			//byte型配列に変換
			byte[] data = enc.GetBytes(msg);
			//送信
			stream.Write(data, 0, data.Length);

		}
		private static void SendData(NetworkStream stream, string msg)
		{
			SendData(stream, msg, Encoding.ASCII);
		}


		//JISでエンコードし、Base64に変換
		private static string GetBase64String(string str)
		{
			Encoding enc = Encoding.GetEncoding(50220);
			string test = Convert.ToBase64String(enc.GetBytes(str));
			return test;
		}

        #endregion

    }
}
