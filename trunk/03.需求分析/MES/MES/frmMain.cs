using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MES.CommClass;
using MES.UserControls;

namespace MES
{
    public partial class frmMain : Form
    {
        ucError myucError = null;
        ucSuccess myucSuccess = null;
        ucGridView myucGridView = null;
        Timer timerShow = null;
        public frmMain()
        {
            InitializeComponent();
            myucError = new ucError();
            myucSuccess = new ucSuccess();
            myucGridView = new ucGridView();
            //窗口标题
            this.Text = StaticStrings.GetFrmTitle();
            //系统启动时间
            lblScanTime.Text = myTimer.CurrentTime();
            //实时时间
            Timer timer = myTimer.GetTimer(1000, true);
            timer.Tick += new EventHandler(timer_Tick);
            //五秒钟窗体显示控制
            timerShow = myTimer.GetTimer(1000, true);
            timerShow.Tick += new EventHandler(timerShow_Tick);
        }

        int count = 0;
        void timerShow_Tick(object sender, EventArgs e)
        {
            count++;
            if (count > 5)
            {
                timerShow.Stop();
                count = 0;
                ShowUc(myucGridView);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = myTimer.CurrentTime();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            lblCenter.Location = new Point((panelTop.Width - lblCenter.Width) / 2, lblCenter.Height);
        }

        /// <summary>
        /// 读取成功时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {   
            ShowUc(myucSuccess);
        }

        /// <summary>
        /// 读取失败时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ShowUc(myucError);
        }
        /// <summary>
        /// 显示五秒钟的窗体
        /// </summary>
        /// <param name="ctl"></param>
        private void ShowUc(Control ctl)
        {
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            timerShow.Start();
        }
    }
}
