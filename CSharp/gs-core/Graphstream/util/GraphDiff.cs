using Java.Io;
using Java.Lang.Reflect;
using Java.Util;
using Java.Util.Zip;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Graph.Implementations;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.File;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.ElementType;

namespace Gs_Core.Graphstream.Util;

public class GraphDiff
{
    protected enum ElementType
    {
        NODE,
        EDGE,
        GRAPH
    }

    private Bridge bridge;
    private readonly LinkedList<Event> events;
    public GraphDiff()
    {
        this.events = new LinkedList<Event>();
        this.bridge = null;
    }

    public GraphDiff(Graph g1, Graph g2) : this()
    {
        if (g2.GetNodeCount() == 0 && g2.GetEdgeCount() == 0 && g2.GetAttributeCount() == 0 && (g1.GetNodeCount() > 0 || g1.GetEdgeCount() > 0))
        {
            events.Add(new GraphCleared(g1));
        }
        else
        {
            AttributeDiff(ElementType.GRAPH, g1, g2);
            for (int idx = 0; idx < g1.GetEdgeCount(); idx++)
            {
                Edge e1 = g1.GetEdge(idx);
                Edge e2 = g2.GetEdge(e1.GetId());
                if (e2 == null)
                {
                    AttributeDiff(ElementType.EDGE, e1, e2);
                    events.Add(new EdgeRemoved(e1.GetId(), e1.GetSourceNode().GetId(), e1.GetTargetNode().GetId(), e1.IsDirected()));
                }
            }

            for (int idx = 0; idx < g1.GetNodeCount(); idx++)
            {
                Node n1 = g1.GetNode(idx);
                Node n2 = g2.GetNode(n1.GetId());
                if (n2 == null)
                {
                    AttributeDiff(ElementType.NODE, n1, n2);
                    events.Add(new NodeRemoved(n1.GetId()));
                }
            }

            for (int idx = 0; idx < g2.GetNodeCount(); idx++)
            {
                Node n2 = g2.GetNode(idx);
                Node n1 = g1.GetNode(n2.GetId());
                if (n1 == null)
                    events.Add(new NodeAdded(n2.GetId()));
                AttributeDiff(ElementType.NODE, n1, n2);
            }

            for (int idx = 0; idx < g2.GetEdgeCount(); idx++)
            {
                Edge e2 = g2.GetEdge(idx);
                Edge e1 = g1.GetEdge(e2.GetId());
                if (e1 == null)
                    events.Add(new EdgeAdded(e2.GetId(), e2.GetSourceNode().GetId(), e2.GetTargetNode().GetId(), e2.IsDirected()));
                AttributeDiff(ElementType.EDGE, e1, e2);
            }
        }
    }

    public virtual void Start(Graph g)
    {
        if (bridge != null)
            End();
        bridge = new Bridge(g);
    }

    public virtual void End()
    {
        if (bridge != null)
        {
            bridge.End();
            bridge = null;
        }
    }

    public virtual void Reset()
    {
        events.Clear();
    }

    public virtual void Apply(Sink g1)
    {
        string sourceId = String.Format("GraphDiff@%x", System.NanoTime());
        Apply(sourceId, g1);
    }

    public virtual void Apply(string sourceId, Sink g1)
    {
        for (int i = 0; i < events.Count; i++)
            events[i].Apply(sourceId, i, g1);
    }

    public virtual void Reverse(Sink g2)
    {
        string sourceId = String.Format("GraphDiff@%x", System.NanoTime());
        Reverse(sourceId, g2);
    }

    public virtual void Reverse(string sourceId, Sink g2)
    {
        for (int i = events.size() - 1; i >= 0; i--)
            events[i].Reverse(sourceId, events.Count + 1 - i, g2);
    }

    private void AttributeDiff(ElementType type, Element e1, Element e2)
    {
        if (e1 == null && e2 == null)
            return;
        else if (e1 == null)
        {
            e2.AttributeKeys().ForEach((key) => events.Add(new AttributeAdded(type, e2.GetId(), key, e2.GetAttribute(key))));
        }
        else if (e2 == null)
        {
            e1.AttributeKeys().ForEach((key) => events.Add(new AttributeRemoved(type, e1.GetId(), key, e1.GetAttribute(key))));
        }
        else
        {
            e2.AttributeKeys().ForEach((key) =>
            {
                if (e1.HasAttribute(key))
                {
                    object o1 = e1.GetAttribute(key);
                    object o2 = e2.GetAttribute(key);
                    if (!(o1 == null ? o2 == null : o1.Equals(o2)))
                        events.Add(new AttributeChanged(type, e1.GetId(), key, o2, o1));
                }
                else
                    events.Add(new AttributeAdded(type, e1.GetId(), key, e2.GetAttribute(key)));
            });
            e1.AttributeKeys().ForEach((key) =>
            {
                if (!e2.HasAttribute(key))
                    events.Add(new AttributeRemoved(type, e1.GetId(), key, e1.GetAttribute(key)));
            });
        }
    }

    public virtual string ToString()
    {
        StringBuilder buffer = new StringBuilder();
        for (int i = 0; i < events.Count; i++)
            buffer.Append(events[i].ToString()).Append("\n");
        return buffer.ToString();
    }

    protected abstract class Event
    {
        abstract void Apply(string sourceId, long timeId, Sink g);
        abstract void Reverse(string sourceId, long timeId, Sink g);
    }

    protected class NodeAdded : Event
    {
        string nodeId;
        public NodeAdded(string nodeId)
        {
            this.nodeId = nodeId;
        }

        public virtual void Apply(string sourceId, long timeId, Sink g)
        {
            g.NodeAdded(sourceId, timeId, nodeId);
        }

        public virtual void Reverse(string sourceId, long timeId, Sink g)
        {
            g.NodeRemoved(sourceId, timeId, nodeId);
        }

        public override string ToString()
        {
            return String.Format("an \"%s\"", nodeId);
        }
    }

    protected class NodeRemoved : NodeAdded
    {
        public NodeRemoved(string nodeId) : base(nodeId)
        {
        }

        public virtual void Apply(string sourceId, long timeId, Sink g)
        {
            base.Reverse(sourceId, timeId, g);
        }

        public virtual void Reverse(string sourceId, long timeId, Sink g)
        {
            base.Apply(sourceId, timeId, g);
        }

        public override string ToString()
        {
            return String.Format("dn \"%s\"", nodeId);
        }
    }

    protected abstract class ElementEvent : Event
    {
        ElementType type;
        string elementId;
        protected ElementEvent(ElementType type, string elementId)
        {
            this.type = type;
            this.elementId = elementId;
        }

        protected virtual Element GetElement(Graph g)
        {
            Element e;
            switch (type)
            {
                case NODE:
                    e = g.GetNode(elementId);
                    break;
                case EDGE:
                    e = g.GetEdge(elementId);
                    break;
                case GRAPH:
                    e = g;
                    break;
                default:
                    e = null;
                    break;
            }

            if (e == null)
                throw new ElementNotFoundException();
            return e;
        }

        protected virtual string ToStringHeader()
        {
            string header;
            switch (type)
            {
                case NODE:
                    header = "cn";
                    break;
                case EDGE:
                    header = "ce";
                    break;
                case GRAPH:
                    header = "cg";
                    break;
                default:
                    header = "??";
                    break;
            }

            return String.Format("%s \"%s\"", header, elementId);
        }

        protected virtual string ToStringValue(object o)
        {
            if (o == null)
                return "null";
            else if (o is string)
                return "\"" + o.ToString() + "\"";
            else if (o is Number)
                return o.ToString();
            else
                return o.ToString();
        }
    }

    protected class AttributeAdded : ElementEvent
    {
        string attrId;
        object value;
        public AttributeAdded(ElementType type, string elementId, string attrId, object value) : base(type, elementId)
        {
            this.attrId = attrId;
            this.value = value;
            if (value != null && value.GetType().IsArray() && Array.GetLength(value) > 0)
            {
                object o = Array.NewInstance(Array.Get(value, 0).GetType(), Array.GetLength(value));
                for (int i = 0; i < Array.GetLength(value); i++)
                    Array.Set(o, i, Array.Get(value, i));
                this.value = o;
            }
        }

        public virtual void Apply(string sourceId, long timeId, Sink g)
        {
            switch (type)
            {
                case NODE:
                    g.NodeAttributeAdded(sourceId, timeId, elementId, attrId, value);
                    break;
                case EDGE:
                    g.EdgeAttributeAdded(sourceId, timeId, elementId, attrId, value);
                    break;
                case GRAPH:
                    g.GraphAttributeAdded(sourceId, timeId, attrId, value);
                    break;
            }
        }

        public virtual void Reverse(string sourceId, long timeId, Sink g)
        {
            switch (type)
            {
                case NODE:
                    g.NodeAttributeRemoved(sourceId, timeId, elementId, attrId);
                    break;
                case EDGE:
                    g.EdgeAttributeRemoved(sourceId, timeId, elementId, attrId);
                    break;
                case GRAPH:
                    g.GraphAttributeRemoved(sourceId, timeId, attrId);
                    break;
            }
        }

        public override string ToString()
        {
            return String.Format("%s +\"%s\":%s", ToStringHeader(), attrId, ToStringValue(value));
        }
    }

    protected class AttributeChanged : ElementEvent
    {
        string attrId;
        object newValue;
        object oldValue;
        public AttributeChanged(ElementType type, string elementId, string attrId, object newValue, object oldValue) : base(type, elementId)
        {
            this.attrId = attrId;
            this.newValue = newValue;
            this.oldValue = oldValue;
            if (newValue != null && newValue.GetType().IsArray() && Array.GetLength(newValue) > 0)
            {
                object o = Array.NewInstance(Array.Get(newValue, 0).GetType(), Array.GetLength(newValue));
                for (int i = 0; i < Array.GetLength(newValue); i++)
                    Array.Set(o, i, Array.Get(newValue, i));
                this.newValue = o;
            }

            if (oldValue != null && oldValue.GetType().IsArray() && Array.GetLength(oldValue) > 0)
            {
                object o = Array.NewInstance(Array.Get(oldValue, 0).GetType(), Array.GetLength(oldValue));
                for (int i = 0; i < Array.GetLength(oldValue); i++)
                    Array.Set(o, i, Array.Get(oldValue, i));
                this.oldValue = o;
            }
        }

        public virtual void Apply(string sourceId, long timeId, Sink g)
        {
            switch (type)
            {
                case NODE:
                    g.NodeAttributeChanged(sourceId, timeId, elementId, attrId, oldValue, newValue);
                    break;
                case EDGE:
                    g.EdgeAttributeChanged(sourceId, timeId, elementId, attrId, oldValue, newValue);
                    break;
                case GRAPH:
                    g.GraphAttributeChanged(sourceId, timeId, attrId, oldValue, newValue);
                    break;
            }
        }

        public virtual void Reverse(string sourceId, long timeId, Sink g)
        {
            switch (type)
            {
                case NODE:
                    g.NodeAttributeChanged(sourceId, timeId, elementId, attrId, newValue, oldValue);
                    break;
                case EDGE:
                    g.EdgeAttributeChanged(sourceId, timeId, elementId, attrId, newValue, oldValue);
                    break;
                case GRAPH:
                    g.GraphAttributeChanged(sourceId, timeId, attrId, newValue, oldValue);
                    break;
            }
        }

        public override string ToString()
        {
            return String.Format("%s \"%s\":%s", ToStringHeader(), attrId, ToStringValue(newValue));
        }
    }

    protected class AttributeRemoved : ElementEvent
    {
        string attrId;
        object oldValue;
        public AttributeRemoved(ElementType type, string elementId, string attrId, object oldValue) : base(type, elementId)
        {
            this.attrId = attrId;
            this.oldValue = oldValue;
        }

        public virtual void Apply(string sourceId, long timeId, Sink g)
        {
            switch (type)
            {
                case NODE:
                    g.NodeAttributeRemoved(sourceId, timeId, elementId, attrId);
                    break;
                case EDGE:
                    g.EdgeAttributeRemoved(sourceId, timeId, elementId, attrId);
                    break;
                case GRAPH:
                    g.GraphAttributeRemoved(sourceId, timeId, attrId);
                    break;
            }
        }

        public virtual void Reverse(string sourceId, long timeId, Sink g)
        {
            switch (type)
            {
                case NODE:
                    g.NodeAttributeAdded(sourceId, timeId, elementId, attrId, oldValue);
                    break;
                case EDGE:
                    g.EdgeAttributeAdded(sourceId, timeId, elementId, attrId, oldValue);
                    break;
                case GRAPH:
                    g.GraphAttributeAdded(sourceId, timeId, attrId, oldValue);
                    break;
            }
        }

        public override string ToString()
        {
            return String.Format("%s -\"%s\":%s", ToStringHeader(), attrId, ToStringValue(oldValue));
        }
    }

    protected class EdgeAdded : Event
    {
        string edgeId;
        string source, target;
        bool directed;
        public EdgeAdded(string edgeId, string source, string target, bool directed)
        {
            this.edgeId = edgeId;
            this.source = source;
            this.target = target;
            this.directed = directed;
        }

        public virtual void Apply(string sourceId, long timeId, Sink g)
        {
            g.EdgeAdded(sourceId, timeId, edgeId, source, target, directed);
        }

        public virtual void Reverse(string sourceId, long timeId, Sink g)
        {
            g.EdgeRemoved(sourceId, timeId, edgeId);
        }

        public override string ToString()
        {
            return String.Format("ae \"%s\" \"%s\" %s \"%s\"", edgeId, source, directed ? ">" : "--", target);
        }
    }

    protected class EdgeRemoved : EdgeAdded
    {
        public EdgeRemoved(string edgeId, string source, string target, bool directed) : base(edgeId, source, target, directed)
        {
        }

        public virtual void Apply(string sourceId, long timeId, Sink g)
        {
            base.Reverse(sourceId, timeId, g);
        }

        public virtual void Reverse(string sourceId, long timeId, Sink g)
        {
            base.Apply(sourceId, timeId, g);
        }

        public override string ToString()
        {
            return String.Format("de \"%s\"", edgeId);
        }
    }

    protected class StepBegins : Event
    {
        double newStep, oldStep;
        public StepBegins(double oldStep, double newStep)
        {
            this.newStep = newStep;
            this.oldStep = oldStep;
        }

        public virtual void Apply(string sourceId, long timeId, Sink g)
        {
            g.StepBegins(sourceId, timeId, newStep);
        }

        public virtual void Reverse(string sourceId, long timeId, Sink g)
        {
            g.StepBegins(sourceId, timeId, oldStep);
        }

        public override string ToString()
        {
            return String.Format("st %f", newStep);
        }
    }

    protected class GraphCleared : Event
    {
        byte[] data;
        public GraphCleared(Graph g)
        {
            this.data = null;
            try
            {
                FileSinkDGS sink = new FileSinkDGS();
                ByteArrayOutputStream bytes = new ByteArrayOutputStream();
                GZIPOutputStream out = new GZIPOutputStream(bytes);
                sink.WriteAll(g, @out);
                @out.Flush();
                @out.Dispose();
                this.data = bytes.ToByteArray();
            }
            catch (IOException e)
            {
                e.PrintStackTrace();
            }
        }

        public virtual void Apply(string sourceId, long timeId, Sink g)
        {
            g.GraphCleared(sourceId, timeId);
        }

        public virtual void Reverse(string sourceId, long timeId, Sink g)
        {
            try
            {
                ByteArrayInputStream bytes = new ByteArrayInputStream(this.data);
                GZIPInputStream in = new GZIPInputStream(bytes);
                FileSourceDGS dgs = new FileSourceDGS();
                dgs.AddSink(g);
                dgs.ReadAll(@in);
                dgs.RemoveSink(g);
                @in.Dispose();
            }
            catch (IOException e)
            {
                e.PrintStackTrace();
            }
        }

        public override string ToString()
        {
            return "cl";
        }
    }

    private class Bridge : Sink
    {
        Graph g;
        Bridge(Graph g)
        {
            this.g = g;
            g.AddSink(this);
        }

        virtual void End()
        {
            g.RemoveSink(this);
        }

        public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
        {
            Event e;
            e = new AttributeAdded(ElementType.GRAPH, null, attribute, value);
            events.Add(e);
        }

        public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
        {
            Event e;
            e = new AttributeChanged(ElementType.GRAPH, null, attribute, newValue, g.GetAttribute(attribute));
            events.Add(e);
        }

        public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
        {
            Event e;
            e = new AttributeRemoved(ElementType.GRAPH, null, attribute, g.GetAttribute(attribute));
            events.Add(e);
        }

        public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
        {
            Event e;
            e = new AttributeAdded(ElementType.NODE, nodeId, attribute, value);
            events.Add(e);
        }

        public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
        {
            Event e;
            e = new AttributeChanged(ElementType.NODE, nodeId, attribute, newValue, g.GetNode(nodeId).GetAttribute(attribute));
            events.Add(e);
        }

        public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
        {
            Event e;
            e = new AttributeRemoved(ElementType.NODE, nodeId, attribute, g.GetNode(nodeId).GetAttribute(attribute));
            events.Add(e);
        }

        public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
        {
            Event e;
            e = new AttributeAdded(ElementType.EDGE, edgeId, attribute, value);
            events.Add(e);
        }

        public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
        {
            Event e;
            e = new AttributeChanged(ElementType.EDGE, edgeId, attribute, newValue, g.GetEdge(edgeId).GetAttribute(attribute));
            events.Add(e);
        }

        public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
        {
            Event e;
            e = new AttributeRemoved(ElementType.EDGE, edgeId, attribute, g.GetEdge(edgeId).GetAttribute(attribute));
            events.Add(e);
        }

        public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
        {
            Event e;
            e = new NodeAdded(nodeId);
            events.Add(e);
        }

        public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
        {
            Node n = g.GetNode(nodeId);
            n.AttributeKeys().ForEach((key) => NodeAttributeRemoved(sourceId, timeId, nodeId, key));
            Event e;
            e = new NodeRemoved(nodeId);
            events.Add(e);
        }

        public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
        {
            Event e;
            e = new EdgeAdded(edgeId, fromNodeId, toNodeId, directed);
            events.Add(e);
        }

        public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
        {
            Edge edge = g.GetEdge(edgeId);
            edge.AttributeKeys().ForEach((key) => EdgeAttributeRemoved(sourceId, timeId, edgeId, key));
            Event e;
            e = new EdgeRemoved(edgeId, edge.GetSourceNode().GetId(), edge.GetTargetNode().GetId(), edge.IsDirected());
            events.Add(e);
        }

        public virtual void GraphCleared(string sourceId, long timeId)
        {
            Event e = new GraphCleared(g);
            events.Add(e);
        }

        public virtual void StepBegins(string sourceId, long timeId, double step)
        {
            Event e = new StepBegins(g.GetStep(), step);
            events.Add(e);
        }
    }

    public static void Main(params string[] args)
    {
        Graph g1 = new AdjacencyListGraph("g1");
        Graph g2 = new AdjacencyListGraph("g2");
        Node a1 = g1.AddNode("A");
        a1.SetAttribute("attr1", "test");
        a1.SetAttribute("attr2", 10);
        a1.SetAttribute("attr3", 12);
        Node a2 = g2.AddNode("A");
        a2.SetAttribute("attr1", "test1");
        a2.SetAttribute("attr2", 10);
        g2.AddNode("B");
        g2.AddNode("C");
        GraphDiff diff = new GraphDiff(g2, g1);
        System.@out.Println(diff);
    }
}
