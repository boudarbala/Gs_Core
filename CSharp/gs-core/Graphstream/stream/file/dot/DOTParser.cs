using Java.Io;
using Java.Util;
using Gs_Core.Graphstream.Stream.SourceBase;
using Gs_Core.Graphstream.Stream.File;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Graph.Implementations.AbstractElement;
using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Dot.ElementType;
using static Org.Graphstream.Stream.File.Dot.EventType;
using static Org.Graphstream.Stream.File.Dot.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Dot.Mode;
using static Org.Graphstream.Stream.File.Dot.What;
using static Org.Graphstream.Stream.File.Dot.TimeFormat;
using static Org.Graphstream.Stream.File.Dot.OutputType;
using static Org.Graphstream.Stream.File.Dot.OutputPolicy;
using static Org.Graphstream.Stream.File.Dot.LayoutPolicy;
using static Org.Graphstream.Stream.File.Dot.Quality;
using static Org.Graphstream.Stream.File.Dot.Option;
using static Org.Graphstream.Stream.File.Dot.AttributeType;
using static Org.Graphstream.Stream.File.Dot.Balise;
using static Org.Graphstream.Stream.File.Dot.GEXFAttribute;
using static Org.Graphstream.Stream.File.Dot.METAAttribute;
using static Org.Graphstream.Stream.File.Dot.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Dot.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Dot.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Dot.NODESAttribute;
using static Org.Graphstream.Stream.File.Dot.NODEAttribute;
using static Org.Graphstream.Stream.File.Dot.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Dot.PARENTAttribute;
using static Org.Graphstream.Stream.File.Dot.EDGESAttribute;
using static Org.Graphstream.Stream.File.Dot.SPELLAttribute;
using static Org.Graphstream.Stream.File.Dot.COLORAttribute;
using static Org.Graphstream.Stream.File.Dot.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Dot.SIZEAttribute;
using static Org.Graphstream.Stream.File.Dot.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Dot.EDGEAttribute;
using static Org.Graphstream.Stream.File.Dot.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Dot.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Dot.IDType;
using static Org.Graphstream.Stream.File.Dot.ModeType;
using static Org.Graphstream.Stream.File.Dot.WeightType;
using static Org.Graphstream.Stream.File.Dot.EdgeType;
using static Org.Graphstream.Stream.File.Dot.NodeShapeType;
using static Org.Graphstream.Stream.File.Dot.EdgeShapeType;
using static Org.Graphstream.Stream.File.Dot.ClassType;
using static Org.Graphstream.Stream.File.Dot.TimeFormatType;
using static Org.Graphstream.Stream.File.Dot.GPXAttribute;
using static Org.Graphstream.Stream.File.Dot.WPTAttribute;
using static Org.Graphstream.Stream.File.Dot.LINKAttribute;
using static Org.Graphstream.Stream.File.Dot.EMAILAttribute;
using static Org.Graphstream.Stream.File.Dot.PTAttribute;
using static Org.Graphstream.Stream.File.Dot.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Dot.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Dot.FixType;
using static Org.Graphstream.Stream.File.Dot.GraphAttribute;
using static Org.Graphstream.Stream.File.Dot.LocatorAttribute;
using static Org.Graphstream.Stream.File.Dot.NodeAttribute;
using static Org.Graphstream.Stream.File.Dot.EdgeAttribute;
using static Org.Graphstream.Stream.File.Dot.DataAttribute;
using static Org.Graphstream.Stream.File.Dot.PortAttribute;
using static Org.Graphstream.Stream.File.Dot.EndPointAttribute;
using static Org.Graphstream.Stream.File.Dot.EndPointType;
using static Org.Graphstream.Stream.File.Dot.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Dot.KeyAttribute;
using static Org.Graphstream.Stream.File.Dot.KeyDomain;
using static Org.Graphstream.Stream.File.Dot.KeyAttrType;
using static Org.Graphstream.Stream.File.Dot.GraphEvents;
using static Org.Graphstream.Stream.File.Dot.ThreadingModel;
using static Org.Graphstream.Stream.File.Dot.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Dot.Measures;
using static Org.Graphstream.Stream.File.Dot.Token;

namespace Gs_Core.Graphstream.Stream.File.Dot;

public class DOTParser : Parser, DOTParserConstants
{
    private FileSourceDOT dot;
    private string sourceId;
    private bool directed;
    private bool strict;
    private HashMap<string, object> globalNodesAttributes;
    private HashMap<string, object> globalEdgesAttributes;
    private HashSet<string> nodeAdded;
    public DOTParser(FileSourceDOT dot, InputStream stream) : this(stream)
    {
        Init(dot);
    }

    public DOTParser(FileSourceDOT dot, Reader stream) : this(stream)
    {
        Init(dot);
    }

    public virtual void Dispose()
    {
        jj_input_stream.Dispose();
    }

    private void Init(FileSourceDOT dot)
    {
        this.dot = dot;
        this.sourceId = String.Format("<DOT stream %x>", System.NanoTime());
        globalNodesAttributes = new HashMap<string, object>();
        globalEdgesAttributes = new HashMap<string, object>();
        nodeAdded = new HashSet<string>();
    }

    private void AddNode(string nodeId, string[] port, HashMap<string, object> attr)
    {
        if (nodeAdded.Contains(nodeId))
        {
            if (attr != null)
            {
                foreach (string key in attr.KeySet())
                    dot.SendAttributeChangedEvent(sourceId, nodeId, ElementType.NODE, key, AttributeChangeEvent.ADD, null, attr[key]);
            }
        }
        else
        {
            dot.SendNodeAdded(sourceId, nodeId);
            nodeAdded.Add(nodeId);
            if (attr == null)
            {
                foreach (string key in globalNodesAttributes.KeySet())
                    dot.SendAttributeChangedEvent(sourceId, nodeId, ElementType.NODE, key, AttributeChangeEvent.ADD, null, globalNodesAttributes[key]);
            }
            else
            {
                foreach (string key in globalNodesAttributes.KeySet())
                {
                    if (!attr.ContainsKey(key))
                        dot.SendAttributeChangedEvent(sourceId, nodeId, ElementType.NODE, key, AttributeChangeEvent.ADD, null, globalNodesAttributes[key]);
                }

                foreach (string key in attr.KeySet())
                    dot.SendAttributeChangedEvent(sourceId, nodeId, ElementType.NODE, key, AttributeChangeEvent.ADD, null, attr[key]);
            }
        }
    }

    private void AddEdges(LinkedList<string> edges, HashMap<string, object> attr)
    {
        HashMap<string, int> hash = new HashMap<string, int>();
        string[] ids = new string[(edges.Count - 1) / 2];
        bool[] directed = new bool[(edges.Count - 1) / 2];
        int count = 0;
        for (int i = 0; i < edges.Count - 1; i += 2)
        {
            string from = edges[i];
            string to = edges[i + 2];
            if (!nodeAdded.Contains(from))
                AddNode(from, null, null);
            if (!nodeAdded.Contains(to))
                AddNode(to, null, null);
            string edgeId = String.Format("(%s;%s)", from, to);
            string rev = String.Format("(%s;%s)", to, from);
            if (hash.ContainsKey(rev))
            {
                directed[hash[rev]] = false;
            }
            else
            {
                hash.Put(edgeId, count);
                ids[count] = edgeId;
                directed[count] = edges[i + 1].Equals("->");
                count++;
            }
        }

        hash.Clear();
        if (count == 1 && attr != null && attr.ContainsKey("id"))
        {
            ids[0] = attr["id"].ToString();
            attr.Remove("id");
        }

        for (int i = 0; i < count; i++)
        {
            bool addedEdge = false;
            string IDtoTry = ids[i];
            while (!addedEdge)
            {
                try
                {
                    dot.SendEdgeAdded(sourceId, ids[i], edges[i * 2], edges[(i + 1) * 2], directed[i]);
                    addedEdge = true;
                }
                catch (IdAlreadyInUseException e)
                {
                    IDtoTry += "'";
                }
            }

            if (attr == null)
            {
                foreach (string key in globalEdgesAttributes.KeySet())
                    dot.SendAttributeChangedEvent(sourceId, ids[i], ElementType.EDGE, key, AttributeChangeEvent.ADD, null, globalEdgesAttributes[key]);
            }
            else
            {
                foreach (string key in globalEdgesAttributes.KeySet())
                {
                    if (!attr.ContainsKey(key))
                        dot.SendAttributeChangedEvent(sourceId, ids[i], ElementType.EDGE, key, AttributeChangeEvent.ADD, null, globalEdgesAttributes[key]);
                }

                foreach (string key in attr.KeySet())
                    dot.SendAttributeChangedEvent(sourceId, ids[i], ElementType.EDGE, key, AttributeChangeEvent.ADD, null, attr[key]);
            }
        }
    }

    private void SetGlobalAttributes(string who, HashMap<string, object> attr)
    {
        if (who.EqualsIgnoreCase("graph"))
        {
            foreach (string key in attr.KeySet())
                dot.SendAttributeChangedEvent(sourceId, sourceId, ElementType.GRAPH, key, AttributeChangeEvent.ADD, null, attr[key]);
        }
        else if (who.EqualsIgnoreCase("node"))
            globalNodesAttributes.PutAll(attr);
        else if (who.EqualsIgnoreCase("edge"))
            globalEdgesAttributes.PutAll(attr);
    }

    public void All()
    {
        Graph();
        label_1:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case GRAPH:
                    case SUBGRAPH:
                    case NODE:
                    case EDGE:
                    case REAL:
                    case STRING:
                    case WORD:
                        break;
                    default:
                        jj_la1[0] = jj_gen;
                        break;
                }

                Statement();
            }

        Jj_consume_token(RBRACE);
    }

    public bool Next()
    {
        bool hasMore = false;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case GRAPH:
            case SUBGRAPH:
            case NODE:
            case EDGE:
            case REAL:
            case STRING:
            case WORD:
                Statement();
                hasMore = true;
                break;
            case RBRACE:
                Jj_consume_token(RBRACE);
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
        Graph();
    }

    private void Graph()
    {
        directed = false;
        strict = false;
        globalNodesAttributes.Clear();
        globalEdgesAttributes.Clear();
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case STRICT:
                Jj_consume_token(STRICT);
                strict = true;
                break;
            default:
                jj_la1[2] = jj_gen;
                break;
        }

        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case GRAPH:
                Jj_consume_token(GRAPH);
                break;
            case DIGRAPH:
                Jj_consume_token(DIGRAPH);
                directed = true;
                break;
            default:
                jj_la1[3] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case REAL:
            case STRING:
            case WORD:
                this.sourceId = Id();
                break;
            default:
                jj_la1[4] = jj_gen;
                break;
        }

        Jj_consume_token(LBRACE);
    }

    private void Subgraph()
    {
        Jj_consume_token(SUBGRAPH);
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case REAL:
            case STRING:
            case WORD:
                Id();
                break;
            default:
                jj_la1[5] = jj_gen;
                break;
        }

        Jj_consume_token(LBRACE);
        label_2:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case GRAPH:
                    case SUBGRAPH:
                    case NODE:
                    case EDGE:
                    case REAL:
                    case STRING:
                    case WORD:
                        break;
                    default:
                        jj_la1[6] = jj_gen;
                        break;
                }

                Statement();
            }

        Jj_consume_token(RBRACE);
    }

    private string Id()
    {
        Token t;
        string id;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case STRING:
                t = Jj_consume_token(STRING);
                id = t.image.Substring(1, t.image.Length - 1);
                break;
            case REAL:
                t = Jj_consume_token(REAL);
                id = t.image;
                break;
            case WORD:
                t = Jj_consume_token(WORD);
                id = t.image;
                break;
            default:
                jj_la1[7] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        return id;
    }

    private void Statement()
    {
        if (Jj_2_1(3))
        {
            EdgeStatement();
        }
        else
        {
            switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
            {
                case REAL:
                case STRING:
                case WORD:
                    NodeStatement();
                    break;
                case GRAPH:
                case NODE:
                case EDGE:
                    AttributeStatement();
                    break;
                case SUBGRAPH:
                    Subgraph();
                    break;
                default:
                    jj_la1[8] = jj_gen;
                    Jj_consume_token(-1);
                    throw new ParseException();
                    break;
            }
        }

        Jj_consume_token(27);
    }

    private void NodeStatement()
    {
        string nodeId;
        string[] port;
        HashMap<string, object> attr = null;
        port = null;
        nodeId = Id();
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case COLON:
                port = Port();
                break;
            default:
                jj_la1[9] = jj_gen;
                break;
        }

        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case LSQBR:
                attr = AttributesList();
                break;
            default:
                jj_la1[10] = jj_gen;
                break;
        }

        AddNode(nodeId, port, attr);
    }

    private string CompassPoint()
    {
        Token pt = null;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case 28:
                pt = Jj_consume_token(28);
                break;
            case 29:
                pt = Jj_consume_token(29);
                break;
            case 30:
                pt = Jj_consume_token(30);
                break;
            case 31:
                pt = Jj_consume_token(31);
                break;
            case 32:
                pt = Jj_consume_token(32);
                break;
            case 33:
                pt = Jj_consume_token(33);
                break;
            case 34:
                pt = Jj_consume_token(34);
                break;
            case 35:
                pt = Jj_consume_token(35);
                break;
            case 36:
                pt = Jj_consume_token(36);
                break;
            case 37:
                pt = Jj_consume_token(37);
                break;
            default:
                jj_la1[11] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        return pt.image;
    }

    private String[] Port()
    {
        string[] p = new[]
        {
            null,
            null
        };
        Jj_consume_token(COLON);
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case REAL:
            case STRING:
            case WORD:
                p[0] = Id();
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case COLON:
                        Jj_consume_token(COLON);
                        p[1] = CompassPoint();
                        break;
                    default:
                        jj_la1[12] = jj_gen;
                        break;
                }

                break;
            case 28:
            case 29:
            case 30:
            case 31:
            case 32:
            case 33:
            case 34:
            case 35:
            case 36:
            case 37:
                p[1] = CompassPoint();
                break;
            default:
                jj_la1[13] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        return p;
    }

    private void EdgeStatement()
    {
        string id;
        LinkedList<string> edges = new LinkedList<string>();
        HashMap<string, object> attr = null;
        id = Id();
        edges.Add(id);
        EdgeRHS(edges);
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case LSQBR:
                attr = AttributesList();
                break;
            default:
                jj_la1[14] = jj_gen;
                break;
        }

        AddEdges(edges, attr);
    }

    private void EdgeRHS(LinkedList<string> edges)
    {
        Token t;
        string i;
        t = Jj_consume_token(EDGE_OP);
        edges.Add(t.image);
        i = Id();
        edges.Add(i);
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case EDGE_OP:
                EdgeRHS(edges);
                break;
            default:
                jj_la1[15] = jj_gen;
                break;
        }
    }

    private void AttributeStatement()
    {
        Token t;
        HashMap<string, object> attr;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case GRAPH:
                t = Jj_consume_token(GRAPH);
                break;
            case NODE:
                t = Jj_consume_token(NODE);
                break;
            case EDGE:
                t = Jj_consume_token(EDGE);
                break;
            default:
                jj_la1[16] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        attr = AttributesList();
        SetGlobalAttributes(t.image, attr);
    }

    private HashMap<string, object> AttributesList()
    {
        HashMap<string, object> attributes = new HashMap<string, object>();
        label_3:
            while (true)
            {
                Jj_consume_token(LSQBR);
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case REAL:
                    case STRING:
                    case WORD:
                        AttributeList(attributes);
                        label_4:
                            while (true)
                            {
                                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                                {
                                    case COMMA:
                                        break;
                                    default:
                                        jj_la1[17] = jj_gen;
                                        break;
                                }

                                Jj_consume_token(COMMA);
                                AttributeList(attributes);
                            }

                        break;
                    default:
                        jj_la1[18] = jj_gen;
                        break;
                }

                Jj_consume_token(RSQBR);
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case LSQBR:
                        break;
                    default:
                        jj_la1[19] = jj_gen;
                        break;
                }
            }

        return attributes;
    }

    private void AttributeList(HashMap<string, object> attributes)
    {
        string key;
        object val;
        Token t;
        key = Id();
        val = Boolean.TRUE;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case EQUALS:
                Jj_consume_token(EQUALS);
                if (Jj_2_2(2))
                {
                    t = Jj_consume_token(REAL);
                    val = Double.ParseDouble(t.image);
                }
                else
                {
                    switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                    {
                        case REAL:
                        case STRING:
                        case WORD:
                            val = Id();
                            break;
                        default:
                            jj_la1[20] = jj_gen;
                            Jj_consume_token(-1);
                            throw new ParseException();
                            break;
                    }
                }

                break;
            default:
                jj_la1[21] = jj_gen;
                break;
        }

        attributes.Put(key, val);
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

    private bool Jj_3R_6()
    {
        Token xsp;
        xsp = jj_scanpos;
        if (Jj_3R_8())
        {
            jj_scanpos = xsp;
            if (Jj_3R_9())
            {
                jj_scanpos = xsp;
                if (Jj_3R_10())
                    return true;
            }
        }

        return false;
    }

    private bool Jj_3_2()
    {
        if (Jj_scan_token(REAL))
            return true;
        return false;
    }

    private bool Jj_3R_8()
    {
        if (Jj_scan_token(STRING))
            return true;
        return false;
    }

    private bool Jj_3R_10()
    {
        if (Jj_scan_token(WORD))
            return true;
        return false;
    }

    private bool Jj_3R_7()
    {
        if (Jj_scan_token(EDGE_OP))
            return true;
        if (Jj_3R_6())
            return true;
        return false;
    }

    private bool Jj_3R_9()
    {
        if (Jj_scan_token(REAL))
            return true;
        return false;
    }

    private bool Jj_3R_5()
    {
        if (Jj_3R_6())
            return true;
        if (Jj_3R_7())
            return true;
        return false;
    }

    private bool Jj_3_1()
    {
        if (Jj_3R_5())
            return true;
        return false;
    }

    public DOTParserTokenManager token_source;
    SimpleCharStream jj_input_stream;
    public Token token;
    public Token jj_nt;
    private int jj_ntk;
    private Token jj_scanpos, jj_lastpos;
    private int jj_la;
    private int jj_gen;
    private readonly int[] jj_la1 = new int[22];
    private static int[] jj_la1_0;
    private static int[] jj_la1_1;
    static DOTParser()
    {
        Jj_la1_init_0();
        Jj_la1_init_1();
    }

    private static void Jj_la1_init_0()
    {
        jj_la1_0 = new int[]
        {
            0x73a0000,
            0x73a2001,
            0x400000,
            0x60000,
            0x7000000,
            0x7000000,
            0x73a0000,
            0x7000000,
            0x73a0000,
            0x4000,
            0x400,
            0xf0000000,
            0x4000,
            0xf7000000,
            0x400,
            0x800000,
            0x320000,
            0x8000,
            0x7000000,
            0x400,
            0x7000000,
            0x10000
        };
    }

    private static void Jj_la1_init_1()
    {
        jj_la1_1 = new int[]
        {
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x3f,
            0x0,
            0x3f,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0
        };
    }

    private readonly JJCalls[] jj_2_rtns = new JJCalls[2];
    private bool jj_rescan = false;
    private int jj_gc = 0;
    public DOTParser(java.io.InputStream stream) : this(stream, null)
    {
    }

    public DOTParser(java.io.InputStream stream, string encoding)
    {
        try
        {
            jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1);
        }
        catch (java.io.UnsupportedEncodingException e)
        {
            throw new Exception(e);
        }

        token_source = new DOTParserTokenManager(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 22; i++)
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
        for (int i = 0; i < 22; i++)
            jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.Length; i++)
            jj_2_rtns[i] = new JJCalls();
    }

    public DOTParser(java.io.Reader stream)
    {
        jj_input_stream = new SimpleCharStream(stream, 1, 1);
        token_source = new DOTParserTokenManager(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 22; i++)
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
        for (int i = 0; i < 22; i++)
            jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.Length; i++)
            jj_2_rtns[i] = new JJCalls();
    }

    public DOTParser(DOTParserTokenManager tm)
    {
        token_source = tm;
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 22; i++)
            jj_la1[i] = -1;
        for (int i = 0; i < jj_2_rtns.Length; i++)
            jj_2_rtns[i] = new JJCalls();
    }

    public virtual void ReInit(DOTParserTokenManager tm)
    {
        token_source = tm;
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 22; i++)
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
        bool[] la1tokens = new bool[38];
        if (jj_kind >= 0)
        {
            la1tokens[jj_kind] = true;
            jj_kind = -1;
        }

        for (int i = 0; i < 22; i++)
        {
            if (jj_la1[i] == jj_gen)
            {
                for (int j = 0; j < 32; j++)
                {
                    if ((jj_la1_0[i] & (1 << j)) != 0)
                    {
                        la1tokens[j] = true;
                    }

                    if ((jj_la1_1[i] & (1 << j)) != 0)
                    {
                        la1tokens[32 + j] = true;
                    }
                }
            }
        }

        for (int i = 0; i < 38; i++)
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
        for (int i = 0; i < 2; i++)
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
