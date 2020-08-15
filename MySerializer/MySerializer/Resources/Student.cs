using MySerializer.Abstract;
using MySerializer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MySerializer.Resources
{
    [Serializable]
    public class Student : VersionHaver, ISerialize
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Student() { }
        public Student(string surname,string name,int age)
        {
            Surname = surname;
            Name = name;
            Age = age;
        }
        public override string ToString()
        {
            return Surname + ";" + Name + ";" + Age.ToString();
        }

        public int CompareTo(object obj)
        {
            return Age - ((Student)obj).Age;
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Student student &&
                   Surname == student.Surname &&
                   Name == student.Name &&
                   Age == student.Age;
        }

        public override int GetHashCode()
        {
            int hashCode = 1871828855;
            hashCode = hashCode * -1521134295 + EqualityComparer<Version>.Default.GetHashCode(Version);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Age.GetHashCode();
            return hashCode;
        }
    }
}
