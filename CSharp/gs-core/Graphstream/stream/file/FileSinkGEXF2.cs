using Java.Io;
using Javax.Xml.Stream;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.File.Gexf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;
using static Org.Graphstream.Stream.File.What;
using static Org.Graphstream.Stream.File.TimeFormat;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSinkGEXF2 : PipeBase, FileSink
{
    class Context
    {
        GEXF gexf;
        Writer output;
        SmartXMLWriter stream;
        bool closeStreamAtEnd;
    }

    Context currentContext;
    virtual Context CreateContext(string fileName)
    {
        FileWriter w = new FileWriter(fileName);
        Context ctx = CreateContext(w);
        ctx.closeStreamAtEnd = true;
        return ctx;
    }

    virtual Context CreateContext(OutputStream output)
    {
        OutputStreamWriter w = new OutputStreamWriter(output);
        return CreateContext(w);
    }

    virtual Context CreateContext(Writer w)
    {
        Context ctx = new Context();
        ctx.output = w;
        ctx.closeStreamAtEnd = false;
        ctx.gexf = new GEXF();
        try
        {
            ctx.stream = new SmartXMLWriter(w, true);
        }
        catch (Exception e)
        {
            throw new IOException(e);
        }

        return ctx;
    }

    protected virtual void Export(Context ctx, Graph g)
    {
        ctx.gexf.Disable(GEXF.Extension.DYNAMICS);
        GraphReplay replay = new GraphReplay("replay");
        replay.AddSink(ctx.gexf);
        replay.Replay(g);
        try
        {
            ctx.gexf.Export(ctx.stream);
            ctx.stream.Dispose();
            if (ctx.closeStreamAtEnd)
                ctx.output.Dispose();
        }
        catch (XMLStreamException e)
        {
            throw new IOException(e);
        }
    }

    public virtual void WriteAll(Graph graph, string fileName)
    {
        Context ctx = CreateContext(fileName);
        Export(ctx, graph);
    }

    public virtual void WriteAll(Graph graph, OutputStream stream)
    {
        Context ctx = CreateContext(stream);
        Export(ctx, graph);
    }

    public virtual void WriteAll(Graph graph, Writer writer)
    {
        Context ctx = CreateContext(writer);
        Export(ctx, graph);
    }

    public virtual void Begin(string fileName)
    {
        if (currentContext != null)
            throw new IOException("cannot call begin() twice without calling end() before.");
        currentContext = CreateContext(fileName);
        AddSink(currentContext.gexf);
    }

    public virtual void Begin(OutputStream stream)
    {
        if (currentContext != null)
            throw new IOException("cannot call begin() twice without calling end() before.");
        currentContext = CreateContext(stream);
        AddSink(currentContext.gexf);
    }

    public virtual void Begin(Writer writer)
    {
        if (currentContext != null)
            throw new IOException("cannot call begin() twice without calling end() before.");
        currentContext = CreateContext(writer);
        AddSink(currentContext.gexf);
    }

    public virtual void Flush()
    {
        if (currentContext != null)
            currentContext.stream.Flush();
    }

    public virtual void End()
    {
        RemoveSink(currentContext.gexf);
        try
        {
            currentContext.gexf.Export(currentContext.stream);
            currentContext.stream.Dispose();
            if (currentContext.closeStreamAtEnd)
                currentContext.output.Dispose();
        }
        catch (XMLStreamException e)
        {
            throw new IOException(e);
        }

        currentContext = null;
    }
}
