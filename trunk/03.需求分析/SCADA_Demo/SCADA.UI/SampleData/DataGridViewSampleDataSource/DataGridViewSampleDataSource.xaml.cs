﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.DataGridViewSampleDataSource
{
	using System; 

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class DataGridViewSampleDataSource { }
#else

	public class DataGridViewSampleDataSource : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public DataGridViewSampleDataSource()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/SCADA.UI;component/SampleData/DataGridViewSampleDataSource/DataGridViewSampleDataSource.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private ItemCollection _Collection = new ItemCollection();

		public ItemCollection Collection
		{
			get
			{
				return this._Collection;
			}
		}
	}

	public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
	{ 
	}

	public class Item : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Number = string.Empty;

		public string Number
		{
			get
			{
				return this._Number;
			}

			set
			{
				if (this._Number != value)
				{
					this._Number = value;
					this.OnPropertyChanged("Number");
				}
			}
		}

		private string _InstallPlace = string.Empty;

		public string InstallPlace
		{
			get
			{
				return this._InstallPlace;
			}

			set
			{
				if (this._InstallPlace != value)
				{
					this._InstallPlace = value;
					this.OnPropertyChanged("InstallPlace");
				}
			}
		}

		private string _Pressure = string.Empty;

		public string Pressure
		{
			get
			{
				return this._Pressure;
			}

			set
			{
				if (this._Pressure != value)
				{
					this._Pressure = value;
					this.OnPropertyChanged("Pressure");
				}
			}
		}

		private string _Device = string.Empty;

		public string Device
		{
			get
			{
				return this._Device;
			}

			set
			{
				if (this._Device != value)
				{
					this._Device = value;
					this.OnPropertyChanged("Device");
				}
			}
		}

		private string _Update = string.Empty;

		public string Update
		{
			get
			{
				return this._Update;
			}

			set
			{
				if (this._Update != value)
				{
					this._Update = value;
					this.OnPropertyChanged("Update");
				}
			}
		}

		private System.Windows.Media.ImageSource _Signal = null;

		public System.Windows.Media.ImageSource Signal
		{
			get
			{
				return this._Signal;
			}

			set
			{
				if (this._Signal != value)
				{
					this._Signal = value;
					this.OnPropertyChanged("Signal");
				}
			}
		}

		private string _ManageArea = string.Empty;

		public string ManageArea
		{
			get
			{
				return this._ManageArea;
			}

			set
			{
				if (this._ManageArea != value)
				{
					this._ManageArea = value;
					this.OnPropertyChanged("ManageArea");
				}
			}
		}

		private string _Temperature = string.Empty;

		public string Temperature
		{
			get
			{
				return this._Temperature;
			}

			set
			{
				if (this._Temperature != value)
				{
					this._Temperature = value;
					this.OnPropertyChanged("Temperature");
				}
			}
		}

		private string _Percent = string.Empty;

		public string Percent
		{
			get
			{
				return this._Percent;
			}

			set
			{
				if (this._Percent != value)
				{
					this._Percent = value;
					this.OnPropertyChanged("Percent");
				}
			}
		}

		private System.Windows.Media.ImageSource _State = null;

		public System.Windows.Media.ImageSource State
		{
			get
			{
				return this._State;
			}

			set
			{
				if (this._State != value)
				{
					this._State = value;
					this.OnPropertyChanged("State");
				}
			}
		}

		private string _Operation = string.Empty;

		public string Operation
		{
			get
			{
				return this._Operation;
			}

			set
			{
				if (this._Operation != value)
				{
					this._Operation = value;
					this.OnPropertyChanged("Operation");
				}
			}
		}

		private string _Area = string.Empty;

		public string Area
		{
			get
			{
				return this._Area;
			}

			set
			{
				if (this._Area != value)
				{
					this._Area = value;
					this.OnPropertyChanged("Area");
				}
			}
		}

		private string _Distance = string.Empty;

		public string Distance
		{
			get
			{
				return this._Distance;
			}

			set
			{
				if (this._Distance != value)
				{
					this._Distance = value;
					this.OnPropertyChanged("Distance");
				}
			}
		}

		private System.Windows.Media.ImageSource _DeviceStatus = null;

		public System.Windows.Media.ImageSource DeviceStatus
		{
			get
			{
				return this._DeviceStatus;
			}

			set
			{
				if (this._DeviceStatus != value)
				{
					this._DeviceStatus = value;
					this.OnPropertyChanged("DeviceStatus");
				}
			}
		}

		private string _GroupName = string.Empty;

		public string GroupName
		{
			get
			{
				return this._GroupName;
			}

			set
			{
				if (this._GroupName != value)
				{
					this._GroupName = value;
					this.OnPropertyChanged("GroupName");
				}
			}
		}

		private string _EventType = string.Empty;

		public string EventType
		{
			get
			{
				return this._EventType;
			}

			set
			{
				if (this._EventType != value)
				{
					this._EventType = value;
					this.OnPropertyChanged("EventType");
				}
			}
		}

		private string _StartTime = string.Empty;

		public string StartTime
		{
			get
			{
				return this._StartTime;
			}

			set
			{
				if (this._StartTime != value)
				{
					this._StartTime = value;
					this.OnPropertyChanged("StartTime");
				}
			}
		}

		private string _EndTime = string.Empty;

		public string EndTime
		{
			get
			{
				return this._EndTime;
			}

			set
			{
				if (this._EndTime != value)
				{
					this._EndTime = value;
					this.OnPropertyChanged("EndTime");
				}
			}
		}

		private string _SureTime = string.Empty;

		public string SureTime
		{
			get
			{
				return this._SureTime;
			}

			set
			{
				if (this._SureTime != value)
				{
					this._SureTime = value;
					this.OnPropertyChanged("SureTime");
				}
			}
		}

		private string _OperateMan = string.Empty;

		public string OperateMan
		{
			get
			{
				return this._OperateMan;
			}

			set
			{
				if (this._OperateMan != value)
				{
					this._OperateMan = value;
					this.OnPropertyChanged("OperateMan");
				}
			}
		}

		private string _Remark = string.Empty;

		public string Remark
		{
			get
			{
				return this._Remark;
			}

			set
			{
				if (this._Remark != value)
				{
					this._Remark = value;
					this.OnPropertyChanged("Remark");
				}
			}
		}

		private string _DealWith = string.Empty;

		public string DealWith
		{
			get
			{
				return this._DealWith;
			}

			set
			{
				if (this._DealWith != value)
				{
					this._DealWith = value;
					this.OnPropertyChanged("DealWith");
				}
			}
		}

		private string _EventProprity = string.Empty;

		public string EventProprity
		{
			get
			{
				return this._EventProprity;
			}

			set
			{
				if (this._EventProprity != value)
				{
					this._EventProprity = value;
					this.OnPropertyChanged("EventProprity");
				}
			}
		}

		private System.Windows.Media.ImageSource _Electric = null;

		public System.Windows.Media.ImageSource Electric
		{
			get
			{
				return this._Electric;
			}

			set
			{
				if (this._Electric != value)
				{
					this._Electric = value;
					this.OnPropertyChanged("Electric");
				}
			}
		}
	}
#endif
}
