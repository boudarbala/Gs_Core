using Java.Io;
using Java.Net;
using Java.Util;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Ui.Geom;
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

public abstract class FileSourceBase : SourceBase, FileSource
{
    protected int QUOTE_CHAR = '"';
    protected int COMMENT_CHAR = '#';
    protected bool eol_is_significant = false;
    protected List<CurrentFile> tok_stack = new List<CurrentFile>();
    protected StreamTokenizer st;
    protected string filename;
    protected HashMap<string, string> attribute_classes = new HashMap<string, string>();
    protected FileSourceBase()
    {
    }

    protected FileSourceBase(bool eol_is_significant)
    {
        this.eol_is_significant = eol_is_significant;
    }

    protected FileSourceBase(bool eol_is_significant, int commentChar, int quoteChar)
    {
        this.eol_is_significant = eol_is_significant;
        this.COMMENT_CHAR = commentChar;
        this.QUOTE_CHAR = quoteChar;
    }

    public virtual void ReadAll(string filename)
    {
        Begin(filename);
        while (NextEvents())
            ;
        End();
    }

    public virtual void ReadAll(URL url)
    {
        Begin(url);
        while (NextEvents())
            ;
        End();
    }

    public virtual void ReadAll(InputStream stream)
    {
        Begin(stream);
        while (NextEvents())
            ;
        End();
    }

    public virtual void ReadAll(Reader reader)
    {
        Begin(reader);
        while (NextEvents())
            ;
        End();
    }

    public virtual void Begin(string filename)
    {
        PushTokenizer(filename);
    }

    public virtual void Begin(InputStream stream)
    {
        PushTokenizer(stream);
    }

    public virtual void Begin(URL url)
    {
        PushTokenizer(url);
    }

    public virtual void Begin(Reader reader)
    {
        PushTokenizer(reader);
    }

    public abstract bool NextEvents();
    public virtual void End()
    {
        PopTokenizer();
    }

    public virtual void AddAttributeClass(string attribute, string attribute_class)
    {
        attribute_classes.Put(attribute, attribute_class);
    }

    protected virtual void Include(string file)
    {
        PushTokenizer(file);
        ContinueParsingInInclude();
        PopTokenizer();
    }

    protected abstract void ContinueParsingInInclude();
    protected virtual void PushTokenizer(string file)
    {
        StreamTokenizer tok;
        CurrentFile cur;
        Reader reader;
        try
        {
            reader = CreateReaderFrom(file);
            tok = CreateTokenizer(reader);
            cur = new CurrentFile(file, tok, reader);
        }
        catch (FileNotFoundException e)
        {
            throw new IOException("cannot read file '" + file + "', not found: " + e.GetMessage());
        }

        ConfigureTokenizer(tok);
        tok_stack.Add(cur);
        st = tok;
        filename = file;
    }

    protected virtual Reader CreateReaderFrom(string file)
    {
        return new BufferedReader(new FileReader(file));
    }

    protected virtual Reader CreateReaderFrom(InputStream stream)
    {
        return new BufferedReader(new InputStreamReader(stream));
    }

    protected virtual void PushTokenizer(URL url)
    {
        PushTokenizer(url.OpenStream(), url.ToString());
    }

    protected virtual void PushTokenizer(InputStream stream)
    {
        PushTokenizer(stream, "<?input-stream?>");
    }

    protected virtual void PushTokenizer(InputStream stream, string name)
    {
        StreamTokenizer tok;
        CurrentFile cur;
        Reader reader;
        reader = CreateReaderFrom(stream);
        tok = CreateTokenizer(reader);
        cur = new CurrentFile(name, tok, reader);
        ConfigureTokenizer(tok);
        tok_stack.Add(cur);
        st = tok;
        filename = name;
    }

    protected virtual void PushTokenizer(Reader reader)
    {
        StreamTokenizer tok;
        CurrentFile cur;
        tok = CreateTokenizer(reader);
        cur = new CurrentFile("<?reader?>", tok, reader);
        ConfigureTokenizer(tok);
        tok_stack.Add(cur);
        st = tok;
        filename = "<?reader?>";
    }

    private StreamTokenizer CreateTokenizer(Reader reader)
    {
        return new StreamTokenizer(new BufferedReader(reader));
    }

    protected virtual void ConfigureTokenizer(StreamTokenizer tok)
    {
        if (COMMENT_CHAR > 0)
            tok.CommentChar(COMMENT_CHAR);
        tok.QuoteChar(QUOTE_CHAR);
        tok.EolIsSignificant(eol_is_significant);
        tok.WordChars('_', '_');
        tok.ParseNumbers();
    }

    protected virtual void PopTokenizer()
    {
        int n = tok_stack.Count;
        if (n <= 0)
            throw new Exception("poped one too many tokenizer");
        n -= 1;
        CurrentFile cur = tok_stack.Remove(n);
        cur.reader.Dispose();
        if (n > 0)
        {
            n -= 1;
            cur = tok_stack[n];
            st = cur.tok;
            filename = cur.file;
        }
    }

    protected virtual void PushBack()
    {
        st.PushBack();
    }

    protected virtual void EatEof()
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_EOF)
            ParseError("garbage at end of file, expecting EOF, " + GotWhat(tok));
    }

    protected virtual void EatEol()
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_EOL)
            ParseError("expecting EOL, " + GotWhat(tok));
    }

    protected virtual int EatEolOrEof()
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_EOL && tok != StreamTokenizer.TT_EOF)
            ParseError("expecting EOL or EOF, " + GotWhat(tok));
        return tok;
    }

    protected virtual void EatWord(string word)
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_WORD)
            ParseError("expecting `" + word + "', " + GotWhat(tok));
        if (!st.sval.Equals(word))
            ParseError("expecting `" + word + "' got `" + st.sval + "'");
    }

    protected virtual void EatWords(params string[] words)
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_WORD)
            ParseError("expecting one of `[" + Arrays.ToString(words) + "]', " + GotWhat(tok));
        bool found = false;
        foreach (string word in words)
        {
            if (st.sval.Equals(word))
            {
                found = true;
                break;
            }
        }

        if (!found)
            ParseError("expecting one of `[" + Arrays.ToString(words) + "]', got `" + st.sval + "'");
    }

    protected virtual string EatOneOfTwoWords(string word1, string word2)
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_WORD)
            ParseError("expecting `" + word1 + "' or  `" + word2 + "', " + GotWhat(tok));
        if (st.sval.Equals(word1))
            return word1;
        if (st.sval.Equals(word2))
            return word2;
        ParseError("expecting `" + word1 + "' or `" + word2 + "' got `" + st.sval + "'");
        return null;
    }

    protected virtual void EatSymbol(char symbol)
    {
        int tok = st.NextToken();
        if (tok != symbol)
            ParseError("expecting symbol `" + symbol + "', " + GotWhat(tok));
    }

    protected virtual int EatSymbols(string symbols)
    {
        int tok = st.NextToken();
        int n = symbols.Length;
        bool f = false;
        for (int i = 0; i < n; ++i)
        {
            if (tok == symbols.CharAt(i))
            {
                f = true;
                i = n;
            }
        }

        if (!f)
            ParseError("expecting one of symbols `" + symbols + "', " + GotWhat(tok));
        return tok;
    }

    protected virtual void EatWordOrPushbak(string word)
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_WORD)
            PushBack();
        if (!st.sval.Equals(word))
            PushBack();
    }

    protected virtual void EatSymbolOrPushback(char symbol)
    {
        int tok = st.NextToken();
        if (tok != symbol)
            PushBack();
    }

    protected virtual void EatAllUntilEol()
    {
        if (!eol_is_significant)
            return;
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_EOF)
            return;
        while ((tok != StreamTokenizer.TT_EOL) && (tok != StreamTokenizer.TT_EOF))
        {
            tok = st.NextToken();
        }
    }

    protected virtual void EatAllEols()
    {
        if (!eol_is_significant)
            return;
        int tok = st.NextToken();
        while (tok == StreamTokenizer.TT_EOL)
            tok = st.NextToken();
        PushBack();
    }

    protected virtual string GetWord()
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_WORD)
            ParseError("expecting a word, " + GotWhat(tok));
        return st.sval;
    }

    protected virtual char GetSymbol()
    {
        int tok = st.NextToken();
        if (tok > 0 && tok != StreamTokenizer.TT_WORD && tok != StreamTokenizer.TT_NUMBER && tok != StreamTokenizer.TT_EOL && tok != StreamTokenizer.TT_EOF && tok != QUOTE_CHAR && tok != COMMENT_CHAR)
        {
            return (char)tok;
        }

        ParseError("expecting a symbol, " + GotWhat(tok));
        return (char)0;
    }

    protected virtual char GetSymbolOrPushback()
    {
        int tok = st.NextToken();
        if (tok > 0 && tok != StreamTokenizer.TT_WORD && tok != StreamTokenizer.TT_NUMBER && tok != StreamTokenizer.TT_EOL && tok != StreamTokenizer.TT_EOF && tok != QUOTE_CHAR && tok != COMMENT_CHAR)
        {
            return (char)tok;
        }

        PushBack();
        return (char)0;
    }

    protected virtual string GetString()
    {
        int tok = st.NextToken();
        if (tok != QUOTE_CHAR)
            ParseError("expecting a string constant, " + GotWhat(tok));
        return st.sval;
    }

    protected virtual string GetWordOrNumber()
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_WORD && tok != StreamTokenizer.TT_NUMBER)
            ParseError("expecting a word or number, " + GotWhat(tok));
        if (tok == StreamTokenizer.TT_NUMBER)
        {
            if ((st.nval - ((int)st.nval)) == 0)
                return Integer.ToString((int)st.nval);
            else
                return Double.ToString(st.nval);
        }
        else
        {
            return st.sval;
        }
    }

    protected virtual string GetStringOrNumber()
    {
        int tok = st.NextToken();
        if (tok != QUOTE_CHAR && tok != StreamTokenizer.TT_NUMBER)
            ParseError("expecting a string constant or a number, " + GotWhat(tok));
        if (tok == StreamTokenizer.TT_NUMBER)
        {
            if ((st.nval - ((int)st.nval)) == 0)
                return Integer.ToString((int)st.nval);
            else
                return Double.ToString(st.nval);
        }
        else
        {
            return st.sval;
        }
    }

    protected virtual string GetStringOrWordOrNumberOrPushback()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_EOL || tok == StreamTokenizer.TT_EOF)
        {
            PushBack();
            return null;
        }

        if (tok == StreamTokenizer.TT_NUMBER)
        {
            if ((st.nval - ((int)st.nval)) == 0)
                return Integer.ToString((int)st.nval);
            else
                return Double.ToString(st.nval);
        }
        else if (tok == StreamTokenizer.TT_WORD || tok == QUOTE_CHAR)
        {
            return st.sval;
        }
        else
        {
            PushBack();
            return null;
        }
    }

    protected virtual string GetStringOrWordOrNumber()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_EOL || tok == StreamTokenizer.TT_EOF)
            ParseError("expecting word, string or number, " + GotWhat(tok));
        if (tok == StreamTokenizer.TT_NUMBER)
        {
            if ((st.nval - ((int)st.nval)) == 0)
                return Integer.ToString((int)st.nval);
            else
                return Double.ToString(st.nval);
        }
        else
        {
            return st.sval;
        }
    }

    protected virtual object GetStringOrWordOrNumberO()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_EOL || tok == StreamTokenizer.TT_EOF)
            ParseError("expecting word, string or number, " + GotWhat(tok));
        if (tok == StreamTokenizer.TT_NUMBER)
        {
            return st.nval;
        }
        else
        {
            return st.sval;
        }
    }

    protected virtual object GetStringOrWordOrSymbolOrNumberO()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_EOL || tok == StreamTokenizer.TT_EOF)
            ParseError("expecting word, string or number, " + GotWhat(tok));
        if (tok == StreamTokenizer.TT_NUMBER)
        {
            return st.nval;
        }
        else if (tok == StreamTokenizer.TT_WORD)
        {
            return st.sval;
        }
        else
            return Character.ToString((char)tok);
    }

    protected virtual string GetWordOrString()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_WORD || tok == QUOTE_CHAR)
            return st.sval;
        ParseError("expecting a word or string, " + GotWhat(tok));
        return null;
    }

    protected virtual string GetWordOrSymbol()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_NUMBER || tok == QUOTE_CHAR || tok == StreamTokenizer.TT_EOF)
            ParseError("expecting a word or symbol, " + GotWhat(tok));
        if (tok == StreamTokenizer.TT_WORD)
            return st.sval;
        else
            return Character.ToString((char)tok);
    }

    protected virtual string GetWordOrSymbolOrPushback()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_NUMBER || tok == QUOTE_CHAR || tok == StreamTokenizer.TT_EOF)
        {
            PushBack();
            return null;
        }

        if (tok == StreamTokenizer.TT_WORD)
            return st.sval;
        else
            return Character.ToString((char)tok);
    }

    protected virtual string GetWordOrSymbolOrString()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_NUMBER || tok == StreamTokenizer.TT_EOF)
            ParseError("expecting a word, symbol or string, " + GotWhat(tok));
        if (tok == QUOTE_CHAR)
            return st.sval;
        if (tok == StreamTokenizer.TT_WORD)
            return st.sval;
        else
            return Character.ToString((char)tok);
    }

    protected virtual string GetAllExceptedEof()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_EOF)
            ParseError("expecting all excepted EOF, " + GotWhat(tok));
        if (tok == StreamTokenizer.TT_NUMBER || tok == StreamTokenizer.TT_EOF)
        {
            if ((st.nval - ((int)st.nval)) == 0)
                return Integer.ToString((int)st.nval);
            else
                return Double.ToString(st.nval);
        }

        if (tok == QUOTE_CHAR)
            return st.sval;
        if (tok == StreamTokenizer.TT_WORD)
            return st.sval;
        else
            return Character.ToString((char)tok);
    }

    protected virtual string GetWordOrSymbolOrEof()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_NUMBER || tok == QUOTE_CHAR)
            ParseError("expecting a word or symbol, " + GotWhat(tok));
        if (tok == StreamTokenizer.TT_WORD)
            return st.sval;
        else if (tok == StreamTokenizer.TT_EOF)
            return "EOF";
        else
            return Character.ToString((char)tok);
    }

    protected virtual string GetWordOrSymbolOrStringOrEolOrEof()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_NUMBER)
            ParseError("expecting a word, symbol or string, " + GotWhat(tok));
        if (tok == QUOTE_CHAR)
            return st.sval;
        if (tok == StreamTokenizer.TT_WORD)
            return st.sval;
        if (tok == StreamTokenizer.TT_EOF)
            return "EOF";
        if (tok == StreamTokenizer.TT_EOL)
            return "EOL";
        return Character.ToString((char)tok);
    }

    protected virtual string GetWordOrNumberOrStringOrEolOrEof()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_NUMBER)
        {
            if (st.nval - ((int)st.nval) != 0)
                return Double.ToString(st.nval);
            return Integer.ToString((int)st.nval);
        }

        if (tok == QUOTE_CHAR)
            return st.sval;
        if (tok == StreamTokenizer.TT_WORD)
            return st.sval;
        if (tok == StreamTokenizer.TT_EOF)
            return "EOF";
        if (tok == StreamTokenizer.TT_EOL)
            return "EOL";
        ParseError("expecting a word, a number, a string, EOL or EOF, " + GotWhat(tok));
        return null;
    }

    protected virtual string GetWordOrStringOrEolOrEof()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_WORD)
            return st.sval;
        if (tok == QUOTE_CHAR)
            return st.sval;
        if (tok == StreamTokenizer.TT_EOL)
            return "EOL";
        if (tok == StreamTokenizer.TT_EOF)
            return "EOF";
        ParseError("expecting a word, a string, EOL or EOF, " + GotWhat(tok));
        return null;
    }

    protected virtual string GetWordOrSymbolOrNumberOrStringOrEolOrEof()
    {
        int tok = st.NextToken();
        if (tok == StreamTokenizer.TT_NUMBER)
        {
            if (st.nval - ((int)st.nval) != 0)
                return Double.ToString(st.nval);
            return Integer.ToString((int)st.nval);
        }

        if (tok == QUOTE_CHAR)
            return st.sval;
        if (tok == StreamTokenizer.TT_WORD)
            return st.sval;
        if (tok == StreamTokenizer.TT_EOF)
            return "EOF";
        if (tok == StreamTokenizer.TT_EOL)
            return "EOL";
        return Character.ToString((char)tok);
    }

    protected virtual double GetNumber()
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_NUMBER)
            ParseError("expecting a number, " + GotWhat(tok));
        return st.nval;
    }

    protected virtual double GetNumberExp()
    {
        int tok = st.NextToken();
        if (tok != StreamTokenizer.TT_NUMBER)
            ParseError("expecting a number, " + GotWhat(tok));
        double nb = st.nval;
        tok = st.NextToken();
        if (tok == StreamTokenizer.TT_WORD && (st.sval.StartsWith("e-") || st.sval.StartsWith("e+")))
        {
            double exp = Double.ParseDouble(st.sval.Substring(2));
            return Math.Pow(nb, exp);
        }
        else
        {
            st.PushBack();
        }

        return nb;
    }

    protected virtual string GotWhat(int token)
    {
        switch (token)
        {
            case StreamTokenizer.TT_NUMBER:
                return "got number `" + st.nval + "'";
            case StreamTokenizer.TT_WORD:
                return "got word `" + st.sval + "'";
            case StreamTokenizer.TT_EOF:
                return "got EOF";
            default:
                if (token == QUOTE_CHAR)
                    return "got string constant `" + st.sval + "'";
                else
                    return "unknown symbol `" + token + "' (" + ((char)token) + ")";
                break;
        }
    }

    protected virtual void ParseError(string message)
    {
        throw new IOException("parse error: " + filename + ": " + st.Lineno() + ": " + message);
    }

    protected virtual bool IsTrue(string @string)
    {
        @string = @string.ToLowerCase();
        if (@string.Equals("1"))
            return true;
        if (@string.Equals("true"))
            return true;
        if (@string.Equals("yes"))
            return true;
        if (@string.Equals("on"))
            return true;
        return false;
    }

    protected virtual bool IsFalse(string @string)
    {
        @string = @string.ToLowerCase();
        if (@string.Equals("0"))
            return true;
        if (@string.Equals("false"))
            return true;
        if (@string.Equals("no"))
            return true;
        if (@string.Equals("off"))
            return true;
        return false;
    }

    protected virtual bool GetBoolean(string value)
    {
        if (IsTrue(value))
            return true;
        if (IsFalse(value))
            return false;
        throw new NumberFormatException("not a truth value `" + value + "'");
    }

    protected virtual double GetReal(string value)
    {
        return Double.ParseDouble(value);
    }

    protected virtual long GetInteger(string value)
    {
        return Long.ParseLong(value);
    }

    protected virtual Point3 GetPoint3(string value)
    {
        int p0 = value.IndexOf(',');
        int p1 = value.IndexOf(',', p0 + 1);
        if (p0 > 0 && p1 > 0)
        {
            string n0, n1, n2;
            float v0, v1, v2;
            n0 = value.Substring(0, p0);
            n1 = value.Substring(p0 + 1, p1);
            n2 = value.Substring(p1 + 1);
            v0 = Float.ParseFloat(n0);
            v1 = Float.ParseFloat(n1);
            v2 = Float.ParseFloat(n2);
            return new Point3(v0, v1, v2);
        }

        throw new NumberFormatException("value '" + value + "' not in a valid point3 format");
    }

    protected class CurrentFile
    {
        public string file;
        public StreamTokenizer tok;
        public Reader reader;
        public CurrentFile(string f, StreamTokenizer t, Reader reader)
        {
            file = f;
            tok = t;
            this.reader = reader;
        }
    }
}
