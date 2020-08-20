using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySerializer.Enums;
using MySerializer.Models;

namespace _MySerializer.Tests
{
    [TestClass]
    public class _MySerializerUnitTests
    {
        /// <summary>
        /// Checking object's serialization in binary file method
        /// </summary>
        /// <param name="student"></param>
        [DataTestMethod]
        [DynamicData(nameof(Data_For_Student_Serialize_In_Binary_File_Method),DynamicDataSourceType.Method)]
        public void Serialize_Student_In_Binary_File(Student student)
        {
            //act
            bool actual = Serializer<Student>.Serialize(student,"binary.dat",SerializeType.BinaryFile);
            //assert
            Assert.IsTrue(actual);
        }
        /// <summary>
        /// Data for checking serialization in binary file method
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Student[]> Data_For_Student_Serialize_In_Binary_File_Method()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Иван",17) },
                new Student[] { new Student("Сидоров", "Сидр", 18) },
                new Student[] { new Student("Петров", "Петр", 19) }
            };
        }

        /// <summary>
        /// Checking object's serialization in xml file method
        /// </summary>
        /// <param name="student"></param>

        [DataTestMethod]
        [DynamicData(nameof(Data_For_Student_Serialize_In_Xml_File_Method),DynamicDataSourceType.Method)]
        public void Serialize_Student_In_Xml_File(Student student)
        {
            //act
            bool actual = Serializer<Student>.Serialize(student, "xmlfile.xml", SerializeType.XmlFile);
            //assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Data for checking serialization in xml file method
        /// </summary>
        /// <returns></returns>

        public static IEnumerable<Student[]> Data_For_Student_Serialize_In_Xml_File_Method()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Иван",17) },
                new Student[] { new Student("Сидоров", "Сидр", 18) },
                new Student[] { new Student("Петров", "Петр", 19) }
            };
        }

        /// <summary>
        /// Checking object's serialization in json file method
        /// </summary>
        /// <param name="student"></param>

        [DataTestMethod]
        [DynamicData(nameof(Data_For_Student_Serialize_In_Json_File_Method),DynamicDataSourceType.Method)]
        public void Serialize_Student_In_Json_File(Student student)
        {
            //act
            bool actual = Serializer<Student>.Serialize(student, "textjson.json", SerializeType.JsonFile);
            //assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Data for checking serialization in json file method
        /// </summary>
        /// <returns></returns>

        public static IEnumerable<Student[]> Data_For_Student_Serialize_In_Json_File_Method()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Иван",17) },
                new Student[] { new Student("Сидоров", "Сидр", 18) },
                new Student[] { new Student("Петров", "Петр", 19) }
            };
        }

        /// <summary>
        /// Checking object's deserialization from binary file method
        /// </summary>
        /// <param name="currVersion"></param>

        [DataTestMethod]
        [DynamicData(nameof(Data_For_Student_Deserialize_From_Binary_File), DynamicDataSourceType.Method)]
        public void Deserialize_Student_From_Binary_File(Version currVersion)
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

        /// <summary>
        /// Data for checking deserialization from binary file method
        /// </summary>
        /// <returns></returns>

        public static IEnumerable<Version[]> Data_For_Student_Deserialize_From_Binary_File()
        {
            return new[]
            {
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.1") }
            };
        }

        /// <summary>
        /// Checking object's deserialization from xml file method
        /// </summary>

        [TestMethod]
        public void Deserialize_Student_From_Xml_File()
        {
            //arrange
            Student expected = new Student("Петров", "Петр", 19);

            //act
            Student actual = Serializer<Student>.Deserialize("xmlfile.xml",Version.Parse("1.0.0.0"),SerializeType.XmlFile);

            //assert

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Checking object's deserialization from json file method
        /// </summary>

        [TestMethod]
        public void Deserialize_Student_From_Json_File()
        {
            //arrange
            Student expected = new Student("Петров", "Петр", 19);

            //act
            Student actual = Serializer<Student>.Deserialize("textjson.json", Version.Parse("1.0.0.0"), SerializeType.JsonFile);

            //assert

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Checking object's list serialization in binary file method
        /// </summary>
        /// <param name="student"></param>

        [DataTestMethod]
        [DynamicData(nameof(Data_For_StudentList_Serialize_In_Binary_File_Method), DynamicDataSourceType.Method)]
        public void Serialize_StudentList_In_Binary_File(StudentsList student)
        {
            //act
            bool actual = Serializer<StudentsList>.Serialize(student, "binaryList.dat", SerializeType.BinaryFile);
            //assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Data for checking serialization object's list in binary file method
        /// </summary>
        /// <returns></returns>

        public static IEnumerable<StudentsList[]> Data_For_StudentList_Serialize_In_Binary_File_Method()
        {
            return new[]
            {
                new StudentsList[] { new StudentsList(new Student("Иванов", "Иван",17), new Student("Бабаев", "Бабай", 17)) },
                new StudentsList[] {new StudentsList( new Student("Сидоров", "Сидр", 18),new Student("Солдатов","Солдат",17))},
                new StudentsList[] {new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18)) }
            };
        }

        /// <summary>
        /// Checking object's list serialization in xml file method
        /// </summary>
        /// <param name="student"></param>

        [DataTestMethod]
        [DynamicData(nameof(Data_For_StudentList_Serialize_In_Xml_File_Method), DynamicDataSourceType.Method)]
        public void Serialize_StudentList_In_Xml_File(StudentsList student)
        {
            //act
            bool actual = Serializer<StudentsList>.Serialize(student, "xmlfileList.xml", SerializeType.XmlFile);
            //assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Data for checking serialization object's list in xml file method
        /// </summary>
        /// <returns></returns>

        public static IEnumerable<StudentsList[]> Data_For_StudentList_Serialize_In_Xml_File_Method()
        {
            return new[]
            {
                new StudentsList[] { new StudentsList(new Student("Иванов", "Иван",17), new Student("Бабаев", "Бабай", 17)) },
                new StudentsList[] {new StudentsList( new Student("Сидоров", "Сидр", 18),new Student("Солдатов","Солдат",17))},
                new StudentsList[] {new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18)) }
            };
        }


        /// <summary>
        /// Checking object's list serialization in json file method
        /// </summary>
        /// <param name="student"></param>

        [DataTestMethod]
        [DynamicData(nameof(Data_For_StudentList_Serialize_In_Json_File_Method), DynamicDataSourceType.Method)]
        public void Serialize_StudentList_In_Json_File(StudentsList student)
        {
            //act
            bool actual = Serializer<StudentsList>.Serialize(student, "textjsonList.json", SerializeType.JsonFile);
            //assert
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Data for checking serialization object's list in json file method
        /// </summary>
        /// <returns></returns>

        public static IEnumerable<StudentsList[]> Data_For_StudentList_Serialize_In_Json_File_Method()
        {
            return new[]
            {
                new StudentsList[] { new StudentsList(new Student("Иванов", "Иван",17), new Student("Бабаев", "Бабай", 17)) },
                new StudentsList[] {new StudentsList( new Student("Сидоров", "Сидр", 18),new Student("Солдатов","Солдат",17))},
                new StudentsList[] {new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18)) }
            };
        }

        /// <summary>
        /// Checking object's list deserialization from binary file method
        /// </summary>
        /// <param name="currVersion"></param>

        [DataTestMethod]
        [DynamicData(nameof(Data_For_StudentList_Deserialize_From_Binary_File), DynamicDataSourceType.Method)]
        public void Deserialize_StudentList_From_Binary_File(Version currVersion)
        {
            //arrange

            StudentsList expected = new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18));
            StudentsList actual = new StudentsList();

            //act
            if (currVersion == Version.Parse("1.0.0.1"))
                Assert.ThrowsException<Exception>(() => { Serializer<Student>.Deserialize("binary.dat", currVersion, SerializeType.BinaryFile); });
            else
            {
                actual = Serializer<StudentsList>.Deserialize("binaryList.dat", currVersion, SerializeType.BinaryFile);
                Assert.AreEqual(expected, actual);
            }

        }

        /// <summary>
        /// Data for checking deserialization from binary file method
        /// </summary>
        /// <returns></returns>

        public static IEnumerable<Version[]> Data_For_StudentList_Deserialize_From_Binary_File()
        {
            return new[]
            {
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.1") }
            };
        }

        /// <summary>
        /// Checking object's list deserialization from xml file method
        /// </summary>

        [TestMethod]
        public void Deserialize_StudentList_From_Xml_File()
        {
            //arrange
            StudentsList expected = new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18));

            //act
            StudentsList actual = Serializer<StudentsList>.Deserialize("xmlfileList.xml", Version.Parse("1.0.0.0"), SerializeType.XmlFile);

            //assert

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Checking object's list deserialization from json file method
        /// </summary>

        [TestMethod]
        public void Deserialize_StudentList_From_Json_File()
        {
            //arrange
            StudentsList expected = new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18));

            //act
            StudentsList actual = Serializer<StudentsList>.Deserialize("textjsonList.json", Version.Parse("1.0.0.0"), SerializeType.JsonFile);

            //assert
            
            Assert.AreEqual(expected, actual);
        }
    }
}
