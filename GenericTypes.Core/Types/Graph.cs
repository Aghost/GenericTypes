using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericTypes.Core.Types
{
    public class Graph<T> {
        public List<GraphNode<T>> Nodes;

        public Graph() { }

        public void Add(GraphNode<T> graphNode) { }
    }

    public class GraphNode<T> {
        public int Id;
        public T Data;
        public List<GraphNode<T>> Neighbors;

        public GraphNode() { }

        public int Test() {
            return Id;
        }
    }
}
