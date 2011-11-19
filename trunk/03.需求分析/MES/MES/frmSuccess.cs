using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MES.CommClass;


namespace MES
{
    public partial class frmSuccess : Form
    {
        public bool ReturnFlag = false;
        public delegate void DelReturnClosedInfo(bool bflag);

       public DelReturnClosedInfo delReturnClosedInfo;
       Timer timer;
        public int timerCount = 0;
        public frmSuccess()
        {
            InitializeComponent();
            this.Text = StaticStrings.GetFrmTitle();

            timer = myTimer.GetTimer(1000,true);//new Timer() { Enabled = true, Interval = 1000 };
            timer.Tick += new EventHandler(timer_Tick);
        }

        //private bool CheckTimerCount()
        //{
        //    if (timerCount > 6)
        //    {
        //        this.Visible = false;
        //        ReturnFlag = true;
        //        if (delReturnClosedInfo != null)
        //        {
        //            delReturnClosedInfo = new DelReturnClosedInfo(ReturnValue);
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        void timer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = myTimer.CurrentTime();
            timerCount++;
            if (timerCount > 6)
            {
                timer.Stop();
                delReturnClosedInfo(true); //= new DelReturnClosedInfo(ReturnValue);
                this.Close();
               // timer.Tick -= new EventHandler(timer_Tick);
                
            }
        }

        private void frmSuccess_SizeChanged(object sender, EventArgs e)
        {
            lblCenter.Location = new Point((panelTop.Width - lblCenter.Width) / 2, lblCenter.Height);
        }
    }
}
