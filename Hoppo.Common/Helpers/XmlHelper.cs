using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hoppo.Common.Helpers
{
    public static class XmlHelper
    {
        public static string SerializeObject<T>(T objects)
        {
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(objects.GetType());
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                serializer.Serialize(ms, objects);
                ms.Position = 0;
                xmlDoc.Load(ms);
                return xmlDoc.InnerXml;
            }
        }

        public static async Task<string> ConvertListToXmlAsync<T>(IEnumerable<T> list)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (MemoryStream stream = new MemoryStream())
            {
                await Task.Run(() => serializer.Serialize(stream, list));
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        public static async Task<string> ConvertListToXmlAsync<T>(T list)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                await Task.Run(() => serializer.Serialize(stream, list));
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
