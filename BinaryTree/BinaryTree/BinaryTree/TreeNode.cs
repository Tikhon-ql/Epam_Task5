
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyBinaryTree
{
    //Binary tree node class
    public class TreeNode<T>:IComparable<T> where T : IComparable
    {
        /// <summary>
        /// Left node
        /// </summary>
        public TreeNode<T> Left { get; set; }
        /// <summary>
        /// Right node
        /// </summary>
        public TreeNode<T> Right { get; set; }
        /// <summary>
        /// Node's data
        /// </summary>
        public T Data { get; set; }
        public TreeNode()
        {

        }
        public TreeNode(T data)
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
    }
}
