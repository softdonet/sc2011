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
            SetLabelFont();
            SetTime();
        }

        private void frmMainNew_Load(object sender, EventArgs e)
        {
            SetLblText();
            SetLocation();
        }

        private void frmMainNew_SizeChanged(object sender, EventArgs e)
        {
            int width = this.Width;
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
            lblCurrentTime.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm");
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
            label13.Text = "200";
            label14.Text = "JPH";
            label15.Text = "250";
            label16.Text = "现在目标";
            label17.Text = "250";
            label18.Text = "PBS在线";
            label19.Text = "300";
            label20.Text = "投入数量";
            label21.Text = "320";
            label22.Text = "CTS在线";
            label23.Text = "230";
            label24.Text = "入库数量";
            label25.Text = "245";
            label26.Text = "停线时间";
            label27.Text = "15:39";

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


        /// <summary>
        /// 设置label字体
        /// </summary>
        private void SetLabelFont()
        {
            SetLabelStyle(lblCurrentTime);
            SetLabelStyle(label2);
            SetLabelStyle(label3);
            SetLabelStyle(label4);
            SetLabelStyle(label5);
            SetLabelStyle(label6);
            SetLabelStyle(label7);
            SetLabelStyle(label8);
            SetLabelStyle(label9);
            SetLabelStyle(label10);
            SetLabelStyle(label11);
            SetLabelStyleSunTi(label12);
            SetLabelStyle(label13);
            SetLabelStyle(label14);
            SetLabelStyle(label15);
            SetLabelStyleSunTi(label16);
            SetLabelStyle(label17);
            SetLabelStyleSunTi(label18);
            SetLabelStyle(label19);
            SetLabelStyleSunTi(label20);
            SetLabelStyle(label21);
            SetLabelStyleSunTi(label22);
            SetLabelStyle(label23);
            SetLabelStyleSunTi(label24);
            SetLabelStyle(label25);
            SetLabelStyleSunTi(label26);
            SetLabelStyle(label27);

        }
        /// <summary>
        /// 设置label字体样式（新罗马）
        /// </summary>
        /// <param name="lbl"></param>
        private void SetLabelStyle(Label lb)
        {
            lb.Font = new Font("Times New Roman", 40F, FontStyle.Bold | FontStyle.Bold);
            lb.ForeColor = Color.White;
        }

        /// <summary>
        /// 设置label字体样式（宋体）
        /// </summary>
        /// <param name="lbl"></param>
        private void SetLabelStyleSunTi(Label lb)
        {
            lb.Font = new Font("宋体", 40F, FontStyle.Bold | FontStyle.Bold);
            lb.ForeColor = Color.White;
        }

        Timer time1 = new Timer();
        private void SetTime()
        {
            time1.Interval = 5000;
            time1.Tick += new EventHandler(time1_Tick);
            time1.Enabled = true;
            time1.Start();
        }

        void time1_Tick(object sender, EventArgs e)
        {
            Random rd = new Random();
            int i = rd.Next(200, 400);
            this.label13.Text = i.ToString();
            i = rd.Next(200, 400);
            this.label17.Text = i.ToString();
            i = rd.Next(200, 400);
            this.label21.Text = i.ToString();
            i = rd.Next(200, 400);
            this.label25.Text = i.ToString();
        }

       

        private void panel_DoubleClick(object sender, EventArgs e)
        {
            Color bColor, fColor;
            bColor = (sender as Panel).BackColor;
            fColor = (sender as Panel).Controls[0].ForeColor;

            frmColorSel frmShow = new frmColorSel();
            frmShow.NewForeColor = fColor;
            frmShow.NewBackGroudColor = bColor;
            if (frmShow.ShowDialog() != System.Windows.Forms.DialogResult.OK) { return; }

            (sender as Panel).BackColor = frmShow.NewBackGroudColor;
            (sender as Panel).Controls[0].ForeColor = frmShow.NewForeColor;
        }

        private void frmMainNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("确定要退出系统吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    this.Close();
                }
            }
             
        } 
    }
}
