using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

using Common.ClassLibrary.Utils;
using Common.ClassLibrary.Extensions;
namespace Common.ClassLibrary.Data.Extensions
{
    /// <summary>
    /// DataTable関連のユーティリティ
    /// </summary>
    /// <history>
    /// 2012/12 Rebuild
    /// </history>
    public static class DataTableExtensions
    {
        #region SelectDataTable

        /// <summary>
        /// DataTableから内容をSelectして、結果を返す
        /// Clone -- Rows.Select() -- ImportRowで返します
        /// </summary>
        /// <param name="targetDataTable"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static DataTable SelectDataTable(this DataTable targetDataTable, string filter)
        {
            return SelectDataTable(targetDataTable, filter, string.Empty);
        }

        public static DataTable SelectDataTable(this DataTable targetDataTable, string filter, string sort)
        {
            DataTable returnDt = null;

            returnDt = targetDataTable.Clone();
            foreach (DataRow dtRow in targetDataTable.Select(filter, sort))
            {
                returnDt.ImportRow(dtRow);
            }
            returnDt.AcceptChanges();
            return returnDt;
        }


        #endregion

        #region GetRowCountDataTable (集計項目として指定した内容をGroup化し、レコード件数を計算して返します)

        /// <summary>
        /// 集計項目として指定した内容をGroup化し、レコード件数を計算して返します
        /// </summary>
        /// <param name="targetDataTable"></param>
        /// <param name="distinctColumnName">Group化する列名</param>
        /// <returns></returns>
        public static DataTable GetRowCountDataTable(this System.Data.DataTable targetDataTable, params string[] distinctColumnName)
        {

            System.Data.DataTable wkDt = null;
            System.Text.StringBuilder whereQuerys = null;
            string whereQuery = string.Empty;
            int count = 0;

            wkDt = targetDataTable.DefaultView.ToTable(true, distinctColumnName);
            wkDt.Columns.Add("_COUNT", System.Type.GetType("System.Int64"));


            foreach (System.Data.DataRow dtRow in wkDt.Rows)
            {
                whereQuerys = new StringBuilder();
                count = 0;
                foreach (string s in distinctColumnName)
                {
                    whereQuerys.Append(s.ToString() + " = '" + dtRow[s.ToString()].ToString() + "'");
                    count += 1;
                    if (count != distinctColumnName.Length)
                    {
                        whereQuerys.Append(" AND ");
                    }
                }
                whereQuery = whereQuerys.ToString();


                dtRow["_COUNT"] = targetDataTable.Select(whereQuery, "").Length;
            }
            return wkDt;

        }

        #endregion

        #region CompareDataTable (同一のデータテーブルであるかどうかを比較して結果を返す)

        /// <summary>
        /// 同一のデータテーブルであるかどうかを比較して結果を返す
        /// </summary>
        /// <param name="source">比較対象元</param>
        /// <param name="dest">比較対象先</param>
        /// <returns></returns>
        public static bool CompareDataTable(this System.Data.DataTable source, System.Data.DataTable dest)
        {

            if (source == null && dest == null)
            {
                return true;
            }
            if (source == null || dest == null)
            {
                return false;
            }

            if (source.Rows.Count != dest.Rows.Count || source.Columns.Count != dest.Columns.Count)
            {
                return false;
            }

            for (Int32 i = 0; i <= source.Rows.Count - 1; i++)
            {
                for (Int32 j = 0; j <= source.Columns.Count - 1; j++)
                {
                    ///Nullが入ると痛いので強制的に文字に置き換えて比較
                    //if ((source.Rows[i][j] + "").ToString() != (dest.Rows[i][j] + "").ToString())
                    //{
                    //    return false;
                    //}

                    if (!source.Rows[i][j].Equals(dest.Rows[i][j]))
                    {
                        return false;
                    }
                }
            }
            return true;

        }

        #endregion

        #region GetSummaryDataRowFromAnyColumns (指定された列の合計を計算したDataRowオブジェクトを返却します)

        /// <summary>
        /// 指定された列の合計を計算したDataRowオブジェクトを返却します
        /// </summary>
        /// <param name="targetDataTable"></param>
        /// <returns></returns>
        public static DataRow GetSummaryDataRowFromAnyColumns(this DataTable targetDataTable, params int[] targetRowPosition)
        {
            decimal sum = 0m;
            DataRow dtRowSummary = targetDataTable.NewRow();

            // 合計を計算してRowにセットする
            for (int i = 0; i <= targetRowPosition.Length; i++)
            {
                sum = 0m;
                for (int j = 0; j <= targetDataTable.Rows.Count - 1; j++)
                {
                    sum += (targetDataTable.Rows[j][i].ToSafeDecimal());
                }
                dtRowSummary[i] = sum.ToString();
            }
            return dtRowSummary;

        }

        #endregion

        #region GetSummaryDataRow (指定されたDataTableの全列の計を計算したDataRowオブジェクトを返却します)

        /// <summary>
        /// 指定されたDataTableの全列の計を計算したDataRowオブジェクトを返却します
        /// </summary>
        /// <param name="targetDataTable"></param>
        /// <returns></returns>
        public static DataRow GetSummaryDataRow(this DataTable targetDataTable)
        {
            decimal sum = 0m;
            DataRow dtRowSummary = targetDataTable.NewRow();

            // 合計を計算してRowにセットする
            for (int i = 0; i <= targetDataTable.Columns.Count - 1; i++)
            {
                sum = 0m;
                for (int j = 0; j <= targetDataTable.Rows.Count - 1; j++)
                {
                    sum += (targetDataTable.Rows[j][i].ToSafeDecimal());
                }
                dtRowSummary[i] = sum.ToString();
            }
            return dtRowSummary;

        }

        #endregion

        #region DataRowsAcceptChanges (DataTable内の全ての行毎にAcceptChangesを行います)

        /// <summary>
        /// DataTable内の全ての行毎にAcceptChangesを行います
        /// </summary>
        /// <param name="targetDataTable"></param>
        public static void DataRowsAcceptChanges(this DataTable targetDataTable)
        {
            foreach (DataRow dtRow in targetDataTable.Rows)
            {
                if (dtRow.RowState != DataRowState.Unchanged)
                {
                    dtRow.AcceptChanges();
                }
            }
        }

        #endregion

        #region CsvToDataTable (CSVをDataTableに変換)

        /// <summary>
        /// CSVをDataTableに変換
        /// </summary>
        /// <param name="csvText">CSVの内容が入ったString</param>
        /// <returns>変換結果のArrayList</returns>
        public static DataTable CsvToDataTable(this string csvText)
        {
            return ObjectConverter.TwoDListToDataTable(ObjectConverter.CsvToArrayList(csvText));
        }

        #endregion

        #region GetUnMatchRow

        public static System.Data.DataTable GetUnMatchRow(this System.Data.DataTable targetA, System.Data.DataTable targetB)
        {
            System.Data.DataTable dt;

            dt = targetA.Clone();
            foreach (DataRow dr in targetA.Rows)
            {
                if (dr.IsSameRowExists(targetB, false) == false)
                {
                    dt.ImportRow(dr);
                }
            }
            return dt;
        }

        #endregion

        #region IsSameRow

        public static bool IsSameRowExists(this DataRow targetRow, DataTable dest)
        {
            return IsSameRowExists(targetRow, dest, false);
        }
        public static bool IsSameRowExists(this DataRow targetRow, DataTable dest, bool isExistsColumnOnly)
        {
            for (int i = 0; i < dest.Rows.Count; i++)
            {
                if (IsSameRow(targetRow, dest.Rows[i], isExistsColumnOnly) == true)
                {
                    return true;
                }
            }
            return false;
        }


        public static bool IsSameRow(this DataRow targetRow, DataRow destRow)
        {
            return IsSameRow(targetRow, destRow, false);
        }
        public static bool IsSameRow(this DataRow targetRow, DataRow destRow, bool isExistsColumnOnly)
        {
            for (int i = 0; i < targetRow.ItemArray.Length; i++)
            {
                //存在カラムのみチェックする場合で、比較先のカラムがあふれたら、その場で抜ける
                if (i >= destRow.ItemArray.Length)
                {
                    if (isExistsColumnOnly)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                }
                if (!(targetRow[i].ToString() == destRow[i].ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region DeleteRows

        public static System.Data.DataTable DeleteRows(this System.Data.DataTable target, System.Data.DataRow[] filter, string KeyName)
        {

            foreach (DataRow dr in filter)
            {
                foreach (DataRow drfilter in target.Select(string.Format("{0} = '{1}'", KeyName, dr[KeyName].ToString())))
                {
                    target.Rows.Remove(drfilter);
                }
            }
            target.AcceptChanges();
            return target;
        }

        #endregion

        #region DataRowCopy (対象のテーブルの1行をコピーして新規行を作成する)(+1)

        /// <summary>
        /// 対象のテーブルの1行をコピーして新規行を作成する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DataRow DataRowCopy(this DataTable target)
        {
            return DataRowCopy(target, 0);
        }

        /// <summary>
        /// 対象のテーブルの1行をコピーして新規行を作成する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DataRow DataRowCopy(this DataTable target, int position)
        {
            System.Data.DataRow dr;
            dr = target.NewRow();
            for (int i = 0; i < dr.ItemArray.Length; i++)
            {
                dr[i] = target.Rows[position][i];
            }
            return dr;
        }
        #endregion

        #region DataTableToTwoDString (DataTableを二次元配列にして返す)

        /// <summary>
        /// DataTableを二次元配列にして返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string[,] DataTableToTwoDSTring(this DataTable target)
        {
            string[,] data;
            data = new string[target.Rows.Count, target.Columns.Count];

            for (int i = 0; i < target.Rows.Count; i++)
            {
                for (int j = 0; j < target.Columns.Count; j++)
                {
                    data[i, j] = target.Rows[i][j].ToString();
                }
            }
            return data;
        }
        #endregion

        #region DataTableToTwoDObject (DataTableを二次元配列にして返す)

        /// <summary>
        /// DataTableを二次元配列にして返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static object[,] DataTableToTwoDObject(this DataTable target)
        {
            object[,] data;
            data = new object[target.Rows.Count, target.Columns.Count];

            for (int i = 0; i < target.Rows.Count; i++)
            {
                for (int j = 0; j < target.Columns.Count; j++)
                {
                    data[i, j] = target.Rows[i][j];
                }
            }
            return data;
        }
        #endregion

        #region DataTableToTwoDObject (DataTableを二次元配列にして返す)

        /// <summary>
        /// DataTableを二次元配列にして返す。
        /// Key:ColumnIndexとValue:ColumnNameを持ったHashTableで
        /// 引数のDataTableからどのカラムをどの位置に割り当てるか指定します。
        /// </summary>
        /// <param name="target"></param>
        /// <param name="colsCnt"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static object[,] DataTableToTwoDObject(this DataTable target, int colsCnt, Hashtable ht)
        {
            object[,] data;
            data = new object[target.Rows.Count, colsCnt];

            for (int i = 0; i < target.Rows.Count; i++)
            {
                foreach (DictionaryEntry de in ht)
                {
                    data[i, int.Parse(de.Key.ToString()) - 1] = target.Rows[i][de.Value.ToString()];
                }
            }
            return data;
        }

        #endregion

        #region DataTableToOneDString (DataTableを１次元配列にして返す) (+1)

        /// <summary>
        /// DataTableを１次元配列にして返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string[] DataTableToOneDString(this DataTable target)
        {
            return DataTableToOneDString(target, 0);
        }

        /// <summary>
        /// DataTableを１次元配列にして返す
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string[] DataTableToOneDString(this DataTable target, int position)
        {
            string[] data;
            data = new string[target.Rows.Count];

            for (int i = 0; i < target.Rows.Count; i++)
            {
                data[i] = target.Rows[i][position].ToString();
            }
            return data;
        }
        #endregion


    }
}
