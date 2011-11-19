using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MES.CommClass
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

       //public static void GetTimer(long millisecond)
       //{
       //    Timer timer = new Timer() { Enabled = true, Interval = 1000 };
       //    timer.Tick += new EventHandler(timer_Tick);
       //}

       //static void timer_Tick(object sender, EventArgs e)
       //{
       //    DateTime.Now.ToString();
       //}

       public static string CurrentTime()
       {
           return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
       }
    }
}
