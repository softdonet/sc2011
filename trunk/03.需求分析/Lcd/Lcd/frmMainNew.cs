using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lcd.CommClass;

namespace Lcd
{
    public partial class frmMainNew : Form
    {
        public frmMainNew()
        {
            InitializeComponent();
            //实时时间
            Timer timer = myTimer.GetTimer(1000, true);
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void frmMainNew_Load(object sender, EventArgs e)
        {
            SetLblText();
            SetLocation();
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


        void timer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = myTimer.CurrentTimeNoSecond();
        }

        private void SetLblText()
        {
            label2.Text = "T1";
            label3.Text = "T2";
            label4.Text = "C1";
            label5.Text = "CM";
            label6.Text = "C2";
            label7.Text = "F1";
            label8.Text = "F2";
            label9.Text = "OK";
            label10.Text = "DR";
            label11.Text = "CTS";

            label12.Text = "运转率";
            label13.Text = "CTS";
            label14.Text = "JPH";
            label15.Text = "CTS";
            label16.Text = "现在目标";
            label17.Text = "CTS";
            label18.Text = "PBS在线";
            label19.Text = "CTS";
            label20.Text = "投入数量";
            label21.Text = "CTS";
            label22.Text = "CTS在线";
            label23.Text = "CTS";
            label24.Text = "入库数量";
            label25.Text = "CTS";
            label26.Text = "停线时间";
            label27.Text = "CTS";

        }

        private void SetLocation()
        {
            lblCurrentTime.Location = new Point((panel1.Width - lblCurrentTime.Width) / 2, (panel1.Height - lblCurrentTime.Height) / 2);
            label2.Location = new Point((panel2.Width - label2.Width) / 2, (panel2.Height - label2.Height) / 2);
            label3.Location = new Point((panel3.Width - label3.Width) / 2, (panel3.Height - label3.Height) / 2);
            label4.Location = new Point((panel4.Width - label4.Width) / 2, (panel4.Height - label4.Height) / 2);
            label5.Location = new Point((panel5.Width - label5.Width) / 2, (panel5.Height - label5.Height) / 2);
            label6.Location = new Point((panel6.Width - label6.Width) / 2, (panel2.Height - label6.Height) / 2);
            label7.Location = new Point((panel7.Width - label7.Width) / 2, (panel2.Height - label7.Height) / 2);

            label8.Location = new Point((panel8.Width - label8.Width) / 2, (panel2.Height - label8.Height) / 2);
            label9.Location = new Point((panel9.Width - label9.Width) / 2, (panel2.Height - label9.Height) / 2);
            label10.Location = new Point((panel10.Width - label10.Width) / 2, (panel10.Height - label10.Height) / 2);
            label11.Location = new Point((panel11.Width - label11.Width) / 2, (panel11.Height - label11.Height) / 2);
            label12.Location = new Point((panel12.Width - label12.Width) / 2, (panel2.Height - label12.Height) / 2);
            label13.Location = new Point((panel13.Width - label13.Width) / 2, (panel3.Height - label13.Height) / 2);
            label14.Location = new Point((panel14.Width - label14.Width) / 2, (panel14.Height - label4.Height) / 2);
            label15.Location = new Point((panel15.Width - label15.Width) / 2, (panel5.Height - label15.Height) / 2);

            label16.Location = new Point((panel16.Width - label16.Width) / 2, (panel16.Height - label16.Height) / 2);
            label17.Location = new Point((panel17.Width - label17.Width) / 2, (panel17.Height - label17.Height) / 2);
            label18.Location = new Point((panel18.Width - label18.Width) / 2, (panel18.Height - label18.Height) / 2);
            label19.Location = new Point((panel19.Width - label19.Width) / 2, (panel19.Height - label19.Height) / 2);
            label20.Location = new Point((panel20.Width - label20.Width) / 2, (panel20.Height - label20.Height) / 2);

            label21.Location = new Point((panel21.Width - label21.Width) / 2, (panel21.Height - label21.Height) / 2);
            label22.Location = new Point((panel22.Width - label22.Width) / 2, (panel22.Height - label22.Height) / 2);
            label23.Location = new Point((panel23.Width - label23.Width) / 2, (panel23.Height - label23.Height) / 2);
            label24.Location = new Point((panel24.Width - label24.Width) / 2, (panel24.Height - label24.Height) / 2);
            label25.Location = new Point((panel25.Width - label25.Width) / 2, (panel25.Height - label25.Height) / 2);
            label26.Location = new Point((panel26.Width - label26.Width) / 2, (panel26.Height - label26.Height) / 2);
            label27.Location = new Point((panel27.Width - label27.Width) / 2, (panel27.Height - label27.Height) / 2);

            
            

        }
       

    }
}
