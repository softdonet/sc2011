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
    public partial class frmWelcome : Form
    {
        ModuleSettings setting = null;
        public frmWelcome(ModuleSettings set)
        {
            InitializeComponent();
            setting = set;
            lblRowOneText.Text = setting.RowOneWelComeText;
            lblRowTwoText.Text = setting.RowTwoWelComeText;
            CommClass.SetStyle.SetDynamicLabelStyle(lblRowOneText, setting.FontFamily, setting.FontSize, Color.Red);
            CommClass.SetStyle.SetDynamicLabelStyle(lblRowTwoText, setting.FontFamily, setting.FontSize, Color.Red);
            this.panel1.BackgroundImage = Image.FromFile(setting.WelComeBackgroundImage);
        }

        private void frmWelcome_Load(object sender, EventArgs e)
        {
            SetLocation();
        }

        /// <summary>
        /// 定位
        /// </summary>
        private void SetLocation()
        {
            lblRowOneText.Location = new Point((panel1.Width - lblRowOneText.Width) / 2, int.Parse(setting.WelComeTextTop));
            lblRowTwoText.Location = new Point((panel1.Width - lblRowTwoText.Width) / 2, int.Parse(setting.WelComeTextTop) + int.Parse(setting.WelComeTextRowledge) + lblRowOneText.Height);
        }
    }
}
