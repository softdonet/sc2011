using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting
{
    public static class Comm
    {
        public static bool UpdateToDB = true;
    }

    /// <summary>
    /// 日志项对象
    /// </summary>
    public class LogItem
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time;
        /// <summary>
        /// 事件
        /// </summary>
        public string Event;
        /// <summary>
        /// 说明
        /// </summary>
        public string Memo;

    }
}
