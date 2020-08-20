using MySerializer.Enums;
using System;
using System.Xml.Serialization;
using System.IO;
using MySerializer.Interfaces;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace MySerializer.Models
{
    public class Serializer<T> where T : IVersionHaver, ISerialize
    {
        /// <summary>
        /// Object's serializing method
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="filename"></param>
        /// <param name="serializeType"></param>
        /// <returns></returns>
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
                case SerializeType.JsonFile:
                    {
                        SerializeInJsonFile(data, filename);
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
        /// <summary>
        /// Binary serializing method
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filename"></param>
        private static void SerializeInBinaryFile(T data, string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filename, FileMode.Create,FileAccess.Write))
            {
                formatter.Serialize(stream, data);
            }
        }


        /// <summary>
        /// Xml serializing method
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filename"></param>
        private static void SerializeInXmlFile(T data, string filename)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(T));
            //using (StreamWriter writer = new StreamWriter(filename))
            //{
            //    serializer.Serialize(writer, data);
            //}
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (FileStream stream = new FileStream(filename, FileMode.Create,FileAccess.Write))
            {
                serializer.WriteObject(stream, data);
            }
        }

        /// <summary>
        /// Json serializing method
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filename"></param>

        private static void SerializeInJsonFile(T data, string filename)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (FileStream stream = new FileStream(filename, FileMode.Create,FileAccess.Write))
            {
                serializer.WriteObject(stream, data);
            }
        }

        /// <summary>
        /// Object's deserializing method
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="currentVersion"></param>
        /// <param name="serializeType"></param>
        /// <returns></returns>

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
                case SerializeType.JsonFile:
                    {
                        return DeserializeInJsonFile(filename,currentVersion);
                    }
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Binary deserializing method
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="version"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Xml deserializing method
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static T DeserializeInXmlFile(string filename, Version version)
        {
            dynamic data = null;
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            using (FileStream reader = new FileStream(filename,FileMode.Open))
            {
                data = (T)serializer.ReadObject(reader);
                if (version == data.Version)
                {
                    return data;
                }
            }
            throw new Exception("Несоответствие версий");
        }

        /// <summary>
        /// Json deserializing method
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static T DeserializeInJsonFile(string filename,Version version)
        {
            dynamic data = null;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (FileStream stream = new FileStream(filename, FileMode.Open))
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
