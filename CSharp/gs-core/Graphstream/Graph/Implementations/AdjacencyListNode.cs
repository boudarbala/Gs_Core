using Java.Security;
using Java.Util;
using Java.Util.Stream;
using gs_core.Graphstream.Graph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using static gs_core.Graphstream.Graph.Implementations.AttributeChangeEvent;
using static gs_core.Graphstream.Graph.Implementations.ElementType;
using static gs_core.Graphstream.Graph.Implementations.EventType;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gs_Core.Graphstream.Graph.Implementations
{
    public class AdjacencyListNode : AbstractNode
    {
        protected static readonly int INITIAL_EDGE_CAPACITY;
        protected static readonly double GROWTH_FACTOR = 1.1;
        static AdjacencyListNode()
        {
            string p = "org.graphstream.graph.node.initialEdgeCapacity";
            int initialEdgeCapacity = 16;
            try
            {
                initialEdgeCapacity = Integer.ValueOf(System.GetProperty(p, "16"));
            }
            catch (AccessControlException e)
            {
            }

            INITIAL_EDGE_CAPACITY = initialEdgeCapacity;
        }

        protected static readonly char I_EDGE = 0;
        protected static readonly char IO_EDGE = 1;
        protected static readonly char O_EDGE = 2;
        protected AbstractEdge[] edges;
        protected int ioStart, oStart, degree;
        protected AdjacencyListNode(AbstractGraph graph, string id) : base(graph, id)
        {
            edges = new AbstractEdge[INITIAL_EDGE_CAPACITY];
            ioStart = oStart = degree = 0;
        }

        protected virtual char EdgeType(AbstractEdge e)
        {
            if (!e.directed || e.source == e.target)
                return IO_EDGE;
            return e.source == this ? O_EDGE : I_EDGE;
        }

        protected virtual T LocateEdge<T extends Edge>(Node opposite, char type)
        {
            int start = 0;
            int end = degree;
            if (type == I_EDGE)
                end = oStart;
            else if (type == O_EDGE)
                start = ioStart;
            for (int i = start; i < end; i++)
                if (edges[i].GetOpposite(this) == opposite)
                    return (T)edges[i];
            return null;
        }

        protected virtual void RemoveEdge(int i)
        {
            if (i >= oStart)
            {
                edges[i] = edges[--degree];
                edges[degree] = null;
                return;
            }

            if (i >= ioStart)
            {
                edges[i] = edges[--oStart];
                edges[oStart] = edges[--degree];
                edges[degree] = null;
                return;
            }

            edges[i] = edges[--ioStart];
            edges[ioStart] = edges[--oStart];
            edges[oStart] = edges[--degree];
            edges[degree] = null;
        }

        protected override bool AddEdgeCallback(AbstractEdge edge)
        {
            if (edges.Length == degree)
            {
                AbstractEdge[] tmp = new AbstractEdge[(int)(GROWTH_FACTOR * edges.Length) + 1];
                System.Arraycopy(edges, 0, tmp, 0, edges.Length);
                Arrays.Fill(edges, null);
                edges = tmp;
            }

            char type = EdgeType(edge);
            if (type == O_EDGE)
            {
                edges[degree++] = edge;
                return true;
            }

            if (type == IO_EDGE)
            {
                edges[degree++] = edges[oStart];
                edges[oStart++] = edge;
                return true;
            }

            edges[degree++] = edges[oStart];
            edges[oStart++] = edges[ioStart];
            edges[ioStart++] = edge;
            return true;
        }

        protected override void RemoveEdgeCallback(AbstractEdge edge)
        {
            char type = EdgeType(edge);
            int i = 0;
            if (type == IO_EDGE)
                i = ioStart;
            else if (type == O_EDGE)
                i = oStart;
            while (edges[i] != edge)
                i++;
            RemoveEdge(i);
        }

        protected override void ClearCallback()
        {
            Arrays.Fill(edges, 0, degree, null);
            ioStart = oStart = degree = 0;
        }

        public override int GetDegree()
        {
            return degree;
        }

        public override int GetInDegree()
        {
            return oStart;
        }

        public override int GetOutDegree()
        {
            return degree - ioStart;
        }

        public override Edge GetEdge(int i)
        {
            if (i < 0 || i >= degree)
                throw new IndexOutOfBoundsException("Node \"" + this + "\"" + " has no edge " + i);
            return edges[i];
        }

        public override Edge GetEnteringEdge(int i)
        {
            if (i < 0 || i >= GetInDegree())
                throw new IndexOutOfBoundsException("Node \"" + this + "\"" + " has no entering edge " + i);
            return edges[i];
        }

        public override Edge GetLeavingEdge(int i)
        {
            if (i < 0 || i >= GetOutDegree())
                throw new IndexOutOfBoundsException("Node \"" + this + "\"" + " has no edge " + i);
            return edges[ioStart + i];
        }

        public override Edge GetEdgeBetween(Node node)
        {
            return LocateEdge(node, IO_EDGE);
        }

        public override Edge GetEdgeFrom(Node node)
        {
            return LocateEdge(node, I_EDGE);
        }

        public override Edge GetEdgeToward(Node node)
        {
            return LocateEdge(node, O_EDGE);
        }

        public override Stream<Edge> Edges()
        {
            return Arrays.Stream(edges, 0, degree);
        }

        public override Stream<Edge> EnteringEdges()
        {
            return Arrays.Stream(edges, 0, oStart);
        }

        public override Stream<Edge> LeavingEdges()
        {
            return Arrays.Stream(edges, ioStart, degree);
        }

        protected class EdgeIterator<T> : IEnumerator<T> where T : Edge
        {
            protected int iPrev, iNext, iEnd;
            protected EdgeIterator(char type)
            {
                iPrev = -1;
                iNext = 0;
                iEnd = degree;
                if (type == I_EDGE)
                    iEnd = oStart;
                else if (type == O_EDGE)
                    iNext = ioStart;
            }

            public virtual bool HasNext()
            {
                return iNext < iEnd;
            }

            public virtual T Next()
            {
                if (iNext >= iEnd)
                    throw new NoSuchElementException();
                iPrev = iNext++;
                return (T)edges[iPrev];
            }

            public virtual void Remove()
            {
                if (iPrev == -1)
                    throw new InvalidOperationException();
                AbstractEdge e = edges[iPrev];
                graph.RemoveEdge(e, true, e.source != this, e.target != this);
                RemoveEdge(iPrev);
                iNext = iPrev;
                iPrev = -1;
                iEnd--;
            }
        }
    }


}

