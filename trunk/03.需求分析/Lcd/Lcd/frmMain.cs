using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;







namespace Lcd
{


    /// <summary>
    /// 液晶屏显示
    /// </summary>
    public partial class frmMain : Form
    {


        #region 构造函数

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.LoadViewDateTime();
        }






        #endregion


        #region 事件类型

        //键盘事件
        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            Keys keys = e.KeyCode;
            if (keys == Keys.Escape)
            {
                this.Close();
            }
            else if (keys == Keys.F10)
            {
            }
            else if (keys == Keys.F9)
            {

            }
        }

        //计算窗体
        private void frmMain_Resize(object sender, EventArgs e)
        {
            int height = this.Height / 6;
            this.panel0.Height =
                    this.panel1.Height =
                    this.panel2.Height =
                    this.panel3.Height =
                    this.panel4.Height =
                    this.panel5.Height =
                    this.panel_6.Height = height;

            int width = this.Width / 4;
            this.panel_12.Width =
                this.panel_16.Width =
                this.panel_21.Width =
                this.panel_20.Width = width;

            this.panel_13.Width =
                this.panel_17.Width =
                this.panel_23.Width =
                this.panel_22.Width = width;

            this.panel_14.Width =
                this.panel_18.Width =
                this.panel_25.Width =
                this.panel_24.Width = width;

            this.panel_15.Width =
                this.panel_19.Width =
                this.panel_27.Width =
                this.panel_26.Width = width;


        }


        #endregion


        #region 显示时钟

        private void LoadViewDateTime()
        {
            this.lblViewDateTime.Text = Convert.ToString(DateTime.Now.ToLocalTime());
            this.timerNow.Interval = 1000;
            this.timerNow.Enabled = true;
        }


        private void timerNow_Tick(object sender, EventArgs e)
        {
            this.lblViewDateTime.Text = Convert.ToString(DateTime.Now.ToLocalTime());
        }



        #endregion



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




    }
}
