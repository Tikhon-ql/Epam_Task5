
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BinaryTree
{
    [Serializable]
    public class TreeNode<T>:IComparable<T> where T : IComparable
    {
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        public int Height { get; set; }
        public T Data { get; set; }
        public TreeNode()
        {

        }
        public TreeNode(T data)
        {
            Data = data;
        }
        public TreeNode(T data,TreeNode<T> parent)
        {
            Data = data;
        }
        public override string ToString()
        {
            return Data.ToString();
        }

        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }
        //public void Serialize(string filename)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(T));
        //    using(StreamWriter writer = new StreamWriter(filename,true))
        //    {
        //        serializer.Serialize(writer,Data);
        //    }
        //}
    }
}
