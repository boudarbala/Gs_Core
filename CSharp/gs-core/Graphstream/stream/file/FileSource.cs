using Java.Io;
using Java.Net;
using Gs_Core.Graphstream.Stream;
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
using static Org.Graphstream.Stream.File.OutputType;
using static Org.Graphstream.Stream.File.OutputPolicy;
using static Org.Graphstream.Stream.File.LayoutPolicy;
using static Org.Graphstream.Stream.File.Quality;
using static Org.Graphstream.Stream.File.Option;

namespace Gs_Core.Graphstream.Stream.File;

public interface FileSource : Source
{
    void ReadAll(string fileName);
    void ReadAll(URL url);
    void ReadAll(InputStream stream);
    void ReadAll(Reader reader);
    void Begin(string fileName);
    void Begin(URL url);
    void Begin(InputStream stream);
    void Begin(Reader reader);
    bool NextEvents();
    bool NextStep();
    void End();
}
