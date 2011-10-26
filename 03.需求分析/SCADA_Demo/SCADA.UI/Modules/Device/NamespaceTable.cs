using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SCADA.UI.Modules.Device
{
    public class NamespaceTable : CoverageItemBase
	{
		private string name;
		private ObservableCollection<Class> items;

		public NamespaceTable()
        {
            this.items = new ObservableCollection<Class>();
        }

        [XmlElement(ElementName = "NamespaceName")]
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

        [XmlElement(ElementName = "Class")]
        public ObservableCollection<Class> Items
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
