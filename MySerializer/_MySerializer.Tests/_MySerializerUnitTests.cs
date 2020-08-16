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
        [DynamicData(nameof(Data_For_Student_Serialize_In_Binary_File_Method),DynamicDataSourceType.Method)]
        public void Serialize_Student_In_Binary_File(Student student)
        {
            //act
            bool actual = Serializer<Student>.Serialize(student,"binary.dat",SerializeType.BinaryFile);
            //assert
            Assert.IsTrue(actual);
        }

        public static IEnumerable<Student[]> Data_For_Student_Serialize_In_Binary_File_Method()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Иван",17) },
                new Student[] { new Student("Сидоров", "Сидр", 18) },
                new Student[] { new Student("Петров", "Петр", 19) }
            };
        }



        [DataTestMethod]
        [DynamicData(nameof(Data_For_Student_Serialize_In_Xml_File_Method),DynamicDataSourceType.Method)]
        public void Serialize_Student_In_Xml_File(Student student)
        {
            //act
            bool actual = Serializer<Student>.Serialize(student, "xmlfile.xml", SerializeType.XmlFile);
            //assert
            Assert.IsTrue(actual);
        }

        public static IEnumerable<Student[]> Data_For_Student_Serialize_In_Xml_File_Method()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Иван",17) },
                new Student[] { new Student("Сидоров", "Сидр", 18) },
                new Student[] { new Student("Петров", "Петр", 19) }
            };
        }



        [DataTestMethod]
        [DynamicData(nameof(Data_For_Student_Serialize_In_Text_File_By_Json_Format_Method),DynamicDataSourceType.Method)]
        public void Serialize_Student_In_Text_File_By_Json_Format(Student student)
        {
            //act
            bool actual = Serializer<Student>.Serialize(student, "textjson.txt", SerializeType.TextFileByJsonFormat);
            //assert
            Assert.IsTrue(actual);
        }

        public static IEnumerable<Student[]> Data_For_Student_Serialize_In_Text_File_By_Json_Format_Method()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Иван",17) },
                new Student[] { new Student("Сидоров", "Сидр", 18) },
                new Student[] { new Student("Петров", "Петр", 19) }
            };
        }



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

        public static IEnumerable<Version[]> Data_For_Student_Deserialize_From_Binary_File()
        {
            return new[]
            {
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.1") }
            };
        }



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



        [TestMethod]
        public void Deserialize_Student_From_Text_File_By_Json()
        {
            //arrange
            Student expected = new Student("Петров", "Петр", 19);

            //act
            Student actual = Serializer<Student>.Deserialize("textjson.txt", Version.Parse("1.0.0.0"), SerializeType.TextFileByJsonFormat);

            //assert

            Assert.AreEqual(expected, actual);
        }



        [DataTestMethod]
        [DynamicData(nameof(Data_For_StudentList_Serialize_In_Binary_File_Method), DynamicDataSourceType.Method)]
        public void Serialize_StudentList_In_Binary_File(StudentsList student)
        {
            //act
            bool actual = Serializer<StudentsList>.Serialize(student, "binaryList.dat", SerializeType.BinaryFile);
            //assert
            Assert.IsTrue(actual);
        }

        public static IEnumerable<StudentsList[]> Data_For_StudentList_Serialize_In_Binary_File_Method()
        {
            return new[]
            {
                new StudentsList[] { new StudentsList(new Student("Иванов", "Иван",17), new Student("Бабаев", "Бабай", 17)) },
                new StudentsList[] {new StudentsList( new Student("Сидоров", "Сидр", 18),new Student("Солдатов","Солдат",17))},
                new StudentsList[] {new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18)) }
            };
        }



        [DataTestMethod]
        [DynamicData(nameof(Data_For_StudentList_Serialize_In_Xml_File_Method), DynamicDataSourceType.Method)]
        public void Serialize_StudentList_In_Xml_File(StudentsList student)
        {
            //act
            bool actual = Serializer<StudentsList>.Serialize(student, "xmlfileList.xml", SerializeType.XmlFile);
            //assert
            Assert.IsTrue(actual);
        }

        public static IEnumerable<StudentsList[]> Data_For_StudentList_Serialize_In_Xml_File_Method()
        {
            return new[]
            {
                new StudentsList[] { new StudentsList(new Student("Иванов", "Иван",17), new Student("Бабаев", "Бабай", 17)) },
                new StudentsList[] {new StudentsList( new Student("Сидоров", "Сидр", 18),new Student("Солдатов","Солдат",17))},
                new StudentsList[] {new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18)) }
            };
        }




        [DataTestMethod]
        [DynamicData(nameof(Data_For_StudentList_Serialize_In_Text_File_By_Json_Format_Method), DynamicDataSourceType.Method)]
        public void Serialize_StudentList_In_Text_File_By_Json_Format_File(StudentsList student)
        {
            //act
            bool actual = Serializer<StudentsList>.Serialize(student, "textjsonList.txt", SerializeType.TextFileByJsonFormat);
            //assert
            Assert.IsTrue(actual);
        }

        public static IEnumerable<StudentsList[]> Data_For_StudentList_Serialize_In_Text_File_By_Json_Format_Method()
        {
            return new[]
            {
                new StudentsList[] { new StudentsList(new Student("Иванов", "Иван",17), new Student("Бабаев", "Бабай", 17)) },
                new StudentsList[] {new StudentsList( new Student("Сидоров", "Сидр", 18),new Student("Солдатов","Солдат",17))},
                new StudentsList[] {new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18)) }
            };
        }


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

        public static IEnumerable<Version[]> Data_For_StudentList_Deserialize_From_Binary_File()
        {
            return new[]
            {
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.0") },
                new Version[] { new Version("1.0.0.1") }
            };
        }



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



        [TestMethod]
        public void Deserialize_StudentList_From_Text_File_By_Json()
        {
            //arrange
            StudentsList expected = new StudentsList(new Student("Петров", "Петр", 19), new Student("Борисов", "Борис", 18));

            //act
            StudentsList actual = Serializer<StudentsList>.Deserialize("xmlfileList.xml", Version.Parse("1.0.0.0"), SerializeType.TextFileByJsonFormat);

            //assert
            
            Assert.AreEqual(expected, actual);
        }
    }
}
