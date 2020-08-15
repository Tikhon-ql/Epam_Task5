using MySerializer.Enums;
using System;
using System.Xml.Serialization;
using System.IO;
using MySerializer.Interfaces;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Runtime.CompilerServices;
using MySerializer.Abstract;

namespace MySerializer.Models
{
    public class Serializer<T> where T : VersionHaver, ISerialize
    {
        public static bool Serialize(T data, string filename, SerializeType serializeType)
        {
            //try
            //{
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
        //}
        //catch(Exception ex)
        //{
        //    string str = ex.GetType().Name.ToString();
        //    return false;
        //}
        }

        private static void SerializeInBinaryFile(T data, string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, data);
            }
        }



        private static void SerializeInXmlFile(T data, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, data);
            }
        }



        private static void SerializeInTextFileByJsonFormat(T data, string filename)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using(FileStream stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(stream, data);
            }
        }



        public static T Deserialize(string filename,Version currentVersion, SerializeType serializeType)
        {
            switch (serializeType)
            {
                case SerializeType.BinaryFile:
                    {
                        return DeserializeInBinaryFile(filename, currentVersion);
                    }
                case SerializeType.XmlFile:
                    {
                        return DeserializeInXmlFile(filename,currentVersion);
                    }
                case SerializeType.TextFileByJsonFormat:
                    {
                        return DeserializeInTextFileByJsonFormat(filename,currentVersion);
                    }
                default:
                    throw new Exception();
            }
        }

        private static T DeserializeInBinaryFile(string filename,Version version)
        {
            dynamic data = null;
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
               data = (T)formatter.Deserialize(stream);
               if(version == data.Version)
               {
                    return data;
               }
            }
            throw new Exception("Несоответствие версий");
        }

        private static T DeserializeInXmlFile(string filename, Version version)
        {
            dynamic data = null;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filename))
            {
                data = (T)serializer.Deserialize(reader);
                if (version == data.Version)
                {
                    return data;
                }
            }
            throw new Exception("Несоответствие версий");
        }

        private static T DeserializeInTextFileByJsonFormat(string filename,Version version)
        {
            dynamic data = null;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (FileStream stream = new FileStream(filename,FileMode.Open))
            {
                data = (T)serializer.ReadObject(stream);
                if (version == data.Version)
                {
                    return data;
                }
            }
            throw new Exception("Несоответствие версий");
        }
    }
}
