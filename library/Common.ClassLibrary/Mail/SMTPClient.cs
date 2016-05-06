using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ClassLibrary.Mail
{
    public class SimpleSMTPClient
    {
        private System.Net.Mail.SmtpClient smtpClient;

        public System.Net.Mail.SendCompletedEventHandler SendCompleted;

        private ClassLibrary.Mail.SMTPAuth auth;

        #region Constructor
        
        public SimpleSMTPClient(string host, string port,string authUser , string authPassword)
        {
            setInit(host, port, authUser, authPassword);
        }

        private void setInit(string host, string port,string authUser, string authPassword)
        {
            smtpClient = new System.Net.Mail.SmtpClient(host, int.Parse(port));

            //初期はsmtp認証
            auth = new SMTPAuth(host,port,authUser,authPassword,SMTPAuth.AuthTypeAttribute.SMTP);

            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            smtpClient.UseDefaultCredentials = false;

            smtpClient.Timeout = 10000;

            //受信サーバSSL要否
            smtpClient.EnableSsl = false;

            smtpClient.Credentials = new System.Net.NetworkCredential(authUser, authPassword);

        }

        #endregion

        public bool Auth()
        {
            return true;
            //return auth.Auth();
        }

        public void Send(System.Net.Mail.MailMessage message)
        {
            smtpClient.Send(message);
        }
        public void SendAsync(System.Net.Mail.MailMessage message , object token)
        {
            smtpClient.SendCompleted += new System.Net.Mail.SendCompletedEventHandler(sendCompletedCallback);
            smtpClient.SendAsync(message, token);
        }
        private void sendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            OnSendCompleted(e);
        }
        protected void OnSendCompleted(System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (SendCompleted != null)
            {
                SendCompleted(this,e);
            }
        }
        

    }
}
