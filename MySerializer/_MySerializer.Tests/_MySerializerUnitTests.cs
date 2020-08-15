using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySerializer.Enums;
using MySerializer.Models;
using MySerializer.Resources;

namespace _MySerializer.Tests
{
    [TestClass]
    public class _MySerializerUnitTests
    {
        [DataTestMethod]
        [DynamicData(nameof(DataForStudentSerializeInBinaryFileMethod),DynamicDataSourceType.Method)]
        public void SerializeStudentInBinaryFile(Student student)
        {
            //act
            bool actual = Serializer<Student>.Serialize(student,"binary.dat",SerializeType.BinaryFile);
            //assert
            Assert.IsTrue(actual);
        }
        public static IEnumerable<Student[]> DataForStudentSerializeInBinaryFileMethod()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Иван",17) },
                new Student[] { new Student("Сидоров", "Сидр", 18) },
                new Student[] { new Student("Петров", "Петр", 19) }
            };
        }
        [DataTestMethod]
        [DynamicData(nameof(DataForStudentSerializeInXmlFileMethod),DynamicDataSourceType.Method)]
        public void SerializeStudentInXmlFile(Student student)
        {
            //act
            bool actual = Serializer<Student>.Serialize(student, "xmlfile.xml", SerializeType.XmlFile);
            //assert
            Assert.IsTrue(actual);
        }
        public static IEnumerable<Student[]> DataForStudentSerializeInXmlFileMethod()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Иван",17) },
                new Student[] { new Student("Сидоров", "Сидр", 18) },
                new Student[] { new Student("Петров", "Петр", 19) }
            };
        }
        [DataTestMethod]
        [DynamicData(nameof(DataForStudentSerializeInTextFileByJsonFormatMethod),DynamicDataSourceType.Method)]
        public void SerializeStudentInTextFileByJsonFormatFile(Student student)
        {
            //act
            bool actual = Serializer<Student>.Serialize(student, "textjson.txt", SerializeType.TextFileByJsonFormat);
            //assert
            Assert.IsTrue(actual);
        }
        public static IEnumerable<Student[]> DataForStudentSerializeInTextFileByJsonFormatMethod()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Иван",17) },
                new Student[] { new Student("Сидоров", "Сидр", 18) },
                new Student[] { new Student("Петров", "Петр", 19) }
            };
        }
        [DataTestMethod]
        [DynamicData(nameof(DataForStudentDeserializeFromBinaryFile), DynamicDataSourceType.Method)]
        public void DeserializeStudentFromBinaryFile(Version currVersion)
        {
            //arrange

            Student expected = new Student("Петров", "Петр", 19);
            Student actual = new Student();

            //act
            if (currVersion == Version.Parse("1.0.0.1"))
                Assert.ThrowsException<Exception>(() => { Serializer<Student>.Deserialize("binary.dat", currVersion, SerializeType.BinaryFile); });
            else
            {
                actual = Serializer<Student>.Deserialize("binary.dat", currVersion, SerializeType.BinaryFile);
                Assert.AreEqual(expected, actual);
            }
              
        }
        public static IEnumerable<Version[]> DataForStudentDeserializeFromBinaryFile()
        {
            return new[]
            {
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.1") }
            };
        }
        [TestMethod]
        public void DeserializeStudentFromXmlFile()
        {
            //arrange
            Student expected = new Student("Петров", "Петр", 19);

            //act
            Student actual = Serializer<Student>.Deserialize("xmlfile.xml",Version.Parse("1.0.0.0"),SerializeType.XmlFile);

            //assert

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DeserializeStudentFromTextFileByJson()
        {
            //arrange
            Student expected = new Student("Петров", "Петр", 19);

            //act
            Student actual = Serializer<Student>.Deserialize("textjson.txt", Version.Parse("1.0.0.0"), SerializeType.TextFileByJsonFormat);

            //assert

            Assert.AreEqual(expected, actual);
        }
    }
}
