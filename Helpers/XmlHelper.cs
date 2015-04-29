using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.IO;

namespace PermissionReporting
{
    public class XmlHelper
    {
        /// <summary>
        /// Serialized and saves an object as XML
        /// </summary>
        /// <param name="Object">SPSecurable object</param>
        /// <param name="FilePath">File path to save the object to</param>
        public static void SerializeToXML(SPSecurableObject Object, string FilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SPSecurableObject));
            TextWriter tw = new StreamWriter(FilePath, false);
            serializer.Serialize(tw, Object);
            tw.Close();
        }

        /// <summary>
        /// Loads SPSecurable object from presaved xml
        /// </summary>
        /// <param name="FilePath">File path to save the object from</param>
        /// <returns>Populated SPSecurable object</returns>
        public static SPSecurableObject LoadFromXml(string FilePath)
        {
            SPSecurableObject Object = new SPSecurableObject();
            XmlSerializer serializer = new XmlSerializer(typeof(SPSecurableObject));
            TextReader tw = new StreamReader(FilePath);
            Object = (SPSecurableObject)serializer.Deserialize(tw);
            tw.Close();
            return Object;
        }
    }
}
