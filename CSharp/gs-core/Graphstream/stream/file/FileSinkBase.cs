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

namespace Gs_Core.Graphstream.Stream.File;

public abstract class FileSinkBase : FileSink
{
    protected Writer output;
    public virtual void WriteAll(Graph graph, string fileName)
    {
        Begin(fileName);
        ExportGraph(graph);
        End();
    }

    public virtual void WriteAll(Graph graph, OutputStream stream)
    {
        Begin(stream);
        ExportGraph(graph);
        End();
    }

    public virtual void WriteAll(Graph graph, Writer writer)
    {
        Begin(writer);
        ExportGraph(graph);
        End();
    }

    protected virtual void ExportGraph(Graph graph)
    {
        string graphId = graph.GetId();
        AtomicLong timeId = new AtomicLong(0);
        graph.AttributeKeys().ForEach((key) => GraphAttributeAdded(graphId, timeId.GetAndIncrement(), key, graph.GetAttribute(key)));
        graph.Nodes().ForEach((node) =>
        {
            string nodeId = node.GetId();
            NodeAdded(graphId, timeId.GetAndIncrement(), nodeId);
            node.AttributeKeys().ForEach((key) => NodeAttributeAdded(graphId, timeId.GetAndIncrement(), nodeId, key, node.GetAttribute(key)));
        });
        graph.Edges().ForEach((edge) =>
        {
            string edgeId = edge.GetId();
            EdgeAdded(graphId, timeId.GetAndIncrement(), edgeId, edge.GetNode0().GetId(), edge.GetNode1().GetId(), edge.IsDirected());
            edge.AttributeKeys().ForEach((key) => EdgeAttributeAdded(graphId, timeId.GetAndIncrement(), edgeId, key, edge.GetAttribute(key)));
        });
    }

    public virtual void Begin(string fileName)
    {
        if (output != null)
            throw new IOException("cannot call begin() twice without calling end() before.");
        output = CreateWriter(fileName);
        OutputHeader();
    }

    public virtual void Begin(OutputStream stream)
    {
        if (output != null)
            throw new IOException("cannot call begin() twice without calling end() before.");
        output = CreateWriter(stream);
        OutputHeader();
    }

    public virtual void Begin(Writer writer)
    {
        if (output != null)
            throw new IOException("cannot call begin() twice without calling end() before.");
        output = CreateWriter(writer);
        OutputHeader();
    }

    public virtual void Flush()
    {
        if (output != null)
            output.Flush();
    }

    public virtual void End()
    {
        OutputEndOfFile();
        output.Flush();
        output.Dispose();
        output = null;
    }

    protected abstract void OutputHeader();
    protected abstract void OutputEndOfFile();
    protected virtual Writer CreateWriter(string fileName)
    {
        return new PrintWriter(fileName);
    }

    protected virtual Writer CreateWriter(OutputStream stream)
    {
        return new PrintWriter(stream);
    }

    protected virtual Writer CreateWriter(Writer writer)
    {
        if (writer is PrintWriter)
            return writer;
        return new PrintWriter(writer);
    }
}
