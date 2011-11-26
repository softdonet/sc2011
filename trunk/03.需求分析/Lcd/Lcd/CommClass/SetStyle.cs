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
        public static void SetDynamicLabelStyle(Label lb, string fontFamily, float size,Color c)
        {
            lb.Font = new Font(fontFamily, size, FontStyle.Bold);
            lb.ForeColor = c;
        }
    }
}
