using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Common.ClassLibrary.Net.http
{
    /// <summary>
    /// httpアクセスを行う
    /// </summary>
    /// <remarks>作成中</remarks>
    public class httpClient
    {

        #region Constructor
        
        public httpClient()
        {
            this.RequestTimeOut = 5000;
            this.ServerEncodingType = Encoding.GetEncoding("EUC-JP");
            this.RequestContentType = "application/x-www-form-urlencoded";  // POST

        }

        public httpClient(int timeout):this()
        {
            this.RequestTimeOut = timeout;
        }

        public httpClient(int timeout,Encoding encoding)
            : this()
        {
            this.RequestTimeOut = timeout;
            this.ServerEncodingType = encoding;
        }

        public httpClient(int timeout,Encoding encoding, string requestContentType)
            : this()
        {
            this.RequestTimeOut = timeout;
            this.ServerEncodingType = encoding;
            this.RequestContentType = requestContentType;
        }
        #endregion


        public enum httpRequestMethodType
        {
            Get,
            Post
        }

        /// <summary>
        /// HTTPリクエスト種類
        /// </summary>
        public httpRequestMethodType RequestMethodType { get; set; }

        public int RequestTimeOut { get; set; }

        public System.Text.Encoding ServerEncodingType { get; set; }

        public string RequestContentType { get; set; }

        public int ResponseCode { get; set; }


        public string GetHtmlSource(string connectionUrl, httpRequestMethodType methodType, string requestContentType)
        {
            this.RequestMethodType = methodType;
            this.RequestContentType = RequestContentType;
            return GetHtmlSource(connectionUrl);
        }

        public string GetHtmlSource(string connectionUrl , httpRequestMethodType methodType)
        {
            this.RequestMethodType = methodType;
            return GetHtmlSource(connectionUrl);
        }
        /// <summary>
        /// ソースの取得処理
        /// </summary>
        /// <param name="connectionUrl"></param>
        /// <param name="encodeType"></param>
        /// <param name="RequestMethod"></param>
        /// <returns></returns>
        public string GetHtmlSource(string connectionUrl)
        {

            Stream stream = null;
            StreamReader sr = null;

            try
            {
                System.Net.WebRequest webReq = HttpWebRequest.Create(connectionUrl);

                //ポスト送信設定 
                if (RequestMethodType == httpRequestMethodType.Get)
                {
                    webReq.Method = "GET";
                }
                else if (RequestMethodType == httpRequestMethodType.Post)
                {
                    webReq.Method = "POST";
                }

                // 5秒でタイムアウトさせる。 
                webReq.Timeout = RequestTimeOut;

                // デフォルトのコンテントタイプ 
                webReq.ContentType = this.RequestContentType;

                // 結果を受け取る。 
                WebResponse webRes = webReq.GetResponse();

                // HttpWebRequest からストリームを取得する。 
                stream = webRes.GetResponseStream();

                //st.Close();

                //データを読み取って文字列で返す
                sr = new StreamReader(stream, ServerEncodingType);
                String str = null;
                str = sr.ReadToEnd();
                return str;

            }

            catch (System.Net.WebException)
            {
                //this.ResponseCode = Int32.Parse(getErrorStatusCode(ex).ToString());
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }

            finally
            {
                if (sr != null) sr.Close();
                if (stream != null) stream.Close();
            }


        }

        //実装中
        private HttpStatusCode getErrorStatusCode(System.Net.WebException ex)
        {
            //HTTPプロトコルエラーかどうか調べる
            if (ex.Status == System.Net.WebExceptionStatus.ProtocolError)
            {
                //HttpWebResponseを取得
                System.Net.HttpWebResponse errres = (System.Net.HttpWebResponse)ex.Response;
                return errres.StatusCode;
            }
            else
            {
                return HttpStatusCode.SeeOther;
            }
        }

    }
}
