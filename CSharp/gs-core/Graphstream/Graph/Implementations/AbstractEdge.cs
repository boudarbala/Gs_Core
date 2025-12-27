using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Gs_Core.Graphstream.Graph.Implementations
{

    public class AbstractEdge : AbstractElement, Edge
    {
        protected AbstractNode source;
        protected AbstractNode target;
        protected bool directed;
        protected AbstractGraph graph;
        protected AbstractEdge(string id, AbstractNode source, AbstractNode target, bool directed) : base(id)
        {
            this.source = source;
            this.target = target;
            this.directed = directed;
            this.graph = (AbstractGraph)source.GetGraph();
        }

        protected override void AttributeChanged(AttributeChangeEvent @event, string attribute, object oldValue, object newValue)
        {
            graph.listeners.SendAttributeChangedEvent(id, ElementType.EDGE, attribute, @event, oldValue, newValue);
        }

        public override string ToString()
        {
            return String.Format("%s[%s-%s%s]", GetId(), source, directed ? ">" : "-", target);
        }

        public override Node GetNode0()
        {
            return source;
        }

        public override Node GetNode1()
        {
            return target;
        }

        public override Node GetOpposite(Node node)
        {
            if (node == source)
                return target;
            if (node == target)
                return source;
            return null;
        }

        public override Node GetSourceNode()
        {
            return source;
        }

        public override Node GetTargetNode()
        {
            return target;
        }

        public virtual bool IsDirected()
        {
            return directed;
        }

        public virtual bool IsLoop()
        {
            return source == target;
        }
    }

}
