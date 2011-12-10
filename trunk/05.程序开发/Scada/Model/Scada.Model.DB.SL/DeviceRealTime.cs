using System;
using System.Text;
namespace Scada.Model.DB.SL
{
    /// <summary>
    /// 类DeviceRealTime。
    /// </summary>
    public class DeviceRealTime
    {
        private Guid _id;
        private Guid _deviceid;
        private string _deviceno;
        private decimal? _temperature;
        private int? _electricity;
        private int? _signal;
        private decimal? _process4value;
        private decimal? _process5value;
        private int? _status;
        private DateTime? _updatetime;
        /// <summary>
        /// Guid
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 设备ID 关联设备表 
        /// </summary>
        public Guid DeviceID
        {
            set { _deviceid = value; }
            get { return _deviceid; }
        }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceNo
        {
            set { _deviceno = value; }
            get { return _deviceno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Temperature
        {
            set { _temperature = value; }
            get { return _temperature; }
        }
        /// <summary>
        /// 1,2,3,4 用图片表示
        /// </summary>
        public int? Electricity
        {
            set { _electricity = value; }
            get { return _electricity; }
        }
        /// <summary>
        /// 1,2,3,4 用图片表示
        /// </summary>
        public int? Signal
        {
            set { _signal = value; }
            get { return _signal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Process4Value
        {
            set { _process4value = value; }
            get { return _process4value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Process5Value
        {
            set { _process5value = value; }
            get { return _process5value; }
        }
        /// <summary>
        /// 1,2,3--离线 在线 告警
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 从设备上采集下来的时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
    }
}

