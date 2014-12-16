using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Common.WinForms.Utility.SplitContainer
{
    public class SplitContainerSyncController
    {

        private System.Windows.Forms.SplitContainer spc1;
        private System.Windows.Forms.SplitContainer spc2;

        public SplitContainerSyncController(System.Windows.Forms.SplitContainer container1, System.Windows.Forms.SplitContainer container2)
        {
            this.spc1 = container1;
            this.spc2 = container2;
            this.spc1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(splitContainer1_SplitterMoved);
            this.spc2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(splitContainer2_SplitterMoved);
        }
            private void syncDistance(System.Windows.Forms.SplitContainer source, System.Windows.Forms.SplitContainer dest)
        {
            dest.SplitterDistance = source.SplitterDistance;
        }


        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            syncDistance(spc1, spc2);
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            syncDistance(spc2, spc1);
        }


    }
}
