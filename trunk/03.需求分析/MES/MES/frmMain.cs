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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Text = StaticStrings.GetFrmTitle();

            Timer timer = myTimer.GetTimer(1000, true);//new Timer() { Enabled = true, Interval = 1000 };
            timer.Tick += new EventHandler(timer_Tick);

            #region 申请读取数据，并绑定
            //Timer timerReadData = myTimer.GetTimer(500);
            //timerReadData.Tick += new EventHandler(timerReadData_Tick);
            dataGrdView.DataSource = new ScanDatas().GetScanData();
            #endregion
            SetGridViewStyle();
        }

        private void SetGridViewStyle()
        {
            dataGrdView.ReadOnly = true;
            dataGrdView.EnableHeadersVisualStyles = false;
            dataGrdView.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;
            dataGrdView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGrdView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Verdana", 25F, FontStyle.Bold);
            dataGrdView.AllowUserToResizeColumns = true;
            dataGrdView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrdView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ColorDialog cd = new ColorDialog();
            dataGrdView.RowsDefaultCellStyle.Font = new Font("Verdana", 20F, FontStyle.Bold | FontStyle.Bold);


            this.dataGrdView.AllowUserToResizeRows = true;
            this.dataGrdView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;// .DisplayedCellsExceptHeaders;
            //this.dataGrdView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader;
          //  this.dataGrdView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }
        void timerReadData_Tick(object sender, EventArgs e)
        {
           
            dataGrdView.DataSource = new ScanDatas().GetScanData();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = myTimer.CurrentTime();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            lblCenter.Location = new Point((panelTop.Width -lblCenter.Width)/2, lblCenter.Height);
        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData==Keys.F5)
        //    {
                
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}
        public bool gettrue;
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                this.Visible = false;
                frmSuccess fs = new frmSuccess();
              //  fs.delReturnClosedInfo = new frmSuccess.DelReturnClosedInfo(GetBoolValue);

                if (gettrue)
                {
                    this.Visible = true;
                    fs.Close();
                }
            }
        }
        
        private void GetBoolValue(bool bflag)
        {
            gettrue = bflag;
            if (gettrue)
            {
                this.Visible = true;
                gettrue = false;
            }
        }
        private void btnF1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            frmSuccess fs = new frmSuccess();
            fs.Show();
            fs.delReturnClosedInfo = new frmSuccess.DelReturnClosedInfo(GetBoolValue);// new frmSuccess.DelReturnClosedInfo(GetBoolValue);

            if (gettrue)
            {
                this.Visible = true;
                fs.Close();
            }
        }
        
       
    }
}
