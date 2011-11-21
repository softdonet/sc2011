using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lcd.CommClass
{
    public static class myTimer
    {
        public static Timer GetTimer(int millisecond, bool flag)
        {
            if (flag)
            {
                return new Timer() { Enabled = true, Interval = millisecond };

            }
            else
            {
                return new Timer() { Enabled = false, Interval = millisecond };

            }

        }

       

        public static string CurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string CurrentTimeNoSecond()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        }
    }
}
