using MySerializer.Abstract;
using MySerializer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerializer.Resources
{
    public class Student : VersionHaver, ISerialize
    {
        public string Surname { get; set; }
        public string TestName { get; set; }
        public DateTime Date { get; set; }
        public int Mark { get; set; }
        public static Version Version { get; set; }

        public Student() { }
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

        public override bool Equals(object obj)
        {
            return obj != null && obj is Student student &&
                   Surname == student.Surname &&
                   TestName == student.TestName &&
                   Date == student.Date &&
                   Mark == student.Mark;
        }

        public override int GetHashCode()
        {
            int hashCode = 412176061;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TestName);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + Mark.GetHashCode();
            return hashCode;
        }
    }
}
