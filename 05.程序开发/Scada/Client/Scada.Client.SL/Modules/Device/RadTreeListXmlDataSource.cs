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
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace Scada.Client.SL.Modules.Device
{
    public class RadTreeListXmlDataSource : TestCoverageItemsList
    {
        private string source;

        public string Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.source = value;
                AddRange(RetrieveData(Application.GetResourceStream(GetResourceUri(value)).Stream));
            }
        }

        internal static Uri GetResourceUri(string resource)
        {
            AssemblyName assemblyName = new AssemblyName(typeof(RadTreeListXmlDataSource).Assembly.FullName);
            string resourcePath = "/" + assemblyName.Name + ";component/Modules/Device/" + resource;


            Uri resourceUri = new Uri(resourcePath, UriKind.Relative);

            return resourceUri;
        }

        private TestCoverageItemsList RetrieveData(Stream xmlStream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TestCoverageItemsList));
            StreamReader reader = new StreamReader(xmlStream);
            TestCoverageItemsList list = (TestCoverageItemsList)serializer.Deserialize(reader);
            return list;
        }
    }
}
