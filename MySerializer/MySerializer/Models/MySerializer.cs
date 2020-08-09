using MySerializer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace MySerializer.Models
{
    public class MySerializer<T>
    {
        public static bool Serialize(T data,string filename,SerializeType serializeType)
        {
            try
            {
                switch (serializeType)
                {
                    case SerializeType.BinaryFile:
                        {
                            SerializeInBinaryFile(data, filename);
                            break;
                        }
                    case SerializeType.XmlFile:
                        {
                            SerializeInXmlFile(data, filename);
                            break;
                        }
                    case SerializeType.TextFileByJsonFormat:
                        {
                            SerializeInTextFileByJsonFormat(data, filename);
                            break;
                        }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static void SerializeInBinaryFile(T data, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, data);
            }
        }
        private static void SerializeInXmlFile(T data, string filename)
        {

        }
        private static void SerializeInTextFileByJsonFormat(T data, string filename)
        {

        }
        public static bool Serialize(string filename, SerializeType serializeType,T data)
        {
            try
            {
                switch (serializeType)
                {
                    case SerializeType.BinaryFile:
                        {
                            DeserializeInBinaryFile(filename,out data);
                            break;
                        }
                    case SerializeType.XmlFile:
                        {
                            DeserializeInXmlFile(filename,out data);
                            break;
                        }
                    case SerializeType.TextFileByJsonFormat:
                        {
                            DeserializeInTextFileByJsonFormat(filename,out data);
                            break;
                        }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void DeserializeInBinaryFile(string filename,out T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filename))
            {
                data = (T)serializer.Deserialize(reader);
            }
        }
        private static void DeserializeInXmlFile(string filename,out T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filename))
            {
                data = (T)serializer.Deserialize(reader);
            }
        }
        private static void DeserializeInTextFileByJsonFormat(string filename,out T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filename))
            {
                data = (T)serializer.Deserialize(reader);
            }
        }
    }
}
