using Java.Io;
using Gs_Core.Graphstream.Stream.File.Dot;
using Gs_Core.Graphstream.Util.Parser;
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
using static Org.Graphstream.Stream.File.AttributeType;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSourceDOT : FileSourceParser
{
    public virtual ParserFactory GetNewParserFactory()
    {
        return new AnonymousParserFactory(this);
    }

    private sealed class AnonymousParserFactory : ParserFactory
    {
        public AnonymousParserFactory(FileSourceDOT parent)
        {
            this.parent = parent;
        }

        private readonly FileSourceDOT parent;
        public Parser NewParser(Reader reader)
        {
            return new DOTParser(this, reader);
        }
    }
}
