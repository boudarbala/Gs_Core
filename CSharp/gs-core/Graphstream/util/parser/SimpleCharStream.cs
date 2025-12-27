using Java.Io;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.Parser.ElementType;
using static Org.Graphstream.Util.Parser.EventType;
using static Org.Graphstream.Util.Parser.AttributeChangeEvent;
using static Org.Graphstream.Util.Parser.Mode;
using static Org.Graphstream.Util.Parser.What;
using static Org.Graphstream.Util.Parser.TimeFormat;
using static Org.Graphstream.Util.Parser.OutputType;
using static Org.Graphstream.Util.Parser.OutputPolicy;
using static Org.Graphstream.Util.Parser.LayoutPolicy;
using static Org.Graphstream.Util.Parser.Quality;
using static Org.Graphstream.Util.Parser.Option;
using static Org.Graphstream.Util.Parser.AttributeType;
using static Org.Graphstream.Util.Parser.Balise;
using static Org.Graphstream.Util.Parser.GEXFAttribute;
using static Org.Graphstream.Util.Parser.METAAttribute;
using static Org.Graphstream.Util.Parser.GRAPHAttribute;
using static Org.Graphstream.Util.Parser.ATTRIBUTESAttribute;
using static Org.Graphstream.Util.Parser.ATTRIBUTEAttribute;
using static Org.Graphstream.Util.Parser.NODESAttribute;
using static Org.Graphstream.Util.Parser.NODEAttribute;
using static Org.Graphstream.Util.Parser.ATTVALUEAttribute;
using static Org.Graphstream.Util.Parser.PARENTAttribute;
using static Org.Graphstream.Util.Parser.EDGESAttribute;
using static Org.Graphstream.Util.Parser.SPELLAttribute;
using static Org.Graphstream.Util.Parser.COLORAttribute;
using static Org.Graphstream.Util.Parser.POSITIONAttribute;
using static Org.Graphstream.Util.Parser.SIZEAttribute;
using static Org.Graphstream.Util.Parser.NODESHAPEAttribute;
using static Org.Graphstream.Util.Parser.EDGEAttribute;
using static Org.Graphstream.Util.Parser.THICKNESSAttribute;
using static Org.Graphstream.Util.Parser.EDGESHAPEAttribute;
using static Org.Graphstream.Util.Parser.IDType;
using static Org.Graphstream.Util.Parser.ModeType;
using static Org.Graphstream.Util.Parser.WeightType;
using static Org.Graphstream.Util.Parser.EdgeType;
using static Org.Graphstream.Util.Parser.NodeShapeType;
using static Org.Graphstream.Util.Parser.EdgeShapeType;
using static Org.Graphstream.Util.Parser.ClassType;
using static Org.Graphstream.Util.Parser.TimeFormatType;
using static Org.Graphstream.Util.Parser.GPXAttribute;
using static Org.Graphstream.Util.Parser.WPTAttribute;
using static Org.Graphstream.Util.Parser.LINKAttribute;
using static Org.Graphstream.Util.Parser.EMAILAttribute;
using static Org.Graphstream.Util.Parser.PTAttribute;
using static Org.Graphstream.Util.Parser.BOUNDSAttribute;
using static Org.Graphstream.Util.Parser.COPYRIGHTAttribute;
using static Org.Graphstream.Util.Parser.FixType;
using static Org.Graphstream.Util.Parser.GraphAttribute;
using static Org.Graphstream.Util.Parser.LocatorAttribute;
using static Org.Graphstream.Util.Parser.NodeAttribute;
using static Org.Graphstream.Util.Parser.EdgeAttribute;
using static Org.Graphstream.Util.Parser.DataAttribute;
using static Org.Graphstream.Util.Parser.PortAttribute;
using static Org.Graphstream.Util.Parser.EndPointAttribute;
using static Org.Graphstream.Util.Parser.EndPointType;
using static Org.Graphstream.Util.Parser.HyperEdgeAttribute;
using static Org.Graphstream.Util.Parser.KeyAttribute;
using static Org.Graphstream.Util.Parser.KeyDomain;
using static Org.Graphstream.Util.Parser.KeyAttrType;
using static Org.Graphstream.Util.Parser.GraphEvents;
using static Org.Graphstream.Util.Parser.ThreadingModel;
using static Org.Graphstream.Util.Parser.CloseFramePolicy;

namespace Gs_Core.Graphstream.Util.Parser;

public class SimpleCharStream
{
    public static readonly bool staticFlag = false;
    int bufsize;
    int available;
    int tokenBegin;
    public int bufpos = -1;
    protected int[] bufline;
    protected int[] bufcolumn;
    protected int column = 0;
    protected int line = 1;
    protected bool prevCharIsCR = false;
    protected bool prevCharIsLF = false;
    protected java.io.Reader inputStream;
    protected char[] buffer;
    protected int maxNextCharInd = 0;
    protected int inBuf = 0;
    protected int tabSize = 8;
    protected virtual void SetTabSize(int i)
    {
        tabSize = i;
    }

    protected virtual int GetTabSize(int i)
    {
        return tabSize;
    }

    protected virtual void ExpandBuff(bool wrapAround)
    {
        char[] newbuffer = new char[bufsize + 2048];
        int[] newbufline = new int[bufsize + 2048];
        int[] newbufcolumn = new int[bufsize + 2048];
        try
        {
            if (wrapAround)
            {
                System.Arraycopy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);
                System.Arraycopy(buffer, 0, newbuffer, bufsize - tokenBegin, bufpos);
                buffer = newbuffer;
                System.Arraycopy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);
                System.Arraycopy(bufline, 0, newbufline, bufsize - tokenBegin, bufpos);
                bufline = newbufline;
                System.Arraycopy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);
                System.Arraycopy(bufcolumn, 0, newbufcolumn, bufsize - tokenBegin, bufpos);
                bufcolumn = newbufcolumn;
                maxNextCharInd = (bufpos += (bufsize - tokenBegin));
            }
            else
            {
                System.Arraycopy(buffer, tokenBegin, newbuffer, 0, bufsize - tokenBegin);
                buffer = newbuffer;
                System.Arraycopy(bufline, tokenBegin, newbufline, 0, bufsize - tokenBegin);
                bufline = newbufline;
                System.Arraycopy(bufcolumn, tokenBegin, newbufcolumn, 0, bufsize - tokenBegin);
                bufcolumn = newbufcolumn;
                maxNextCharInd = (bufpos -= tokenBegin);
            }
        }
        catch (Throwable t)
        {
            throw new Exception(t.GetMessage());
        }

        bufsize += 2048;
        available = bufsize;
        tokenBegin = 0;
    }

    protected virtual void FillBuff()
    {
        if (maxNextCharInd == available)
        {
            if (available == bufsize)
            {
                if (tokenBegin > 2048)
                {
                    bufpos = maxNextCharInd = 0;
                    available = tokenBegin;
                }
                else if (tokenBegin < 0)
                    bufpos = maxNextCharInd = 0;
                else
                    ExpandBuff(false);
            }
            else if (available > tokenBegin)
                available = bufsize;
            else if ((tokenBegin - available) < 2048)
                ExpandBuff(true);
            else
                available = tokenBegin;
        }

        int i;
        try
        {
            if ((i = inputStream.Read(buffer, maxNextCharInd, available - maxNextCharInd)) == -1)
            {
                inputStream.Dispose();
                throw new IOException();
            }
            else
                maxNextCharInd += i;
            return;
        }
        catch (java.io.IOException e)
        {
            --bufpos;
            Backup(0);
            if (tokenBegin == -1)
                tokenBegin = bufpos;
            throw e;
        }
    }

    public virtual char BeginToken()
    {
        tokenBegin = -1;
        char c = ReadChar();
        tokenBegin = bufpos;
        return c;
    }

    protected virtual void UpdateLineColumn(char c)
    {
        column++;
        if (prevCharIsLF)
        {
            prevCharIsLF = false;
            line += (column = 1);
        }
        else if (prevCharIsCR)
        {
            prevCharIsCR = false;
            if (c == '\n')
            {
                prevCharIsLF = true;
            }
            else
                line += (column = 1);
        }

        switch (c)
        {
            case '\r':
                prevCharIsCR = true;
                break;
            case '\n':
                prevCharIsLF = true;
                break;
            case '\t':
                column--;
                column += (tabSize - (column % tabSize));
                break;
            default:
                break;
        }

        bufline[bufpos] = line;
        bufcolumn[bufpos] = column;
    }

    public virtual char ReadChar()
    {
        if (inBuf > 0)
        {
            --inBuf;
            if (++bufpos == bufsize)
                bufpos = 0;
            return buffer[bufpos];
        }

        if (++bufpos >= maxNextCharInd)
            FillBuff();
        char c = buffer[bufpos];
        UpdateLineColumn(c);
        return c;
    }

    public virtual 
    ///
    /// @deprecated
    /// @see #getEndColumn
    //////
int GetColumn()
    {
        return bufcolumn[bufpos];
    }

    public virtual 
    ///
    /// @deprecated
    /// @see #getEndLine
    //////
int GetLine()
    {
        return bufline[bufpos];
    }

    public virtual int GetEndColumn()
    {
        return bufcolumn[bufpos];
    }

    public virtual int GetEndLine()
    {
        return bufline[bufpos];
    }

    public virtual int GetBeginColumn()
    {
        return bufcolumn[tokenBegin];
    }

    public virtual int GetBeginLine()
    {
        return bufline[tokenBegin];
    }

    public virtual void Backup(int amount)
    {
        inBuf += amount;
        if ((bufpos -= amount) < 0)
            bufpos += bufsize;
    }

    public SimpleCharStream(java.io.Reader dstream, int startline, int startcolumn, int buffersize)
    {
        inputStream = dstream;
        line = startline;
        column = startcolumn - 1;
        available = bufsize = buffersize;
        buffer = new char[buffersize];
        bufline = new int[buffersize];
        bufcolumn = new int[buffersize];
    }

    public SimpleCharStream(java.io.Reader dstream, int startline, int startcolumn) : this(dstream, startline, startcolumn, 4096)
    {
    }

    public SimpleCharStream(java.io.Reader dstream) : this(dstream, 1, 1, 4096)
    {
    }

    public virtual void ReInit(java.io.Reader dstream, int startline, int startcolumn, int buffersize)
    {
        inputStream = dstream;
        line = startline;
        column = startcolumn - 1;
        if (buffer == null || buffersize != buffer.Length)
        {
            available = bufsize = buffersize;
            buffer = new char[buffersize];
            bufline = new int[buffersize];
            bufcolumn = new int[buffersize];
        }

        prevCharIsLF = prevCharIsCR = false;
        tokenBegin = inBuf = maxNextCharInd = 0;
        bufpos = -1;
    }

    public virtual void ReInit(java.io.Reader dstream, int startline, int startcolumn)
    {
        ReInit(dstream, startline, startcolumn, 4096);
    }

    public virtual void ReInit(java.io.Reader dstream)
    {
        ReInit(dstream, 1, 1, 4096);
    }

    public SimpleCharStream(java.io.InputStream dstream, string encoding, int startline, int startcolumn, int buffersize) : this(encoding == null ? new InputStreamReader(dstream) : new InputStreamReader(dstream, encoding), startline, startcolumn, buffersize)
    {
    }

    public SimpleCharStream(java.io.InputStream dstream, int startline, int startcolumn, int buffersize) : this(new InputStreamReader(dstream), startline, startcolumn, buffersize)
    {
    }

    public SimpleCharStream(java.io.InputStream dstream, string encoding, int startline, int startcolumn) : this(dstream, encoding, startline, startcolumn, 4096)
    {
    }

    public SimpleCharStream(java.io.InputStream dstream, int startline, int startcolumn) : this(dstream, startline, startcolumn, 4096)
    {
    }

    public SimpleCharStream(java.io.InputStream dstream, string encoding) : this(dstream, encoding, 1, 1, 4096)
    {
    }

    public SimpleCharStream(java.io.InputStream dstream) : this(dstream, 1, 1, 4096)
    {
    }

    public virtual void ReInit(java.io.InputStream dstream, string encoding, int startline, int startcolumn, int buffersize)
    {
        ReInit(encoding == null ? new InputStreamReader(dstream) : new InputStreamReader(dstream, encoding), startline, startcolumn, buffersize);
    }

    public virtual void ReInit(java.io.InputStream dstream, int startline, int startcolumn, int buffersize)
    {
        ReInit(new InputStreamReader(dstream), startline, startcolumn, buffersize);
    }

    public virtual void ReInit(java.io.InputStream dstream, string encoding)
    {
        ReInit(dstream, encoding, 1, 1, 4096);
    }

    public virtual void ReInit(java.io.InputStream dstream)
    {
        ReInit(dstream, 1, 1, 4096);
    }

    public virtual void ReInit(java.io.InputStream dstream, string encoding, int startline, int startcolumn)
    {
        ReInit(dstream, encoding, startline, startcolumn, 4096);
    }

    public virtual void ReInit(java.io.InputStream dstream, int startline, int startcolumn)
    {
        ReInit(dstream, startline, startcolumn, 4096);
    }

    public virtual string GetImage()
    {
        if (bufpos >= tokenBegin)
            return new string (buffer, tokenBegin, bufpos - tokenBegin + 1);
        else
            return new string (buffer, tokenBegin, bufsize - tokenBegin) + new string (buffer, 0, bufpos + 1);
    }

    public virtual char[] GetSuffix(int len)
    {
        char[] ret = new char[len];
        if ((bufpos + 1) >= len)
            System.Arraycopy(buffer, bufpos - len + 1, ret, 0, len);
        else
        {
            System.Arraycopy(buffer, bufsize - (len - bufpos - 1), ret, 0, len - bufpos - 1);
            System.Arraycopy(buffer, 0, ret, len - bufpos - 1, bufpos + 1);
        }

        return ret;
    }

    public virtual void Done()
    {
        buffer = null;
        bufline = null;
        bufcolumn = null;
    }

    public virtual void Dispose()
    {
        inputStream.Dispose();
    }

    public virtual void AdjustBeginLineColumn(int newLine, int newCol)
    {
        int start = tokenBegin;
        int len;
        if (bufpos >= tokenBegin)
        {
            len = bufpos - tokenBegin + inBuf + 1;
        }
        else
        {
            len = bufsize - tokenBegin + bufpos + 1 + inBuf;
        }

        int i = 0, j = 0, k = 0;
        int nextColDiff = 0, columnDiff = 0;
        while (i < len && bufline[j = start % bufsize] == bufline[k = ++start % bufsize])
        {
            bufline[j] = newLine;
            nextColDiff = columnDiff + bufcolumn[k] - bufcolumn[j];
            bufcolumn[j] = newCol + columnDiff;
            columnDiff = nextColDiff;
            i++;
        }

        if (i < len)
        {
            bufline[j] = newLine++;
            bufcolumn[j] = newCol + columnDiff;
            while (i++ < len)
            {
                if (bufline[j = start % bufsize] != bufline[++start % bufsize])
                    bufline[j] = newLine++;
                else
                    bufline[j] = newLine;
            }
        }

        line = bufline[j];
        column = bufcolumn[j];
    }
}
