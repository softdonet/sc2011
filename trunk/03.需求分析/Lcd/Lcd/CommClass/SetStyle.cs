using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Lcd.CommClass
{
   public static class SetStyle
    {
        public static Form frm { get; set; }
        public static void SetDynamicLabelStyle(Label lb, string fontFamily, float size,Color c)
        {
            lb.Font = new Font(fontFamily, size, FontStyle.Bold);
            lb.ForeColor = c;
        }
       public static Double dOpacity = 0;
       static Timer timerOpacity;
        public static void SetOpacityAdd(Form pfrm)
        {
            frm = pfrm;
            timerOpacity = myTimer.GetTimer(100, true);
            timerOpacity.Tick += new EventHandler(timerOpacityAdd_Tick);
        }

        static void timerOpacityAdd_Tick(object sender, EventArgs e)
        {
            if (dOpacity <= 1.0)
            {
                dOpacity += 0.1;
                frm.Opacity = dOpacity;
            }
            else
            {
                timerOpacity.Stop();
            }

        }

        public static void SetOpacityMinus(Form frm)
        {
            timerOpacity = myTimer.GetTimer(100, true);
            timerOpacity.Tick += new EventHandler(timerOpacityMinus_Tick);
        }

        static void timerOpacityMinus_Tick(object sender, EventArgs e)
        {
            if (dOpacity >= 0)
            {
                dOpacity -= 0.1;
                frm.Opacity = dOpacity;
            }
            else
            {
                timerOpacity.Stop();
            }

        }
    }
}
