using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Lcd.CommClass
{
    #region  ÅäÖÃµÄ²Ù×÷ÀàModuleConfig

    public class ModuleConfig
    {
        public static ModuleSettings GetSettings()
        {
            ModuleSettings data = null;
            XmlSerializer serializer = new XmlSerializer(typeof(ModuleSettings));
            try
            {
                string fileName = "Config.xml";
                FileStream fs = new FileStream(fileName, FileMode.Open);
                data = (ModuleSettings)serializer.Deserialize(fs);
                fs.Close();
            }
            catch
            {
                data = new ModuleSettings();
            }


            return data;
        }


        public static void SaveSettings(ModuleSettings data)
        {
            string fileName = "Config.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(ModuleSettings));

            // serialize the object
            FileStream fs = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(fs, data);
            fs.Close();
        }

    }

    #endregion
}
