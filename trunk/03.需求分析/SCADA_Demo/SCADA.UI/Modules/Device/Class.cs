using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SCADA.UI.Modules.Device
{
    public class Class : CoverageItemBase
	{
		private string name;
		private ObservableCollection<Method> items;

		public Class()
        {
            this.items = new ObservableCollection<Method>();
        }

        [XmlElement(ElementName = "ClassName")]
        public string Name
        {
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
        }

        [XmlElement(ElementName = "Method")]
        public ObservableCollection<Method> Items
        {
			get
			{
				return this.items;
			}
			set
			{
				this.items = value;
			}
        }
    }
}
