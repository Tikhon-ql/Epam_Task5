
using MySerializer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySerializer.Resources
{
    [Serializable]
    public class StudentsList : IVersionHaver, ICollection<Student>,ISerialize
    {
        List<Student> students = new List<Student>();
        public int Count => students.Count;

        public bool IsReadOnly => false;

        public Version Version { get; set; } = new Version("1.0.0.0");

        public void Add(Student item)
        {
            students.Add(item);
        }
        public StudentsList(params Student[] students)
        {
            foreach(Student item in students)
            {
                this.Add(item);
            }
        }
        public void Clear()
        {
            students.Clear();
        }

        public bool Contains(Student item)
        {
            return students.Contains(item);
        }

        public void CopyTo(Student[] array, int arrayIndex)
        {
            students.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return students.GetEnumerator();
        }

        public bool Remove(Student item)
        {
            return students.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return students.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is StudentsList list &&
                   Version == list.Version &&
                   students.SequenceEqual(list.students) &&
                   Count == list.Count;
        }

        public override int GetHashCode()
        {
            int hashCode = -695961709;
            hashCode = hashCode * -1521134295 + EqualityComparer<Version>.Default.GetHashCode(Version);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Student>>.Default.GetHashCode(students);
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            hashCode = hashCode * -1521134295 + IsReadOnly.GetHashCode();
            return hashCode;
        }
    }
}
