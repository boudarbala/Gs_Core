using Java.Util;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream;
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

    public abstract class AbstractNode : AbstractElement, Node
    {
        protected AbstractGraph graph;
        protected AbstractNode(AbstractGraph graph, string id) : base(id)
        {
            this.graph = graph;
        }

        protected override void AttributeChanged(AttributeChangeEvent @event, string attribute, object oldValue, object newValue)
        {
            graph.listeners.SendAttributeChangedEvent(id, SourceBase.ElementType.NODE, attribute, @event, oldValue, newValue);
        }

        public virtual Graph GetGraph()
        {
            return graph;
        }

        public override Edge GetEdgeToward(int index)
        {
            return GetEdgeToward(graph.GetNode(index));
        }

        public override Edge GetEdgeToward(string id)
        {
            return GetEdgeToward(graph.GetNode(id));
        }

        public override Edge GetEdgeFrom(int index)
        {
            return GetEdgeFrom(graph.GetNode(index));
        }

        public override Edge GetEdgeFrom(string id)
        {
            return GetEdgeFrom(graph.GetNode(id));
        }

        public override Edge GetEdgeBetween(int index)
        {
            return GetEdgeBetween(graph.GetNode(index));
        }

        public override Edge GetEdgeBetween(string id)
        {
            return GetEdgeBetween(graph.GetNode(id));
        }

        public override IEnumerator<Node> GetBreadthFirstIterator()
        {
            return new BreadthFirstIterator(this);
        }

        public override IEnumerator<Node> GetBreadthFirstIterator(bool directed)
        {
            return new BreadthFirstIterator(this, directed);
        }

        public override IEnumerator<Node> GetDepthFirstIterator()
        {
            return new DepthFirstIterator(this);
        }

        public override IEnumerator<Node> GetDepthFirstIterator(bool directed)
        {
            return new DepthFirstIterator(this, directed);
        }

        protected abstract bool AddEdgeCallback(AbstractEdge edge);
        protected abstract void RemoveEdgeCallback(AbstractEdge edge);
        protected abstract void ClearCallback();
        public virtual bool IsEnteringEdge(Edge e)
        {
            return e.GetTargetNode() == this || (!e.IsDirected() && e.GetSourceNode() == this);
        }

        public virtual bool IsLeavingEdge(Edge e)
        {
            return e.GetSourceNode() == this || (!e.IsDirected() && e.GetTargetNode() == this);
        }

        public virtual bool IsIncidentEdge(Edge e)
        {
            return e.GetSourceNode() == this || e.GetTargetNode() == this;
        }
    }


}
