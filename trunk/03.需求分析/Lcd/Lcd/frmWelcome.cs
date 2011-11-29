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
        bool toLeft = false;


        public frmWelcome(string welcomeText)
        {
            InitializeComponent();
            this.scrollingText1.ScrollText = "                " + welcomeText;
            //this.lblCurrent.Text = welcomeText;
            CommClass.SetStyle.SetDynamicLabelStyle(lblCurrent, "楷体", 45F, Color.Red);
            Timer timerFrequee = myTimer.GetTimer(50, true);
            timerFrequee.Tick += new EventHandler(timerFrequee_Tick);
            timerFrequee.Enabled = false;
        }

        void timerFrequee_Tick(object sender, EventArgs e)
        {
            int Ox = this.Width;
            int Oy = this.Height;
            int x = lblCurrent.Location.X;
            int y = lblCurrent.Location.Y;
            Frequee(lblCurrent, Ox, Oy, x, y);
        }

        private void Frequee(Label lblCurrent, int Ox, int Oy, int x, int y)
        {
            if (toLeft == false)
            {
                if (x <= (Ox - lblCurrent.Width))
                {
                    x += 1;
                    lblCurrent.Location = new Point(x, y);
                }
                else
                {
                    toLeft = true;
                }
            }
            else
            {
                if (toLeft)
                {
                    if (x >= 0)
                    {
                        x -= 1;
                        lblCurrent.Location = new Point(x, y);
                    }
                    else
                    {
                        toLeft = false;
                    }
                }
            }
        }

    }
}
