using System;
using System.Collections.Generic;
using System.Text;

namespace Solutions.Library
{
    public class BinaryTreeNode<T> : Node<T>
    {
        public BinaryTreeNode<T> Parent { get; set; }

        private BinaryTreeNode<T> left;
        public BinaryTreeNode<T> Left
        {
            get
            {
                return this.left;
            }
            set
            {
                this.left = value;

                if (this.left != null)
                {
                    this.left.Parent = this;
                }
            }
        }

        private BinaryTreeNode<T> right;
        public BinaryTreeNode<T> Right
        {
            get
            {
                return this.right;
            }
            set
            {
                this.right = value;

                if (this.right != null)
                {
                    this.right.Parent = this;
                }
            }
        }

        public BinaryTreeNode() : base() { }
        public BinaryTreeNode(T data) : base(data) { }
        public BinaryTreeNode(T data, BinaryTreeNode<T> left, BinaryTreeNode<T> right) : base(data)
        {
            this.Left = left;
            this.Right = right;
        }
    }
}
