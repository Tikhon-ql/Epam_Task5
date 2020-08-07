
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
        public int Count { get; set; }
        public TreeNode<T> Root { get; set; }

        public BinaryTree(params T[] data)
        {
            foreach (T item in data)
                Add(item);
        }
        public object Current => this;

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
        public bool Serialize(string filename)
        {
            try
            {
                List<T> list = new List<T>();
                FillTreeNodeDataInList(Root, list);
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

        private void FillTreeNodeDataInList(TreeNode<T> node, List<T> list)
        {
            if (node != null)
            {
                FillTreeNodeDataInList(node.Left, list);
                list.Add(node.Data);
                FillTreeNodeDataInList(node.Right, list);
            }
        }

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

        private void AddRange(List<T> list)
        {
            foreach(T item in list)
            {
                Add(item);
            }
        }

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
        public T Search(T data)
        { 
            TreeNode<T> current = Root;
            while (current != null)
            {
                int res = current.CompareTo(data);
                if (res < 0)
                    current = current.Left;
                else
                {
                    if (res > 0)
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
            //InOrder(Root);
            //return null;
            throw new NotImplementedException();
        }


        public void InOrder(TreeNode<T> node)
        {
            if(node != null)
            {
                InOrder(node.Left);
                InOrder(node.Right);
            }
        }

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
        private void FillList(TreeNode<T> node,ICollection<TreeNode<T>> list)
        {
            if(node != null)
            {
                FillList(node.Left, list);
                list.Add(node);
                FillList(node.Right,list);
            }
        }
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
        //public T GetElement(int index)
        //{
        //    if (index < Count)
        //    {
        //        TreeNode<T> tmp = Root;
        //        for(int i = 0; i < Count; i++)
        //        {
        //            if(i != index)
        //            {
        //                tmp 
        //            }
        //        }
        //    }
        //    else
        //        throw new IndexOutOfRangeException();
        //}
    }
}