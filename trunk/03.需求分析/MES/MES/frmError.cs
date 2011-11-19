using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MES.CommClass;

namespace MES
{
    public partial class frmError : Form
    {
        public frmError()
        {
            InitializeComponent();
            this.Text = StaticStrings.GetFrmTitle();

            Timer timer = new Timer() { Enabled = true, Interval = 1000 };
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = myTimer.CurrentTime();
        }

        private void frmError_SizeChanged(object sender, EventArgs e)
        {
            lblCenter.Location = new Point((panelTop.Width - lblCenter.Width) / 2, lblCenter.Height);
        }
    }
}
