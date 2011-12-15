using System;
using System.Text;
using System.Collections.Generic;


namespace Scada.Model.DB.SL
{


    /// <summary>
    /// ��DeviceInfo��
    /// </summary>
    public class DeviceInfo
    {

        private Guid _id;
        private string _deviceno;
        private string _hardtype;
        private DateTime? _productdate;
        private string _devicemac;
        private string _simno;
        private Guid _manageareaid;
        private string _installplace;
        private decimal? _longitude;
        private decimal? _dimensionality;
        private decimal? _high;
        private string _comment;
        private string _connectpoint;
        private string _username;
        private string _password;
        private string _coordinate;
        private int? _connecttype;
        private string _maindns;
        private string _seconddns;
        private string _centerip;
        private string _domain;
        private int? _port;
        private int? _collectfreq;
        private int? _reportinterval;
        private decimal? _alarmtop;
        private decimal? _alarmdown;
        private decimal? _windage;
        private string _version;
        private int? _timetype;
        private string _lcdscreendisplaytype;
        private int? _useurgencybutton;
        private int? _process1enable;
        private decimal? _process1highervalue;
        private int? _process1highvalue;
        private decimal? _process1lowvalue;
        private decimal? _process1lowervalue;
        private decimal? _process1ratevalue;
        private int? _process2enable;
        private decimal? _process2highervalue;
        private int? _process2highvalue;
        private decimal? _process2lowvalue;
        private decimal? _process2lowervalue;
        private decimal? _process2ratevalue;
        private decimal? _process3enable;
        private decimal? _process3highervalue;
        private int? _process3highvalue;
        private decimal? _process3lowvalue;
        private decimal? _process3lowervalue;
        private decimal? _process3ratevalue;
        private decimal? _process4enable;
        private decimal? _process4highervalue;
        private int? _process4highvalue;
        private decimal? _process4lowvalue;
        private decimal? _process4lowervalue;
        private decimal? _process4ratevalue;
        private decimal? _process5enable;
        private decimal? _process5highervalue;
        private int? _process5highvalue;
        private decimal? _process5lowvalue;
        private decimal? _process5lowervalue;
        private decimal? _process5ratevalue;


        //�豸����ά����Ա��Ϣ
        private List<Guid> _deviceMainValue = new List<Guid>();

        //�豸�Զ���ʱ��
        private List<DateTime> _deviceCustomTime = new List<DateTime>();


        /// <summary>
        /// 
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
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
        public string HardType
        {
            set { _hardtype = value; }
            get { return _hardtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ProductDate
        {
            set { _productdate = value; }
            get { return _productdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceMAC
        {
            set { _devicemac = value; }
            get { return _devicemac; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SIMNo
        {
            set { _simno = value; }
            get { return _simno; }
        }
        /// <summary>
        /// �������ID, �����豸����
        /// </summary>
        public Guid ManageAreaID
        {
            set { _manageareaid = value; }
            get { return _manageareaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InstallPlace
        {
            set { _installplace = value; }
            get { return _installplace; }
        }
        /// <summary>
        /// ��ǰ���꾭��
        /// </summary>
        public decimal? Longitude
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// ��ǰ����ά��
        /// </summary>
        public decimal? Dimensionality
        {
            set { _dimensionality = value; }
            get { return _dimensionality; }
        }
        /// <summary>
        /// ��ǰ����߶�
        /// </summary>
        public decimal? High
        {
            set { _high = value; }
            get { return _high; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ConnectPoint
        {
            set { _connectpoint = value; }
            get { return _connectpoint; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Coordinate
        {
            set { _coordinate = value; }
            get { return _coordinate; }
        }
        /// <summary>
        /// �̶�IP , ����
        /// </summary>
        public int? ConnectType
        {
            set { _connecttype = value; }
            get { return _connecttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MainDNS
        {
            set { _maindns = value; }
            get { return _maindns; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SecondDNS
        {
            set { _seconddns = value; }
            get { return _seconddns; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CenterIP
        {
            set { _centerip = value; }
            get { return _centerip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Domain
        {
            set { _domain = value; }
            get { return _domain; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? port
        {
            set { _port = value; }
            get { return _port; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CollectFreq
        {
            set { _collectfreq = value; }
            get { return _collectfreq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReportInterval
        {
            set { _reportinterval = value; }
            get { return _reportinterval; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AlarmTop
        {
            set { _alarmtop = value; }
            get { return _alarmtop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AlarmDown
        {
            set { _alarmdown = value; }
            get { return _alarmdown; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Windage
        {
            set { _windage = value; }
            get { return _windage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Version
        {
            set { _version = value; }
            get { return _version; }
        }
        /// <summary>
        /// ����ʱ��ɼ��� ����ʱ���ϱ�������ʱ��ɼ����ϱ� 
        /// </summary>
        public int? TimeType
        {
            set { _timetype = value; }
            get { return _timetype; }
        }

        /// <summary>
        /// ������ʾ��������ʾ������Ԥ��������ʾ
        /// </summary>
        public string LCDScreenDisplayType
        {
            set { _lcdscreendisplaytype = value; }
            get { return _lcdscreendisplaytype; }
        }
        /// <summary>
        /// ��/��
        /// </summary>
        public int? UseUrgencyButton
        {
            set { _useurgencybutton = value; }
            get { return _useurgencybutton; }
        }
        /// <summary>
        /// ����ֵ1����������Ч
        /// </summary>
        public int? Process1Enable
        {
            set { _process1enable = value; }
            get { return _process1enable; }
        }
        /// <summary>
        /// ����ֵ1�߸߱���ֵ
        /// </summary>
        public decimal? Process1HigherValue
        {
            set { _process1highervalue = value; }
            get { return _process1highervalue; }
        }
        /// <summary>
        /// ����ֵ1�߱���ֵ
        /// </summary>
        public int? Process1HighValue
        {
            set { _process1highvalue = value; }
            get { return _process1highvalue; }
        }
        /// <summary>
        /// ����ֵ1�ͱ���ֵ
        /// </summary>
        public decimal? Process1LowValue
        {
            set { _process1lowvalue = value; }
            get { return _process1lowvalue; }
        }
        /// <summary>
        /// ����ֵ1�͵ͱ���ֵ
        /// </summary>
        public decimal? Process1LowerValue
        {
            set { _process1lowervalue = value; }
            get { return _process1lowervalue; }
        }
        /// <summary>
        /// ����ֵ1���ʱ���ֵ
        /// </summary>
        public decimal? Process1RateValue
        {
            set { _process1ratevalue = value; }
            get { return _process1ratevalue; }
        }
        /// <summary>
        /// ����ֵ2����������Ч
        /// </summary>
        public int? Process2Enable
        {
            set { _process2enable = value; }
            get { return _process2enable; }
        }
        /// <summary>
        /// ����ֵ2�߸߱���ֵ
        /// </summary>
        public decimal? Process2HigherValue
        {
            set { _process2highervalue = value; }
            get { return _process2highervalue; }
        }
        /// <summary>
        /// ����ֵ2�߱���ֵ
        /// </summary>
        public int? Process2HighValue
        {
            set { _process2highvalue = value; }
            get { return _process2highvalue; }
        }
        /// <summary>
        /// ����ֵ2�ͱ���ֵ
        /// </summary>
        public decimal? Process2LowValue
        {
            set { _process2lowvalue = value; }
            get { return _process2lowvalue; }
        }
        /// <summary>
        /// ����ֵ2�͵ͱ���ֵ
        /// </summary>
        public decimal? Process2LowerValue
        {
            set { _process2lowervalue = value; }
            get { return _process2lowervalue; }
        }
        /// <summary>
        /// ����ֵ2���ʱ���ֵ
        /// </summary>
        public decimal? Process2RateValue
        {
            set { _process2ratevalue = value; }
            get { return _process2ratevalue; }
        }
        /// <summary>
        /// ����ֵ3����������Ч
        /// </summary>
        public decimal? Process3Enable
        {
            set { _process3enable = value; }
            get { return _process3enable; }
        }
        /// <summary>
        /// ����ֵ3�߸߱���ֵ
        /// </summary>
        public decimal? Process3HigherValue
        {
            set { _process3highervalue = value; }
            get { return _process3highervalue; }
        }
        /// <summary>
        /// ����ֵ3�߱���ֵ
        /// </summary>
        public int? Process3HighValue
        {
            set { _process3highvalue = value; }
            get { return _process3highvalue; }
        }
        /// <summary>
        /// ����ֵ3�ͱ���ֵ
        /// </summary>
        public decimal? Process3LowValue
        {
            set { _process3lowvalue = value; }
            get { return _process3lowvalue; }
        }
        /// <summary>
        /// ����ֵ3�͵ͱ���ֵ
        /// </summary>
        public decimal? Process3LowerValue
        {
            set { _process3lowervalue = value; }
            get { return _process3lowervalue; }
        }
        /// <summary>
        /// ����ֵ3���ʱ���ֵ
        /// </summary>
        public decimal? Process3RateValue
        {
            set { _process3ratevalue = value; }
            get { return _process3ratevalue; }
        }
        /// <summary>
        /// ����ֵ4����������Ч
        /// </summary>
        public decimal? Process4Enable
        {
            set { _process4enable = value; }
            get { return _process4enable; }
        }
        /// <summary>
        /// ����ֵ4�߸߱���ֵ
        /// </summary>
        public decimal? Process4HigherValue
        {
            set { _process4highervalue = value; }
            get { return _process4highervalue; }
        }
        /// <summary>
        /// ����ֵ4�߱���ֵ
        /// </summary>
        public int? Process4HighValue
        {
            set { _process4highvalue = value; }
            get { return _process4highvalue; }
        }
        /// <summary>
        /// ����ֵ4�ͱ���ֵ
        /// </summary>
        public decimal? Process4LowValue
        {
            set { _process4lowvalue = value; }
            get { return _process4lowvalue; }
        }
        /// <summary>
        /// ����ֵ4�͵ͱ���ֵ
        /// </summary>
        public decimal? Process4LowerValue
        {
            set { _process4lowervalue = value; }
            get { return _process4lowervalue; }
        }
        /// <summary>
        /// ����ֵ4���ʱ���ֵ
        /// </summary>
        public decimal? Process4RateValue
        {
            set { _process4ratevalue = value; }
            get { return _process4ratevalue; }
        }
        /// <summary>
        /// ����ֵ5����������Ч
        /// </summary>
        public decimal? Process5Enable
        {
            set { _process5enable = value; }
            get { return _process5enable; }
        }
        /// <summary>
        /// ����ֵ5�߸߱���ֵ
        /// </summary>
        public decimal? Process5HigherValue
        {
            set { _process5highervalue = value; }
            get { return _process5highervalue; }
        }
        /// <summary>
        /// ����ֵ5�߱���ֵ
        /// </summary>
        public int? Process5HighValue
        {
            set { _process5highvalue = value; }
            get { return _process5highvalue; }
        }
        /// <summary>
        /// ����ֵ5�ͱ���ֵ
        /// </summary>
        public decimal? Process5LowValue
        {
            set { _process5lowvalue = value; }
            get { return _process5lowvalue; }
        }
        /// <summary>
        /// ����ֵ5�͵ͱ���ֵ
        /// </summary>
        public decimal? Process5LowerValue
        {
            set { _process5lowervalue = value; }
            get { return _process5lowervalue; }
        }
        /// <summary>
        /// ����ֵ5���ʱ���ֵ
        /// </summary>
        public decimal? Process5RateValue
        {
            set { _process5ratevalue = value; }
            get { return _process5ratevalue; }
        }

        public List<Guid> DeviceMainValue
        {
            set { _deviceMainValue = value; }
            get { return _deviceMainValue; }
        }

        public List<DateTime> DeviceCustomTime
        {
            set { _deviceCustomTime = value; }
            get { return _deviceCustomTime; }
        }

    }
}

