using BinaryTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;
using BinaryTree.Resources;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<Student> tree = new BinaryTree<Student>();
            tree.Add(new Student("A","A",new DateTime(10,10,10),5));
            tree.Add(new Student("B","A",new DateTime(11,10,10),7));
            tree.Add(new Student("C","A",new DateTime(12,10,10),8));
            tree.Add(new Student("D","A",new DateTime(13,10,10),9));
            tree.Add(new Student("E","A",new DateTime(14,10,10),10));
            tree.Add(new Student("F","A",new DateTime(15,10,10),4));
            tree.Remove(new Student("A", "A", new DateTime(10, 10, 10), 5));
            Console.WriteLine();
            tree.Balance();
            tree.Serialize(@"D:\Тихон\Авы\Task5\BinaryTree\BinaryTree\bin\Debug\test.xml");
            BinaryTree<Student> newTree = new BinaryTree<Student>();
            bool res = newTree.Deserialize(@"D:\Тихон\Авы\Task5\BinaryTree\BinaryTree\bin\Debug\test.xml");
            Console.WriteLine(res);
            Console.ReadKey();
        }
    }
}
