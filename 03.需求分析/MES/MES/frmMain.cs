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
        int code = 0;
        public frmMain()
        {
            InitializeComponent();
            this.KeyPreview = true;

            recvmessagefuns = new MessageFuns();
            moduleSettings = ModuleConfig.GetSettings();
            seriesMsg = new SeriesMsg(moduleSettings);
            seriesMsg.OnUserDataReceived += new SeriesMsg.UserDataReceived(seriesMsg_OnUserDataReceived);

            myucError = new ucError();
            myucSuccess = new ucSuccess();
            myucGridView = new ucGridView(this);
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
            timerShow.Stop();
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
                //暂时不用
                //SetText("Error", false);
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
                    if (sdm != null && myucGridView.queue.SingleOrDefault(e => e.BODYNO == sdm.BODYNO) != null)
                    {
                        code++;
                        myucGridView.InsertNewData(sdm);
                        myucSuccess.lblScanNumber.Text = str;
                        ShowUc(myucSuccess, true);
                    }
                }
                else
                {
                    ShowUc(myucError, true);
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
                ShowUc(myucGridView, false);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = myTimer.CurrentTime();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            lblCenter.Location = new Point((panelTop.Width - lblCenter.Width) / 2, (panelTop.Height-lblCenter.Height)/2);
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
            code += 1;
            sdm.BODYNO = code.ToString("0000000000");//"barRedae";
            sdm.SEQ = code.ToString("0000");
            
            myucGridView.InsertNewData(sdm);
            //----------------------------------------
            myucSuccess.lblScanNumber.Text = sdm.SEQ;// "0001";
            ShowUc(myucSuccess, true);
        }

        /// <summary>
        /// 读取失败时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ShowUc(myucError, true);
        }

        /// <summary>
        /// 显示五秒钟的窗体
        /// </summary>
        /// <param name="ctl">要呈现的窗体</param>
        /// <param name="RequireWait">是否需要停留几秒再返回</param>
        private void ShowUc(Control ctl, bool RequireWait)
        {
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            if (RequireWait)
            {
                timerShow.Start();
            }
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            ShowUc(myucGridView, false);
        }


        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        if (MessageBox.Show("确定要退出系统吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    break;
                case Keys.F1:
                    {
                        frmSettings fs = new frmSettings();
                        fs.ShowDialog();
                    }
                    break;
                //default:
            }

        }

    }
}
