using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Solutions.Library
{
    public enum GraphNodeState
    {
        Unvisited, Visited, Visiting
    }

    public class GraphNode<T> : Node<T>
    {
        public ICollection<GraphNode<T>> Adjacent { get; set; }
        public GraphNodeState State { get; set; }

        public GraphNode(T data) : base(data)
        {
            this.Adjacent = new Collection<GraphNode<T>>();
            this.State = GraphNodeState.Unvisited;
        }
    }
}
