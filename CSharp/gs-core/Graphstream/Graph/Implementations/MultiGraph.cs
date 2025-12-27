using gs_core.Graphstream.Graph;
using Gs_Core.Graphstream.Graph.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static gs_core.Graphstream.Graph.Implementations.AttributeChangeEvent;
using static gs_core.Graphstream.Graph.Implementations.ElementType;
using static gs_core.Graphstream.Graph.Implementations.EventType;

namespace Gs_Core.Graphstream.Graph.Implementations
{

    public class MultiGraph : AdjacencyListGraph
    {
        public MultiGraph(string id, bool strictChecking, bool autoCreate, int initialNodeCapacity, int initialEdgeCapacity) : base(id, strictChecking, autoCreate, initialNodeCapacity, initialEdgeCapacity)
        {
            SetNodeFactory(new AnonymousNodeFactory(this));
        }

        private sealed class AnonymousNodeFactory : NodeFactory
        {
            public AnonymousNodeFactory(MultiGraph parent)
            {
                this.parent = parent;
            }

            private readonly MultiGraph parent;
            public MultiNode NewInstance(string id, Graph graph)
            {
                return new MultiNode((AbstractGraph)graph, id);
            }
        }

        public MultiGraph(string id, bool strictChecking, bool autoCreate) : this(id, strictChecking, autoCreate, DEFAULT_NODE_CAPACITY, DEFAULT_EDGE_CAPACITY)
        {
        }

        public MultiGraph(string id) : this(id, true, false)
        {
        }
    }


}
