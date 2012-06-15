using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.ViewModel;

namespace Scada.Client.VM.Modules.BingMaps
{
    public class PushPinDeviceViewModel
    {
        private string nodeValue;
        public string NodeValue
        {
            get { return nodeValue; }
            set
            {
                nodeValue = value;
            }
        }

        private Guid nodeKey;
        public Guid NodeKey
        {
            get { return nodeKey; }
            set
            {
                nodeKey = value;
            }
        }

        private Int32 nodeType;
        public Int32 NodeType
        {
            get { return nodeType; }
            set
            {
                nodeType = value;
            }
        }

        private string installPlace;
        public string InstallPlace
        {
            get { return installPlace; }
            set
            {
                installPlace = value;
            }
        }
        private decimal? temperature;
        public decimal? Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
            }
        }
        private int? electricity;
        public int? Electricity
        {
            get { return electricity; }
            set
            {
                electricity = value;
            }
        }
        private int? signal;
        public int? Signal
        {
            get { return signal; }
            set
            {
                signal = value;
            }
        }
        private int? status;
        public int? Status
        {
            get { return status; }
            set
            {
                status = value;
            }
        }
        private DateTime? updateTime;
        public DateTime? UpdateTime
        {
            get { return updateTime; }
            set
            {
                updateTime = value;
            }
        }
        private float? longitude;
        public float? Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
            }
        }
        private float? dimensionality;
        public float? Dimensionality
        {
            get { return dimensionality; }
            set
            {
                dimensionality = value;
            }
        }



  
        //高度
        public float? High { get; set; }
        //维护人员
        public string MaintenanceName { get; set; }
        //联系方式
        public string Mobile { get; set; }
        //备注
        public string Comment { get; set; }

    }
}
