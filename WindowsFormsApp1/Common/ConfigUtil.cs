using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsFormsApp1.Common
{
    public class ConfigUtil
    {
        private static string FILE_NAME_CONFIG_XML = "config.xml";

        public static void CreateConfigXml()
        {
            if (File.Exists(FILE_NAME_CONFIG_XML)) return;
            var writer = XmlWriter.Create(FILE_NAME_CONFIG_XML);

            writer.WriteStartDocument();
            writer.WriteStartElement("config");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public static string GetConfigContent(string key)
        {
            using (XmlReader xmlReader = XmlReader.Create(FILE_NAME_CONFIG_XML))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlReader);
                XmlNode configNode = xmlDocument.SelectSingleNode("config");
                XmlElement element = (XmlElement)configNode.SelectSingleNode(key);
                if (element == null) return string.Empty;

                return element.InnerText;
            }
        }

        public static void SetConfigContent(string key, string val)
        {
            if (!File.Exists(FILE_NAME_CONFIG_XML)) CreateConfigXml();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(FILE_NAME_CONFIG_XML);
            XmlNode configNode = xmlDocument.SelectSingleNode("config");

            XmlElement element = (XmlElement)configNode.SelectSingleNode(key);

            if (element == null)
            {
                XmlNode xmlNode = xmlDocument.CreateNode(XmlNodeType.Element, key, "");
                xmlNode.InnerText = val;

                var root = xmlDocument.DocumentElement;
                root.AppendChild(xmlNode);
            }
            else
            {
                element.InnerText = val;
            }

            xmlDocument.Save(FILE_NAME_CONFIG_XML);
       
        }
    }
}
