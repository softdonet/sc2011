using System;
using System.Collections.Generic;
namespace Scada.Model.Entity
{
    /// <summary>
    /// ¿‡DeviceAlarm°£
    /// </summary>
    public class DeviceAlarm
    {
        private Guid _id;
        private Guid _deviceid;
        private string _deviceno;
        private int? _eventtype;
        private int? _eventlevel;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private DateTime? _confirmtime;
        private string _dealstatus;
        private string _dealpeople;
        private string _comment;
        /// <summary>
        /// Guid
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid DeviceID
        {
            set { _deviceid = value; }
            get { return _deviceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceNo
        {
            set { _deviceno = value; }
            get { return _deviceno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EventType
        {
            set { _eventtype = value; }
            get { return _eventtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EventLevel
        {
            set { _eventlevel = value; }
            get { return _eventlevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ConfirmTime
        {
            set { _confirmtime = value; }
            get { return _confirmtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DealStatus
        {
            set { _dealstatus = value; }
            get { return _dealstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DealPeople
        {
            set { _dealpeople = value; }
            get { return _dealpeople; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
    }
}

