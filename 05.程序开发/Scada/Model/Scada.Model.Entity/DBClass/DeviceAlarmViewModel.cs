using System;


namespace Scada.Model.Entity
{


    public class DeviceAlarmViewModel : NotifyPropertyChangedObject
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
        /// ID
        /// </summary>
        public Guid ID
        {
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
            get { return _id; }
        }

        /// <summary>
        /// DeviceID
        /// </summary>
        public Guid DeviceID
        {
            set
            {
                _deviceid = value;
                OnPropertyChanged("DeviceID");
            }
            get { return _deviceid; }
        }

        /// <summary>
        /// DeviceNo
        /// </summary>
        public string DeviceNo
        {
            set
            {
                _deviceno = value;
                OnPropertyChanged("DeviceNo");
            }
            get { return _deviceno; }
        }

        /// <summary>
        /// EventType
        /// </summary>
        public int? EventType
        {
            set
            {
                _eventtype = value;
                OnPropertyChanged("EventType");
            }
            get { return _eventtype; }
        }

        /// <summary>
        /// EventLevel
        /// </summary>
        public int? EventLevel
        {
            set
            {
                _eventlevel = value;
                OnPropertyChanged("EventLevel");
            }
            get { return _eventlevel; }
        }

        /// <summary>
        /// StartTime
        /// </summary>
        public DateTime? StartTime
        {
            set
            {
                _starttime = value;
                OnPropertyChanged("StartTime");
            }
            get { return _starttime; }
        }

        /// <summary>
        /// EndTime
        /// </summary>
        public DateTime? EndTime
        {
            set
            {
                _endtime = value;
                OnPropertyChanged("EndTime");
            }
            get { return _endtime; }
        }

        /// <summary>
        /// ConfirmTime
        /// </summary>
        public DateTime? ConfirmTime
        {
            set
            {
                _confirmtime = value;
                OnPropertyChanged("ConfirmTime");
            }
            get { return _confirmtime; }
        }

        /// <summary>
        /// DealStatus
        /// </summary>
        public string DealStatus
        {
            set
            {
                _dealstatus = value;
                OnPropertyChanged("DealStatus");
            }
            get { return _dealstatus; }
        }

        /// <summary>
        /// DealPeople
        /// </summary>
        public string DealPeople
        {
            set
            {
                _dealpeople = value;
                OnPropertyChanged("DealPeople");
            }
            get { return _dealpeople; }
        }

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment
        {
            set
            {
                _comment = value;
                OnPropertyChanged("Comment");
            }
            get { return _comment; }
        }

    }
}
