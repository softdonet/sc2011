using System.Xml.Serialization;

namespace SCADA.UI.Modules.Device
{
    public class CoverageItemBase
    {
        private int blocksCovered;
        private int blocksNotCovered;
        private int linesCovered;
        private int linesNotCovered;
        private int linesPartiallyCovered;

        public CoverageItemBase()
        {

        }

        [XmlElement(ElementName = "BlocksCovered")]
        public int BlocksCovered
        {
            get
			{
				return this.blocksCovered;
			}
            set
			{
				this.blocksCovered = value;
			}
        }

        [XmlElement(ElementName = "BlocksNotCovered")]
        public int BlocksNotCovered
        {
			get
			{
				return this.blocksNotCovered;
			}
			set
			{
				this.blocksNotCovered = value;
			}
        }
        
        [XmlElement(ElementName = "LinesCovered")]
        public int LinesCovered
        {
			get
			{
				return this.linesCovered;
			}
			set
			{
				this.linesCovered = value;
			}
        }

        [XmlElement(ElementName = "LinesNotCovered")]
        public int LinesNotCovered
        {
			get
			{
				return this.linesNotCovered;
			}
			set
			{
				this.linesNotCovered = value;
			}
        }

        [XmlElement(ElementName = "LinesPartiallyCovered")]
        public int LinesPartiallyCovered
        {
			get
			{
				return this.linesPartiallyCovered;
			}
			set
			{
				this.linesPartiallyCovered = value;
			}
        }
    }
}
