using Java.Awt;
using Java.Io;
using Java.Util;
using Gs_Core.Graphstream.Graph.Implementations.AbstractElement;
using Gs_Core.Graphstream.Stream.SourceBase;
using Gs_Core.Graphstream.Stream.File;
using Gs_Core.Graphstream.Util.Parser;
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

public class DGSParser : Parser
{
    enum Token
    {
        AN,
        CN,
        DN,
        AE,
        CE,
        DE,
        CG,
        ST,
        CL,
        TF,
        EOF
    }

    protected static readonly int BUFFER_SIZE = 4096;
    public static readonly int ARRAY_OPEN = '{';
    public static readonly int ARRAY_CLOSE = '}';
    public static readonly int MAP_OPEN = '[';
    public static readonly int MAP_CLOSE = ']';
    Reader reader;
    int line, column;
    int bufferCapacity, bufferPosition;
    char[] buffer;
    int[] pushback;
    int pushbackOffset;
    FileSourceDGS dgs;
    string sourceId;
    Token lastDirective;
    public DGSParser(FileSourceDGS dgs, Reader reader)
    {
        this.dgs = dgs;
        this.reader = reader;
        bufferCapacity = 0;
        buffer = new char[BUFFER_SIZE];
        pushback = new int[10];
        pushbackOffset = -1;
        this.sourceId = String.Format("<DGS stream %x>", System.NanoTime());
    }

    public virtual void Dispose()
    {
        reader.Dispose();
    }

    public virtual void Open()
    {
        Header();
    }

    public virtual void All()
    {
        Header();
        while (Next())
            ;
    }

    protected virtual int NextChar()
    {
        int c;
        if (pushbackOffset >= 0)
            return pushback[pushbackOffset--];
        if (bufferCapacity == 0 || bufferPosition >= bufferCapacity)
        {
            bufferCapacity = reader.Read(buffer, 0, BUFFER_SIZE);
            bufferPosition = 0;
        }

        if (bufferCapacity <= 0)
            return -1;
        c = buffer[bufferPosition++];
        if (c == '\r')
        {
            if (bufferPosition < bufferCapacity)
            {
                if (buffer[bufferPosition] == '\n')
                    bufferPosition++;
            }
            else
            {
                c = NextChar();
                if (c != '\n')
                    Pushback(c);
            }

            c = '\n';
        }

        if (c == '\n')
        {
            line++;
            column = 0;
        }
        else
            column++;
        return c;
    }

    protected virtual void Pushback(int c)
    {
        if (c < 0)
            return;
        if (pushbackOffset + 1 >= pushback.Length)
            throw new IOException("pushback buffer overflow");
        pushback[++pushbackOffset] = c;
    }

    protected virtual void SkipLine()
    {
        int c;
        while ((c = NextChar()) != '\n' && c >= 0)
            ;
    }

    protected virtual void SkipWhitespaces()
    {
        int c;
        while ((c = NextChar()) == ' ' || c == '\t')
            ;
        Pushback(c);
    }

    protected virtual void Header()
    {
        int[] dgs = new int[6];
        for (int i = 0; i < 6; i++)
            dgs[i] = NextChar();
        if (dgs[0] != 'D' || dgs[1] != 'G' || dgs[2] != 'S')
            throw ParseException(String.Format("bad magic header, 'DGS' expected, got '%c%c%c'", dgs[0], dgs[1], dgs[2]));
        if (dgs[3] != '0' || dgs[4] != '0' || dgs[5] < '0' || dgs[5] > '5')
            throw ParseException(String.Format("bad version \"%c%c%c\"", dgs[0], dgs[1], dgs[2]));
        if (NextChar() != '\n')
            throw ParseException("end-of-line is missing");
        SkipLine();
    }

    public virtual bool Next()
    {
        int c;
        string nodeId;
        string edgeId, source, target;
        lastDirective = Directive();
        switch (lastDirective)
        {
            case AN:
                nodeId = Id();
                dgs.SendNodeAdded(sourceId, nodeId);
                Attributes(ElementType.NODE, nodeId);
                break;
            case CN:
                nodeId = Id();
                Attributes(ElementType.NODE, nodeId);
                break;
            case DN:
                nodeId = Id();
                dgs.SendNodeRemoved(sourceId, nodeId);
                break;
            case AE:
                edgeId = Id();
                source = Id();
                SkipWhitespaces();
                c = NextChar();
                if (c != '<' && c != '>')
                    Pushback(c);
                target = Id();
                switch (c)
                {
                    case '>':
                        dgs.SendEdgeAdded(sourceId, edgeId, source, target, true);
                        break;
                    case '<':
                        dgs.SendEdgeAdded(sourceId, edgeId, target, source, true);
                        break;
                    default:
                        dgs.SendEdgeAdded(sourceId, edgeId, source, target, false);
                        break;
                }

                Attributes(ElementType.EDGE, edgeId);
                break;
            case CE:
                edgeId = Id();
                Attributes(ElementType.EDGE, edgeId);
                break;
            case DE:
                edgeId = Id();
                dgs.SendEdgeRemoved(sourceId, edgeId);
                break;
            case CG:
                Attributes(ElementType.GRAPH, null);
                break;
            case ST:
                double step;
                step = Double.ValueOf(Id());
                dgs.SendStepBegins(sourceId, step);
                break;
            case CL:
                dgs.SendGraphCleared(sourceId);
                break;
            case TF:
                break;
            case EOF:
                return false;
        }

        SkipWhitespaces();
        c = NextChar();
        if (c == '#')
        {
            SkipLine();
            return true;
        }

        if (c < 0)
            return false;
        if (c != '\n')
            throw ParseException("eol expected, got '%c'", c);
        return true;
    }

    public virtual bool NextStep()
    {
        bool r;
        Token next;
        do
        {
            r = Next();
            next = Directive();
            if (next != Token.EOF)
            {
                Pushback(next.Name().CharAt(1));
                Pushback(next.Name().CharAt(0));
            }
        }
        while (next != Token.ST && next != Token.EOF);
        return r;
    }

    protected virtual void Attributes(ElementType type, string id)
    {
        int c;
        SkipWhitespaces();
        while ((c = NextChar()) != '\n' && c != '#' && c >= 0)
        {
            Pushback(c);
            Attribute(type, id);
            SkipWhitespaces();
        }

        Pushback(c);
    }

    protected virtual void Attribute(ElementType type, string elementId)
    {
        string key;
        object value = null;
        int c;
        AttributeChangeEvent ch = AttributeChangeEvent.CHANGE;
        SkipWhitespaces();
        c = NextChar();
        if (c == '+')
            ch = AttributeChangeEvent.ADD;
        else if (c == '-')
            ch = AttributeChangeEvent.REMOVE;
        else
            Pushback(c);
        key = Id();
        if (key == null)
            throw ParseException("attribute key expected");
        if (ch != AttributeChangeEvent.REMOVE)
        {
            SkipWhitespaces();
            c = NextChar();
            if (c == '=' || c == ':')
            {
                SkipWhitespaces();
                value = Value(true);
            }
            else
            {
                value = Boolean.TRUE;
                Pushback(c);
            }
        }

        dgs.SendAttributeChangedEvent(sourceId, elementId, type, key, ch, null, value);
    }

    protected virtual object Value(bool array)
    {
        int c;
        LinkedList<object> l = null;
        object o;
        do
        {
            SkipWhitespaces();
            c = NextChar();
            Pushback(c);
            switch (c)
            {
                case '\'':
                case '"':
                    o = String();
                    break;
                case '#':
                    o = Color();
                    break;
                case ARRAY_OPEN:
                    NextChar();
                    SkipWhitespaces();
                    o = Value(true);
                    SkipWhitespaces();
                    if (NextChar() != ARRAY_CLOSE)
                        throw ParseException("'%c' expected", ARRAY_CLOSE);
                    if (!o.GetType().IsArray())
                        o = new object[]
                        {
                            o
                        };
                    break;
                case MAP_OPEN:
                    o = Map();
                    break;
                default:
                {
                    string word = Id();
                    if (word == null)
                        throw ParseException("missing value");
                    if ((c >= '0' && c <= '9') || c == '-')
                    {
                        try
                        {
                            if (word.IndexOf('.') > 0)
                                o = Double.ValueOf(word);
                            else
                            {
                                try
                                {
                                    o = Integer.ValueOf(word);
                                }
                                catch (NumberFormatException e)
                                {
                                    o = Long.ValueOf(word);
                                }
                            }
                        }
                        catch (NumberFormatException e)
                        {
                            throw ParseException("invalid number format '%s'", word);
                        }
                    }
                    else
                    {
                        if (word.EqualsIgnoreCase("true"))
                            o = Boolean.TRUE;
                        else if (word.EqualsIgnoreCase("false"))
                            o = Boolean.FALSE;
                        else
                            o = word;
                    }

                    break;
                }

                    break;
            }

            c = NextChar();
            if (l == null && array && c == ',')
            {
                l = new LinkedList<object>();
                l.Add(o);
            }
            else if (l != null)
                l.Add(o);
        }
        while (array && c == ',');
        Pushback(c);
        if (l == null)
            return o;
        return l.ToArray();
    }

    protected virtual Color Color()
    {
        int c;
        int r, g, b, a;
        StringBuilder hexa = new StringBuilder();
        c = NextChar();
        if (c != '#')
            throw ParseException("'#' expected");
        for (int i = 0; i < 6; i++)
        {
            c = NextChar();
            if ((c >= 0 && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
                hexa.AppendCodePoint(c);
            else
                throw ParseException("hexadecimal value expected");
        }

        r = Integer.ParseInt(hexa.Substring(0, 2), 16);
        g = Integer.ParseInt(hexa.Substring(2, 4), 16);
        b = Integer.ParseInt(hexa.Substring(4, 6), 16);
        c = NextChar();
        if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
        {
            hexa.AppendCodePoint(c);
            c = NextChar();
            if ((c >= 0 && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
                hexa.AppendCodePoint(c);
            else
                throw ParseException("hexadecimal value expected");
            a = Integer.ParseInt(hexa.Substring(6, 8), 16);
        }
        else
        {
            a = 255;
            Pushback(c);
        }

        return new Color(r, g, b, a);
    }

    protected virtual object Array()
    {
        int c;
        LinkedList<object> array = new LinkedList<object>();
        c = NextChar();
        if (c != ARRAY_OPEN)
            throw ParseException("'%c' expected", ARRAY_OPEN);
        SkipWhitespaces();
        c = NextChar();
        while (c != ARRAY_CLOSE)
        {
            Pushback(c);
            array.Add(Value(false));
            SkipWhitespaces();
            c = NextChar();
            if (c != ARRAY_CLOSE && c != ',')
                throw ParseException("'%c' or ',' expected, got '%c'", ARRAY_CLOSE, c);
            if (c == ',')
            {
                SkipWhitespaces();
                c = NextChar();
            }
        }

        if (c != ARRAY_CLOSE)
            throw ParseException("'%c' expected", ARRAY_CLOSE);
        return array.ToArray();
    }

    protected virtual object Map()
    {
        int c;
        HashMap<string, object> map = new HashMap<string, object>();
        string key;
        object value;
        c = NextChar();
        if (c != MAP_OPEN)
            throw ParseException("'%c' expected", MAP_OPEN);
        c = NextChar();
        while (c != MAP_CLOSE)
        {
            Pushback(c);
            key = Id();
            if (key == null)
                throw ParseException("id expected here, '%c'", c);
            SkipWhitespaces();
            c = NextChar();
            if (c == '=' || c == ':')
            {
                SkipWhitespaces();
                value = Value(false);
            }
            else
            {
                value = Boolean.TRUE;
                Pushback(c);
            }

            map.Put(key, value);
            SkipWhitespaces();
            c = NextChar();
            if (c != MAP_CLOSE && c != ',')
                throw ParseException("'%c' or ',' expected, got '%c'", MAP_CLOSE, c);
            if (c == ',')
            {
                SkipWhitespaces();
                c = NextChar();
            }
        }

        if (c != MAP_CLOSE)
            throw ParseException("'%c' expected", MAP_CLOSE);
        return map;
    }

    protected virtual Token Directive()
    {
        int c1, c2;
        do
        {
            c1 = NextChar();
            if (c1 == '#')
                SkipLine();
            if (c1 < 0)
                return Token.EOF;
        }
        while (c1 == '#' || c1 == '\n');
        c2 = NextChar();
        if (c1 >= 'A' && c1 <= 'Z')
            c1 -= 'A' - 'a';
        if (c2 >= 'A' && c2 <= 'Z')
            c2 -= 'A' - 'a';
        switch (c1)
        {
            case 'a':
                if (c2 == 'n')
                    return Token.AN;
                else if (c2 == 'e')
                    return Token.AE;
                break;
            case 'c':
                switch (c2)
                {
                    case 'n':
                        return Token.CN;
                    case 'e':
                        return Token.CE;
                    case 'g':
                        return Token.CG;
                    case 'l':
                        return Token.CL;
                }

                break;
            case 'd':
                if (c2 == 'n')
                    return Token.DN;
                else if (c2 == 'e')
                    return Token.DE;
                break;
            case 's':
                if (c2 == 't')
                    return Token.ST;
                break;
            case 't':
                if (c1 == 'f')
                    return Token.TF;
                break;
        }

        throw ParseException("unknown directive '%c%c'", c1, c2);
    }

    protected virtual string String()
    {
        int c, s;
        StringBuilder builder;
        bool slash;
        slash = false;
        builder = new StringBuilder();
        c = NextChar();
        if (c != '"' && c != '\'')
            throw ParseException("string expected");
        s = c;
        while ((c = NextChar()) != s || slash)
        {
            if (slash && c != s)
                builder.Append("\\");
            slash = c == '\\';
            if (!slash)
            {
                if (!Character.IsValidCodePoint(c))
                    throw ParseException("invalid code-point 0x%X", c);
                builder.AppendCodePoint(c);
            }
        }

        return builder.ToString();
    }

    protected virtual string Id()
    {
        int c;
        StringBuilder builder = new StringBuilder();
        SkipWhitespaces();
        c = NextChar();
        Pushback(c);
        if (c == '"' || c == '\'')
        {
            return String();
        }
        else
        {
            bool stop = false;
            while (!stop)
            {
                c = NextChar();
                switch (Character.GetType(c))
                {
                    case Character.LOWERCASE_LETTER:
                    case Character.UPPERCASE_LETTER:
                    case Character.DECIMAL_DIGIT_NUMBER:
                        break;
                    case Character.DASH_PUNCTUATION:
                        if (c != '-')
                            stop = true;
                        break;
                    case Character.MATH_SYMBOL:
                        if (c != '+')
                            stop = true;
                        break;
                    case Character.CONNECTOR_PUNCTUATION:
                        if (c != '_')
                            stop = true;
                        break;
                    case Character.OTHER_PUNCTUATION:
                        if (c != '.')
                            stop = true;
                        break;
                    default:
                        stop = true;
                        break;
                }

                if (!stop)
                    builder.AppendCodePoint(c);
            }

            Pushback(c);
        }

        if (builder.Length == 0)
            return null;
        return builder.ToString();
    }

    protected virtual ParseException ParseException(string message, params object[] args)
    {
        return new ParseException(String.Format(String.Format("parse error at (%d;%d) : %s", line, column, message), args));
    }
}
