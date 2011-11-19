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
        delegate void SetTextHandle(string str, bool result);
        ModuleSettings moduleSettings;

        private MessageFuns recvmessagefuns;
        private SeriesMsg seriesMsg;

        ucError myucError = null;
        ucSuccess myucSuccess = null;
        ucGridView myucGridView = null;
        Timer timerShow = null;
        int code = 1;
        public frmMain()
        {
            InitializeComponent();

            recvmessagefuns = new MessageFuns();
            moduleSettings = ModuleConfig.GetSettings();
            seriesMsg = new SeriesMsg(moduleSettings);
            seriesMsg.OnUserDataReceived += new SeriesMsg.UserDataReceived(seriesMsg_OnUserDataReceived);

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

        /// <summary>
        /// 串口数据到达处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void seriesMsg_OnUserDataReceived(object sender, byte[] e)
        {
            recvmessagefuns.InputData(e);
            if (recvmessagefuns.Verify())
            {
                string strMsg = recvmessagefuns.GetBarCode();
                SetText(strMsg, true);
            }
            else
            {
                SetText("Error", false);
            }
        }

        /// <summary>
        /// 异步数据显示在界面
        /// </summary>
        /// <param name="str"></param>
        private void SetText(string str, bool result)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTextHandle(SetText), str, result);
            }
            else
            {
                if (result)
                {
                    ScanDataModel sdm = new ScanDataModel();
                    sdm.ScanTime = DateTime.Now.ToString();
                    sdm.BODYNO = str;
                    sdm.SEQ = code.ToString("0000");
                    code++;
                    myucGridView.GetNewData(sdm);
                    myucSuccess.lblScanNumber.Text = str;
                    ShowUc(myucSuccess);

                }
                else
                {
                    ShowUc(myucError);
                }

            }
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
            //测试GridView代码------------------------

            ScanDataModel sdm=new ScanDataModel();
            sdm.ScanTime =DateTime.Now.ToString();
            sdm.BODYNO ="barRedae";
            sdm.SEQ = code.ToString("0000");
            code++;
            myucGridView.GetNewData(sdm);
            //----------------------------------------
            myucSuccess.lblScanNumber.Text = "0001";
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




        private void frmMain_Load(object sender, EventArgs e)
        {
            ShowUc(myucGridView);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ScanDataModel sdm = new ScanDataModel();
            sdm.ScanTime = DateTime.Now.ToString();
            sdm.BODYNO = "00ewrwr01";
            sdm.SEQ = code.ToString("0000");
            code++;
            myucGridView.GetNewData(sdm);
            ShowUc(myucGridView);
        }

    }
}
