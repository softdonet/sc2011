using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lcd
{
    public partial class frmColorSel : Form
    {


        public Color NewForeColor
        {
            set;
            get;
        }

        public Color NewBackGroudColor
        {
            get;
            set;
        }
        public string CurrentValue
        { 
            get;
            set;
        }

        public frmColorSel()
        {
            InitializeComponent();
            this.KeyPreview = true;
            if (CurrentValue==null)
            {
                lblCurrent.Visible = false;
                txtCurrentValue.Visible = false;
            }
            else
            {
                lblCurrent.Visible = true;
                txtCurrentValue.Visible = true;
            }

        }


        private void butOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            NewBackGroudColor = this.txtBackColor.BackColor;
            NewForeColor = this.txtForeColor.BackColor;
            int tempValue ;
            if (Int32.TryParse(this.txtCurrentValue.Text, out tempValue))
            {
                CurrentValue = this.txtCurrentValue.Text;
            }
            else
            {
                MessageBox.Show("请输入数字！");
                return;
            }
            
            this.Close();
        }

        private void txtForeColor_DoubleClick(object sender, EventArgs e)
        {
            TextBox tBox = sender as TextBox;
            this.colorDialog.ShowDialog();
            tBox.BackColor = this.colorDialog.Color;
        }

        private void butCannel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmColorSel_Load(object sender, EventArgs e)
        {
            this.txtBackColor.BackColor = NewBackGroudColor;
            this.txtForeColor.BackColor = NewForeColor;

        }

        private void frmColorSel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
