using Java.Io;
using Java.Net;
using Java.Util;
using Java.Util.Zip;
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

public class FileSourceDGS1And2 : FileSourceBase
{
    protected enum AttributeType
    {
        NUMBER,
        VECTOR,
        STRING
    }

    protected class AttributeFormat
    {
        public string name;
        public AttributeType type;
        public AttributeFormat(string name, AttributeType type)
        {
            this.name = name;
            this.type = type;
        }

        public virtual string GetName()
        {
            return name;
        }

        public virtual AttributeType GetType()
        {
            return type;
        }
    }

    protected int version;
    protected string graphName;
    protected int stepCountAnnounced;
    protected int eventCountAnnounced;
    protected int stepCount;
    protected int eventCount;
    protected List<AttributeFormat> nodesFormat = new List<AttributeFormat>();
    protected List<AttributeFormat> edgesFormat = new List<AttributeFormat>();
    protected HashMap<string, object> attributes = new HashMap<string, object>();
    public FileSourceDGS1And2() : base(true)
    {
    }

    public override bool NextEvents()
    {
        string key = GetWordOrSymbolOrStringOrEolOrEof();
        string tag = null;
        if (key.Equals("ce"))
        {
            tag = GetStringOrWordOrNumber();
            ReadAttributes(edgesFormat);
            foreach (string k in attributes.KeySet())
            {
                object value = attributes[k];
                SendEdgeAttributeChanged(graphName, tag, k, null, value);
            }

            if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                return false;
        }
        else if (key.Equals("cn"))
        {
            tag = GetStringOrWordOrNumber();
            ReadAttributes(nodesFormat);
            foreach (string k in attributes.KeySet())
            {
                object value = attributes[k];
                SendNodeAttributeChanged(graphName, tag, k, null, value);
            }

            if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                return false;
        }
        else if (key.Equals("ae"))
        {
            tag = GetStringOrWordOrNumber();
            string fromTag = GetStringOrWordOrNumber();
            string toTag = GetStringOrWordOrNumber();
            ReadAttributes(edgesFormat);
            SendEdgeAdded(graphName, tag, fromTag, toTag, false);
            if (attributes != null)
            {
                foreach (string k in attributes.KeySet())
                {
                    object value = attributes[k];
                    SendEdgeAttributeAdded(graphName, tag, k, value);
                }
            }

            if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                return false;
        }
        else if (key.Equals("an"))
        {
            tag = GetStringOrWordOrNumber();
            ReadAttributes(nodesFormat);
            SendNodeAdded(graphName, tag);
            if (attributes != null)
            {
                foreach (string k in attributes.KeySet())
                {
                    object value = attributes[k];
                    SendNodeAttributeAdded(graphName, tag, k, value);
                }
            }

            if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                return false;
        }
        else if (key.Equals("de"))
        {
            tag = GetStringOrWordOrNumber();
            SendEdgeRemoved(graphName, tag);
            if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                return false;
        }
        else if (key.Equals("dn"))
        {
            tag = GetStringOrWordOrNumber();
            SendNodeRemoved(graphName, tag);
            if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                return false;
        }
        else if (key.Equals("st"))
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
                return false;
        }
        else if (key == "#")
        {
            EatAllUntilEol();
        }
        else if (key == "EOL")
        {
            return true;
        }
        else if (key == "EOF")
        {
            return false;
        }
        else
        {
            ParseError("found an unknown key in file '" + key + "' (expecting an,ae,cn,ce,dn,de or st)");
        }

        return true;
    }

    public virtual bool NextStep()
    {
        string key = "";
        string tag = null;
        while (!key.Equals("st") && !key.Equals("EOF"))
        {
            key = GetWordOrSymbolOrStringOrEolOrEof();
            if (key.Equals("ce"))
            {
                tag = GetStringOrWordOrNumber();
                ReadAttributes(edgesFormat);
                foreach (string k in attributes.KeySet())
                {
                    object value = attributes[k];
                    SendEdgeAttributeChanged(graphName, tag, k, null, value);
                }

                if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                    return false;
            }
            else if (key.Equals("cn"))
            {
                tag = GetStringOrWordOrNumber();
                ReadAttributes(nodesFormat);
                foreach (string k in attributes.KeySet())
                {
                    object value = attributes[k];
                    SendNodeAttributeChanged(graphName, tag, k, null, value);
                }

                if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                    return false;
            }
            else if (key.Equals("ae"))
            {
                tag = GetStringOrWordOrNumber();
                string fromTag = GetStringOrWordOrNumber();
                string toTag = GetStringOrWordOrNumber();
                ReadAttributes(edgesFormat);
                SendEdgeAdded(graphName, tag, fromTag, toTag, false);
                if (attributes != null)
                {
                    foreach (string k in attributes.KeySet())
                    {
                        object value = attributes[k];
                        SendNodeAttributeAdded(graphName, tag, k, value);
                    }
                }

                if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                    return false;
            }
            else if (key.Equals("an"))
            {
                tag = GetStringOrWordOrNumber();
                ReadAttributes(nodesFormat);
                SendNodeAdded(graphName, tag);
                if (attributes != null)
                {
                    foreach (string k in attributes.KeySet())
                    {
                        object value = attributes[k];
                        SendNodeAttributeAdded(graphName, tag, k, value);
                    }
                }

                if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                    return false;
            }
            else if (key.Equals("de"))
            {
                tag = GetStringOrWordOrNumber();
                SendEdgeRemoved(graphName, tag);
                if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                    return false;
            }
            else if (key.Equals("dn"))
            {
                tag = GetStringOrWordOrNumber();
                SendNodeRemoved(graphName, tag);
                if (EatEolOrEof() == StreamTokenizer.TT_EOF)
                    return false;
            }
            else if (key.Equals("st"))
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
                    return false;
            }
            else if (key == "#")
            {
                EatAllUntilEol();
            }
            else if (key == "EOL")
            {
            }
            else if (key == "EOF")
            {
                return false;
            }
            else
            {
                ParseError("found an unknown key in file '" + key + "' (expecting an,ae,cn,ce,dn,de or st)");
            }
        }

        return true;
    }

    protected virtual void ReadAttributes(List<AttributeFormat> formats)
    {
        attributes.Clear();
        if (formats.Count > 0)
        {
            foreach (AttributeFormat format in formats)
            {
                if (format.type == AttributeType.NUMBER)
                {
                    ReadNumberAttribute(format.name);
                }
                else if (format.type == AttributeType.VECTOR)
                {
                    ReadVectorAttribute(format.name);
                }
                else if (format.type == AttributeType.STRING)
                {
                    ReadStringAttribute(format.name);
                }
            }
        }
    }

    protected virtual void ReadNumberAttribute(string name)
    {
        int tok = st.NextToken();
        if (IsNull(tok))
        {
            attributes.Put(name, new Double(0));
        }
        else
        {
            st.PushBack();
            double n = GetNumber();
            attributes.Put(name, new Double(n));
        }
    }

    protected virtual void ReadVectorAttribute(string name)
    {
        int tok = st.NextToken();
        if (IsNull(tok))
        {
            attributes.Put(name, new List<Double>());
        }
        else
        {
            bool loop = true;
            List<Double> vector = new List<Double>();
            while (loop)
            {
                if (tok != StreamTokenizer.TT_NUMBER)
                    ParseError("expecting a number, " + GotWhat(tok));
                vector.Add(st.nval);
                tok = st.NextToken();
                if (tok != ',')
                {
                    loop = false;
                    st.PushBack();
                }
                else
                {
                    tok = st.NextToken();
                }
            }

            attributes.Put(name, vector);
        }
    }

    protected virtual void ReadStringAttribute(string name)
    {
        string s = GetStringOrWordOrNumber();
        attributes.Put(name, s);
    }

    protected virtual bool IsNull(int tok)
    {
        if (tok == StreamTokenizer.TT_WORD)
            return (st.sval.Equals("null"));
        return false;
    }

    public override void Begin(string filename)
    {
        base.Begin(filename);
        Init();
    }

    public override void Begin(InputStream stream)
    {
        base.Begin(stream);
        Init();
    }

    public override void Begin(Reader reader)
    {
        base.Begin(reader);
        Init();
    }

    public override void Begin(URL url)
    {
        base.Begin(url);
        Init();
    }

    protected virtual void Init()
    {
        st.ParseNumbers();
        string magic = EatOneOfTwoWords("DGS001", "DGS002");
        if (magic.Equals("DGS001"))
            version = 1;
        else
            version = 2;
        EatEol();
        graphName = GetWord();
        stepCountAnnounced = (int)GetNumber();
        eventCountAnnounced = (int)GetNumber();
        EatEol();
        if (graphName != null)
        {
            attributes.Clear();
            attributes.Put("label", graphName);
            SendGraphAttributeAdded(graphName, "label", graphName);
        }
        else
        {
            graphName = "DGS_";
        }

        graphName = String.Format("%s_%d", graphName, System.CurrentTimeMillis() + ((long)Math.Random() * 10));
        ReadAttributeFormat();
    }

    protected virtual void ReadAttributeFormat()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_WORD && st.sval.Equals("nodes"))
        {
            ParseAttributeFormat(nodesFormat);
            tok = st.NextToken();
        }

        if (tok == StreamTokenizer.TT_WORD && st.sval.Equals("edges"))
        {
            ParseAttributeFormat(edgesFormat);
        }
        else
        {
            st.PushBack();
        }
    }

    protected virtual void ParseAttributeFormat(List<AttributeFormat> format)
    {
        int tok = st.NextToken();
        while (tok != StreamTokenizer.TT_EOL)
        {
            if (tok == StreamTokenizer.TT_WORD)
            {
                string name = st.sval;
                EatSymbol(':');
                tok = st.NextToken();
                if (tok == StreamTokenizer.TT_WORD)
                {
                    string type = st.sval.ToLowerCase();
                    if (type.Equals("number") || type.Equals("n"))
                    {
                        format.Add(new AttributeFormat(name, AttributeType.NUMBER));
                    }
                    else if (type.Equals("string") || type.Equals("s"))
                    {
                        format.Add(new AttributeFormat(name, AttributeType.STRING));
                    }
                    else if (type.Equals("vector") || type.Equals("v"))
                    {
                        format.Add(new AttributeFormat(name, AttributeType.VECTOR));
                    }
                    else
                    {
                        ParseError("unknown attribute type `" + type + "' (only `number', `vector' and `string' are accepted)");
                    }
                }
                else
                {
                    ParseError("expecting an attribute type, got `" + GotWhat(tok) + "'");
                }
            }
            else
            {
                ParseError("expecting an attribute name, got `" + GotWhat(tok) + "'");
            }

            tok = st.NextToken();
        }
    }

    protected override void ContinueParsingInInclude()
    {
    }

    protected override Reader CreateReaderFrom(string file)
    {
        InputStream is = null;
        try
        {
            @is = new GZIPInputStream(new FileInputStream(file));
        }
        catch (IOException e)
        {
            @is = new FileInputStream(file);
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
        tok.WordChars('_', '_');
        tok.OrdinaryChar('1');
        tok.OrdinaryChar('2');
        tok.OrdinaryChar('3');
        tok.OrdinaryChar('4');
        tok.OrdinaryChar('5');
        tok.OrdinaryChar('6');
        tok.OrdinaryChar('7');
        tok.OrdinaryChar('8');
        tok.OrdinaryChar('9');
        tok.OrdinaryChar('0');
        tok.OrdinaryChar('.');
        tok.OrdinaryChar('-');
        tok.WordChars('1', '1');
        tok.WordChars('2', '2');
        tok.WordChars('3', '3');
        tok.WordChars('4', '4');
        tok.WordChars('5', '5');
        tok.WordChars('6', '6');
        tok.WordChars('7', '7');
        tok.WordChars('8', '8');
        tok.WordChars('9', '9');
        tok.WordChars('0', '0');
        tok.WordChars('.', '.');
        tok.WordChars('-', '-');
    }
}
