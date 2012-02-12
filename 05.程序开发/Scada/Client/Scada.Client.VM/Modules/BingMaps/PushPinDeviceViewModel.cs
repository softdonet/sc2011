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
    public class PushPinDeviceViewModel : NotificationObject
    {
        private string nodeValue;
        public string NodeValue
        {
            get { return nodeValue; }
            set
            {
                nodeValue = value;
                this.RaisePropertyChanged("NodeValue");
            }
        }

        private Guid nodeKey;
        public Guid NodeKey
        {
            get { return nodeKey; }
            set
            {
                nodeKey = value;
                this.RaisePropertyChanged("NodeKey");
            }
        }

        private Int32 nodeType;
        public Int32 NodeType
        {
            get { return nodeType; }
            set
            {
                nodeType = value;
                this.RaisePropertyChanged("NodeType");

            }
        }

        private string installPlace;
        public string InstallPlace
        {
            get { return installPlace; }
            set
            {
                installPlace = value;
                this.RaisePropertyChanged("InstallPlace");

            }
        }
        private decimal? temperature;
        public decimal? Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                this.RaisePropertyChanged("Temperature");

            }
        }
        private int? electricity;
        public int? Electricity
        {
            get { return electricity; }
            set
            {
                electricity = value;
                this.RaisePropertyChanged("Electricity");

            }
        }
        private int? signal;
        public int? Signal
        {
            get { return signal; }
            set
            {
                signal = value;
                this.RaisePropertyChanged("Signal");
            }
        }
        private int? status;
        public int? Status
        {
            get { return status; }
            set
            {
                status = value;
                this.RaisePropertyChanged("Status");
            }
        }
        private DateTime? updateTime;
        public DateTime? UpdateTime
        {
            get { return updateTime; }
            set
            {
                updateTime = value;
                this.RaisePropertyChanged("UpdateTime");
            }
        }
        private float? longitude;
        public float? Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;

                this.RaisePropertyChanged("Longitude");
            }
        }
        private float? dimensionality;
        public float? Dimensionality
        {
            get { return dimensionality; }
            set
            {
                dimensionality = value;
                this.RaisePropertyChanged("Dimensionality");
            }
        }


    }
}
