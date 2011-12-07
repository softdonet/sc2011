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
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reflection;

namespace Scada.Client.SL.Modules.Device
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
