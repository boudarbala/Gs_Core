using Java.Util;
using Java.Util.Stream;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Graph.Implementations;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Ui.View;
using Gs_Core.Graphstream.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using static Org.Graphstream.Graph.Implementations.AttributeChangeEvent;
using static Org.Graphstream.Graph.Implementations.ElementType;
using static Org.Graphstream.Graph.Implementations.EventType;

namespace Gs_Core.Graphstream.Graph.Implementations
{
    public abstract class AbstractGraph : AbstractElement, Graph, Replayable
    {
        GraphListeners listeners;
        private bool strictChecking;
        private bool autoCreate;
        private NodeFactory<TWildcardTodoAbstractNode> nodeFactory;
        private EdgeFactory<TWildcardTodoAbstractEdge> edgeFactory;
        private double step = 0;
        private long replayId = 0;
        public AbstractGraph(string id) : this(id, true, false)
        {
        }

        public AbstractGraph(string id, bool strictChecking, bool autoCreate) : base(id)
        {
            this.strictChecking = strictChecking;
            this.autoCreate = autoCreate;
            this.listeners = new GraphListeners(this);
        }

        protected override void AttributeChanged(AttributeChangeEvent @event, string attribute, object oldValue, object newValue)
        {
            listeners.SendAttributeChangedEvent(id, SourceBase.ElementType.GRAPH, attribute, @event, oldValue, newValue);
        }

        public override IEnumerator<Node> Iterator()
        {
            return Nodes().Iterator();
        }

        public override NodeFactory<TWildcardTodoNode> NodeFactory()
        {
            return nodeFactory;
        }

        public override EdgeFactory<TWildcardTodoEdge> EdgeFactory()
        {
            return edgeFactory;
        }

        public override void SetNodeFactory(NodeFactory<TWildcardTodoNode> nf)
        {
            nodeFactory = (NodeFactory<TWildcardTodoAbstractNode>)nf;
        }

        public override void SetEdgeFactory(EdgeFactory<TWildcardTodoEdge> ef)
        {
            edgeFactory = (EdgeFactory<TWildcardTodoAbstractEdge>)ef;
        }

        public override bool IsStrict()
        {
            return strictChecking;
        }

        public override void SetStrict(bool on)
        {
            strictChecking = on;
        }

        public override bool IsAutoCreationEnabled()
        {
            return autoCreate;
        }

        public override double GetStep()
        {
            return step;
        }

        public override void SetAutoCreate(bool on)
        {
            autoCreate = on;
        }

        public override void StepBegins(double time)
        {
            listeners.SendStepBegins(time);
            this.step = time;
        }

        public virtual Viewer Display()
        {
            return Display(true);
        }

        public virtual Viewer Display(bool autoLayout)
        {
            try
            {
                Display display = Display.GetDefault();
                return display.Display(this, autoLayout);
            }
            catch (MissingDisplayException e)
            {
                throw new Exception("Cannot launch viewer.", e);
            }
        }

        public override void Clear()
        {
            listeners.SendGraphCleared();
            Nodes().ForEach((n) => ((AbstractNode)n).ClearCallback());
            ClearCallback();
            ClearAttributesWithNoEvent();
        }

        public override Node AddNode(string id)
        {
            AbstractNode node = (AbstractNode)GetNode(id);
            if (node != null)
            {
                if (strictChecking)
                    throw new IdAlreadyInUseException("id \"" + id + "\" already in use. Cannot create a node.");
                return node;
            }

            node = nodeFactory.NewInstance(id, this);
            AddNodeCallback(node);
            listeners.SendNodeAdded(id);
            return node;
        }

        public override Edge AddEdge(string id, Node from, Node to, bool directed)
        {
            return AddEdge(id, (AbstractNode)from, from.GetId(), (AbstractNode)to, to.GetId(), directed);
        }

        public override Node RemoveNode(Node node)
        {
            if (node == null)
                return null;
            RemoveNode((AbstractNode)node, true);
            return node;
        }

        public override Edge RemoveEdge(Edge edge)
        {
            if (edge == null)
                return null;
            RemoveEdge((AbstractEdge)edge, true, true, true);
            return edge;
        }

        public override Edge RemoveEdge(Node node1, Node node2)
        {
            Edge edge = node1.GetEdgeToward(node2);
            if (edge == null)
            {
                if (strictChecking)
                    throw new ElementNotFoundException("There is no edge from \"%s\" to \"%s\". Cannot remove it.", node1.GetId(), node2.GetId());
                return null;
            }

            return RemoveEdge(edge);
        }

        public override Iterable<AttributeSink> AttributeSinks()
        {
            return listeners.AttributeSinks();
        }

        public override Iterable<ElementSink> ElementSinks()
        {
            return listeners.ElementSinks();
        }

        public override void AddAttributeSink(AttributeSink sink)
        {
            listeners.AddAttributeSink(sink);
        }

        public override void AddElementSink(ElementSink sink)
        {
            listeners.AddElementSink(sink);
        }

        public override void AddSink(Sink sink)
        {
            listeners.AddSink(sink);
        }

        public override void ClearAttributeSinks()
        {
            listeners.ClearAttributeSinks();
        }

        public override void ClearElementSinks()
        {
            listeners.ClearElementSinks();
        }

        public override void ClearSinks()
        {
            listeners.ClearSinks();
        }

        public override void RemoveAttributeSink(AttributeSink sink)
        {
            listeners.RemoveAttributeSink(sink);
        }

        public override void RemoveElementSink(ElementSink sink)
        {
            listeners.RemoveElementSink(sink);
        }

        public override void RemoveSink(Sink sink)
        {
            listeners.RemoveSink(sink);
        }

        public override void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
        {
            listeners.EdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
        }

        public override void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
        {
            listeners.EdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
        }

        public override void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
        {
            listeners.EdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
        }

        public override void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
        {
            listeners.GraphAttributeAdded(sourceId, timeId, attribute, value);
        }

        public override void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
        {
            listeners.GraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
        }

        public override void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
        {
            listeners.GraphAttributeRemoved(sourceId, timeId, attribute);
        }

        public override void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
        {
            listeners.NodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
        }

        public override void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
        {
            listeners.NodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
        }

        public override void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
        {
            listeners.NodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
        }

        public override void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
        {
            listeners.EdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
        }

        public override void EdgeRemoved(string sourceId, long timeId, string edgeId)
        {
            listeners.EdgeRemoved(sourceId, timeId, edgeId);
        }

        public override void GraphCleared(string sourceId, long timeId)
        {
            listeners.GraphCleared(sourceId, timeId);
        }

        public override void NodeAdded(string sourceId, long timeId, string nodeId)
        {
            listeners.NodeAdded(sourceId, timeId, nodeId);
        }

        public override void NodeRemoved(string sourceId, long timeId, string nodeId)
        {
            listeners.NodeRemoved(sourceId, timeId, nodeId);
        }

        public override void StepBegins(string sourceId, long timeId, double step)
        {
            listeners.StepBegins(sourceId, timeId, step);
        }

        public override Replayable.Controller GetReplayController()
        {
            return new GraphReplayController();
        }

        protected abstract void AddNodeCallback(AbstractNode node);
        protected abstract void AddEdgeCallback(AbstractEdge edge);
        protected abstract void RemoveNodeCallback(AbstractNode node);
        protected abstract void RemoveEdgeCallback(AbstractEdge edge);
        protected abstract void ClearCallback();
        protected virtual Edge AddEdge(string edgeId, AbstractNode src, string srcId, AbstractNode dst, string dstId, bool directed)
        {
            AbstractEdge edge = (AbstractEdge)GetEdge(edgeId);
            if (edge != null)
            {
                if (strictChecking)
                    throw new IdAlreadyInUseException("id \"" + edgeId + "\" already in use. Cannot create an edge.");
                if ((edge.GetSourceNode() == src && edge.GetTargetNode() == dst) || (!directed && edge.GetTargetNode() == src && edge.GetSourceNode() == dst))
                    return edge;
                return null;
            }

            if (src == null || dst == null)
            {
                if (strictChecking)
                    throw new ElementNotFoundException(String.Format("Cannot create edge %s[%s-%s%s]. Node '%s' does not exist.", edgeId, srcId, directed ? ">" : "-", dstId, src == null ? srcId : dstId));
                if (!autoCreate)
                    return null;
                if (src == null)
                    src = (AbstractNode)AddNode(srcId);
                if (dst == null)
                    dst = (AbstractNode)AddNode(dstId);
            }

            if (src.GetGraph() != this || dst.GetGraph() != this)
            {
                throw new ElementNotFoundException("At least one of two nodes does not belong to the graph.");
            }

            edge = edgeFactory.NewInstance(edgeId, src, dst, directed);
            if (!src.AddEdgeCallback(edge))
            {
                if (strictChecking)
                    throw new EdgeRejectedException("Edge " + edge + " was rejected by node " + src);
                return null;
            }

            if (src != dst && !dst.AddEdgeCallback(edge))
            {
                src.RemoveEdgeCallback(edge);
                if (strictChecking)
                    throw new EdgeRejectedException("Edge " + edge + " was rejected by node " + dst);
                return null;
            }

            AddEdgeCallback(edge);
            listeners.SendEdgeAdded(edgeId, srcId, dstId, directed);
            return edge;
        }

        private void RemoveAllEdges(AbstractNode node)
        {
            Collection<Edge> toRemove = node.Edges().Collect(Collectors.ToList());
            toRemove.ForEach(this.RemoveEdge());
        }

        protected virtual void RemoveNode(AbstractNode node, bool graphCallback)
        {
            if (node == null)
            {
                throw new NullReferenceException("node reference is null");
            }

            if (node.GetGraph() != this)
            {
                throw new ElementNotFoundException("Node \"" + node.GetId() + "\" does not belong to this graph");
            }

            RemoveAllEdges(node);
            listeners.SendNodeRemoved(node.GetId());
            if (graphCallback)
                RemoveNodeCallback(node);
        }

        protected virtual void RemoveEdge(AbstractEdge edge, bool graphCallback, bool sourceCallback, bool targetCallback)
        {
            if (edge == null)
            {
                throw new NullReferenceException("edge reference is null");
            }

            AbstractNode src = (AbstractNode)edge.GetSourceNode();
            AbstractNode dst = (AbstractNode)edge.GetTargetNode();
            if (src.GetGraph() != this || dst.GetGraph() != this)
            {
                throw new ElementNotFoundException("Edge \"" + edge.GetId() + "\" does not belong to this graph");
            }

            listeners.SendEdgeRemoved(edge.GetId());
            if (sourceCallback)
                src.RemoveEdgeCallback(edge);
            if (src != dst && targetCallback)
                dst.RemoveEdgeCallback(edge);
            if (graphCallback)
                RemoveEdgeCallback(edge);
        }

        class GraphReplayController : SourceBase, Controller
        {
            GraphReplayController() : base(this.id + "replay")
            {
            }

            public override void Replay()
            {
                string sourceId = String.Format("%s-replay-%x", id, replayId++);
                Replay(sourceId);
            }

            public override void Replay(string sourceId)
            {
                AttributeKeys().ForEach((key) => SendGraphAttributeAdded(sourceId, key, GetAttribute(key)));
                for (int i = 0; i < GetNodeCount(); i++)
                {
                    Node node = GetNode(i);
                    string nodeId = node.GetId();
                    SendNodeAdded(sourceId, nodeId);
                    node.AttributeKeys().ForEach((key) => SendNodeAttributeAdded(sourceId, nodeId, key, node.GetAttribute(key)));
                }

                for (int i = 0; i < GetEdgeCount(); i++)
                {
                    Edge edge = GetEdge(i);
                    string edgeId = edge.GetId();
                    SendEdgeAdded(sourceId, edgeId, edge.GetNode0().GetId(), edge.GetNode1().GetId(), edge.IsDirected());
                    edge.AttributeKeys().ForEach((key) => SendEdgeAttributeAdded(sourceId, edgeId, key, edge.GetAttribute(key)));
                }
            }
        }
    }


}

