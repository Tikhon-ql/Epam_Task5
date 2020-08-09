using System;
using System.Collections.Generic;
using MyBinaryTree.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBinaryTree;

namespace _BinaryTree.Tests
{
    [TestClass]
    public class BinaryTreeUnitTests
    {
        /// <summary>
        /// Checking add method
        /// </summary>
        /// <param name="student"></param>
        [DynamicData(nameof(TestMethodAdd),DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void Add_Student_Method_Test(Student student)
        {
            //arrange
            BinaryTree<Student> tree = new BinaryTree<Student>();
            //act
            bool actual = tree.Add(student);
            //assert
            Assert.IsTrue(actual);
        }
        /// <summary>
        /// Getting data for add test method
        /// </summary>
        /// <returns></returns>
        
        private static IEnumerable<Student[]> TestMethodAdd()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5) },
                new Student[] { new Student("Сидоров", "Тест1", new DateTime(2012, 11, 10), 7) },
                new Student[] { new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6) }
            };
        }
        /// <summary>
        /// Checking remove method
        /// </summary>
        /// <param name="student"></param>
        [DynamicData(nameof(TestMethodRemove),DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void Remove_Student_Method_Test(Student student)
        {
            //arrange
            BinaryTree<Student> tree = new BinaryTree<Student>(new Student("Иванов", "Тест1", new DateTime(2010, 10, 10),5), new Student("Смирнов", "Тест1", new DateTime(2010, 10, 10),8), new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6));
            //act
            bool actual = tree.Remove(student);
            //assert
            Assert.IsTrue(actual);
        }
        /// <summary>
        ///  Getting data for remove test method
        /// </summary>
        /// <returns></returns>
      
        private static IEnumerable<Student[]> TestMethodRemove()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5) },
                new Student[] { new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6) }
            };
        }
        /// <summary>
        /// Checking serach method
        /// </summary>
        /// <param name="student"></param>
        [DynamicData(nameof(TestMethodSearch),DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void Search_Student_Method_Test(Student student)
        {
            //arrange
            BinaryTree<Student> tree = new BinaryTree<Student>(new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5), new Student("Смирнов", "Тест1", new DateTime(2010, 10, 10), 8), new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6));
            Student expected = student;
            //act
            Student actual = tree.Search(student);
            //assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Getting data for  test method
        /// </summary>
        /// <returns></returns>
     
        private static IEnumerable<Student[]> TestMethodSearch()
        {
            return new[]
            {
                new Student[] { new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5) },
                new Student[] { new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6) }
            };
        }
        /// <summary>
        /// Checking balance binary tree method
        /// </summary>
        [TestMethod]
        public void Balance_BinaryTree_Methode_Test()
        {
            //arrange
            BinaryTree<Student> tree = new BinaryTree<Student>(new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5), new Student("Смирнов", "Тест1", new DateTime(2010, 10, 10), 6), new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 8));
            BinaryTree<Student> expected = new BinaryTree<Student>(new Student("Смирнов", "Тест1", new DateTime(2010, 10, 10), 6), new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5), new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 8));

            //act

            tree.Balance();

            //assert
            Assert.AreEqual(expected,tree);
        }
        /// <summary>
        /// Checking serialize and desirialize binary tree method
        /// </summary>
        /// <param name="students"></param>
        [DynamicData(nameof(TestMethodSerializeAndDesirealize), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void Serialize_and_Deserialize_BinaryTree(object students)
        {
            //arrange
            List<Student> studs = students as List<Student>;
            BinaryTree<Student> expected = new BinaryTree<Student>(studs.ToArray());
            BinaryTree<Student> actual = new BinaryTree<Student>();
            //act
            bool ser = expected.Serialize("test.xml");
            bool des = actual.Deserialize("test.xml");
            //assert
            Assert.AreEqual(expected,actual);
            Assert.IsTrue(ser);
            Assert.IsTrue(des);
        }
        private static IEnumerable<object[]> TestMethodSerializeAndDesirealize()
        {
            return new[]
            {
                new object[] { new List<Student> { new Student("Сидоров", "Тест1", new DateTime(2010, 8, 7), 2), new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5) } },
                new object[] { new List<Student> { new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6), new Student("Птренко", "Тест1", new DateTime(2010, 8, 4), 7) } }
            };
        }
    }
}
