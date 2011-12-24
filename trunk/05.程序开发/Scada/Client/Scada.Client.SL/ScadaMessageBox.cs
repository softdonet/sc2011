using System;
using System.Windows;



namespace Scada.Client.SL
{

    public class ScadaMessageBox
    {

        /// <summary>
        /// 显示确认框
        /// </summary>
        /// <param name="messageBoxText">内容</param>
        /// <param name="caption">标题</param>
        /// <returns></returns>
        public static MessageBoxResult ShowOKCancelMessage(string messageBoxText, string caption)
        {
            return MessageBox.Show(messageBoxText, caption, MessageBoxButton.OKCancel);
        }

        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="messageBoxText">内容</param>
        /// <param name="caption">标题</param>
        public static void ShowWarnMessage(string messageBoxText, string caption)
        {
            MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK);
        }

    }

}
