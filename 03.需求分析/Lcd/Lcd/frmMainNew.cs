using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lcd.CommClass;
using System.Xml.Linq;

namespace Lcd
{
    public partial class frmMainNew : Form
    {

        #region 公共变量
        public static int iLabel21 = 320;
        public static int iLabel25 = 245;
        ModuleSettings setting;
        frmWelcome frmWel = null;
        #endregion

        #region 构造函数
        public frmMainNew()
        {
            setting = ModuleConfig.GetSettings();
            InitializeComponent();

            frmWel = new frmWelcome(setting);
            frmWel.Visible = false;
           
            Timer timerChange = myTimer.GetTimer(1000, true);
            timerChange.Tick += new EventHandler(timerChange_Tick);

            //实时时间
            Timer timer = myTimer.GetTimer(1000, true);
            timer.Tick += new EventHandler(timer_Tick);
            SetLabelFont();

        }

        bool welComeFromIsShow = false;
        int count = 0;
        /// <summary>
        /// 窗体切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timerChange_Tick(object sender, EventArgs e)
        {
            count++;
            if (frmWel.Visible == true)
            {
                if (count > setting.WelComeFromTime)
                {
                    //CommClass.SetStyle.SetOpacityAdd(this);
                    frmWel.Visible = false;
                    this.Visible = true;
                    count = 0;
                }
            }
            else
            {
                if (count > setting.MainFormTime)
                {
                    //this.Visible = false;
                    if (!welComeFromIsShow)
                    {
                        frmWel.Opacity = 0;
                        frmWel.Show();
                        welComeFromIsShow = true;
                    }
                    CommClass.SetStyle.SetOpacityAdd(frmWel);
                    frmWel.Visible = true;
                    count = 0;
                }
            }
            SetLocation();
        }

        private void frmMainNew_Load(object sender, EventArgs e)
        {
            SetLblText();
            SetLocation();
            SetColor();
        }
        #endregion

        #region 事件和算法

        //动态改变Cell的宽和高
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

        private void panel_DoubleClick(object sender, EventArgs e)
        {
            Color bColor, fColor;
            bColor = (sender as Panel).BackColor;
            fColor = (sender as Panel).Controls[0].ForeColor;
            string strValue = string.Empty; ;
            bool IsFlag = false;
         //  Control []c= (sender as Panel).Controls.Find("label13", true);
           foreach (Control item in (sender as Panel).Controls)
           {
               if (item is Label)
               {
                   if (item.Name == "label13" || item.Name == "label15" ||
                       item.Name == "label17" || item.Name == "label19" ||
                       item.Name == "label21" || item.Name == "label23" || item.Name == "label25")
                   {
                       strValue = item.Text;
                       IsFlag = true;
                   }
                   else
                   {
                       IsFlag = false;
                   }
                   
               }
               
           }
            frmColorSel frmShow = new frmColorSel(IsFlag);
            frmShow.NewForeColor = fColor;
            frmShow.NewBackGroudColor = bColor;
            frmShow.CurrentValue = strValue;

            if (frmShow.ShowDialog() != System.Windows.Forms.DialogResult.OK) { return; }

            (sender as Panel).BackColor = frmShow.NewBackGroudColor;
            (sender as Panel).Controls[0].ForeColor = frmShow.NewForeColor;

            if (IsFlag)
            {
                (sender as Panel).Controls[0].Text = frmShow.CurrentValue;
            }
        }

        private void frmMainNew_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (MessageBox.Show("确定要退出系统吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        this.Close();
                    }
                    break;
                case Keys.F1:
                    iLabel21 += 1;
                    label21.Text = iLabel21.ToString();

                    break;
                case Keys.F2:
                    if (iLabel21 > 0)
                    {
                        iLabel21 -= 1;
                        label21.Text = iLabel21.ToString();
                    }
                    break;
                case Keys.F3:
                    iLabel25 += 1;
                    label25.Text = iLabel25.ToString();

                    break;
                case Keys.F4:
                    if (iLabel25 > 0)
                    {
                        iLabel25 -= 1;
                        label25.Text = iLabel25.ToString();
                    }
                    break;
                case Keys.H:
                    frmSettings fs = new frmSettings(setting);
                    fs.ShowDialog();
                    break;
                //default:
            }


        }

        #region 算法


        void timer_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = myTimer.CurrentTimeNoSecond();
        }
        //初始化Label的值
        private void SetLblText()
        {
            lblCurrentTime.Text = myTimer.CurrentTimeNoSecond();
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

            label12.Text = "运 转 率";
            label13.Text = "200";
            label14.Text = "JPH";
            label15.Text = "250";
            label16.Text = "计划数量";
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
        /// <summary>
        /// 定位
        /// </summary>
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
            label14.Location = new Point((panel14.Width - label14.Width) / 2, (panel14.Height - label14.Height) / 2);
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
            SetDynamicLabelStyle(lblCurrentTime, "Times New Roman", 70);
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
            SetLabelStyleNumber(label13);
            SetDynamicLabelStyle(label14, "Times New Roman", 70);//大号字体
            SetLabelStyleNumber(label15);
            SetLabelStyleSunTi(label16);
            SetLabelStyleNumber(label17);
            SetLabelStyleSunTi(label18);
            SetLabelStyleNumber(label19);
            SetLabelStyleSunTi(label20);
            SetLabelStyleNumber(label21);
            SetLabelStyleSunTi(label22);
            SetLabelStyleNumber(label23);
            SetLabelStyleSunTi(label24);
            SetLabelStyleNumber(label25);
            SetLabelStyleSunTi(label26);
            SetLabelStyleNumber(label27);

        }

        /// <summary>
        /// 设置label字体样式（新罗马）
        /// </summary>
        /// <param name="lbl"></param>
        private void SetLabelStyle(Label lb)
        {
            lb.Font = new Font("Times New Roman", 60F, FontStyle.Bold | FontStyle.Bold);
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
        /// <summary>
        /// 设置label字体样式（数字）
        /// </summary>
        /// <param name="lbl"></param>
        private void SetLabelStyleNumber(Label lb)
        {
            lb.Font = new Font("Times New Roman", setting.NumFontSize, FontStyle.Bold | FontStyle.Bold);
            lb.ForeColor = Color.White;
        }

        private void SetDynamicLabelStyle(Label lb, string fontFamily, float size)
        {
            lb.Font = new Font(fontFamily, size, FontStyle.Bold);
        }

        #endregion

        private void frmMainNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveColor();
        }

        /// <summary>
        /// 保存颜色
        /// </summary>
        void SaveColor()
        {
            setting.FColor1 = panel1.Controls[0].ForeColor.ToArgb();
            setting.FColor2 = panel2.Controls[0].ForeColor.ToArgb();
            setting.FColor3 = panel3.Controls[0].ForeColor.ToArgb();
            setting.FColor4 = panel4.Controls[0].ForeColor.ToArgb();
            setting.FColor5 = panel5.Controls[0].ForeColor.ToArgb();
            setting.FColor6 = panel6.Controls[0].ForeColor.ToArgb();
            setting.FColor7 = panel7.Controls[0].ForeColor.ToArgb();
            setting.FColor8 = panel8.Controls[0].ForeColor.ToArgb();
            setting.FColor9 = panel9.Controls[0].ForeColor.ToArgb();
            setting.FColor10 = panel10.Controls[0].ForeColor.ToArgb();
            setting.FColor11 = panel11.Controls[0].ForeColor.ToArgb();
            setting.FColor12 = panel12.Controls[0].ForeColor.ToArgb();
            setting.FColor13 = panel13.Controls[0].ForeColor.ToArgb();
            setting.FColor14 = panel14.Controls[0].ForeColor.ToArgb();
            setting.FColor15 = panel15.Controls[0].ForeColor.ToArgb();
            setting.FColor16 = panel16.Controls[0].ForeColor.ToArgb();
            setting.FColor17 = panel17.Controls[0].ForeColor.ToArgb();
            setting.FColor18 = panel18.Controls[0].ForeColor.ToArgb();
            setting.FColor19 = panel19.Controls[0].ForeColor.ToArgb();
            setting.FColor20 = panel20.Controls[0].ForeColor.ToArgb();
            setting.FColor21 = panel21.Controls[0].ForeColor.ToArgb();
            setting.FColor22 = panel22.Controls[0].ForeColor.ToArgb();
            setting.FColor23 = panel23.Controls[0].ForeColor.ToArgb();
            setting.FColor24 = panel24.Controls[0].ForeColor.ToArgb();
            setting.FColor25 = panel25.Controls[0].ForeColor.ToArgb();
            setting.FColor26 = panel26.Controls[0].ForeColor.ToArgb();
            setting.FColor27 = panel27.Controls[0].ForeColor.ToArgb();

            setting.BColor1 = panel1.BackColor.ToArgb();
            setting.BColor2 = panel2.BackColor.ToArgb();
            setting.BColor3 = panel3.BackColor.ToArgb();
            setting.BColor4 = panel4.BackColor.ToArgb();
            setting.BColor5 = panel5.BackColor.ToArgb();
            setting.BColor6 = panel6.BackColor.ToArgb();
            setting.BColor7 = panel7.BackColor.ToArgb();
            setting.BColor8 = panel8.BackColor.ToArgb();
            setting.BColor9 = panel9.BackColor.ToArgb();
            setting.BColor10 = panel10.BackColor.ToArgb();
            setting.BColor11 = panel11.BackColor.ToArgb();
            setting.BColor12 = panel12.BackColor.ToArgb();
            setting.BColor13 = panel13.BackColor.ToArgb();
            setting.BColor14 = panel14.BackColor.ToArgb();
            setting.BColor15 = panel15.BackColor.ToArgb();
            setting.BColor16 = panel16.BackColor.ToArgb();
            setting.BColor17 = panel17.BackColor.ToArgb();
            setting.BColor18 = panel18.BackColor.ToArgb();
            setting.BColor19 = panel19.BackColor.ToArgb();
            setting.BColor20 = panel20.BackColor.ToArgb();
            setting.BColor21 = panel21.BackColor.ToArgb();
            setting.BColor22 = panel22.BackColor.ToArgb();
            setting.BColor23 = panel23.BackColor.ToArgb();
            setting.BColor24 = panel24.BackColor.ToArgb();
            setting.BColor25 = panel25.BackColor.ToArgb();
            setting.BColor26 = panel26.BackColor.ToArgb();
            setting.BColor27 = panel27.BackColor.ToArgb();
            ModuleConfig.SaveSettings(setting);

        }

        /// <summary>
        /// 加载颜色
        /// </summary>
        void SetColor()
        {
            panel1.Controls[0].ForeColor = Color.FromArgb(setting.FColor1);
            panel2.Controls[0].ForeColor = Color.FromArgb(setting.FColor2);
            panel3.Controls[0].ForeColor = Color.FromArgb(setting.FColor3);
            panel4.Controls[0].ForeColor = Color.FromArgb(setting.FColor4);
            panel5.Controls[0].ForeColor = Color.FromArgb(setting.FColor5);
            panel6.Controls[0].ForeColor = Color.FromArgb(setting.FColor6);
            panel7.Controls[0].ForeColor = Color.FromArgb(setting.FColor7);
            panel8.Controls[0].ForeColor = Color.FromArgb(setting.FColor8);
            panel9.Controls[0].ForeColor = Color.FromArgb(setting.FColor9);
            panel10.Controls[0].ForeColor = Color.FromArgb(setting.FColor10);
            panel11.Controls[0].ForeColor = Color.FromArgb(setting.FColor11);
            panel12.Controls[0].ForeColor = Color.FromArgb(setting.FColor12);
            panel13.Controls[0].ForeColor = Color.FromArgb(setting.FColor13);
            panel14.Controls[0].ForeColor = Color.FromArgb(setting.FColor14);
            panel15.Controls[0].ForeColor = Color.FromArgb(setting.FColor15);
            panel16.Controls[0].ForeColor = Color.FromArgb(setting.FColor16);
            panel17.Controls[0].ForeColor = Color.FromArgb(setting.FColor17);
            panel18.Controls[0].ForeColor = Color.FromArgb(setting.FColor18);
            panel19.Controls[0].ForeColor = Color.FromArgb(setting.FColor19);
            panel20.Controls[0].ForeColor = Color.FromArgb(setting.FColor20);
            panel21.Controls[0].ForeColor = Color.FromArgb(setting.FColor21);
            panel22.Controls[0].ForeColor = Color.FromArgb(setting.FColor22);
            panel23.Controls[0].ForeColor = Color.FromArgb(setting.FColor23);
            panel24.Controls[0].ForeColor = Color.FromArgb(setting.FColor24);
            panel25.Controls[0].ForeColor = Color.FromArgb(setting.FColor25);
            panel26.Controls[0].ForeColor = Color.FromArgb(setting.FColor26);
            panel27.Controls[0].ForeColor = Color.FromArgb(setting.FColor27);

            panel1.BackColor = Color.FromArgb(setting.BColor1);
            panel2.BackColor = Color.FromArgb(setting.BColor2);
            panel3.BackColor = Color.FromArgb(setting.BColor3);
            panel4.BackColor = Color.FromArgb(setting.BColor4);
            panel5.BackColor = Color.FromArgb(setting.BColor5);
            panel6.BackColor = Color.FromArgb(setting.BColor6);
            panel7.BackColor = Color.FromArgb(setting.BColor7);
            panel8.BackColor = Color.FromArgb(setting.BColor8);
            panel9.BackColor = Color.FromArgb(setting.BColor9);
            panel10.BackColor = Color.FromArgb(setting.BColor10);
            panel11.BackColor = Color.FromArgb(setting.BColor11);
            panel12.BackColor = Color.FromArgb(setting.BColor12);
            panel13.BackColor = Color.FromArgb(setting.BColor13);
            panel14.BackColor = Color.FromArgb(setting.BColor14);
            panel15.BackColor = Color.FromArgb(setting.BColor15);
            panel16.BackColor = Color.FromArgb(setting.BColor16);
            panel17.BackColor = Color.FromArgb(setting.BColor17);
            panel18.BackColor = Color.FromArgb(setting.BColor18);
            panel19.BackColor = Color.FromArgb(setting.BColor19);
            panel20.BackColor = Color.FromArgb(setting.BColor20);
            panel21.BackColor = Color.FromArgb(setting.BColor21);
            panel22.BackColor = Color.FromArgb(setting.BColor22);
            panel23.BackColor = Color.FromArgb(setting.BColor23);
            panel24.BackColor = Color.FromArgb(setting.BColor24);
            panel25.BackColor = Color.FromArgb(setting.BColor25);
            panel26.BackColor = Color.FromArgb(setting.BColor26);
            panel27.BackColor = Color.FromArgb(setting.BColor27);
        }
        #endregion
    }
}
