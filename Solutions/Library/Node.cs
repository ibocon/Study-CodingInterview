using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace Solutions.Library
{
    [DebuggerDisplay("Data = {Data}")]
    public class Node<T>
    {
        public T Data { get; set; }

        public Node() { }
        public Node(T data)
        {
            this.Data = data;
        }
    }
}
