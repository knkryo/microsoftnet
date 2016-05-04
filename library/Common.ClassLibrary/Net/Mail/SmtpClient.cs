using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net.Mail;

namespace Common.ClassLibrary.Net.Mail
{
    public class SmtpClient:System.Net.Mail.SmtpClient
    {

        public SmtpClient(string host, int port) : base(host, port)
        {
            this.AuthHost = host;
            this.AuthPort = port;
        }

        public enum MailServerAuthType
        {
            None = 0,
            Smtp = 1,
            Pop = 2
        }

        public string LastErrorMessage { get; set; }
        public bool IsFail { get; set; }

        public string AuthHost { get; set; }

        public int AuthPort { get; set; }

        public string AuthUserId { get; set; }

        public string AuthPassword { get; set; }

        public MailServerAuthType AuthType { get; set; }

        #region Send
        
        public new void Send(MailMessage message)
        {
            try
            {
                LastErrorMessage = string.Empty;
                if (Auth() == true)
                {
                    base.Send(message);
                }
            }
            catch (ArgumentNullException ex)
            {
                //送信エラー
                LastErrorMessage = ex.ToString();

            }
            catch (ArgumentOutOfRangeException ex)
            {
                //送信エラー
                LastErrorMessage = ex.ToString();

            }
            catch (InvalidOperationException ex)
            {
                //送信エラー
                LastErrorMessage = ex.ToString();

            }
            catch (SmtpFailedRecipientsException ex)
            {
                //送信エラー
                LastErrorMessage = ex.ToString();

            }
            catch (SmtpException ex)
            {
                //送信エラー
                LastErrorMessage = ex.ToString();

            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                throw ex;
            }
            finally
            {
            }
        }

        #endregion

        public bool Auth()
        {
            if (this.AuthType == MailServerAuthType.None)
            {
                return true;
            }
                if (this.AuthType == MailServerAuthType.Smtp)
            {
                return this.SmtpAuth();
            }
            else if (this.AuthType == MailServerAuthType.Pop)
            {
                return this.PopAuth();
            }
            else
            {
                return false;
            }
        }

        #region PopAuth

        /// <summary>
        ///    POPサーバー認証
        /// </summary>
        /// <param name="host">サーバー名</param>
        /// <param name="port">ポート番号</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="passWord">パスワード</param>
        /// <remarks>認証結果</remarks>
        public bool PopAuth()
        {
            string msg = "";
            NetworkStream stream = null;
            bool auth = true;

            //TcpClientの作成
            TcpClient client = new TcpClient();
            //タイムアウトの設定

            client.ReceiveTimeout = 10000;
            client.SendTimeout = 10000;

            try
            {
                //サーバーに接続

                client.Connect(AuthHost, AuthPort);
                //ストリームの取得

                stream = client.GetStream();
                //受信
                msg = receiveData(stream);

                //USERの送信
                sendData(stream, "USER " + AuthUserId + System.Environment.NewLine);
                //受信
                msg = receiveData(stream);

                //PASSの送信
                sendData(stream, "PASS " + AuthPassword + System.Environment.NewLine);
                //受信
                msg = receiveData(stream);

                //STATの送信
                sendData(stream, "STAT" + System.Environment.NewLine);
                //受信
                msg = receiveData(stream);

                //QUITの送信
                sendData(stream, "QUIT" + System.Environment.NewLine);
                //受信
                msg = receiveData(stream);

            }
            catch
            {
                auth = false;
            }
            finally
            {
                //切断
                if ((stream != null))
                {
                    stream.Close();
                }
                client.Close();
            }

            return auth;
        }

        #endregion

        #region SMTPAuth

        /// <summary>
        ///    SMTPサーバー認証
        /// </summary>
        /// <param name="host">サーバー名</param>
        /// <param name="port">ポート番号</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="passWord">パスワード</param>
        /// <remarks>認証結果</remarks>
        public bool SmtpAuth()
        {
            string msg = "";
            NetworkStream stream = null;
            bool auth = true;

            //TcpClientの作成
            TcpClient client = new TcpClient();
            //タイムアウトの設定

            client.ReceiveTimeout = 10000;
            client.SendTimeout = 10000;

            try
            {
                //サーバーに接続

                client.Connect(AuthHost, AuthPort);
                //ストリームの取得

                stream = client.GetStream();
                //受信
                msg = receiveData(stream);

                //返り値の始めが"220"ではない場合、エラー
                if (!msg.StartsWith("220"))
                {
                    throw new Exception();
                }

                //EHLOの送信
                sendData(stream, "EHLO " + System.Environment.NewLine);
                //受信
                msg = receiveData(stream);

                if (msg.StartsWith("501"))
                {
                    sendData(stream, "AUTH LOGIN " + System.Environment.NewLine);
                    sendData(stream, this.getBase64String(this.AuthUserId) + System.Environment.NewLine);
                    msg = receiveData(stream);
                    sendData(stream, this.getBase64String(this.AuthPassword) + System.Environment.NewLine);
                    //受信
                    msg = receiveData(stream);

                    if (msg.StartsWith("235"))
                    {
                        auth = true;
                        return auth;
                    }

                }
                else
                {
                    //返り値の始めが"250"ではない場合、エラー
                    if (!msg.StartsWith("250"))
                    {
                        throw new Exception();
                    }
                }

                sendData(stream, "AUTH LOGIN " + System.Environment.NewLine);
                //受信
                msg = receiveData(stream);

                if (!msg.StartsWith("334"))
                {
                    if (msg.StartsWith("502"))
                    {
                        //認証の必要なし

                        return true;
                    }
                    throw new Exception();
                }

                sendData(stream, getBase64String(this.AuthUserId) + System.Environment.NewLine);
                //受信
                msg = receiveData(stream);

                sendData(stream, getBase64String(this.AuthPassword) + System.Environment.NewLine);
                //受信
                msg = receiveData(stream);

                //返り値の始めが"235"ではない場合、エラー
                if (!msg.StartsWith("235"))
                {
                    throw new Exception();
                }

                //QUITの送信
                sendData(stream, "QUIT" + System.Environment.NewLine);
                //受信
                msg = receiveData(stream);

            }
            catch
            {
                auth = false;
            }
            finally
            {
                //切断
                if ((stream != null))
                {
                    stream.Close();
                }
                client.Close();
            }

            return auth;
        }

        #endregion

        #region receiveData
        
        //データを受信する
        private string receiveData(NetworkStream stream, bool multiLines, int bufferSize, Encoding enc)
        {
            byte[] data = new byte[bufferSize];
            int len = 0;
            string msg = "";
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            //すべて受信する
            //(無限ループに陥る恐れあり)
            do
            {
                //受信
                len = stream.Read(data, 0, data.Length);
                ms.Write(data, 0, len);
                //文字列に変換する
                msg = enc.GetString(ms.ToArray());
            } while (stream.DataAvailable || ((!multiLines || msg.StartsWith("-ERR")) && !msg.EndsWith(System.Environment.NewLine)) || (multiLines && !msg.EndsWith(System.Environment.NewLine + "." + System.Environment.NewLine)));

            ms.Close();

            //"-ERR"を受け取った時は例外をスロー
            if (msg.StartsWith("-ERR"))
            {
                throw new ApplicationException("Received Error");
            }
            //表示
            //Console.Write(("S: " + msg))

            return msg;
        }
        private string receiveData(NetworkStream stream, bool multiLines, int bufferSize)
        {
            return receiveData(stream, multiLines, bufferSize, Encoding.GetEncoding(50220));
        }
        private string receiveData(NetworkStream stream, bool multiLines)
        {
            return receiveData(stream, multiLines, 256);
        }

        private string receiveData(NetworkStream stream)
        {
            return receiveData(stream, false);
        }

        #endregion

        #region sendData
        
        //データを送信する
        private void sendData(NetworkStream stream, string msg, Encoding enc)
        {
            //byte型配列に変換
            byte[] data = enc.GetBytes(msg);
            //送信
            stream.Write(data, 0, data.Length);

        }
        private void sendData(NetworkStream stream, string msg)
        {
            sendData(stream, msg, Encoding.ASCII);
        }

        #endregion

        #region getBase64String
        
        //JISでエンコードし、Base64に変換
        private string getBase64String(string str)
        {
            Encoding enc = Encoding.GetEncoding(50220);
            string test = Convert.ToBase64String(enc.GetBytes(str));
            return test;
        }

        #endregion

    }
}
