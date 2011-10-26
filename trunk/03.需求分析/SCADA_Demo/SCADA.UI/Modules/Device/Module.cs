using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SCADA.UI.Modules.Device
{
    public class Module : CoverageItemBase
	{
		private string name;
		private ObservableCollection<NamespaceTable> items;

		public Module()
        {
            this.items = new ObservableCollection<NamespaceTable>();
        }

        [XmlElement(ElementName="ModuleName")]
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

        [XmlElement(ElementName="NamespaceTable")]
        public ObservableCollection<NamespaceTable> Items
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
