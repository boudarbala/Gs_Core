using Java.Util.Concurrent;
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

public class FileSinkFactory
{
    private static readonly ConcurrentHashMap<string, Class<TWildcardTodoFileSink>> ext2sink;
    static FileSinkFactory()
    {
        ext2sink = new ConcurrentHashMap<string, Class<TWildcardTodoFileSink>>();
        ext2sink.Put("dgs", typeof(FileSinkDGS));
        ext2sink.Put("dgsz", typeof(FileSinkDGS));
        ext2sink.Put("dgml", typeof(FileSinkDynamicGML));
        ext2sink.Put("gml", typeof(FileSinkGML));
        ext2sink.Put("graphml", typeof(FileSinkGraphML));
        ext2sink.Put("dot", typeof(FileSinkDOT));
        ext2sink.Put("svg", typeof(FileSinkSVG));
        ext2sink.Put("pgf", typeof(FileSinkTikZ));
        ext2sink.Put("tikz", typeof(FileSinkTikZ));
        ext2sink.Put("tex", typeof(FileSinkTikZ));
        ext2sink.Put("gexf", typeof(FileSinkGEXF));
        ext2sink.Put("xml", typeof(FileSinkGEXF));
        ext2sink.Put("png", typeof(FileSinkImages));
        ext2sink.Put("jpg", typeof(FileSinkImages));
    }

    public static FileSink SinkFor(string filename)
    {
        if (filename.LastIndexOf('.') > 0)
        {
            string ext = filename.Substring(filename.LastIndexOf('.') + 1);
            ext = ext.ToLowerCase();
            if (ext2sink.ContainsKey(ext))
            {
                Class<TWildcardTodoFileSink> fsink = ext2sink[ext];
                try
                {
                    return fsink.NewInstance();
                }
                catch (InstantiationException e)
                {
                    e.PrintStackTrace();
                }
                catch (IllegalAccessException e)
                {
                    e.PrintStackTrace();
                }
            }
        }

        return null;
    }
}
