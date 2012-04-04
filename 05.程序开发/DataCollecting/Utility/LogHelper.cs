using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace Utility
{
    /// <summary>
    /// 日志帮助类
    /// yanghk at 2012-4-4
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 记录系统异常
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteExceptionLog(Exception ex)
        {
            string dir = Application.StartupPath + "\\logs";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string filePath = dir + "\\" + DateTime.Now.Date.ToShortDateString() + ".txt";
            FileStream fs1 = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs1);
            string info = "[" + DateTime.Now.ToString() + "]  " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine;
            sw.WriteLine(info);
            sw.Close();
            fs1.Close();
        }

        /// <summary>
        /// 记录普通运行日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteInformationLog(string msg)
        {
            string dir = Application.StartupPath + "\\logs";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string filePath = dir + "\\" + DateTime.Now.Date.ToShortDateString() + "-sys.txt";
            FileStream fs1 = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs1);
            sw.WriteLine(msg);
            sw.Close();
            fs1.Close();
        }
    }
}
