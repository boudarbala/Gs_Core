using Java.Util;
using Java.Util.Stream;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Graph.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using static Org.Graphstream.Graph.Implementations.AttributeChangeEvent;
using static Org.Graphstream.Graph.Implementations.ElementType;
using static Org.Graphstream.Graph.Implementations.EventType;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gs_Core.Graphstream.Graph.Implementations
{

    public class AdjacencyListGraph : AbstractGraph
    {
        public static readonly double GROW_FACTOR = 1.1;
        public static readonly int DEFAULT_NODE_CAPACITY = 128;
        public static readonly int DEFAULT_EDGE_CAPACITY = 1024;
        protected HashMap<string, AbstractNode> nodeMap;
        protected HashMap<string, AbstractEdge> edgeMap;
        protected AbstractNode[] nodeArray;
        protected AbstractEdge[] edgeArray;
        protected int nodeCount;
        protected int edgeCount;
        public AdjacencyListGraph(string id, bool strictChecking, bool autoCreate, int initialNodeCapacity, int initialEdgeCapacity) : base(id, strictChecking, autoCreate)
        {
            SetNodeFactory(new AnonymousNodeFactory(this));
            SetEdgeFactory(new AnonymousEdgeFactory(this));
            if (initialNodeCapacity < DEFAULT_NODE_CAPACITY)
                initialNodeCapacity = DEFAULT_NODE_CAPACITY;
            if (initialEdgeCapacity < DEFAULT_EDGE_CAPACITY)
                initialEdgeCapacity = DEFAULT_EDGE_CAPACITY;
            nodeMap = new HashMap<string, AbstractNode>(4 * initialNodeCapacity / 3 + 1);
            edgeMap = new HashMap<string, AbstractEdge>(4 * initialEdgeCapacity / 3 + 1);
            nodeArray = new AbstractNode[initialNodeCapacity];
            edgeArray = new AbstractEdge[initialEdgeCapacity];
            nodeCount = edgeCount = 0;
        }

        private sealed class AnonymousNodeFactory : NodeFactory
        {
            public AnonymousNodeFactory(AdjacencyListGraph parent)
            {
                this.parent = parent;
            }

            private readonly AdjacencyListGraph parent;
            public AdjacencyListNode NewInstance(string id, Graph graph)
            {
                return new AdjacencyListNode((AbstractGraph)graph, id);
            }
        }

        private sealed class AnonymousEdgeFactory : EdgeFactory
        {
            public AnonymousEdgeFactory(AdjacencyListGraph parent)
            {
                this.parent = parent;
            }

            private readonly AdjacencyListGraph parent;
            public AbstractEdge NewInstance(string id, Node src, Node dst, bool directed)
            {
                return new AbstractEdge(id, (AbstractNode)src, (AbstractNode)dst, directed);
            }
        }

        public AdjacencyListGraph(string id, bool strictChecking, bool autoCreate) : this(id, strictChecking, autoCreate, DEFAULT_NODE_CAPACITY, DEFAULT_EDGE_CAPACITY)
        {
        }

        public AdjacencyListGraph(string id) : this(id, true, false)
        {
        }

        protected override void AddEdgeCallback(AbstractEdge edge)
        {
            edgeMap.Put(edge.GetId(), edge);
            if (edgeCount == edgeArray.Length)
            {
                AbstractEdge[] tmp = new AbstractEdge[(int)(edgeArray.Length * GROW_FACTOR) + 1];
                System.Arraycopy(edgeArray, 0, tmp, 0, edgeArray.Length);
                Arrays.Fill(edgeArray, null);
                edgeArray = tmp;
            }

            edgeArray[edgeCount] = edge;
            edge.SetIndex(edgeCount++);
        }

        protected override void AddNodeCallback(AbstractNode node)
        {
            nodeMap.Put(node.GetId(), node);
            if (nodeCount == nodeArray.Length)
            {
                AbstractNode[] tmp = new AbstractNode[(int)(nodeArray.Length * GROW_FACTOR) + 1];
                System.Arraycopy(nodeArray, 0, tmp, 0, nodeArray.Length);
                Arrays.Fill(nodeArray, null);
                nodeArray = tmp;
            }

            nodeArray[nodeCount] = node;
            node.SetIndex(nodeCount++);
        }

        protected override void RemoveEdgeCallback(AbstractEdge edge)
        {
            edgeMap.Remove(edge.GetId());
            int i = edge.GetIndex();
            edgeArray[i] = edgeArray[--edgeCount];
            edgeArray[i].SetIndex(i);
            edgeArray[edgeCount] = null;
        }

        protected override void RemoveNodeCallback(AbstractNode node)
        {
            nodeMap.Remove(node.GetId());
            int i = node.GetIndex();
            nodeArray[i] = nodeArray[--nodeCount];
            nodeArray[i].SetIndex(i);
            nodeArray[nodeCount] = null;
        }

        protected override void ClearCallback()
        {
            nodeMap.Clear();
            edgeMap.Clear();
            Arrays.Fill(nodeArray, 0, nodeCount, null);
            Arrays.Fill(edgeArray, 0, edgeCount, null);
            nodeCount = edgeCount = 0;
        }

        public override Stream<Node> Nodes()
        {
            return Arrays.Stream(nodeArray, 0, nodeCount);
        }

        public override Stream<Edge> Edges()
        {
            return Arrays.Stream(edgeArray, 0, edgeCount);
        }

        public override Edge GetEdge(string id)
        {
            return edgeMap[id];
        }

        public override Edge GetEdge(int index)
        {
            if (index < 0 || index >= edgeCount)
                throw new IndexOutOfBoundsException("Edge " + index + " does not exist");
            return edgeArray[index];
        }

        public override int GetEdgeCount()
        {
            return edgeCount;
        }

        public override Node GetNode(string id)
        {
            return nodeMap[id];
        }

        public override Node GetNode(int index)
        {
            if (index < 0 || index > nodeCount)
                throw new IndexOutOfBoundsException("Node " + index + " does not exist");
            return nodeArray[index];
        }

        public override int GetNodeCount()
        {
            return nodeCount;
        }

        protected class EdgeIterator<T> : IEnumerator<T> where T : Edge
        {
            int iNext = 0;
            int iPrev = -1;
            public virtual bool HasNext()
            {
                return iNext < edgeCount;
            }

            public virtual T Next()
            {
                if (iNext >= edgeCount)
                    throw new NoSuchElementException();
                iPrev = iNext++;
                return (T)edgeArray[iPrev];
            }

            public virtual void Remove()
            {
                if (iPrev == -1)
                    throw new InvalidOperationException();
                RemoveEdge(edgeArray[iPrev], true, true, true);
                iNext = iPrev;
                iPrev = -1;
            }
        }

        protected class NodeIterator<T> : IEnumerator<T> where T : Node
        {
            int iNext = 0;
            int iPrev = -1;
            public virtual bool HasNext()
            {
                return iNext < nodeCount;
            }

            public virtual T Next()
            {
                if (iNext >= nodeCount)
                    throw new NoSuchElementException();
                iPrev = iNext++;
                return (T)nodeArray[iPrev];
            }

            public virtual void Remove()
            {
                if (iPrev == -1)
                    throw new InvalidOperationException();
                RemoveNode(nodeArray[iPrev], true);
                iNext = iPrev;
                iPrev = -1;
            }
        }
    }



}
