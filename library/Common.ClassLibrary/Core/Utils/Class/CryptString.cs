using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Common.ClassLibrary.Utils
{

    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 文字列の暗号化クラス
    /// </summary>
    /// <remarks>
    /// 文字列の暗号化と複合化を行います
    /// </remarks>
    /// <history>
    ///     2012/12 Rebuild
    /// </history>
    /// ------------------------------------------------------------------------------------------
    public class CryptString
    {

        #region 文字列を暗号化する (EncryptString)

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 文字列を暗号化する
        /// </summary>
        /// <param name="str">暗号化する文字列</param>
        /// <param name="key">パスワード</param>
        /// <returns>暗号化された文字列</returns>
        /// ------------------------------------------------------------------------------------------
        public string EncryptString(string str, string key)
        {

            byte[] bytesIn = null;
            byte[] bytesKey = null;

            DESCryptoServiceProvider des = null;
            System.IO.MemoryStream memStream = null;

            System.Security.Cryptography.ICryptoTransform desdecrypt = null;

            System.Security.Cryptography.CryptoStream cryptStreem = null;

            byte[] bytesOut = null;


            try
            {
                bytesIn = System.Text.Encoding.UTF8.GetBytes(str);
                bytesKey = System.Text.Encoding.UTF8.GetBytes(key);

                des = new DESCryptoServiceProvider();
                memStream = new System.IO.MemoryStream();

                //共有キーと初期化ベクタを設定
                des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
                des.IV = ResizeBytesArray(bytesKey, des.IV.Length);

                //DES暗号化オブジェクトの作成
                desdecrypt = des.CreateEncryptor();

                //書き込むためのCryptoStreamの作成
                cryptStreem = new System.Security.Cryptography.CryptoStream(memStream, desdecrypt, System.Security.Cryptography.CryptoStreamMode.Write);
                //書き込む
                cryptStreem.Write(bytesIn, 0, bytesIn.Length);
                cryptStreem.FlushFinalBlock();

                bytesOut = memStream.ToArray();

                //閉じる
                cryptStreem.Close();
                memStream.Close();

                //Base64で文字列に変更して結果を返す
                return System.Convert.ToBase64String(bytesOut);


            }
            finally
            {
                bytesIn = null;
                des = null;
                bytesKey = null;

            }

        }

        #endregion

        #region 暗号化された文字列を復号化する (DecryptString)

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 暗号化された文字列を復号化する
        /// </summary>
        /// <param name="str">暗号化された文字列</param>
        /// <param name="key">パスワード</param>
        /// <returns>復号化された文字列</returns>
        /// ------------------------------------------------------------------------------------------
        public string DecryptString(string str, string key)
        {

            DESCryptoServiceProvider des = null;
            byte[] bytesKey = null;
            byte[] bytesIn = null;

            System.IO.MemoryStream memStream = null;
            ICryptoTransform desdecrypt = null;
            CryptoStream cryptStreem = null;
            System.IO.StreamReader sReader = null;
            string result = string.Empty;


            try
            {
                des = new DESCryptoServiceProvider();

                //共有キーと初期化ベクタを決定
                //パスワードをバイト配列にする
                bytesKey = System.Text.Encoding.UTF8.GetBytes(key);

                //共有キーと初期化ベクタを設定
                des.Key = ResizeBytesArray(bytesKey, des.Key.Length);
                des.IV = ResizeBytesArray(bytesKey, des.IV.Length);

                //Base64で文字列をバイト配列に戻す
                bytesIn = System.Convert.FromBase64String(str);

                //暗号化されたデータを読み込むためのMemoryStream
                memStream = new System.IO.MemoryStream(bytesIn);

                //DES復号化オブジェクトの作成
                desdecrypt = des.CreateDecryptor();

                //読み込むためのCryptoStreamの作成
                cryptStreem = new CryptoStream(memStream, desdecrypt, CryptoStreamMode.Read);

                //復号化されたデータを取得するためのStreamReader
                sReader = new System.IO.StreamReader(cryptStreem, System.Text.Encoding.UTF8);

                //復号化されたデータを取得する
                result = sReader.ReadToEnd();

                //閉じる
                sReader.Close();
                cryptStreem.Close();
                memStream.Close();


            }
            finally
            {
                des = null;
                bytesKey = null;
                bytesIn = null;
                memStream = null;
                desdecrypt = null;
                cryptStreem = null;
                sReader = null;

            }

            return result;


        }

        #endregion

        #region Private Method

        #region 共有キー用に、バイト配列のサイズを変更する (ResizeBytesArray)

        /// ------------------------------------------------------------------------------------------
        /// <summary>
        /// 共有キー用に、バイト配列のサイズを変更する
        /// </summary>
        /// <param name="bytes">サイズを変更するバイト配列</param>
        /// <param name="newSize">バイト配列の新しい大きさ</param>
        /// <returns>サイズが変更されたバイト配列</returns>
        /// ------------------------------------------------------------------------------------------
        private static byte[] ResizeBytesArray(byte[] bytes, int newSize)
        {

            byte[] newBytes = new byte[newSize];
            int i = 0;
            int pos = 0;


            if (bytes.Length <= newSize)
            {

                for (i = 0; i <= bytes.Length - 1; i++)
                {
                    newBytes[i] = bytes[i];

                }


            }
            else
            {

                for (i = 0; i <= bytes.Length - 1; i++)
                {
                    newBytes[pos] = Convert.ToByte(newBytes[pos] ^ bytes[i]);

                    pos += 1;


                    if (pos >= newBytes.Length)
                    {
                        pos = 0;

                    }

                }

            }

            return newBytes;

        }

        #endregion

        #endregion

    }

}
