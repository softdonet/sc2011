using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataCollecting
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            this.chkWriteToDB.Checked = Comm.UpdateToDB;
        }

        private void lbtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
