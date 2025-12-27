using gs_core.Graphstream.Graph.Implementations;
using Java.Util;
using gs_core.Graphstream.Graph;
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
    public class MultiNode : AdjacencyListNode
    {
        protected HashMap<AbstractNode, IList<AbstractEdge>> neighborMap;
        public MultiNode(AbstractGraph graph, string id) : base(graph, id)
        {
            neighborMap = new HashMap<AbstractNode, IList<AbstractEdge>>(4 * INITIAL_EDGE_CAPACITY / 3 + 1);
        }

        protected override T LocateEdge<T extends Edge>(Node opposite, char type)
        {
            IList<AbstractEdge> l = neighborMap[opposite];
            if (l == null)
                return null;
            foreach (AbstractEdge e in l)
            {
                char etype = EdgeType(e);
                if ((type != I_EDGE || etype != O_EDGE) && (type != O_EDGE || etype != I_EDGE))
                    return (T)e;
            }

            return null;
        }

        protected override void RemoveEdge(int i)
        {
            AbstractNode opposite = (AbstractNode)edges[i].GetOpposite(this);
            IList<AbstractEdge> l = neighborMap[opposite];
            l.Remove(edges[i]);
            if (l.IsEmpty())
                neighborMap.Remove(opposite);
            base.RemoveEdge(i);
        }

        protected override bool AddEdgeCallback(AbstractEdge edge)
        {
            AbstractNode opposite = (AbstractNode)edge.GetOpposite(this);
            IList<AbstractEdge> l = neighborMap[opposite];
            if (l == null)
            {
                l = new LinkedList<AbstractEdge>();
                neighborMap.Put(opposite, l);
            }

            l.Add(edge);
            return base.AddEdgeCallback(edge);
        }

        protected override void ClearCallback()
        {
            neighborMap.Clear();
            base.ClearCallback();
        }

        public virtual Collection<T> GetEdgeSetBetween<T extends Edge>(Node node)
        {
            IList<AbstractEdge> l = neighborMap[node];
            if (l == null)
                return Collections.EmptyList();
            return (Collection<T>)Collections.UnmodifiableList(l);
        }

        public virtual Collection<T> GetEdgeSetBetween<T extends Edge>(string id)
        {
            return GetEdgeSetBetween(graph.GetNode(id));
        }

        public virtual Collection<T> GetEdgeSetBetween<T extends Edge>(int index)
        {
            return GetEdgeSetBetween(graph.GetNode(index));
        }
    }



}

