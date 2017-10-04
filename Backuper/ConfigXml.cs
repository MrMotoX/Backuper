using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backuper
{
    static class ConfigXml
    {
        private static string fileName = "config.xml";
        private static XmlDocument doc = new XmlDocument();

        public static void LoadFromFile()
        {
            if (File.Exists(fileName))
            {
                doc.Load(fileName);
            }
        }

        public static string GetField(string field)
        {
            XmlNodeList nodes = doc.GetElementsByTagName(field);
            if (nodes.Count > 0)
	        {
                return nodes[0].InnerText;
	        }
            return "";
        }

        public static void SaveToFieldInFile(string field, string value)
        {
            XmlNodeList nodes = doc.GetElementsByTagName(field);
            if (nodes.Count > 0)
            {
                XmlNode newNode = nodes[0];
                newNode.InnerText = value;
                doc.ReplaceChild(newNode, nodes[0]);
            }
            else
            {
                XmlNode newNode = doc.CreateNode("element", field, "");
                newNode.InnerText = value;
                doc.AppendChild(newNode);
            }
            doc.Save(fileName);
        }
    }
}
