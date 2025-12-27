using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static gs_core.Graphstream.Graph.Implementations.ElementType;
using static gs_core.Graphstream.Graph.Implementations.EventType;
using static gs_core.Graphstream.Graph.Implementations.AttributeChangeEvent;

namespace Gs_Core.Graphstream.Graph.Implementations
{
    public class DefaultGraph : SingleGraph
    {
        public DefaultGraph(string id, bool strictChecking, bool autoCreate, int initialNodeCapacity, int initialEdgeCapacity) : base(id, strictChecking, autoCreate, initialNodeCapacity, initialEdgeCapacity)
        {
        }

        public DefaultGraph(string id, bool strictChecking, bool autoCreate) : base(id, strictChecking, autoCreate)
        {
        }

        public DefaultGraph(string id) : base(id)
        {
        }
    }



}

