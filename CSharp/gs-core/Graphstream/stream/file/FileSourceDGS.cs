using Java.Io;
using Java.Util.Zip;
using Gs_Core.Graphstream.Stream.File.Dgs;
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

namespace Gs_Core.Graphstream.Stream.File;

public class FileSourceDGS : FileSourceParser
{
    public virtual ParserFactory GetNewParserFactory()
    {
        return new AnonymousParserFactory(this);
    }

    private sealed class AnonymousParserFactory : ParserFactory
    {
        public AnonymousParserFactory(FileSourceDGS parent)
        {
            this.parent = parent;
        }

        private readonly FileSourceDGS parent;
        public Parser NewParser(Reader reader)
        {
            return new DGSParser(this, reader);
        }
    }

    public override bool NextStep()
    {
        try
        {
            return ((DGSParser)parser).NextStep();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    protected override Reader CreateReaderForFile(string filename)
    {
        InputStream is = null;
        @is = new FileInputStream(filename);
        if (@is.MarkSupported())
            @is.Mark(128);
        try
        {
            @is = new GZIPInputStream(@is);
        }
        catch (IOException e1)
        {
            if (@is.MarkSupported())
            {
                try
                {
                    @is.Reset();
                }
                catch (IOException e2)
                {
                    e2.PrintStackTrace();
                }
            }
            else
            {
                try
                {
                    @is.Dispose();
                }
                catch (IOException e2)
                {
                    e2.PrintStackTrace();
                }

                @is = new FileInputStream(filename);
            }
        }

        return new BufferedReader(new InputStreamReader(@is));
    }
}
