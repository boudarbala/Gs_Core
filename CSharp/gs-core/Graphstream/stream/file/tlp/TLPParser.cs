using Java.Io;
using Java.Util;
using Gs_Core.Graphstream.Stream.SourceBase;
using Gs_Core.Graphstream.Stream.File;
using Gs_Core.Graphstream.Graph.Implementations.AbstractElement;
using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Tlp.ElementType;
using static Org.Graphstream.Stream.File.Tlp.EventType;
using static Org.Graphstream.Stream.File.Tlp.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Tlp.Mode;
using static Org.Graphstream.Stream.File.Tlp.What;
using static Org.Graphstream.Stream.File.Tlp.TimeFormat;
using static Org.Graphstream.Stream.File.Tlp.OutputType;
using static Org.Graphstream.Stream.File.Tlp.OutputPolicy;
using static Org.Graphstream.Stream.File.Tlp.LayoutPolicy;
using static Org.Graphstream.Stream.File.Tlp.Quality;
using static Org.Graphstream.Stream.File.Tlp.Option;
using static Org.Graphstream.Stream.File.Tlp.AttributeType;
using static Org.Graphstream.Stream.File.Tlp.Balise;
using static Org.Graphstream.Stream.File.Tlp.GEXFAttribute;
using static Org.Graphstream.Stream.File.Tlp.METAAttribute;
using static Org.Graphstream.Stream.File.Tlp.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Tlp.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Tlp.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Tlp.NODESAttribute;
using static Org.Graphstream.Stream.File.Tlp.NODEAttribute;
using static Org.Graphstream.Stream.File.Tlp.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Tlp.PARENTAttribute;
using static Org.Graphstream.Stream.File.Tlp.EDGESAttribute;
using static Org.Graphstream.Stream.File.Tlp.SPELLAttribute;
using static Org.Graphstream.Stream.File.Tlp.COLORAttribute;
using static Org.Graphstream.Stream.File.Tlp.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Tlp.SIZEAttribute;
using static Org.Graphstream.Stream.File.Tlp.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Tlp.EDGEAttribute;
using static Org.Graphstream.Stream.File.Tlp.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Tlp.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Tlp.IDType;
using static Org.Graphstream.Stream.File.Tlp.ModeType;
using static Org.Graphstream.Stream.File.Tlp.WeightType;
using static Org.Graphstream.Stream.File.Tlp.EdgeType;
using static Org.Graphstream.Stream.File.Tlp.NodeShapeType;
using static Org.Graphstream.Stream.File.Tlp.EdgeShapeType;
using static Org.Graphstream.Stream.File.Tlp.ClassType;
using static Org.Graphstream.Stream.File.Tlp.TimeFormatType;
using static Org.Graphstream.Stream.File.Tlp.GPXAttribute;
using static Org.Graphstream.Stream.File.Tlp.WPTAttribute;
using static Org.Graphstream.Stream.File.Tlp.LINKAttribute;
using static Org.Graphstream.Stream.File.Tlp.EMAILAttribute;
using static Org.Graphstream.Stream.File.Tlp.PTAttribute;
using static Org.Graphstream.Stream.File.Tlp.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Tlp.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Tlp.FixType;
using static Org.Graphstream.Stream.File.Tlp.GraphAttribute;
using static Org.Graphstream.Stream.File.Tlp.LocatorAttribute;
using static Org.Graphstream.Stream.File.Tlp.NodeAttribute;
using static Org.Graphstream.Stream.File.Tlp.EdgeAttribute;
using static Org.Graphstream.Stream.File.Tlp.DataAttribute;
using static Org.Graphstream.Stream.File.Tlp.PortAttribute;
using static Org.Graphstream.Stream.File.Tlp.EndPointAttribute;
using static Org.Graphstream.Stream.File.Tlp.EndPointType;
using static Org.Graphstream.Stream.File.Tlp.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Tlp.KeyAttribute;
using static Org.Graphstream.Stream.File.Tlp.KeyDomain;
using static Org.Graphstream.Stream.File.Tlp.KeyAttrType;
using static Org.Graphstream.Stream.File.Tlp.GraphEvents;
using static Org.Graphstream.Stream.File.Tlp.ThreadingModel;
using static Org.Graphstream.Stream.File.Tlp.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Tlp.Measures;
using static Org.Graphstream.Stream.File.Tlp.Token;
using static Org.Graphstream.Stream.File.Tlp.Extension;
using static Org.Graphstream.Stream.File.Tlp.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Tlp.AttrType;
using static Org.Graphstream.Stream.File.Tlp.Resolutions;
using static Org.Graphstream.Stream.File.Tlp.PropertyType;

namespace Gs_Core.Graphstream.Stream.File.Tlp;

public class TLPParser : Parser, TLPParserConstants
{
    protected enum PropertyType
    {
        BOOL,
        COLOR,
        DOUBLE,
        LAYOUT,
        INT,
        SIZE,
        STRING
    }

    protected class Cluster
    {
        int index;
        string name;
        LinkedList<string> nodes;
        LinkedList<string> edges;
        Cluster(int index, string name)
        {
            this.index = index;
            this.name = name;
            this.nodes = new LinkedList<string>();
            this.edges = new LinkedList<string>();
        }
    }

    private FileSourceTLP tlp;
    private string sourceId;
    private Cluster root;
    private HashMap<int, Cluster> clusters;
    private Stack<Cluster> stack;
    public TLPParser(FileSourceTLP tlp, InputStream stream) : this(stream)
    {
        Init(tlp);
    }

    public TLPParser(FileSourceTLP tlp, Reader stream) : this(stream)
    {
        Init(tlp);
    }

    public virtual void Dispose()
    {
        jj_input_stream.Dispose();
        clusters.Clear();
    }

    private void Init(FileSourceTLP tlp)
    {
        this.tlp = tlp;
        this.sourceId = String.Format("<DOT stream %x>", System.NanoTime());
        this.clusters = new HashMap<int, Cluster>();
        this.stack = new Stack<Cluster>();
        this.root = new Cluster(0, "<root>");
        this.clusters.Put(0, this.root);
        this.stack.Push(this.root);
    }

    private void AddNode(string id)
    {
        if (stack.Count > 1 && (!root.nodes.Contains(id) || !stack[stack.Count - 2].nodes.Contains(id)))
            throw new ParseException("parent cluster do not contain the node");
        if (stack.Count == 1)
            tlp.SendNodeAdded(sourceId, id);
        stack.Peek().nodes.Add(id);
    }

    private void AddEdge(string id, string source, string target)
    {
        if (stack.Count > 1 && (!root.edges.Contains(id) || !stack[stack.Count - 2].edges.Contains(id)))
            throw new ParseException("parent cluster " + stack[stack.Count - 2].name + " do not contain the edge");
        if (stack.Count == 1)
            tlp.SendEdgeAdded(sourceId, id, source, target, false);
        stack.Peek().edges.Add(id);
    }

    private void IncludeEdge(string id)
    {
        if (stack.Count > 1 && (!root.edges.Contains(id) || !stack[stack.Count - 2].edges.Contains(id)))
            throw new ParseException("parent cluster " + stack[stack.Count - 2].name + " do not contain the edge");
        stack.Peek().edges.Add(id);
    }

    private void GraphAttribute(string key, object value)
    {
        tlp.SendAttributeChangedEvent(sourceId, sourceId, ElementType.GRAPH, key, AttributeChangeEvent.ADD, null, value);
    }

    private void PushCluster(int i, string name)
    {
        Cluster c = new Cluster(i, name);
        clusters.Put(i, c);
        stack.Push(c);
    }

    private void PopCluster()
    {
        if (stack.Count > 1)
            stack.Pop();
    }

    private void NewProperty(int cluster, string name, PropertyType type, string nodeDefault, string edgeDefault, HashMap<string, string> nodes, HashMap<string, string> edges)
    {
        object nodeDefaultValue = Convert(type, nodeDefault);
        object edgeDefaultValue = Convert(type, edgeDefault);
        Cluster c = clusters[cluster];
        foreach (string id in c.nodes)
        {
            object value = nodeDefaultValue;
            if (nodes.ContainsKey(id))
                value = Convert(type, nodes[id]);
            tlp.SendAttributeChangedEvent(sourceId, id, ElementType.NODE, name, AttributeChangeEvent.ADD, null, value);
        }

        foreach (string id in c.edges)
        {
            object value = edgeDefaultValue;
            if (edges.ContainsKey(id))
                value = Convert(type, edges[id]);
            tlp.SendAttributeChangedEvent(sourceId, id, ElementType.EDGE, name, AttributeChangeEvent.ADD, null, value);
        }
    }

    private object Convert(PropertyType type, string value)
    {
        switch (type)
        {
            case BOOL:
                return Boolean.ValueOf(value);
            case INT:
                return Integer.ValueOf(value);
            case DOUBLE:
                return Double.ValueOf(value);
            case LAYOUT:
            case COLOR:
            case SIZE:
            case STRING:
                return value;
        }

        return value;
    }

    public void All()
    {
        Tlp();
        label_1:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case OBRACKET:
                        break;
                    default:
                        jj_la1[0] = jj_gen;
                        break;
                }

                Statement();
            }

        Jj_consume_token(CBRACKET);
        Jj_consume_token(0);
    }

    public bool Next()
    {
        bool hasMore = false;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case OBRACKET:
                Statement();
                hasMore = true;
                break;
            case 0:
                Jj_consume_token(0);
                break;
            default:
                jj_la1[1] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        return hasMore;
    }

    public void Open()
    {
        Tlp();
    }

    private void Tlp()
    {
        Jj_consume_token(OBRACKET);
        Jj_consume_token(TLP);
        Jj_consume_token(STRING);
        label_2:
            while (true)
            {
                if (Jj_2_1(2))
                {
                }
                else
                {
                    break;
                }

                Headers();
            }
    }

    private void Headers()
    {
        string s;
        Jj_consume_token(OBRACKET);
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case DATE:
                Jj_consume_token(DATE);
                s = String();
                GraphAttribute("date", s);
                break;
            case AUTHOR:
                Jj_consume_token(AUTHOR);
                s = String();
                GraphAttribute("author", s);
                break;
            case COMMENTS:
                Jj_consume_token(COMMENTS);
                s = String();
                GraphAttribute("comments", s);
                break;
            default:
                jj_la1[2] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        Jj_consume_token(CBRACKET);
    }

    private void Statement()
    {
        if (Jj_2_2(2))
        {
            Nodes();
        }
        else if (Jj_2_3(2))
        {
            Edge();
        }
        else if (Jj_2_4(2))
        {
            Cluster();
        }
        else if (Jj_2_5(2))
        {
            Property();
        }
        else
        {
            Jj_consume_token(-1);
            throw new ParseException();
        }
    }

    private void Nodes()
    {
        Token i;
        Jj_consume_token(OBRACKET);
        Jj_consume_token(NODES);
        label_3:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case INTEGER:
                        break;
                    default:
                        jj_la1[3] = jj_gen;
                        break;
                }

                i = Jj_consume_token(INTEGER);
                AddNode(i.image);
            }

        Jj_consume_token(CBRACKET);
    }

    private void Edge()
    {
        Token i, s, t;
        Jj_consume_token(OBRACKET);
        Jj_consume_token(EDGE);
        i = Jj_consume_token(INTEGER);
        s = Jj_consume_token(INTEGER);
        t = Jj_consume_token(INTEGER);
        Jj_consume_token(CBRACKET);
        AddEdge(i.image, s.image, t.image);
    }

    private void Edges()
    {
        Token i;
        Jj_consume_token(OBRACKET);
        Jj_consume_token(EDGES);
        label_4:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case INTEGER:
                        break;
                    default:
                        jj_la1[4] = jj_gen;
                        break;
                }

                i = Jj_consume_token(INTEGER);
                IncludeEdge(i.image);
            }

        Jj_consume_token(CBRACKET);
    }

    private void Cluster()
    {
        Token index;
        string name;
        Jj_consume_token(OBRACKET);
        Jj_consume_token(CLUSTER);
        index = Jj_consume_token(INTEGER);
        name = String();
        PushCluster(Integer.ValueOf(index.image), name);
        Nodes();
        Edges();
        label_5:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case OBRACKET:
                        break;
                    default:
                        jj_la1[5] = jj_gen;
                        break;
                }

                Cluster();
            }

        Jj_consume_token(CBRACKET);
        PopCluster();
    }

    private void Property()
    {
        PropertyType type;
        int cluster;
        string name;
        string nodeDefault, edgeDefault;
        string value;
        Token t;
        HashMap<string, string> nodes = new HashMap<string, string>();
        HashMap<string, string> edges = new HashMap<string, string>();
        Jj_consume_token(OBRACKET);
        Jj_consume_token(PROPERTY);
        cluster = Integer();
        type = Type();
        name = String();
        Jj_consume_token(OBRACKET);
        Jj_consume_token(DEF);
        nodeDefault = String();
        edgeDefault = String();
        Jj_consume_token(CBRACKET);
        label_6:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case OBRACKET:
                        break;
                    default:
                        jj_la1[6] = jj_gen;
                        break;
                }

                Jj_consume_token(OBRACKET);
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case NODE:
                        Jj_consume_token(NODE);
                        t = Jj_consume_token(INTEGER);
                        value = String();
                        nodes.Put(t.image, value);
                        break;
                    case EDGE:
                        Jj_consume_token(EDGE);
                        t = Jj_consume_token(INTEGER);
                        value = String();
                        edges.Put(t.image, value);
                        break;
                    default:
                        jj_la1[7] = jj_gen;
                        Jj_consume_token(-1);
                        throw new ParseException();
                        break;
                }

                Jj_consume_token(CBRACKET);
            }

        Jj_consume_token(CBRACKET);
        NewProperty(cluster, name, type, nodeDefault, edgeDefault, nodes, edges);
    }

    private PropertyType Type()
    {
        Token t;
        t = Jj_consume_token(PTYPE);
        return PropertyType.ValueOf(t.image.ToUpperCase());
    }

    private string String()
    {
        Token t;
        t = Jj_consume_token(STRING);
        return t.image.Substring(1, t.image.Length - 1);
    }

    private int Integer()
    {
        Token t;
        t = Jj_consume_token(INTEGER);
        return Integer.ValueOf(t.image);
    }

    private bool Jj_2_1(int xla)
    {
        jj_la = xla;
        jj_lastpos = jj_scanpos = token;
        try
        {
            return !Jj_3_1();
        }
        catch (LookaheadSuccess ls)
        {
            return true;
        }
        finally
        {
            Jj_save(0, xla);
        }
    }

    private bool Jj_2_2(int xla)
    {
        jj_la = xla;
        jj_lastpos = jj_scanpos = token;
        try
        {
            return !Jj_3_2();
        }
        catch (LookaheadSuccess ls)
        {
            return true;
        }
        finally
        {
            Jj_save(1, xla);
        }
    }

    private bool Jj_2_3(int xla)
    {
        jj_la = xla;
        jj_lastpos = jj_scanpos = token;
        try
        {
            return !Jj_3_3();
        }
        catch (LookaheadSuccess ls)
        {
            return true;
        }
        finally
        {
            Jj_save(2, xla);
        }
    }

    private bool Jj_2_4(int xla)
    {
        jj_la = xla;
        jj_lastpos = jj_scanpos = token;
        try
        {
            return !Jj_3_4();
        }
        catch (LookaheadSuccess ls)
        {
            return true;
        }
        finally
        {
            Jj_save(3, xla);
        }
    }

    private bool Jj_2_5(int xla)
    {
        jj_la = xla;
        jj_lastpos = jj_scanpos = token;
        try
        {
            return !Jj_3_5();
        }
        catch (LookaheadSuccess ls)
        {
            return true;
        }
        finally
        {
            Jj_save(4, xla);
        }
    }

    private bool Jj_3_1()
    {
        if (Jj_3R_7())
            return true;
        return false;
    }

    private bool Jj_3_5()
    {
        if (Jj_3R_11())
            return true;
        return false;
    }

    private bool Jj_3R_9()
    {
        if (Jj_scan_token(OBRACKET))
            return true;
        if (Jj_scan_token(EDGE))
            return true;
        return false;
    }

    private bool Jj_3_4()
    {
        if (Jj_3R_10())
            return true;
        return false;
    }

    private bool Jj_3_3()
    {
        if (Jj_3R_9())
            return true;
        return false;
    }

    private bool Jj_3R_14()
    {
        if (Jj_scan_token(COMMENTS))
            return true;
        return false;
    }

    private bool Jj_3R_13()
    {
        if (Jj_scan_token(AUTHOR))
            return true;
        return false;
    }

    private bool Jj_3_2()
    {
        if (Jj_3R_8())
            return true;
        return false;
    }

    private bool Jj_3R_12()
    {
        if (Jj_scan_token(DATE))
            return true;
        return false;
    }

    private bool Jj_3R_11()
    {
        if (Jj_scan_token(OBRACKET))
            return true;
        if (Jj_scan_token(PROPERTY))
            return true;
        return false;
    }

    private bool Jj_3R_10()
    {
        if (Jj_scan_token(OBRACKET))
            return true;
        if (Jj_scan_token(CLUSTER))
            return true;
        return false;
    }

    private bool Jj_3R_7()
    {
        if (Jj_scan_token(OBRACKET))
            return true;
        Token xsp;
        xsp = jj_scanpos;
        if (Jj_3R_12())
        {
            jj_scanpos = xsp;
            if (Jj_3R_13())
            {
                jj_scanpos = xsp;
                if (Jj_3R_14())
                    return true;
            }
        }

        return false;
    }

    private bool Jj_3R_8()
    {
        if (Jj_scan_token(OBRACKET))
            return true;
        if (Jj_scan_token(NODES))
            return true;
        return false;
    }

    public TLPParserTokenManager token_source;
    SimpleCharStream jj_input_stream;
    public Token token;
    public Token jj_nt;
    private int jj_ntk;
    private Token jj_scanpos, jj_lastpos;
    private int jj_la;
    private int jj_gen;
    private readonly int[] jj_la1 = new int[8];
    private static int[] jj_la1_0;
    static TLPParser()
    {
        Jj_la1_init_0();
    }

    private static void Jj_la1_init_0()
    {
        jj_la1_0 = new int[]
        {
            0x400,
            0x401,
            0x380000,
            0x1000000,
            0x1000000,
            0x400,
            0x400,
            0x14000
        };
    }

    private readonly JJCalls[] jj_2_rtns = new JJCalls[5];
    private bool jj_rescan = false;
    private int jj_gc = 0;
    public TLPParser(java.io.InputStream stream) : this(stream, null)
    {
    }

    public TLPParser(java.io.InputStream stream, string encoding)
    {
        try
        {
            jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1);
        }
        catch (java.io.UnsupportedEncodingException e)
        {
            throw new Exception(e);
        }

        token_source = new TLPParserTokenManager(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 8; i++)
            jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.Length; i++)
            jj_2_rtns[i] = new JJCalls();
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
        for (int i = 0; i < 8; i++)
            jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.Length; i++)
            jj_2_rtns[i] = new JJCalls();
    }

    public TLPParser(java.io.Reader stream)
    {
        jj_input_stream = new SimpleCharStream(stream, 1, 1);
        token_source = new TLPParserTokenManager(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 8; i++)
            jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.Length; i++)
            jj_2_rtns[i] = new JJCalls();
    }

    public virtual void ReInit(java.io.Reader stream)
    {
        jj_input_stream.ReInit(stream, 1, 1);
        token_source.ReInit(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 8; i++)
            jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.Length; i++)
            jj_2_rtns[i] = new JJCalls();
    }

    public TLPParser(TLPParserTokenManager tm)
    {
        token_source = tm;
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 8; i++)
            jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.Length; i++)
            jj_2_rtns[i] = new JJCalls();
    }

    public virtual void ReInit(TLPParserTokenManager tm)
    {
        token_source = tm;
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 8; i++)
            jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.Length; i++)
            jj_2_rtns[i] = new JJCalls();
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
            if (++jj_gc > 100)
            {
                jj_gc = 0;
                for (int i = 0; i < jj_2_rtns.Length; i++)
                {
                    JJCalls c = jj_2_rtns[i];
                    while (c != null)
                    {
                        if (c.gen < jj_gen)
                            c.first = null;
                        c = c.next;
                    }
                }
            }

            return token;
        }

        token = oldToken;
        jj_kind = kind;
        throw GenerateParseException();
    }

    private sealed class LookaheadSuccess : Exception
    {
        private static readonly long serialVersionUID = -7986896058452164869;
    }

    private readonly LookaheadSuccess jj_ls = new LookaheadSuccess();
    private bool Jj_scan_token(int kind)
    {
        if (jj_scanpos == jj_lastpos)
        {
            jj_la--;
            if (jj_scanpos.next == null)
            {
                jj_lastpos = jj_scanpos = jj_scanpos.next = token_source.GetNextToken();
            }
            else
            {
                jj_lastpos = jj_scanpos = jj_scanpos.next;
            }
        }
        else
        {
            jj_scanpos = jj_scanpos.next;
        }

        if (jj_rescan)
        {
            int i = 0;
            Token tok = token;
            while (tok != null && tok != jj_scanpos)
            {
                i++;
                tok = tok.next;
            }

            if (tok != null)
                Jj_add_error_token(kind, i);
        }

        if (jj_scanpos.kind != kind)
            return true;
        if (jj_la == 0 && jj_scanpos == jj_lastpos)
            throw jj_ls;
        return false;
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
    private int[] jj_lasttokens = new int[100];
    private int jj_endpos;
    private void Jj_add_error_token(int kind, int pos)
    {
        if (pos >= 100)
            return;
        if (pos == jj_endpos + 1)
        {
            jj_lasttokens[jj_endpos++] = kind;
        }
        else if (jj_endpos != 0)
        {
            jj_expentry = new int[jj_endpos];
            for (int i = 0; i < jj_endpos; i++)
            {
                jj_expentry[i] = jj_lasttokens[i];
            }

            jj_entries_loop:
                for (java.util.Iterator<?> it = jj_expentries.iterator(); it.HasNext();)
                {
                    int[] oldentry = (int[])(it.Next());
                    if (oldentry.Length == jj_expentry.Length)
                    {
                        for (int i = 0; i < jj_expentry.Length; i++)
                        {
                            if (oldentry[i] != jj_expentry[i])
                            {
                                continue;
                            }
                        }

                        jj_expentries.Add(jj_expentry);
                        break;
                    }
                }

            if (pos != 0)
                jj_lasttokens[(jj_endpos = pos) - 1] = kind;
        }
    }

    public virtual ParseException GenerateParseException()
    {
        jj_expentries.Clear();
        bool[] la1tokens = new bool[28];
        if (jj_kind >= 0)
        {
            la1tokens[jj_kind] = true;
            jj_kind = -1;
        }

        for (int i = 0; i < 8; i++)
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

        for (int i = 0; i < 28; i++)
        {
            if (la1tokens[i])
            {
                jj_expentry = new int[1];
                jj_expentry[0] = i;
                jj_expentries.Add(jj_expentry);
            }
        }

        jj_endpos = 0;
        Jj_rescan_token();
        Jj_add_error_token(0, 0);
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

    private void Jj_rescan_token()
    {
        jj_rescan = true;
        for (int i = 0; i < 5; i++)
        {
            try
            {
                JJCalls p = jj_2_rtns[i];
                do
                {
                    if (p.gen > jj_gen)
                    {
                        jj_la = p.arg;
                        jj_lastpos = jj_scanpos = p.first;
                        switch (i)
                        {
                            case 0:
                                Jj_3_1();
                                break;
                            case 1:
                                Jj_3_2();
                                break;
                            case 2:
                                Jj_3_3();
                                break;
                            case 3:
                                Jj_3_4();
                                break;
                            case 4:
                                Jj_3_5();
                                break;
                        }
                    }

                    p = p.next;
                }
                while (p != null);
            }
            catch (LookaheadSuccess ls)
            {
            }
        }

        jj_rescan = false;
    }

    private void Jj_save(int index, int xla)
    {
        JJCalls p = jj_2_rtns[index];
        while (p.gen > jj_gen)
        {
            if (p.next == null)
            {
                p = p.next = new JJCalls();
                break;
            }

            p = p.next;
        }

        p.gen = jj_gen + xla - jj_la;
        p.first = token;
        p.arg = xla;
    }

    sealed class JJCalls
    {
        int gen;
        Token first;
        int arg;
        JJCalls next;
    }
}
