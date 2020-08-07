using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree.Resources
{
    [Serializable]
    public class Student:IComparable
    {
        public string Surname { get; set; }
        public string TestName { get; set; }
        public DateTime Date { get; set; }
        public int Mark { get; set; }

        public Student() {}
        public Student(string surname, string testName, DateTime date, int mark)
        {
            Surname = surname;
            TestName = testName;
            Date = date;
            Mark = mark;
        }
        public override string ToString()
        {
            return Surname + ";" + TestName + ";" + Date.ToShortDateString() + ";" + Mark.ToString();
        }

        public int CompareTo(object obj)
        {
            return Mark - ((Student)obj).Mark;
        }
    }
}
