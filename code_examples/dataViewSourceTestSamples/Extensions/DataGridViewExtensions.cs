using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace dataViewSourceTestSamples.Extensions
{
    public static class DataGridViewExtensions
    {
        #region GenerateColumnsFromDataTable
        
        /// <summary>
        /// DataTableから自動的にカラムを生成する
        /// </summary>
        /// <param name="dgv">対象のデータグリッド</param>
        /// <param name="dataTable">対象のデータテーブル</param>
        public static void GenerateColumnsFromDataTable(this DataGridView dgv , DataTable dataTable)
        {
            GenerateColumnsFromDataTable(dgv, dataTable, "{0}_columns_");
        }
        
        /// <summary>
        /// DataTableから自動的にカラムを生成する
        /// </summary>
        /// <param name="dgv">対象のデータグリッド</param>
        /// <param name="dataTable">対象のデータテーブル</param>
        /// <param name="columnSuffix">カラムの接頭文字(既定：gridView名_columns_)</param>
        public static void GenerateColumnsFromDataTable(this DataGridView dgv, DataTable dataTable, string columnSuffix)
        {
            GenerateColumnsFromDataTable(dgv, dataTable, columnSuffix, null);
        }

        /// <summary>
        /// DataTableから自動的にカラムを生成する
        /// </summary>
        /// <param name="dgv">対象のデータグリッド</param>
        /// <param name="dataTable">対象のデータテーブル</param>
        /// <param name="columnSuffix">カラムの接頭文字(既定：gridView名_columns_)</param>
        /// <param name="cellTemplate">セルテンプレート(既定：DataGridViewTextBoxCell)</param>
        public static void GenerateColumnsFromDataTable(this DataGridView dgv, DataTable dataTable, string columnSuffix, DataGridViewCell cellTemplate)
        {
            GenerateColumnsFromDataTable(dgv, dataTable, columnSuffix, cellTemplate, false);
        }

        /// <summary>
        /// DataTableから自動的にカラムを生成する
        /// </summary>
        /// <param name="dgv">対象のデータグリッド</param>
        /// <param name="dataTable">対象のデータテーブル</param>
        /// <param name="columnSuffix">カラムの接頭文字(既定：gridView名_columns_)</param>
        /// <param name="cellTemplate">セルテンプレート(既定：DataGridViewTextBoxCell)</param>
        /// <param name="clearColumn">既に存在するカラムをクリアするかどうかを指定します</param>
        public static void GenerateColumnsFromDataTable(this DataGridView dgv, DataTable dataTable, string columnSuffix, DataGridViewCell cellTemplate, bool clearColumn)
        {
            try
            {
                int index = 0;

                dgv.SuspendLayout();
                dgv.AutoGenerateColumns = false;
                if (clearColumn == false)
                {
                    dgv.Columns.Clear();
                }

                if (cellTemplate == null) { cellTemplate = new DataGridViewTextBoxCell(); }

                foreach (DataColumn dc in dataTable.Columns)
                {
                    DataGridViewColumn dvc = new DataGridViewColumn();
                    dvc.Name = string.Format("{0}{1}", columnSuffix, dc.ColumnName);
                    dvc.DataPropertyName = dc.ColumnName;
                    dvc.HeaderText = dc.ColumnName;
                    dvc.DisplayIndex = index;
                    dvc.CellTemplate = cellTemplate;
                    dgv.Columns.Add(dvc);
                    index++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dgv.ResumeLayout();
            }
        }

        #endregion

        #region GenerateColumnsFromDataView (実装はGenerateColumnsFromDataTable)

        /// <summary>
        /// DataViewから自動的にカラムを生成する
        /// </summary>
        /// <param name="dgv">対象のデータグリッド</param>
        /// <param name="dataView">対象のDataView</param>
        public static void GenerateColumnsFromDataView(this DataGridView dgv, DataView dataView)
        {
            GenerateColumnsFromDataView(dgv, dataView, string.Format("{0}_columns_",dgv.Name));
        }
        
        /// <summary>
        /// DataViewから自動的にカラムを生成する
        /// </summary>
        /// <param name="dgv">対象のデータグリッド</param>
        /// <param name="dataView">対象のDataView</param>
        /// <param name="columnSuffix">カラムの接頭文字(既定：gridView名_columns_)</param>
        public static void GenerateColumnsFromDataView(this DataGridView dgv, DataView dataView , string columnSuffix)
        {
            DataGridViewTextBoxCell dgtc = new DataGridViewTextBoxCell();
            GenerateColumnsFromDataView(dgv, dataView, columnSuffix, dgtc);
        }

        /// <summary>
        /// DataViewから自動的にカラムを生成する
        /// </summary>
        /// <param name="dgv">対象のデータグリッド</param>
        /// <param name="dataView">対象のDataView</param>
        /// <param name="columnSuffix">カラムの接頭文字(既定：gridView名_columns_)</param>
        /// <param name="cellTemplate">セルテンプレート(既定：DataGridViewTextBoxCell)</param>
        public static void GenerateColumnsFromDataView(this DataGridView dgv, DataView dataView, string columnSuffix, DataGridViewCell cellTemplate)
        {
            GenerateColumnsFromDataView(dgv, dataView, columnSuffix, cellTemplate, false);
        }

        /// <summary>
        /// DataViewから自動的にカラムを生成する
        /// </summary>
        /// <param name="dgv">対象のデータグリッド</param>
        /// <param name="dataView">対象のDataView</param>
        /// <param name="columnSuffix">カラムの接頭文字(既定：gridView名_columns_)</param>
        /// <param name="cellTemplate">セルテンプレート(既定：DataGridViewTextBoxCell)</param>
        /// <param name="clearColumn">既に存在するカラムをクリアするかどうかを指定します</param>
        public static void GenerateColumnsFromDataView(this DataGridView dgv, DataView dataView, string columnSuffix, DataGridViewCell cellTemplate, bool clearColumn)
        {
            GenerateColumnsFromDataTable(dgv, dataView.Table, columnSuffix, cellTemplate, clearColumn);
        }

        #endregion

    }
}
