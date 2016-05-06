using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Globalization;

using Common.ClassLibrary.Extensions;

namespace Common.ClassLibrary.Utils
{
    /// ------------------------------------------------------------------------------------------
    /// <summary>
    /// 値の変換を行うユーティリティクラス
    /// </summary>
    /// <history>
    /// 2012/12 Rebuild
    /// </history>
    /// ------------------------------------------------------------------------------------------
    public class ObjectConverter
    {

        #region CsvToArrayList

        /// <summary>
        /// CSV形式のテキストをArrayListに変換
        /// </summary>
        /// <param name="csvText">CSVの内容が入ったString</param>
        /// <returns>変換結果のArrayList</returns>
        public static List<List<string>> CsvToArrayList(string csvText)
        {
            List<List<string>> csvRecords = new List<List<string>>();

            // ----------------------------------------------------------------
            // 事前準備
            // ----------------------------------------------------------------

            //前後の改行を削除しておく
            csvText = csvText.Trim(new char[] { Convert.ToChar("\r"), Convert.ToChar("\n") });

            //一行取り出すための正規表現
            System.Text.RegularExpressions.Regex regLine = new System.Text.RegularExpressions.Regex("^.*(?:\\n|$)", System.Text.RegularExpressions.RegexOptions.Multiline);

            //1行のCSVから各フィールドを取得するための正規表現
            System.Text.RegularExpressions.Regex regCsv = new System.Text.RegularExpressions.Regex("\\s*(\"(?:[^\"]|\"\")*\"|[^,]*)\\s*,", System.Text.RegularExpressions.RegexOptions.None);


            System.Text.RegularExpressions.Match mLine = regLine.Match(csvText);

            // ----------------------------------------------------------------
            // 分割メイン処理
            // ----------------------------------------------------------------
            while (mLine.Success)
            {
                string line = mLine.Value;

                //改行記号が"で囲まれているか調べる
                while ((csvText.GetCharCount("\"") % 2) == 1)
                {
                    mLine = mLine.NextMatch();
                    if (!mLine.Success)
                    {
                        throw new ApplicationException("不正なCSV");
                    }
                    line += mLine.Value;
                }

                //行の最後の改行記号を削除
                line = line.TrimEnd(new char[] { Convert.ToChar("\r"), Convert.ToChar("\n") });

                //最後に「,」をつける
                line += ",";

                // ----------------------------------------------------------------
                // 1つの行からフィールドを取り出す
                // ----------------------------------------------------------------
                List<string> csvFields = new List<string>();
                System.Text.RegularExpressions.Match m = regCsv.Match(line);
                while (m.Success)
                {
                    string field = m.Groups[1].Value;

                    // 前後の空白を削除
                    field = field.Trim();

                    // "で囲まれている時
                    if (field.StartsWith("\"") && field.EndsWith("\""))
                    {
                        // 前後の"を取る
                        field = field.Substring(1, field.Length - 2);
                        // 「""」を「"」にする
                        field = field.Replace("\"\"", "\"");
                    }
                    csvFields.Add(field);
                    m = m.NextMatch();
                }

                csvRecords.Add(csvFields);

                mLine = mLine.NextMatch();
            }

            return csvRecords;
        }

        #endregion

        #region TwoDListToDataTable

        /// <summary>
        /// 二次元配列をDataTableへ変換
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static System.Data.DataTable TwoDListToDataTable(List<List<string>> target)
        {
            return TwoDListToDataTable(target, 0);
        }

        /// <summary>
        /// 二次元配列をDataTableへ変換
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static System.Data.DataTable TwoDListToDataTable(List<List<string>> target, int headerRowPos)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataRow dtRow = null;

            // 最大列数に合わせてカラムを生成
            if (target == null)
            {
                return null;
            }

            //カラム名を付与する
            if (headerRowPos > 0)
            {
                //TODO カラム名重複チェック未対応
                for (int i = 0; i <= getMaxColumnCount(target) - 1; i++)
                {
                    string colName = ConvertNullToStringEmpty(target[headerRowPos - 1][i]);
                    if (colName.Length > 0 && dt.Columns.Contains(colName) == false)
                    {
                        dt.Columns.Add(colName);
                    }
                    else
                    {
                        dt.Columns.Add();
                    }
                }
            }
            else
            {
                //デフォルトのカラム名
                for (int i = 0; i <= getMaxColumnCount(target) - 1; i++)
                {
                    dt.Columns.Add();
                }
            }

            dt.AcceptChanges();

            for (int i = headerRowPos; i <= target.Count - 1; i++)
            {
                dtRow = dt.NewRow();
                for (int j = 0; j <= target[i].Count - 1; j++)
                {
                    dtRow[j] = target[i][j].ToString();
                }

                dt.Rows.Add(dtRow);
            }
            return dt;


        }

        /// <summary>
        /// 最大の列数を取得
        /// </summary>
        /// <param name="targetList"></param>
        /// <returns></returns>
        private static int getMaxColumnCount(List<List<string>> targetList)
        {
            int count = 0;
            if (targetList == null)
            {
                return 0;
            }
            for (int i = 0; i <= targetList.Count - 1; i++)
            {
                if (count < targetList[i].Count)
                {
                    count = targetList[i].Count;
                }
            }
            return count;
        }

        #endregion

        #region TwoDListToDataTable

        /// <summary>
        /// 二次元配列をDataTableへ変換
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static System.Data.DataTable TwoDObjectToDataTable(object[,] target, int headerRowPos)
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataRow dtRow = null;

            // 最大列数に合わせてカラムを生成
            if (target == null)
            {
                return null;
            }

            //カラム名を付与する
            if (headerRowPos > 0)
            {
                for (int i = target.GetLowerBound(0); i <= target.GetLength(1); i++)
                {
                    string colName = ConvertNullToStringEmpty(target[headerRowPos, i]);
                    if (colName.Length > 0 && dt.Columns.Contains(colName) == false)
                    {
                        dt.Columns.Add(colName);
                    }
                    else
                    {
                        dt.Columns.Add();
                    }
                }
            }
            else
            {
                //デフォルトのカラム名
                for (int i = target.GetLowerBound(0); i <= target.GetLength(1); i++)
                {
                    dt.Columns.Add();
                }
            }

            dt.AcceptChanges();

            for (int row = headerRowPos + 1; row <= target.GetLength(0); row++)
            {
                dtRow = dt.NewRow();
                for (int col = target.GetLowerBound(0); col <= target.GetLength(1); col++)
                {
                    dtRow[col - target.GetLowerBound(0)] = ConvertNullToStringEmpty(target[row, col]);
                }

                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        #endregion

        #region ConvertNullToStringEmpty

        public static string ConvertNullToStringEmpty(object target)
        {
            return (target == null ? string.Empty : target.ToString());
        }

        #endregion

    }
}
