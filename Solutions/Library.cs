﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Solutions.Library
{
    [DebuggerDisplay("Data = {Data}")]
    public class LinkedListNode<T>
    {
        private T value;

        public LinkedListNode<T> Next { get; set; }
        public LinkedListNode<T> Prev { get; set; }
        public T Data { get; set; }

        public LinkedListNode(T data, LinkedListNode<T> next, LinkedListNode<T> previous)
        {
            Data = data;
            SetNext(next);
            SetPrevious(previous);
        }

        public LinkedListNode(T value)
        {
            this.value = value;
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
