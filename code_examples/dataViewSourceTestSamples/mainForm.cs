using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dataViewSourceTestSamples.Extensions;

namespace dataViewSourceTestSamples
{
    public partial class mainForm : Form
    {
        private DataSets.MasterDataSet.ZIPCODEDataTable _currentDataTable;
        private DataView _currentDataViewForDisplay;    //この画面に表示するためのもの
        private DataView _currentDataViewForPrivate;    //サブ画面に表示するためのもの (DataTableは同じでSortが異なるもの)
        private int _currentPrefCode = 1;               //分割読込用

        private string connectionString = @"Data Source=(LocalDB)\v11.0;Initial Catalog={0};Integrated Security=True";
        public mainForm()
        {
            InitializeComponent();
        }

        #region Events

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.executeFormInitialize();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            this.executeRead(false);
            this.executeSort();

            this.executeDataBind();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            this.executeSort();
        }

        private void btnReadBlock_Click(object sender, EventArgs e)
        {
            this.executeRead(true);
            _currentPrefCode++;
            this.executeSort();
            this.executeDataBind();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.executeClear();
        }

        private void btnShowSubDialog_Click(object sender, EventArgs e)
        {
            subDialogForm f = new subDialogForm();
            f.ShowDialog(_currentDataViewForPrivate);

            if (f.IsSelected)
            {
                DataRowView drv = f.SelectedItem;
                string key1 = drv["ZIPCODE"].ToString();
                this.bs.Position = this.bs.Find("ZIPCODE", key1);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            string currentPK = string.Empty;
            outputLog(string.Format("CellClick:Row={0},Col={1}", e.RowIndex, e.ColumnIndex));
            DataRowView[] dr;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) { return; }
            currentPK = this.dgvMain.SelectedRows[0].Cells[0].Value.ToString();
            dr = _currentDataViewForPrivate.FindRows(new string[] { currentPK });
            if (dr != null && dr.Length > 0)
            {
                outputLog(string.Format("CellClick:Row={0},SelectedValue = {1} {2}", e.RowIndex, dr[0]["CITY_NAME"], dr[0]["TOWN_NAME"]));
            }
            else
            {
                outputLog(string.Format("CellClick:Row={0},該当なし", e.RowIndex));
            }
        }


        #endregion

        #region Private Method

        private string getConnectionString()
        {
            return string.Format(connectionString, getCurrentDBName("App_Data", "ZipCode.mdf"));
        }
        private string getCurrentDBName(string relateFolder, string databaseName)
        {
            return System.IO.Path.Combine(Application.StartupPath, relateFolder).ToString() + databaseName;
        }

        private void executeFormInitialize()
        {
            this.dgvMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void executeRead(bool isAppend)
        {
            //計測
            if (_currentDataTable == null)
            {
                _currentDataTable = new DataSets.MasterDataSet.ZIPCODEDataTable();
            }
            DataSets.MasterDataSetTableAdapters.ZIPCODETableAdapter adpt = new DataSets.MasterDataSetTableAdapters.ZIPCODETableAdapter();

            if (isAppend == false)
            {
                adpt.Fill(_currentDataTable);
            }
            else
            {
                outputLog("Fill Start");
                adpt.FillOfPrefecture(_currentDataTable, _currentPrefCode.ToString(), isAppend);
                outputLog("Fill End");
            }
            outputLog(String.Format("Rows Count = {0}", _currentDataTable.Rows.Count.ToString()));

        }

        private void executeSort()
        {
            if (_currentDataViewForDisplay == null)
            {
                _currentDataViewForDisplay = _currentDataTable.DefaultView;
            }
            if (_currentDataViewForPrivate == null)
            {
                _currentDataViewForPrivate = new DataView(_currentDataTable);
            }

            outputLog("Sort Start(ForDisplay)");
            _currentDataViewForDisplay.Sort = "CITY_NAME_KANA";
            outputLog("Sort End(ForDisplay)");

            outputLog("Sort Start(ForPrivate)");
            _currentDataViewForPrivate.Sort = "XJIS_CITY_CODE";
            outputLog("Sort End(ForPrivate)");
        }

        private void executeDataBind()
        {
            //bindingSource経由に変更
            //outputLog("DataSource Bind Start");
            //this.dataGridView1.DataSource = _currentDataViewForDisplay;
            //outputLog("DataSource Bind End");
            if (this.dgvMain.Columns.Count == 0)
            {
                this.dgvMain.GenerateColumnsFromDataTable(_currentDataTable);
            }

            outputLog("BinsingSource Bind Start");
            this.bs.DataSource = _currentDataViewForDisplay;
            this.dgvMain.DataSource = this.bs;
            outputLog("BinsingSource Bind End");



        }

        private void executeClear()
        {
            this._currentDataTable = null;
            this._currentPrefCode = 1;
            this._currentDataViewForDisplay = null;
            this.dgvMain.DataSource = null;
            this.txtLog.Text = string.Empty;
        }

        private void outputLog(string message)
        {
            string logValue = string.Format("time:{0}.{1} : {2}", System.DateTime.Now.ToString("HH:mm:ss"), System.DateTime.Now.Millisecond, message);
            Console.WriteLine(logValue);
            this.txtLog.Text += logValue + System.Environment.NewLine;
            this.txtLog.SelectionStart = this.txtLog.Text.Length;
            this.txtLog.ScrollToCaret();
        }

        #endregion

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            outputLog("bindingNavigatorMoveNextItem_Click");
            getCurrentRow();
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            outputLog("bindingNavigatorMovePreviousItem_Click");
            getCurrentRow();
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            outputLog("bindingNavigatorMoveLastItem_Click");
            getCurrentRow();
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            outputLog("bindingNavigatorMoveFirstItem_Click");
            getCurrentRow();
        }

        private void getCurrentRow()
        {
            DataRowView drv = (DataRowView)this.bs.Current;
            outputLog(string.Format("selected value : ZIPCODE = {0}", drv["ZIPCODE"].ToString()));
        }


    }



}
