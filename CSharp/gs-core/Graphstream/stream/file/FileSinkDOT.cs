using Java.Io;
using Java.Util.Concurrent.Atomic;
using Gs_Core.Graphstream.Graph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;
using static Org.Graphstream.Stream.File.What;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSinkDOT : FileSinkBase
{
    protected PrintWriter out;
    protected string graphName = "";
    protected bool digraph;
    protected enum What
    {
        NODE,
        EDGE,
        OTHER
    }

    public FileSinkDOT() : this(false)
    {
    }

    public FileSinkDOT(bool digraph)
    {
        this.digraph = digraph;
    }

    public virtual void SetDirected(bool digraph)
    {
        this.digraph = digraph;
    }

    public virtual bool IsDirected()
    {
        return digraph;
    }

    protected override void ExportGraph(Graph graph)
    {
        string graphId = graph.GetId();
        AtomicLong timeId = new AtomicLong(0);
        graph.AttributeKeys().ForEach((key) => GraphAttributeAdded(graphId, timeId.GetAndIncrement(), key, graph.GetAttribute(key)));
        foreach (Node node in graph)
        {
            string nodeId = node.GetId();
            @out.Printf("\t\"%s\" %s;%n", nodeId, OutputAttributes(node));
        }

        graph.Edges().ForEach((edge) =>
        {
            string fromNodeId = edge.GetNode0().GetId();
            string toNodeId = edge.GetNode1().GetId();
            string attr = OutputAttributes(edge);
            if (digraph)
            {
                @out.Printf("\t\"%s\" -> \"%s\"", fromNodeId, toNodeId);
                if (!edge.IsDirected())
                    @out.Printf(" -> \"%s\"", fromNodeId);
            }
            else
                @out.Printf("\t\"%s\" -- \"%s\"", fromNodeId, toNodeId);
            @out.Printf(" %s;%n", attr);
        });
    }

    protected override void OutputHeader()
    {
        @out = (PrintWriter)output;
        @out.Printf("%s {%n", digraph ? "digraph" : "graph");
        if (graphName.Length > 0)
            @out.Printf("\tgraph [label=%s];%n", graphName);
    }

    protected override void OutputEndOfFile()
    {
        @out.Printf("}%n");
    }

    public virtual void EdgeAttributeAdded(string graphId, long timeId, string edgeId, string attribute, object value)
    {
    }

    public virtual void EdgeAttributeChanged(string graphId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void EdgeAttributeRemoved(string graphId, long timeId, string edgeId, string attribute)
    {
    }

    public virtual void GraphAttributeAdded(string graphId, long timeId, string attribute, object value)
    {
        @out.Printf("\tgraph [ %s ];%n", OutputAttribute(attribute, value, true));
    }

    public virtual void GraphAttributeChanged(string graphId, long timeId, string attribute, object oldValue, object newValue)
    {
        @out.Printf("\tgraph [ %s ];%n", OutputAttribute(attribute, newValue, true));
    }

    public virtual void GraphAttributeRemoved(string graphId, long timeId, string attribute)
    {
    }

    public virtual void NodeAttributeAdded(string graphId, long timeId, string nodeId, string attribute, object value)
    {
        @out.Printf("\t\"%s\" [ %s ];%n", nodeId, OutputAttribute(attribute, value, true));
    }

    public virtual void NodeAttributeChanged(string graphId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        @out.Printf("\t\"%s\" [ %s ];%n", nodeId, OutputAttribute(attribute, newValue, true));
    }

    public virtual void NodeAttributeRemoved(string graphId, long timeId, string nodeId, string attribute)
    {
    }

    public virtual void EdgeAdded(string graphId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        if (digraph)
        {
            @out.Printf("\t\"%s\" -> \"%s\"", fromNodeId, toNodeId);
            if (!directed)
                @out.Printf(" -> \"%s\"", fromNodeId);
            @out.Printf(";%n");
        }
        else
            @out.Printf("\t\"%s\" -- \"%s\";%n", fromNodeId, toNodeId);
    }

    public virtual void EdgeRemoved(string graphId, long timeId, string edgeId)
    {
    }

    public virtual void GraphCleared(string graphId, long timeId)
    {
    }

    public virtual void NodeAdded(string graphId, long timeId, string nodeId)
    {
        @out.Printf("\t\"%s\";%n", nodeId);
    }

    public virtual void NodeRemoved(string graphId, long timeId, string nodeId)
    {
    }

    public virtual void StepBegins(string graphId, long timeId, double step)
    {
    }

    protected virtual string OutputAttribute(string key, object value, bool first)
    {
        bool quote = true;
        if (value is Number)
            quote = false;
        return String.Format("%s\"%s\"=%s%s%s", first ? "" : ",", key, quote ? "\"" : "", value, quote ? "\"" : "");
    }

    protected virtual string OutputAttributes(Element e)
    {
        if (e.GetAttributeCount() == 0)
            return "";
        StringBuilder buffer = new StringBuilder("[");
        AtomicBoolean first = new AtomicBoolean(true);
        e.AttributeKeys().ForEach((key) =>
        {
            bool quote = true;
            object value = e.GetAttribute(key);
            if (value is Number)
                quote = false;
            buffer.Append(String.Format("%s\"%s\"=%s%s%s", first.Get() ? "" : ",", key, quote ? "\"" : "", value, quote ? "\"" : ""));
            first.Set(false);
        });
        return buffer.Append(']').ToString();
    }
}
