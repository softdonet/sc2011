﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.TimeTableSampleDataSource
{
	using System; 

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class TimeTableSampleDataSource { }
#else

	public class TimeTableSampleDataSource : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public TimeTableSampleDataSource()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/SCADA.UI;component/SampleData/TimeTableSampleDataSource/TimeTableSampleDataSource.xaml", System.UriKind.Relative);
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

		private string _TimeTable = string.Empty;

		public string TimeTable
		{
			get
			{
				return this._TimeTable;
			}

			set
			{
				if (this._TimeTable != value)
				{
					this._TimeTable = value;
					this.OnPropertyChanged("TimeTable");
				}
			}
		}

		private string _Hour = string.Empty;

		public string Hour
		{
			get
			{
				return this._Hour;
			}

			set
			{
				if (this._Hour != value)
				{
					this._Hour = value;
					this.OnPropertyChanged("Hour");
				}
			}
		}

		private string _Minute = string.Empty;

		public string Minute
		{
			get
			{
				return this._Minute;
			}

			set
			{
				if (this._Minute != value)
				{
					this._Minute = value;
					this.OnPropertyChanged("Minute");
				}
			}
		}
	}

	public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
	{ 
	}
#endif
}
