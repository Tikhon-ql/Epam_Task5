
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MyBinaryTree
{
    public class BinaryTree<T>: IEnumerable<T> where T : IComparable
    {
        /// <summary>
        /// Node's count
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Tree's root
        /// </summary>
        public TreeNode<T> Root { get; set; }

        public BinaryTree(params T[] data)
        {
            foreach (T item in data)
                Add(item);
        }
        /// <summary>
        /// Adding node method
        /// </summary>
        /// <param name="data">Node's data</param>
        /// <returns></returns>
        public bool Add(T data)
        {
            try
            {
                if (Root == null)
                    Root = new TreeNode<T>(data);
                else
                    AddTo(Root, data);
                Count++;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void AddTo(TreeNode<T> node,T data)
        {
           if(data.CompareTo(node.Data) < 0)
            {
                if (node.Left == null)
                {
                    node.Left = new TreeNode<T>(data);
                }
                   
                else
                    AddTo(node.Left, data);
            }
            else
            {
                if (data.CompareTo(node.Data) > 0)
                {
                    if (node.Right == null)
                    {
                        node.Right = new TreeNode<T>(data);
                    }
                    else
                        AddTo(node.Right, data);
                }
                else
                    throw new Exception();
            }
        }

        /// <summary>
        /// Serializeing tree method
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Serialize(string filename)
        {
            try
            {
                List<T> list = new List<T>();
                FillListByTreeNodeData(Root, list);
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    serializer.Serialize(writer, list);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Filling list by tree node data method
        /// </summary>
        /// <param name="node"></param>
        /// <param name="list"></param>
        private void FillListByTreeNodeData(TreeNode<T> node, List<T> list)
        {
            if (node != null)
            {
                FillListByTreeNodeData(node.Left, list);
                list.Add(node.Data);
                FillListByTreeNodeData(node.Right, list);
            }
        }
        /// <summary>
        /// Deserializeing tree method
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Deserialize(string filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (StreamReader reader = new StreamReader(filename))
                {
                    List<T> list = (List<T>)serializer.Deserialize(reader);
                    AddRange(list);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Adding list method
        /// </summary>
        /// <param name="list"></param>
        private void AddRange(List<T> list)
        {
            foreach(T item in list)
            {
                Add(item);
            }
        }
        /// <summary>
        /// Remove tree node method
        /// </summary>
        /// <param name="data">Remove data</param>
        /// <returns></returns>
        public bool Remove(T data)
        {
            try
            {
                TreeNode<T> parent = new TreeNode<T>();
                TreeNode<T> current = FindWithParent(data,out parent);
                if (current == null)
                    return false;
                Count--;
                if(current.Right == null)
                {
                    if (parent == null)
                        Root = current.Left;
                    else
                    {
                        int result = parent.CompareTo(current.Data);
                        if (result > 0)
                            parent.Left = current.Left;
                        else
                        {
                            if(result < 0)
                            {
                                parent.Right = current.Left;
                            }
                        }
                    }
                }
                else
                {
                    if(current.Right.Left == null)
                    {
                        current.Right.Left = current.Left;
                        if (parent == null)
                            Root = current.Right;
                        else
                        {
                            int result = parent.CompareTo(current.Data);
                            if (result > 0)
                                parent.Left = current.Right;
                            else
                            {
                                if (result < 0)
                                {
                                    parent.Right = current.Right;
                                }
                            }
                        }
                    }
                    else
                    {
                        TreeNode<T> leftMost = current.Right.Left;
                        TreeNode<T> leftMostParent = current.Right;

                        while(leftMost.Left != null)
                        {
                            leftMostParent = leftMost;
                            leftMost = leftMost.Left;
                        }

                        leftMostParent.Left = leftMost.Right;
                        leftMost.Left = current.Left;
                        leftMost.Right = current.Right;
                        if (parent == null)
                            Root = leftMost;
                        else
                        {
                            int result = parent.CompareTo(current.Data);
                            if (result > 0)
                                parent.Left = leftMost;
                            else
                            {
                                if (result < 0)
                                {
                                    parent.Right = leftMost;
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private TreeNode<T> FindMin(TreeNode<T> node)
        {
            if (node == null)
                return null;

            TreeNode<T> right = FindMin(node.Right);
            TreeNode<T> left = FindMin(node.Left);

            if (node.CompareTo(left.Data) > 0)
            {
                if (node.CompareTo(right.Data) > 0)
                    return node;
                else
                    return right;
            }
            else
            {
                if (left.CompareTo(right.Data) > 0)
                    return left;
                else
                    return right;
            }
        }
        /// <summary>
        /// Searching data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Search(T data)
        { 
            TreeNode<T> current = Root;
            while (current != null)
            {
                int res = current.CompareTo(data);
                if (res > 0)
                    current = current.Left;
                else
                {
                    if (res < 0)
                        current = current.Right;
                    else
                        break;
                }
            }
            return current.Data;
        }
        private TreeNode<T> FindWithParent(T data,out TreeNode<T> parent)
        {
            TreeNode<T> current = Root;
            parent = null;
            while(current != null)
            {
                int result = current.CompareTo(data);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else
                {
                    if (result < 0)
                    {
                        parent = current;
                        current = current.Right;
                    }
                    else
                        break;
                }
            }
            return current;
        }
        public int CompareTo(T other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Balancing binary tree
        /// </summary>
        public void Balance()
        {
            List<TreeNode<T>> list = new List<TreeNode<T>>();
            FillList(Root, list);
            RemoveAll(list);
            Root = null;
            int count = Count;
            Count = 0;
            BalanceBinaryTree(0,count - 1,list);
        }
        private void BalanceBinaryTree(int min,int max, List<TreeNode<T>> list)
        {
            if(min <= max)
            {
                int midle = Convert.ToInt32( Math.Ceiling ( (double)min + max ) / 2);
                Add(list[midle].Data);
                BalanceBinaryTree(min,midle - 1,list);
                BalanceBinaryTree(midle + 1,max,list);
            }
        }
        /// <summary>
        /// Filling list by tree nodes
        /// </summary>
        /// <param name="node"></param>
        /// <param name="list">List</param>
        private void FillList(TreeNode<T> node,ICollection<TreeNode<T>> list)
        {
            if(node != null)
            {
                FillList(node.Left, list);
                list.Add(node);
                FillList(node.Right,list);
            }
        }
        /// <summary>
        /// Remove all tree nodes
        /// </summary>
        /// <param name="list"></param>
        private void RemoveAll(List<TreeNode<T>> list)
        {
            foreach(TreeNode<T> item in list)
            {
                item.Right = null;
                item.Left = null;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        private bool IsEquals(TreeNode<T> node1,TreeNode<T> node2)
        {
            if(node1 != null && node2 != null)
            {
                if (!node1.Data.Equals(node2.Data))
                    return false;
                IsEquals(node1.Left,node2.Left);
                IsEquals(node1.Right, node2.Right);
            }
            return true;
        }
        public override bool Equals(object obj)
        {
            List<T> list1 = new List<T>();
            List<T> list2 = new List<T>();
            FillListByTreeNodeData(Root, list1);
            FillListByTreeNodeData(((BinaryTree<T>)obj).Root, list2);
            return list1.SequenceEqual(list2);
        }

        public override int GetHashCode()
        {
            int hashCode = 1823673881;
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TreeNode<T>>.Default.GetHashCode(Root);
            return hashCode;
        }
    }
}