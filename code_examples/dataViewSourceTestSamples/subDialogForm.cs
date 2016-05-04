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
    public partial class subDialogForm : Form
    {
        public subDialogForm()
        {
            InitializeComponent();
        }
        public virtual void ShowDialog(DataView dv)
        {
            this.dataGridView1.GenerateColumnsFromDataView(dv);

            this.bs.DataSource = dv;
            this.dataGridView1.DataSource = this.bs;
            this.ShowDialog();
        }
        public bool IsCancel { get; set; }
        public bool IsSelected { get; set; }
        public DataRowView SelectedItem { get; set; }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            clearItems();
            if (bs.Current == null) { return; }
            this.SelectedItem = (DataRowView)bs.Current;
            this.IsSelected = true;
            this.Close();
            return;
        }

        private void clearItems()
        {
            this.IsCancel = false;
            this.IsSelected = false;
            this.SelectedItem = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearItems();
            this.IsCancel = true;
            this.Close();
            return;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            executeFilter();
        }
        private void executeFilter()
        {
            string qryBase = "CITY_NAME LIKE '%{0}%' OR TOWN_NAME LIKE '%{0}%' OR TOWN_NAME_KANA LIKE '%{0}%'";
            this.bs.RemoveFilter();
            if (this.txtFilter.Text.Length > 0)
            {
                this.bs.Filter = string.Format(qryBase, this.txtFilter.Text);
            }

        }

    }


}
