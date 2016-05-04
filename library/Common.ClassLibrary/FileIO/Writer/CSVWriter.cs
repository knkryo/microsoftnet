using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Common.ClassLibrary.FileIO.Writer
{
    public class CSVWriter :IDisposable
    {
        public CSVWriter()
        {
            this.IsOverWriteable = false;
            this.IsWriteHeader = false;
            this.IsCustomHeader = false;
            this.CustomHeader = string.Empty;
            this.OutputEncoding = System.Text.Encoding.GetEncoding(932);
            this.EnclosedChar = "\"";
            this.SeparatorChar = ",";
            this.BreakChar = "\r\n";
        }

        #region Property
        
        /// <summary>
        /// 同名のファイルが存在する際に上書をするかどうかを指定します
        /// </summary>
        public bool IsOverWriteable { get; set; }

        /// <summary>
        /// ヘッダを出力するかどうかを指定します
        /// </summary>
        public bool IsWriteHeader { get; set; }

        /// <summary>
        /// カスタムヘッダを利用するかどうかを指定します。利用しない場合、
        /// ヘッダはDataTableのカラム名が利用されます
        /// </summary>
        public bool IsCustomHeader { get; set; }

        /// <summary>
        /// カスタムヘッダを指定します。指定した値はそのままヘッダ文字列として出力されます
        /// </summary>
        public string CustomHeader { get; set; }

        /// <summary>
        /// エンコーディングを指定します (既定値：SHIFT-JIS)
        /// </summary>
        public Encoding OutputEncoding { get; set; }

        /// <summary>
        /// 括りに利用する文字列を指定します。(既定値：")
        /// 必要に応じて出力したい場合には、本区切り文字を未指定としてください。
        /// </summary>
        protected string EnclosedChar { get; set; }

        /// <summary>
        /// 区切り文字を指定します。(既定値：,)
        /// 本プロパティを省略した場合、","が採用されます。
        /// </summary>
        public string SeparatorChar { get; set; }

        /// <summary>
        /// 改行を指定します。(既定値：crlf)
        /// </summary>
        public string BreakChar { get; set; }

        #endregion

        #region Method
        
        public bool Write(System.Data.DataTable dt, string csvPath)
        {
            string buffer;
            List<String> buf = new List<string>();

            int rowCount = dt.Rows.Count;
            int colCount = dt.Columns.Count;
            DataRow current;

            try
            {

                //書き込むファイルを開く
                using (System.IO.StreamWriter sr = new System.IO.StreamWriter(csvPath, false, OutputEncoding))
                {
                    // --------------------------------------------------------
                    // ヘッダを書き込む
                    // --------------------------------------------------------
                    if (IsWriteHeader)
                    {
                        if (this.IsCustomHeader)
                        {
                            buffer = this.CustomHeader;
                        }
                        else
                        {
                            for (int i = 0; i < colCount - 1; i++)
                            {
                                buf.Add(EncloseDoubleQuotesIfNeed(dt.Columns[i].Caption));
                            }
                            buffer = String.Join(this.SeparatorChar, buf.ToArray());
                            buf.Clear();
                        }
                        //値を出力
                        sr.Write(buffer);
                        //改行する
                        sr.Write(this.BreakChar);
                    }

                    // --------------------------------------------------------
                    // レコードを書き込む
                    // --------------------------------------------------------
                    for (int i = 0; i < rowCount - 1; i++)
                    {
                        current = dt.Rows[i];
                        for (int j = 0; j < colCount; j++)
                        {
                            buf.Add(EncloseDoubleQuotesIfNeed(current[j].ToString()));
                        }
                        buffer = String.Join(this.SeparatorChar, buf.ToArray());
                        buf.Clear();

                        //値を出力
                        sr.Write(buffer);
                        //改行する
                        sr.Write(this.BreakChar);
                    }

                    //閉じる
                    sr.Close();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }

        }

        #endregion

        #region Private

        /// <summary>
        /// 必要ならば、文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotesIfNeed(string field)
        {
            if (NeedEncloseDoubleQuotes(field))
            {
                return EncloseDoubleQuotes(field);
            }
            return field;
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む
        /// </summary>
        private string EncloseDoubleQuotes(string field)
        {
            if (field.IndexOf(this.EnclosedChar) > -1)
            {
                //"を""とする
                field = field.Replace(this.EnclosedChar,this.EnclosedChar+this.EnclosedChar);
            }
            return string.Concat(this.EnclosedChar, field, this.EnclosedChar);
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む必要があるか調べる
        /// </summary>
        private bool NeedEncloseDoubleQuotes(string field)
        {
            return field.IndexOf(this.EnclosedChar) > -1 ||
                field.IndexOf(this.SeparatorChar) > -1 ||
                field.IndexOf(this.BreakChar) > -1;
        }

        #endregion

        public void Dispose()
        {

        }
    }
}
