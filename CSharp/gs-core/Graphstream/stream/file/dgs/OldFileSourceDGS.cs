using Java.Io;
using Java.Net;
using Java.Util;
using Java.Util.Zip;
using Gs_Core.Graphstream.Stream.File;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Dgs.ElementType;
using static Org.Graphstream.Stream.File.Dgs.EventType;
using static Org.Graphstream.Stream.File.Dgs.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Dgs.Mode;
using static Org.Graphstream.Stream.File.Dgs.What;
using static Org.Graphstream.Stream.File.Dgs.TimeFormat;
using static Org.Graphstream.Stream.File.Dgs.OutputType;
using static Org.Graphstream.Stream.File.Dgs.OutputPolicy;
using static Org.Graphstream.Stream.File.Dgs.LayoutPolicy;
using static Org.Graphstream.Stream.File.Dgs.Quality;
using static Org.Graphstream.Stream.File.Dgs.Option;
using static Org.Graphstream.Stream.File.Dgs.AttributeType;
using static Org.Graphstream.Stream.File.Dgs.Balise;
using static Org.Graphstream.Stream.File.Dgs.GEXFAttribute;
using static Org.Graphstream.Stream.File.Dgs.METAAttribute;
using static Org.Graphstream.Stream.File.Dgs.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Dgs.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Dgs.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Dgs.NODESAttribute;
using static Org.Graphstream.Stream.File.Dgs.NODEAttribute;
using static Org.Graphstream.Stream.File.Dgs.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Dgs.PARENTAttribute;
using static Org.Graphstream.Stream.File.Dgs.EDGESAttribute;
using static Org.Graphstream.Stream.File.Dgs.SPELLAttribute;
using static Org.Graphstream.Stream.File.Dgs.COLORAttribute;
using static Org.Graphstream.Stream.File.Dgs.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Dgs.SIZEAttribute;
using static Org.Graphstream.Stream.File.Dgs.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Dgs.EDGEAttribute;
using static Org.Graphstream.Stream.File.Dgs.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Dgs.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Dgs.IDType;
using static Org.Graphstream.Stream.File.Dgs.ModeType;
using static Org.Graphstream.Stream.File.Dgs.WeightType;
using static Org.Graphstream.Stream.File.Dgs.EdgeType;
using static Org.Graphstream.Stream.File.Dgs.NodeShapeType;
using static Org.Graphstream.Stream.File.Dgs.EdgeShapeType;
using static Org.Graphstream.Stream.File.Dgs.ClassType;
using static Org.Graphstream.Stream.File.Dgs.TimeFormatType;
using static Org.Graphstream.Stream.File.Dgs.GPXAttribute;
using static Org.Graphstream.Stream.File.Dgs.WPTAttribute;
using static Org.Graphstream.Stream.File.Dgs.LINKAttribute;
using static Org.Graphstream.Stream.File.Dgs.EMAILAttribute;
using static Org.Graphstream.Stream.File.Dgs.PTAttribute;
using static Org.Graphstream.Stream.File.Dgs.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Dgs.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Dgs.FixType;
using static Org.Graphstream.Stream.File.Dgs.GraphAttribute;
using static Org.Graphstream.Stream.File.Dgs.LocatorAttribute;
using static Org.Graphstream.Stream.File.Dgs.NodeAttribute;
using static Org.Graphstream.Stream.File.Dgs.EdgeAttribute;
using static Org.Graphstream.Stream.File.Dgs.DataAttribute;
using static Org.Graphstream.Stream.File.Dgs.PortAttribute;
using static Org.Graphstream.Stream.File.Dgs.EndPointAttribute;
using static Org.Graphstream.Stream.File.Dgs.EndPointType;
using static Org.Graphstream.Stream.File.Dgs.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Dgs.KeyAttribute;
using static Org.Graphstream.Stream.File.Dgs.KeyDomain;
using static Org.Graphstream.Stream.File.Dgs.KeyAttrType;
using static Org.Graphstream.Stream.File.Dgs.GraphEvents;
using static Org.Graphstream.Stream.File.Dgs.ThreadingModel;
using static Org.Graphstream.Stream.File.Dgs.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Dgs.Measures;
using static Org.Graphstream.Stream.File.Dgs.Token;

namespace Gs_Core.Graphstream.Stream.File.Dgs;

public class OldFileSourceDGS : FileSourceBase
{
    protected int version;
    protected string graphName;
    protected int stepCountAnnounced;
    protected int eventCountAnnounced;
    protected int stepCount;
    protected int eventCount;
    protected HashMap<string, object> attributes = new HashMap<string, object>();
    protected bool finished;
    public OldFileSourceDGS() : base(true)
    {
    }

    public override bool NextEvents()
    {
        if (finished)
            return false;
        return Next(false, false);
    }

    public virtual bool NextStep()
    {
        if (finished)
            return false;
        return Next(true, false);
    }

    protected virtual bool Next(bool readSteps, bool stop)
    {
        string key = null;
        bool loop = readSteps;
        do
        {
            key = GetWordOrSymbolOrStringOrEolOrEof();
            if (key.Equals("ce"))
            {
                ReadCE();
            }
            else if (key.Equals("cn"))
            {
                ReadCN();
            }
            else if (key.Equals("ae"))
            {
                ReadAE();
            }
            else if (key.Equals("an"))
            {
                ReadAN();
            }
            else if (key.Equals("de"))
            {
                ReadDE();
            }
            else if (key.Equals("dn"))
            {
                ReadDN();
            }
            else if (key.Equals("cg"))
            {
                ReadCG();
            }
            else if (key.Equals("st"))
            {
                if (readSteps)
                {
                    if (stop)
                    {
                        loop = false;
                        PushBack();
                    }
                    else
                    {
                        stop = true;
                        ReadST();
                    }
                }
                else
                {
                    ReadST();
                }
            }
            else if (key.Equals("#"))
            {
                EatAllUntilEol();
                return Next(readSteps, stop);
            }
            else if (key.Equals("EOL"))
            {
                return Next(readSteps, stop);
            }
            else if (key.Equals("EOF"))
            {
                finished = true;
                return false;
            }
            else
            {
                ParseError("unknown token '" + key + "'");
            }
        }
        while (loop);
        return true;
    }

    protected virtual void ReadCE()
    {
        string tag = GetStringOrWordOrNumber();
        ReadAttributes(attributes);
        foreach (string key in attributes.KeySet())
        {
            object value = attributes[key];
            if (value == null)
                SendEdgeAttributeRemoved(graphName, tag, key);
            else
                SendEdgeAttributeChanged(graphName, tag, key, null, value);
        }

        if (EatEolOrEof() == StreamTokenizer.TT_EOF)
            PushBack();
    }

    protected virtual void ReadCN()
    {
        string tag = GetStringOrWordOrNumber();
        ReadAttributes(attributes);
        foreach (string key in attributes.KeySet())
        {
            object value = attributes[key];
            if (value == null)
                SendNodeAttributeRemoved(graphName, tag, key);
            else
                SendNodeAttributeChanged(graphName, tag, key, null, value);
        }

        if (EatEolOrEof() == StreamTokenizer.TT_EOF)
            PushBack();
    }

    protected virtual void ReadCG()
    {
        ReadAttributes(attributes);
        foreach (string key in attributes.KeySet())
        {
            object value = attributes[key];
            if (value == null)
                SendGraphAttributeRemoved(graphName, key);
            else
                SendGraphAttributeChanged(graphName, key, null, value);
        }

        if (EatEolOrEof() == StreamTokenizer.TT_EOF)
            PushBack();
    }

    protected virtual void ReadAE()
    {
        int dir = 0;
        bool directed = false;
        string dirc = null;
        string tag = null;
        string fromTag = null;
        string toTag = null;
        tag = GetStringOrWordOrNumber();
        fromTag = GetStringOrWordOrNumber();
        dirc = GetWordOrSymbolOrNumberOrStringOrEolOrEof();
        if (dirc.Equals(">"))
        {
            directed = true;
            dir = 1;
        }
        else if (dirc.Equals("<"))
        {
            directed = true;
            dir = 2;
        }
        else
        {
            PushBack();
        }

        toTag = GetStringOrWordOrNumber();
        if (dir == 2)
        {
            string tmp = toTag;
            toTag = fromTag;
            fromTag = tmp;
        }

        ReadAttributes(attributes);
        SendEdgeAdded(graphName, tag, fromTag, toTag, directed);
        foreach (string key in attributes.KeySet())
        {
            object value = attributes[key];
            SendEdgeAttributeAdded(graphName, tag, key, value);
        }

        if (EatEolOrEof() == StreamTokenizer.TT_EOF)
            PushBack();
    }

    protected virtual void ReadAN()
    {
        string tag = GetStringOrWordOrNumber();
        ReadAttributes(attributes);
        SendNodeAdded(graphName, tag);
        foreach (string key in attributes.KeySet())
        {
            object value = attributes[key];
            SendNodeAttributeAdded(graphName, tag, key, value);
        }

        if (EatEolOrEof() == StreamTokenizer.TT_EOF)
            PushBack();
    }

    protected virtual void ReadDE()
    {
        string tag = GetStringOrWordOrNumber();
        SendEdgeRemoved(graphName, tag);
        if (EatEolOrEof() == StreamTokenizer.TT_EOF)
            PushBack();
    }

    protected virtual void ReadDN()
    {
        string tag = GetStringOrWordOrNumber();
        SendNodeRemoved(graphName, tag);
        if (EatEolOrEof() == StreamTokenizer.TT_EOF)
            PushBack();
    }

    protected virtual void ReadST()
    {
        string w = GetWordOrNumber();
        try
        {
            double time = Double.ParseDouble(w);
            SendStepBegins(graphName, time);
        }
        catch (NumberFormatException e)
        {
            ParseError("expecting a number after `st', got `" + w + "'");
        }

        if (EatEolOrEof() == StreamTokenizer.TT_EOF)
            PushBack();
    }

    protected virtual void ReadAttributes(HashMap<string, object> attributes)
    {
        bool del = false;
        string key = GetWordOrSymbolOrStringOrEolOrEof();
        attributes.Clear();
        if (key.Equals("-"))
        {
            key = GetWordOrSymbolOrStringOrEolOrEof();
            del = true;
        }

        if (key.Equals("+"))
            key = GetWordOrSymbolOrStringOrEolOrEof();
        while (!key.Equals("EOF") && !key.Equals("EOL") && !key.Equals("]"))
        {
            if (del)
                attributes.Put(key, null);
            else
                attributes.Put(key, ReadAttributeValue(key));
            key = GetWordOrSymbolOrStringOrEolOrEof();
            if (key.Equals("-"))
            {
                key = GetWordOrStringOrEolOrEof();
                del = true;
            }

            if (key.Equals("+"))
            {
                key = GetWordOrStringOrEolOrEof();
                del = false;
            }
        }

        PushBack();
    }

    protected virtual object ReadAttributeValue(string key)
    {
        List<object> vector = null;
        object value = null;
        object value2 = null;
        string next = null;
        if (key != null)
            EatSymbols(":=");
        value = GetStringOrWordOrSymbolOrNumberO();
        if (value.Equals("["))
        {
            HashMap<string, object> map = new HashMap<string, object>();
            ReadAttributes(map);
            EatSymbol(']');
            value = map;
        }
        else if (value.Equals("{"))
        {
            vector = ReadAttributeArray(key);
            EatSymbol('}');
        }
        else
        {
            PushBack();
            value = GetStringOrWordOrNumberO();
            if (key != null)
            {
                next = GetWordOrSymbolOrNumberOrStringOrEolOrEof();
                while (next.Equals(","))
                {
                    if (vector == null)
                    {
                        vector = new List<object>();
                        vector.Add(value);
                    }

                    value2 = GetStringOrWordOrNumberO();
                    next = GetWordOrSymbolOrNumberOrStringOrEolOrEof();
                    vector.Add(value2);
                }

                PushBack();
            }
        }

        if (vector != null)
            return vector.ToArray();
        else
            return value;
    }

    protected virtual List<object> ReadAttributeArray(string key)
    {
        List<object> list = new List<object>();
        object value;
        string next;
        do
        {
            value = ReadAttributeValue(null);
            next = GetWordOrSymbolOrNumberOrStringOrEolOrEof();
            list.Add(value);
        }
        while (next.Equals(","));
        PushBack();
        return list;
    }

    public override void Begin(string filename)
    {
        base.Begin(filename);
        Begin();
    }

    public override void Begin(URL url)
    {
        base.Begin(url);
        Begin();
    }

    public override void Begin(InputStream stream)
    {
        base.Begin(stream);
        Begin();
    }

    public override void Begin(Reader reader)
    {
        base.Begin(reader);
        Begin();
    }

    protected virtual void Begin()
    {
        st.ParseNumbers();
        EatWords("DGS003", "DGS004");
        version = 3;
        EatEol();
        graphName = GetWordOrString();
        stepCountAnnounced = (int)GetNumber();
        eventCountAnnounced = (int)GetNumber();
        EatEol();
        if (graphName != null)
            SendGraphAttributeAdded(graphName, "label", graphName);
        else
            graphName = "DGS_";
        graphName = String.Format("%s_%d", graphName, System.CurrentTimeMillis() + ((long)Math.Random() * 10));
    }

    protected override void ContinueParsingInInclude()
    {
    }

    protected override Reader CreateReaderFrom(string file)
    {
        InputStream is = null;
        @is = new FileInputStream(file);
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

                @is = new FileInputStream(file);
            }
        }

        return new BufferedReader(new InputStreamReader(@is));
    }

    protected override Reader CreateReaderFrom(InputStream stream)
    {
        return new BufferedReader(new InputStreamReader(stream));
    }

    protected override void ConfigureTokenizer(StreamTokenizer tok)
    {
        if (COMMENT_CHAR > 0)
            tok.CommentChar(COMMENT_CHAR);
        tok.EolIsSignificant(eol_is_significant);
        tok.ParseNumbers();
        tok.WordChars('_', '_');
    }
}
