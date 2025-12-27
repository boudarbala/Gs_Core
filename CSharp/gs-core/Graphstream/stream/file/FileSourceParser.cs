using Java.Io;
using Java.Net;
using Gs_Core.Graphstream.Stream;
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
using static Org.Graphstream.Stream.File.Balise;
using static Org.Graphstream.Stream.File.GEXFAttribute;
using static Org.Graphstream.Stream.File.METAAttribute;
using static Org.Graphstream.Stream.File.GRAPHAttribute;
using static Org.Graphstream.Stream.File.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.NODESAttribute;
using static Org.Graphstream.Stream.File.NODEAttribute;
using static Org.Graphstream.Stream.File.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.PARENTAttribute;
using static Org.Graphstream.Stream.File.EDGESAttribute;
using static Org.Graphstream.Stream.File.SPELLAttribute;
using static Org.Graphstream.Stream.File.COLORAttribute;
using static Org.Graphstream.Stream.File.POSITIONAttribute;
using static Org.Graphstream.Stream.File.SIZEAttribute;
using static Org.Graphstream.Stream.File.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.EDGEAttribute;
using static Org.Graphstream.Stream.File.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.IDType;
using static Org.Graphstream.Stream.File.ModeType;
using static Org.Graphstream.Stream.File.WeightType;
using static Org.Graphstream.Stream.File.EdgeType;
using static Org.Graphstream.Stream.File.NodeShapeType;
using static Org.Graphstream.Stream.File.EdgeShapeType;
using static Org.Graphstream.Stream.File.ClassType;
using static Org.Graphstream.Stream.File.TimeFormatType;
using static Org.Graphstream.Stream.File.GPXAttribute;
using static Org.Graphstream.Stream.File.WPTAttribute;
using static Org.Graphstream.Stream.File.LINKAttribute;
using static Org.Graphstream.Stream.File.EMAILAttribute;
using static Org.Graphstream.Stream.File.PTAttribute;
using static Org.Graphstream.Stream.File.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.FixType;
using static Org.Graphstream.Stream.File.GraphAttribute;
using static Org.Graphstream.Stream.File.LocatorAttribute;
using static Org.Graphstream.Stream.File.NodeAttribute;
using static Org.Graphstream.Stream.File.EdgeAttribute;
using static Org.Graphstream.Stream.File.DataAttribute;
using static Org.Graphstream.Stream.File.PortAttribute;
using static Org.Graphstream.Stream.File.EndPointAttribute;
using static Org.Graphstream.Stream.File.EndPointType;
using static Org.Graphstream.Stream.File.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.KeyAttribute;
using static Org.Graphstream.Stream.File.KeyDomain;
using static Org.Graphstream.Stream.File.KeyAttrType;

namespace Gs_Core.Graphstream.Stream.File;

public abstract class FileSourceParser : SourceBase, FileSource
{
    protected ParserFactory factory;
    protected Parser parser;
    public abstract ParserFactory GetNewParserFactory();
    protected FileSourceParser()
    {
        factory = GetNewParserFactory();
    }

    public virtual void ReadAll(string fileName)
    {
        Parser parser = factory.NewParser(CreateReaderForFile(fileName));
        try
        {
            parser.All();
            parser.Dispose();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    public virtual void ReadAll(URL url)
    {
        Parser parser = factory.NewParser(new InputStreamReader(url.OpenStream()));
        try
        {
            parser.All();
            parser.Dispose();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    public virtual void ReadAll(InputStream stream)
    {
        Parser parser = factory.NewParser(new InputStreamReader(stream));
        try
        {
            parser.All();
            parser.Dispose();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    public virtual void ReadAll(Reader reader)
    {
        Parser parser = factory.NewParser(reader);
        try
        {
            parser.All();
            parser.Dispose();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    public virtual void Begin(string fileName)
    {
        if (parser != null)
            End();
        parser = factory.NewParser(CreateReaderForFile(fileName));
        try
        {
            parser.Open();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    public virtual void Begin(URL url)
    {
        parser = factory.NewParser(new InputStreamReader(url.OpenStream()));
        try
        {
            parser.Open();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    public virtual void Begin(InputStream stream)
    {
        parser = factory.NewParser(new InputStreamReader(stream));
        try
        {
            parser.Open();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    public virtual void Begin(Reader reader)
    {
        parser = factory.NewParser(reader);
        try
        {
            parser.Open();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    public virtual bool NextEvents()
    {
        try
        {
            return parser.Next();
        }
        catch (ParseException e)
        {
            throw new IOException(e);
        }
    }

    public virtual bool NextStep()
    {
        return NextEvents();
    }

    public virtual void End()
    {
        parser.Dispose();
        parser = null;
    }

    protected virtual Reader CreateReaderForFile(string filename)
    {
        return new FileReader(filename);
    }
}
