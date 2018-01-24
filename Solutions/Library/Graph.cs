using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Solutions.Library
{
    public class Graph<T>
    {
        public ICollection<GraphNode<T>> Nodes { get; set; }

        public Graph()
        {
            this.Nodes = new Collection<GraphNode<T>>();
        }

        public Graph(Collection<GraphNode<T>> nodes)
        {
            this.Nodes = nodes;
        }
    }
}
