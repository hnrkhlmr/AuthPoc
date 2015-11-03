using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace AuthPoc.Web
{
    public class Helper
    {
        public static void SerializeToXML<T>(T item, string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = @"D:\xml\";
            }
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            using (var textWriter = new StreamWriter(fileName + typeof(T).ToString()))
            {
                serializer.Serialize(textWriter, item);
            }
        }

        public static T DeserializeFromXML<T>(string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = @"D:\xml\";
            }
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            
            using (var textReader = new StreamReader(fileName + typeof(T).ToString()))
            {
                return (T)deserializer.Deserialize(textReader);
            }            
        }
    }
}