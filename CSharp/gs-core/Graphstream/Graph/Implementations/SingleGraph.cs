using gs_core.Graphstream.Graph.Implementations;
using gs_core.Graphstream.Graph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static gs_core.Graphstream.Graph.Implementations.AttributeChangeEvent;
using static gs_core.Graphstream.Graph.Implementations.ElementType;
using static gs_core.Graphstream.Graph.Implementations.EventType;

namespace Gs_Core.Graphstream.Graph.Implementations
{

    public class SingleGraph : AdjacencyListGraph
    {
        public SingleGraph(string id, bool strictChecking, bool autoCreate, int initialNodeCapacity, int initialEdgeCapacity) : base(id, strictChecking, autoCreate, initialNodeCapacity, initialEdgeCapacity)
        {
            SetNodeFactory(new AnonymousNodeFactory(this));
        }

        private sealed class AnonymousNodeFactory : NodeFactory
        {
            public AnonymousNodeFactory(SingleGraph parent)
            {
                this.parent = parent;
            }

            private readonly SingleGraph parent;
            public SingleNode NewInstance(string id, Graph graph)
            {
                return new SingleNode((AbstractGraph)graph, id);
            }
        }

        public SingleGraph(string id, bool strictChecking, bool autoCreate) : this(id, strictChecking, autoCreate, DEFAULT_NODE_CAPACITY, DEFAULT_EDGE_CAPACITY)
        {
        }

        public SingleGraph(string id) : this(id, true, false)
        {
        }
    }


}
