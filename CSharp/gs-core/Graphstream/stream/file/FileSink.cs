using Java.Io;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;

namespace Gs_Core.Graphstream.Stream.File;

public interface FileSink : Sink
{
    void WriteAll(Graph graph, string fileName);
    void WriteAll(Graph graph, OutputStream stream);
    void WriteAll(Graph graph, Writer writer);
    void Begin(string fileName);
    void Begin(OutputStream stream);
    void Begin(Writer writer);
    void Flush();
    void End();
}
