using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.Common
{
    /// <summary>
    /// 实时数据块实体
    /// </summary>
    public class RealTimeDataBlock
    {
        /// <summary>
        /// 数据序号
        /// </summary>
        private byte blockNo;
        public byte BlockNo
        {
            get { return blockNo; }
            set { blockNo = value; }
        }

        /// <summary>
        /// 时间戳
        /// </summary>
        private DateTime sateTimeMark;
        public DateTime SateTimeMark
        {
            get { return sateTimeMark; }
            set { sateTimeMark = value; }
        }

        /// <summary>
        /// 温度1
        /// </summary>
        private decimal? temperature1;
        public decimal? Temperature1
        {
            get { return temperature1; }
            set { temperature1 = value; }
        }
        /// <summary>
        /// 温度2
        /// </summary>
        private decimal? temperature2;
        public decimal? Temperature2
        {
            get { return temperature2; }
            set { temperature2 = value; }
        }
        /// <summary>
        /// 温度3
        /// </summary>
        private decimal? temperature3;
        public decimal? Temperature3
        {
            get { return temperature3; }
            set { temperature3 = value; }
        }
        /// <summary>
        /// 温度4
        /// </summary>
        private decimal? temperature4;
        public decimal? Temperature4
        {
            get { return temperature4; }
            set { temperature4 = value; }
        }
       
        /// <summary>
        /// 温度5
        /// </summary>
        private decimal? temperature5;
        public decimal? Temperature5
        {
            get { return temperature5; }
            set { temperature5 = value; }
        }

        /// <summary>
        /// 湿度
        /// </summary>
        private decimal? humidity;
        public decimal? Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }
      

        /// <summary>
        /// 电量
        /// </summary>
        private decimal? electric;
        public decimal? Electric
        {
            get { return electric; }
            set { electric = value; }
        }
       

        /// <summary>
        /// 信号
        /// </summary>
        private decimal? signal;
        public decimal? Signal
        {
            get { return signal; }
            set { signal = value; }
        }
    }
}
