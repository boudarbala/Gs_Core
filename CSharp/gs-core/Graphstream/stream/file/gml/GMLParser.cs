using Java.Io;
using Gs_Core.Graphstream.Stream.File;
using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Gml.ElementType;
using static Org.Graphstream.Stream.File.Gml.EventType;
using static Org.Graphstream.Stream.File.Gml.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Gml.Mode;
using static Org.Graphstream.Stream.File.Gml.What;
using static Org.Graphstream.Stream.File.Gml.TimeFormat;
using static Org.Graphstream.Stream.File.Gml.OutputType;
using static Org.Graphstream.Stream.File.Gml.OutputPolicy;
using static Org.Graphstream.Stream.File.Gml.LayoutPolicy;
using static Org.Graphstream.Stream.File.Gml.Quality;
using static Org.Graphstream.Stream.File.Gml.Option;
using static Org.Graphstream.Stream.File.Gml.AttributeType;
using static Org.Graphstream.Stream.File.Gml.Balise;
using static Org.Graphstream.Stream.File.Gml.GEXFAttribute;
using static Org.Graphstream.Stream.File.Gml.METAAttribute;
using static Org.Graphstream.Stream.File.Gml.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Gml.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Gml.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Gml.NODESAttribute;
using static Org.Graphstream.Stream.File.Gml.NODEAttribute;
using static Org.Graphstream.Stream.File.Gml.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Gml.PARENTAttribute;
using static Org.Graphstream.Stream.File.Gml.EDGESAttribute;
using static Org.Graphstream.Stream.File.Gml.SPELLAttribute;
using static Org.Graphstream.Stream.File.Gml.COLORAttribute;
using static Org.Graphstream.Stream.File.Gml.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Gml.SIZEAttribute;
using static Org.Graphstream.Stream.File.Gml.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gml.EDGEAttribute;
using static Org.Graphstream.Stream.File.Gml.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Gml.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gml.IDType;
using static Org.Graphstream.Stream.File.Gml.ModeType;
using static Org.Graphstream.Stream.File.Gml.WeightType;
using static Org.Graphstream.Stream.File.Gml.EdgeType;
using static Org.Graphstream.Stream.File.Gml.NodeShapeType;
using static Org.Graphstream.Stream.File.Gml.EdgeShapeType;
using static Org.Graphstream.Stream.File.Gml.ClassType;
using static Org.Graphstream.Stream.File.Gml.TimeFormatType;
using static Org.Graphstream.Stream.File.Gml.GPXAttribute;
using static Org.Graphstream.Stream.File.Gml.WPTAttribute;
using static Org.Graphstream.Stream.File.Gml.LINKAttribute;
using static Org.Graphstream.Stream.File.Gml.EMAILAttribute;
using static Org.Graphstream.Stream.File.Gml.PTAttribute;
using static Org.Graphstream.Stream.File.Gml.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Gml.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Gml.FixType;
using static Org.Graphstream.Stream.File.Gml.GraphAttribute;
using static Org.Graphstream.Stream.File.Gml.LocatorAttribute;
using static Org.Graphstream.Stream.File.Gml.NodeAttribute;
using static Org.Graphstream.Stream.File.Gml.EdgeAttribute;
using static Org.Graphstream.Stream.File.Gml.DataAttribute;
using static Org.Graphstream.Stream.File.Gml.PortAttribute;
using static Org.Graphstream.Stream.File.Gml.EndPointAttribute;
using static Org.Graphstream.Stream.File.Gml.EndPointType;
using static Org.Graphstream.Stream.File.Gml.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Gml.KeyAttribute;
using static Org.Graphstream.Stream.File.Gml.KeyDomain;
using static Org.Graphstream.Stream.File.Gml.KeyAttrType;
using static Org.Graphstream.Stream.File.Gml.GraphEvents;
using static Org.Graphstream.Stream.File.Gml.ThreadingModel;
using static Org.Graphstream.Stream.File.Gml.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Gml.Measures;
using static Org.Graphstream.Stream.File.Gml.Token;
using static Org.Graphstream.Stream.File.Gml.Extension;
using static Org.Graphstream.Stream.File.Gml.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Gml.AttrType;

namespace Gs_Core.Graphstream.Stream.File.Gml;

public class GMLParser : Parser, GMLParserConstants
{
    bool inGraph = false;
    GMLContext ctx;
    bool step;
    public GMLParser(FileSourceGML gml, InputStream stream) : this(stream)
    {
        this.ctx = new GMLContext(gml);
    }

    public GMLParser(FileSourceGML gml, Reader stream) : this(stream)
    {
        this.ctx = new GMLContext(gml);
    }

    public virtual bool IsInGraph()
    {
        return inGraph;
    }

    public virtual void Open()
    {
    }

    public virtual bool Next()
    {
        KeyValues kv = null;
        kv = NextEvents();
        ctx.HandleKeyValues(kv);
        return (kv != null);
    }

    public virtual bool Step()
    {
        KeyValues kv = null;
        step = false;
        while ((kv = NextEvents()) != null && !step)
            ctx.HandleKeyValues(kv);
        if (kv != null)
            ctx.SetNextStep(kv);
        return (kv != null);
    }

    public virtual void Dispose()
    {
        jj_input_stream.Dispose();
    }

    public void Start()
    {
        List();
    }

    public void All()
    {
        KeyValues values = new KeyValues();
        string key;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case GRAPH:
                GraphStart();
                ctx.SetIsInGraph(true);
                ctx.SetDirected(false);
                break;
            case DIGRAPH:
                DiGraphStart();
                ctx.SetIsInGraph(true);
                ctx.SetDirected(true);
                break;
            default:
                jj_la1[0] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        label_1:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case STRING:
                    case KEY:
                    case COMMENT:
                        break;
                    default:
                        jj_la1[1] = jj_gen;
                        break;
                }

                key = KeyValue(values);
                values.key = key;
                ctx.HandleKeyValues(values);
                values.Clear();
            }

        GraphEnd();
        values.key = null;
        inGraph = false;
        Jj_consume_token(0);
    }

    public void GraphStart()
    {
        Jj_consume_token(GRAPH);
        Jj_consume_token(LSQBR);
    }

    public void DiGraphStart()
    {
        Jj_consume_token(DIGRAPH);
        Jj_consume_token(LSQBR);
    }

    public void GraphEnd()
    {
        Jj_consume_token(RSQBR);
    }

    public KeyValues NextEvents()
    {
        KeyValues values = new KeyValues();
        string key;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case GRAPH:
                GraphStart();
                values.key = null;
                ctx.SetIsInGraph(true);
                ctx.SetDirected(false);
                break;
            case DIGRAPH:
                DiGraphStart();
                values.key = null;
                ctx.SetIsInGraph(true);
                ctx.SetDirected(true);
                break;
            case RSQBR:
                GraphEnd();
                values.key = null;
                ctx.SetIsInGraph(false);
                break;
            case STRING:
            case KEY:
            case COMMENT:
                key = KeyValue(values);
                values.key = key;
                break;
            case 0:
                Jj_consume_token(0);
                values = null;
                break;
            default:
                jj_la1[2] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return values;
        }

        throw new Exception("Missing return statement in function");
    }

    public KeyValues List()
    {
        KeyValues values = new KeyValues();
        label_2:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case STRING:
                    case KEY:
                    case COMMENT:
                        break;
                    default:
                        jj_la1[3] = jj_gen;
                        break;
                }

                KeyValue(values);
            }

        {
            if (true)
                return values;
        }

        throw new Exception("Missing return statement in function");
    }

    public string KeyValue(KeyValues values)
    {
        Token k;
        string key;
        object v;
        bool isGraph = false;
        label_3:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case COMMENT:
                        break;
                    default:
                        jj_la1[4] = jj_gen;
                        break;
                }

                Jj_consume_token(COMMENT);
            }

        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case KEY:
                k = Jj_consume_token(KEY);
                key = k.image;
                if (key.EqualsIgnoreCase("step"))
                    step = true;
                break;
            case STRING:
                k = Jj_consume_token(STRING);
                key = k.image.Substring(1, k.image.Length - 2);
                break;
            default:
                jj_la1[5] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        v = Value(key);
        values.Put(key, v);
        values.line = k.beginLine;
        values.column = k.beginColumn;
        {
            if (true)
                return key;
        }

        throw new Exception("Missing return statement in function");
    }

    public object Value(string key)
    {
        Token t;
        object val;
        KeyValues kv;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case REAL:
                t = Jj_consume_token(REAL);
                if (t.image.IndexOf('.') < 0)
                    val = Integer.ValueOf(t.image);
                else
                    val = Double.ValueOf(t.image);
                break;
            case STRING:
                t = Jj_consume_token(STRING);
                val = t.image.Substring(1, t.image.Length - 1);
                break;
            case KEY:
                t = Jj_consume_token(KEY);
                val = t.image;
                break;
            case LSQBR:
                Jj_consume_token(LSQBR);
                kv = List();
                val = kv;
                Jj_consume_token(RSQBR);
                break;
            default:
                jj_la1[6] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return val;
        }

        throw new Exception("Missing return statement in function");
    }

    public GMLParserTokenManager token_source;
    SimpleCharStream jj_input_stream;
    public Token token;
    public Token jj_nt;
    private int jj_ntk;
    private int jj_gen;
    private readonly int[] jj_la1 = new int[7];
    private static int[] jj_la1_0;
    static GMLParser()
    {
        Jj_la1_init_0();
    }

    private static void Jj_la1_init_0()
    {
        jj_la1_0 = new int[]
        {
            0x3000,
            0xc800,
            0xfa01,
            0xc800,
            0x8000,
            0x4800,
            0x4d00
        };
    }

    public GMLParser(java.io.InputStream stream) : this(stream, null)
    {
    }

    public GMLParser(java.io.InputStream stream, string encoding)
    {
        try
        {
            jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1);
        }
        catch (java.io.UnsupportedEncodingException e)
        {
            throw new Exception(e);
        }

        token_source = new GMLParserTokenManager(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 7; i++)
            jj_la1[i] = -1;
    }

    public virtual void ReInit(java.io.InputStream stream)
    {
        ReInit(stream, null);
    }

    public virtual void ReInit(java.io.InputStream stream, string encoding)
    {
        try
        {
            jj_input_stream.ReInit(stream, encoding, 1, 1);
        }
        catch (java.io.UnsupportedEncodingException e)
        {
            throw new Exception(e);
        }

        token_source.ReInit(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 7; i++)
            jj_la1[i] = -1;
    }

    public GMLParser(java.io.Reader stream)
    {
        jj_input_stream = new SimpleCharStream(stream, 1, 1);
        token_source = new GMLParserTokenManager(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 7; i++)
            jj_la1[i] = -1;
    }

    public virtual void ReInit(java.io.Reader stream)
    {
        jj_input_stream.ReInit(stream, 1, 1);
        token_source.ReInit(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 7; i++)
            jj_la1[i] = -1;
    }

    public GMLParser(GMLParserTokenManager tm)
    {
        token_source = tm;
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 7; i++)
            jj_la1[i] = -1;
    }

    public virtual void ReInit(GMLParserTokenManager tm)
    {
        token_source = tm;
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 7; i++)
            jj_la1[i] = -1;
    }

    private Token Jj_consume_token(int kind)
    {
        Token oldToken;
        if ((oldToken = token).next != null)
            token = token.next;
        else
            token = token.next = token_source.GetNextToken();
        jj_ntk = -1;
        if (token.kind == kind)
        {
            jj_gen++;
            return token;
        }

        token = oldToken;
        jj_kind = kind;
        throw GenerateParseException();
    }

    public Token GetNextToken()
    {
        if (token.next != null)
            token = token.next;
        else
            token = token.next = token_source.GetNextToken();
        jj_ntk = -1;
        jj_gen++;
        return token;
    }

    public Token GetToken(int index)
    {
        Token t = token;
        for (int i = 0; i < index; i++)
        {
            if (t.next != null)
                t = t.next;
            else
                t = t.next = token_source.GetNextToken();
        }

        return t;
    }

    private int Jj_ntk()
    {
        if ((jj_nt = token.next) == null)
            return (jj_ntk = (token.next = token_source.GetNextToken()).kind);
        else
            return (jj_ntk = jj_nt.kind);
    }

    private java.util.List<int[]> jj_expentries = new List<int[]>();
    private int[] jj_expentry;
    private int jj_kind = -1;
    public virtual ParseException GenerateParseException()
    {
        jj_expentries.Clear();
        bool[] la1tokens = new bool[16];
        if (jj_kind >= 0)
        {
            la1tokens[jj_kind] = true;
            jj_kind = -1;
        }

        for (int i = 0; i < 7; i++)
        {
            if (jj_la1[i] == jj_gen)
            {
                for (int j = 0; j < 32; j++)
                {
                    if ((jj_la1_0[i] & (1 << j)) != 0)
                    {
                        la1tokens[j] = true;
                    }
                }
            }
        }

        for (int i = 0; i < 16; i++)
        {
            if (la1tokens[i])
            {
                jj_expentry = new int[1];
                jj_expentry[0] = i;
                jj_expentries.Add(jj_expentry);
            }
        }

        int[, ] exptokseq = new int[jj_expentries.Count];
        for (int i = 0; i < jj_expentries.Count; i++)
        {
            exptokseq[i] = jj_expentries[i];
        }

        return new ParseException(token, exptokseq, tokenImage);
    }

    public void Enable_tracing()
    {
    }

    public void Disable_tracing()
    {
    }
}
