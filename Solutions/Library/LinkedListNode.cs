using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Solutions.Library
{
    public class LinkedListNode<T> : Node<T>
    {
        public LinkedListNode<T> Next{ get; set; }
        public LinkedListNode<T> Prev { get; set; }

        public LinkedListNode(T data) : base(data) { }
        public LinkedListNode(T data, LinkedListNode<T> next, LinkedListNode<T> previous) : base(data)
        {
            SetNext(next);
            SetPrevious(previous);
        }

        public void SetNext(LinkedListNode<T> next)
        {
            Next = next;
            if (next != null && next.Prev != this)
            {
                next.SetPrevious(this);
            }
        }

        public void SetPrevious(LinkedListNode<T> prev)
        {
            Prev = prev;
            if (prev != null && prev.Next != this)
            {
                prev.SetNext(this);
            }
        }
    }

}
