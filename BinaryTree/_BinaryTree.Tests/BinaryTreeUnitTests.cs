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
        [TestMethod]
        [DynamicData(nameof(TestMethodAdd),DynamicDataSourceType.Method)]
        public void Add_Student_Method_Test(Student student)
        {
            //arrange
            BinaryTree<Student> tree = new BinaryTree<Student>();
            //act
            bool actual = tree.Add(student);
            //assert
            Assert.IsTrue(actual);
        }
        private static IEnumerable<Student> TestMethodAdd()
        {
            yield return new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5);
            yield return new Student("Сидоров", "Тест1", new DateTime(2012, 11, 10), 7);
            yield return new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6);
        }

        [TestMethod]
        [DynamicData(nameof(TestMethodRemove),DynamicDataSourceType.Method)]
        public void Remove_Student_Method_Test(Student student)
        {
            //arrange
            BinaryTree<Student> tree = new BinaryTree<Student>(new Student("Иванов", "Тест1", new DateTime(2010, 10, 10),5), new Student("Смирнов", "Тест1", new DateTime(2010, 10, 10),8), new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6));
            //act
            bool actual = tree.Remove(student);
            //assert
            Assert.IsTrue(actual);
        }

        private static IEnumerable<Student> TestMethodRemove()
        {
            yield return new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5);
            yield return new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6);
        }

        [TestMethod]
        [DynamicData(nameof(TestMethodSearch),DynamicDataSourceType.Method)]
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
        private static IEnumerable<Student> TestMethodSearch()
        {
            yield return new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5);
            yield return new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 6);
        }

        [TestMethod]
        public void Balance_BinaryTree_Methode_Test(Student student)
        {
            //arrange
            BinaryTree<Student> tree = new BinaryTree<Student>(new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5), new Student("Смирнов", "Тест1", new DateTime(2010, 10, 10), 6), new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 8));
            BinaryTree<Student> expected = new BinaryTree<Student>(new Student("Смирнов", "Тест1", new DateTime(2010, 10, 10), 6), new Student("Иванов", "Тест1", new DateTime(2010, 10, 10), 5), new Student("Петров", "Тест1", new DateTime(2008, 9, 11), 8));

            //act

            tree.Balance();

            //assert
            Assert.AreEqual(expected,tree);
        }
    }
}
