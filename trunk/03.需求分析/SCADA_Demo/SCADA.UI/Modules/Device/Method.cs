using System.Xml.Serialization;

namespace SCADA.UI.Modules.Device
{
    public class Method : CoverageItemBase
    {
		private string name;

        public Method()
        {
            
        }

        [XmlElement(ElementName = "MethodName")]
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
    }
}

