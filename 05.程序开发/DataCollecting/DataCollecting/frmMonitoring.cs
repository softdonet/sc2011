using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataCollecting.NetServer;
using Utility;
using NetData;

namespace DataCollecting
{
    public partial class frmMonitoring : Form
    {
        ConmmandAnalysis commandAnalysis = null;
        public frmMonitoring(ConmmandAnalysis commandAnalysis1)
        {
            commandAnalysis = commandAnalysis1;
            commandAnalysis.OnReceiveMsg += new ConmmandAnalysis.OnReceiveHandle(commandAnalysis_OnReceiveMsg);
            InitializeComponent();
        }

        void commandAnalysis_OnReceiveMsg(string str)
        {
            SetText(str);
        }

        /// <summary>
        /// 异步数据显示在界面
        /// </summary>
        /// <param name="str"></param>
        delegate void SetTextHandle(string str);
        private void SetText(string str)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTextHandle(SetText), str);
            }
            else
            {
                this.textBox1.Text += str + Environment.NewLine;
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
            }
        }
    }
}
