using System;
using System.Text;
namespace Scada.Model.DB.SL
{
    /// <summary>
    /// ¿‡DeviceLog°£
    /// </summary>
    public class DeviceLog
    {
        private Guid _id;
        private Guid _deviceid;
        private string _deviceno;
        private string _logpeople;
        private DateTime? _logtime;
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
        /// Guid
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
        public string LogPeople
        {
            set { _logpeople = value; }
            get { return _logpeople; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LogTIme
        {
            set { _logtime = value; }
            get { return _logtime; }
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

