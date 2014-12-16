using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Common.WinForms.Utility.DataGridView
{
    public class DataGridViewSyncScrollController
    {
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.DataGridView dgv2;

        public DataGridViewSyncScrollController(System.Windows.Forms.DataGridView grid1, System.Windows.Forms.DataGridView grid2)
        {
            this.dgv1 = grid1;
            this.dgv2 = grid2;
            this.dgv1.Scroll += new ScrollEventHandler(grid1_Scroll);
            this.dgv2.Scroll += new ScrollEventHandler(grid2_Scroll);
        }
        private void grid1_Scroll(object sender, ScrollEventArgs e)
        {
            syncOffset(dgv1, dgv2);
        }
        private void grid2_Scroll(object sender, ScrollEventArgs e)
        {
            syncOffset(dgv2, dgv1);
        }
        private void syncOffset(System.Windows.Forms.DataGridView source, System.Windows.Forms.DataGridView dest)
        {
            dest.HorizontalScrollingOffset = source.HorizontalScrollingOffset;
            dest.FirstDisplayedScrollingRowIndex = source.FirstDisplayedScrollingRowIndex;
        }

    }
}
