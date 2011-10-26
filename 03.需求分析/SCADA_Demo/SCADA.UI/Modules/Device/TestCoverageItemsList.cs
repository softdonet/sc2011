using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SCADA.UI.Modules.Device
{
    [XmlRoot(ElementName = "CoverageDSPriv")]
    public class TestCoverageItemsList : ObservableCollection<Module>
    {
        public void AddRange(IEnumerable<Module> range)
        {
            foreach (Module node in range)
            {
                this.Add(node);
            }
        }
    }
}
