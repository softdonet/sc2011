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
    public partial class frmMainNew : Form
    {
        public frmMainNew()
        {
            InitializeComponent();
        }

        private void frmMainNew_SizeChanged(object sender, EventArgs e)
        {
            int width =this.Width;
            int height = this.Height;
            int iRowCount = tableLayoutPanel1.RowCount;
            int iColumeCount = tableLayoutPanel1.ColumnCount;
            for (int iRow = 0; iRow < iRowCount; iRow++)
            {
                this.tableLayoutPanel1.RowStyles[iRow].Height = height / iRowCount;
            }
            for (int iCol = 0; iCol < iColumeCount; iCol++)
            {
                this.tableLayoutPanel1.ColumnStyles[iCol].Width = width / iColumeCount;
                
            }
           
        }      
       
    }
}
