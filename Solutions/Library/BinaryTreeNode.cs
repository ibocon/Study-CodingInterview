using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions.Library
{
    public class BinaryTreeNode<T> : Node<T>
    {
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public BinaryTreeNode() : base() { }
        public BinaryTreeNode(T data) : base(data) { }
        public BinaryTreeNode(T data, BinaryTreeNode<T> left, BinaryTreeNode<T> right) : base(data)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
