using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace Solutions.Library
{
    // https://msdn.microsoft.com/en-us/library/ms379572(v=vs.80).aspx
    public class BinaryTree<T>
    {
        private IComparer<T> comparer;
        public BinaryTreeNode<T> Root { get; set; }

        public BinaryTree(IComparer<T> comparer) : this(comparer, null) { }

        public BinaryTree(IComparer<T> comparer, BinaryTreeNode<T> root)
        {
            this.Root = root;
            this.comparer = comparer;
        }

        public virtual void Clear()
        {
            this.Root = null;
        }

        public bool Contains(T data)
        {
            // search the tree for a node that contains data
            BinaryTreeNode<T> current = this.Root;
            int result;
            while (current != null)
            {
                result = comparer.Compare(current.Data, data);
                if (result == 0)
                    // we found data
                    return true;
                else if (result > 0)
                    // current.Value > data, search current's left subtree
                    current = current.Left;
                else if (result < 0)
                    // current.Value < data, search current's right subtree
                    current = current.Right;
            }

            return false;       // didn't find data
        }

        public virtual void Add(T data)
        {
            // create a new Node instance
            BinaryTreeNode<T> n = new BinaryTreeNode<T>(data);
            int result;

            // now, insert n into the tree
            // trace down the tree until we hit a NULL
            BinaryTreeNode<T> current = this.Root, parent = null;
            while (current != null)
            {
                result = comparer.Compare(current.Data, data);
                if (result == 0)
                    // they are equal - attempting to enter a duplicate - do nothing
                    return;
                else if (result > 0)
                {
                    // current.Value > data, must add n to current's left subtree
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // current.Value < data, must add n to current's right subtree
                    parent = current;
                    current = current.Right;
                }
            }

            // We're ready to add the node!
            if (parent == null)
            {
                // the tree was empty, make n the root
                this.Root = n;
            }
            else
            {
                result = comparer.Compare(parent.Data, data);
                if (result > 0)
                    // parent.Value > data, therefore n must be added to the left subtree
                    parent.Left = n;
                else
                    // parent.Value < data, therefore n must be added to the right subtree
                    parent.Right = n;
            }
        }

        public bool Remove(T data)
        {
            // first make sure there exist some items in this tree
            if (this.Root == null)
                return false;       // no items to remove

            // Now, try to find data in the tree
            BinaryTreeNode<T> current = this.Root, parent = null;
            int result = comparer.Compare(current.Data, data);
            while (result != 0)
            {
                if (result > 0)
                {
                    // current.Value > data, if data exists it's in the left subtree
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // current.Value < data, if data exists it's in the right subtree
                    parent = current;
                    current = current.Right;
                }

                // If current == null, then we didn't find the item to remove
                if (current == null)
                    return false;
                else
                    result = comparer.Compare(current.Data, data);
            }

            // At this point, we've found the node to remove

            // We now need to "rethread" the tree
            // CASE 1: If current has no right child, then current's left child becomes
            //         the node pointed to by the parent
            if (current.Right == null)
            {
                if (parent == null) { this.Root = current.Left; }
                else
                {
                    result = comparer.Compare(parent.Data, current.Data);
                    if (result > 0)
                        // parent.Value > current.Value, so make current's left child a left child of parent
                        parent.Left = current.Left;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's left child a right child of parent
                        parent.Right = current.Left;
                }
            }
            // CASE 2: If current's right child has no left child, then current's right child
            //         replaces current in the tree
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null) { this.Root = current.Right; }
                else
                {
                    result = comparer.Compare(parent.Data, current.Data);
                    if (result > 0)
                        // parent.Value > current.Value, so make current's right child a left child of parent
                        parent.Left = current.Right;
                    else if (result < 0)
                        // parent.Value < current.Value, so make current's right child a right child of parent
                        parent.Right = current.Right;
                }
            }
            // CASE 3: If current's right child has a left child, replace current with current's
            //          right child's left-most descendent
            else
            {
                // We first need to find the right node's left-most child
                BinaryTreeNode<T> leftmost = current.Right.Left, lmParent = current.Right;
                while (leftmost.Left != null)
                {
                    lmParent = leftmost;
                    leftmost = leftmost.Left;
                }

                // the parent's left subtree becomes the leftmost's right subtree
                lmParent.Left = leftmost.Right;

                // assign leftmost's left and right to current's left and right children
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                {
                    this.Root = leftmost;
                }
                else
                {
                    result = comparer.Compare(parent.Data, current.Data);
                    if (result > 0)
                        // parent.Value > current.Value, so make leftmost a left child of parent
                        parent.Left = leftmost;
                    else if (result < 0)
                        // parent.Value < current.Value, so make leftmost a right child of parent
                        parent.Right = leftmost;
                }
            }

            return true;
        }
    }
}
