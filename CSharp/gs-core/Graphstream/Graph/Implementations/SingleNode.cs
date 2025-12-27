using gs_core.Graphstream.Graph;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using static gs_core.Graphstream.Graph.Implementations.AttributeChangeEvent;
using static gs_core.Graphstream.Graph.Implementations.ElementType;
using static gs_core.Graphstream.Graph.Implementations.EventType;

namespace Gs_Core.Graphstream.Graph.Implementations
{
    public class SingleNode : AdjacencyListNode
    {
        protected class TwoEdges
        {
            protected AbstractEdge in, out;
    }

        protected HashMap<AbstractNode, TwoEdges> neighborMap;
        protected SingleNode(AbstractGraph graph, string id) : base(graph, id)
        {
            neighborMap = new HashMap<AbstractNode, TwoEdges>(4 * INITIAL_EDGE_CAPACITY / 3 + 1);
        }

        protected override T LocateEdge<T extends Edge>(Node opposite, char type)
        {
            TwoEdges ee = neighborMap[opposite];
            if (ee == null)
                return null;
            if (type == IO_EDGE)
                return (T)(ee.@in == null ? ee.@out : ee.@in);
            return (T)(type == I_EDGE ? ee.@in : ee.@out);
        }

        protected override void RemoveEdge(int i)
        {
            AbstractNode opposite = (AbstractNode)edges[i].GetOpposite(this);
            TwoEdges ee = neighborMap[opposite];
            char type = EdgeType(edges[i]);
            if (type != O_EDGE)
                ee.@in = null;
            if (type != I_EDGE)
                ee.@out = null;
            if (ee.@in == null && ee.@out == null)
                neighborMap.Remove(opposite);
            base.RemoveEdge(i);
        }

        protected override bool AddEdgeCallback(AbstractEdge edge)
        {
            AbstractNode opposite = (AbstractNode)edge.GetOpposite(this);
            TwoEdges ee = neighborMap[opposite];
            if (ee == null)
                ee = new TwoEdges();
            char type = EdgeType(edge);
            if (type != O_EDGE)
            {
                if (ee.@in != null)
                    return false;
                ee.@in = edge;
            }

            if (type != I_EDGE)
            {
                if (ee.@out != null)
                    return false;
                ee.@out = edge;
            }

            neighborMap.Put(opposite, ee);
            return base.AddEdgeCallback(edge);
        }

        protected override void ClearCallback()
        {
            neighborMap.Clear();
            base.ClearCallback();
        }
    }




}

