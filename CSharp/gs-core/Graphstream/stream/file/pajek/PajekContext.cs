using Java.Util;
using Gs_Core.Graphstream.Graph.Implementations.AbstractElement;
using Gs_Core.Graphstream.Stream.SourceBase;
using Gs_Core.Graphstream.Stream.File;
using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Pajek.ElementType;
using static Org.Graphstream.Stream.File.Pajek.EventType;
using static Org.Graphstream.Stream.File.Pajek.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Pajek.Mode;
using static Org.Graphstream.Stream.File.Pajek.What;
using static Org.Graphstream.Stream.File.Pajek.TimeFormat;
using static Org.Graphstream.Stream.File.Pajek.OutputType;
using static Org.Graphstream.Stream.File.Pajek.OutputPolicy;
using static Org.Graphstream.Stream.File.Pajek.LayoutPolicy;
using static Org.Graphstream.Stream.File.Pajek.Quality;
using static Org.Graphstream.Stream.File.Pajek.Option;
using static Org.Graphstream.Stream.File.Pajek.AttributeType;
using static Org.Graphstream.Stream.File.Pajek.Balise;
using static Org.Graphstream.Stream.File.Pajek.GEXFAttribute;
using static Org.Graphstream.Stream.File.Pajek.METAAttribute;
using static Org.Graphstream.Stream.File.Pajek.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Pajek.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Pajek.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Pajek.NODESAttribute;
using static Org.Graphstream.Stream.File.Pajek.NODEAttribute;
using static Org.Graphstream.Stream.File.Pajek.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Pajek.PARENTAttribute;
using static Org.Graphstream.Stream.File.Pajek.EDGESAttribute;
using static Org.Graphstream.Stream.File.Pajek.SPELLAttribute;
using static Org.Graphstream.Stream.File.Pajek.COLORAttribute;
using static Org.Graphstream.Stream.File.Pajek.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Pajek.SIZEAttribute;
using static Org.Graphstream.Stream.File.Pajek.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Pajek.EDGEAttribute;
using static Org.Graphstream.Stream.File.Pajek.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Pajek.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Pajek.IDType;
using static Org.Graphstream.Stream.File.Pajek.ModeType;
using static Org.Graphstream.Stream.File.Pajek.WeightType;
using static Org.Graphstream.Stream.File.Pajek.EdgeType;
using static Org.Graphstream.Stream.File.Pajek.NodeShapeType;
using static Org.Graphstream.Stream.File.Pajek.EdgeShapeType;
using static Org.Graphstream.Stream.File.Pajek.ClassType;
using static Org.Graphstream.Stream.File.Pajek.TimeFormatType;
using static Org.Graphstream.Stream.File.Pajek.GPXAttribute;
using static Org.Graphstream.Stream.File.Pajek.WPTAttribute;
using static Org.Graphstream.Stream.File.Pajek.LINKAttribute;
using static Org.Graphstream.Stream.File.Pajek.EMAILAttribute;
using static Org.Graphstream.Stream.File.Pajek.PTAttribute;
using static Org.Graphstream.Stream.File.Pajek.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Pajek.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Pajek.FixType;
using static Org.Graphstream.Stream.File.Pajek.GraphAttribute;
using static Org.Graphstream.Stream.File.Pajek.LocatorAttribute;
using static Org.Graphstream.Stream.File.Pajek.NodeAttribute;
using static Org.Graphstream.Stream.File.Pajek.EdgeAttribute;
using static Org.Graphstream.Stream.File.Pajek.DataAttribute;
using static Org.Graphstream.Stream.File.Pajek.PortAttribute;
using static Org.Graphstream.Stream.File.Pajek.EndPointAttribute;
using static Org.Graphstream.Stream.File.Pajek.EndPointType;
using static Org.Graphstream.Stream.File.Pajek.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Pajek.KeyAttribute;
using static Org.Graphstream.Stream.File.Pajek.KeyDomain;
using static Org.Graphstream.Stream.File.Pajek.KeyAttrType;
using static Org.Graphstream.Stream.File.Pajek.GraphEvents;
using static Org.Graphstream.Stream.File.Pajek.ThreadingModel;
using static Org.Graphstream.Stream.File.Pajek.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Pajek.Measures;
using static Org.Graphstream.Stream.File.Pajek.Token;
using static Org.Graphstream.Stream.File.Pajek.Extension;
using static Org.Graphstream.Stream.File.Pajek.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Pajek.AttrType;
using static Org.Graphstream.Stream.File.Pajek.Resolutions;

namespace Gs_Core.Graphstream.Stream.File.Pajek;

public class PajekContext
{
    FileSourcePajek pajek;
    string sourceId;
    protected bool directed = false;
    protected string weightAttributeName = "weight";
    public PajekContext(FileSourcePajek pajek)
    {
        this.pajek = pajek;
        this.sourceId = String.Format("<Pajek stream %d>", System.CurrentTimeMillis());
    }

    protected virtual void SetDirected(bool on)
    {
        directed = on;
    }

    protected virtual int AddNodes(Token nb)
    {
        int n = GetInt(nb);
        for (int i = 1; i <= n; ++i)
        {
            pajek.SendNodeAdded(sourceId, String.Format("%d", i));
        }

        return n;
    }

    protected virtual void AddGraphAttribute(string attr, string value)
    {
        pajek.SendAttributeChangedEvent(sourceId, sourceId, ElementType.GRAPH, attr, AttributeChangeEvent.ADD, null, value);
    }

    protected virtual void AddNodeLabel(string nb, string label)
    {
        pajek.SendAttributeChangedEvent(sourceId, nb, ElementType.NODE, "ui.label", AttributeChangeEvent.ADD, null, label);
    }

    protected virtual void AddNodeGraphics(string id, NodeGraphics graphics)
    {
        pajek.SendAttributeChangedEvent(sourceId, id, ElementType.NODE, "ui.style", AttributeChangeEvent.ADD, null, graphics.GetStyle());
    }

    protected virtual void AddNodePosition(string id, Token x, Token y, Token z)
    {
        object[] pos = new object[3];
        pos[0] = (Double)GetReal(x);
        pos[1] = (Double)GetReal(y);
        pos[2] = z != null ? (Double)GetReal(z) : 0;
        pajek.SendAttributeChangedEvent(sourceId, id, ElementType.NODE, "xyz", AttributeChangeEvent.ADD, null, pos);
    }

    protected virtual string AddEdge(string src, string trg)
    {
        string id = String.Format("%s_%s_%d", src, trg, (long)(Math.Random() * 100000) + System.CurrentTimeMillis());
        pajek.SendEdgeAdded(sourceId, id, src, trg, directed);
        return id;
    }

    protected virtual void AddEdges(EdgeMatrix mat)
    {
        int size = mat.Count;
        int edgeid = 0;
        for (int line = 0; line < size; line++)
        {
            for (int col = 0; col < size; col++)
            {
                if (mat.HasEdge(line, col))
                {
                    string id = String.Format("%d_%d_%d", line + 1, col + 1, edgeid++);
                    if (mat.HasEdge(col, line))
                    {
                        pajek.SendEdgeAdded(sourceId, id, String.Format("%d", line + 1), String.Format("%d", col + 1), false);
                        mat.Set(col, line, false);
                    }
                    else
                    {
                        pajek.SendEdgeAdded(sourceId, id, String.Format("%d", line + 1), String.Format("%d", col + 1), true);
                    }
                }
            }
        }
    }

    protected virtual void AddEdgeWeight(string id, Token nb)
    {
        pajek.SendAttributeChangedEvent(sourceId, id, ElementType.EDGE, weightAttributeName, AttributeChangeEvent.ADD, null, GetReal(nb));
    }

    protected virtual void AddEdgeGraphics(string id, EdgeGraphics graphics)
    {
        pajek.SendAttributeChangedEvent(sourceId, id, ElementType.EDGE, "ui.style", AttributeChangeEvent.ADD, null, graphics.GetStyle());
    }

    protected static int GetInt(Token nb)
    {
        try
        {
            return Integer.ParseInt(nb.image);
        }
        catch (Exception e)
        {
            throw new ParseException(String.Format("%d:%d: %s not an integer", nb.beginLine, nb.beginColumn, nb.image));
        }
    }

    protected static double GetReal(Token nb)
    {
        try
        {
            return Double.ParseDouble(nb.image);
        }
        catch (Exception e)
        {
            throw new ParseException(String.Format("%d:%d: %s not a real", nb.beginLine, nb.beginColumn, nb.image));
        }
    }

    public static string ToColorValue(Token R, Token G, Token B)
    {
        double r = GetReal(R);
        double g = GetReal(G);
        double b = GetReal(B);
        return String.Format("rgb(%d, %d, %d)", (int)(r * 255), (int)(g * 255), (int)(b * 255));
    }
}

abstract class Graphics
{
    protected StringBuffer graphics = new StringBuffer();
    public abstract void AddKey(string key, string value, Token tk);
    public virtual string GetStyle()
    {
        return graphics.ToString();
    }

    protected virtual double GetReal(string nb, Token tk)
    {
        try
        {
            return Double.ParseDouble(nb);
        }
        catch (Exception e)
        {
            throw new ParseException(String.Format("%d:%d: %s not a real", tk.beginLine, tk.beginColumn, nb));
        }
    }

    protected virtual int GetInt(string nb, Token tk)
    {
        try
        {
            return Integer.ParseInt(nb);
        }
        catch (Exception e)
        {
            throw new ParseException(String.Format("%d:%d: %s not an integer", tk.beginLine, tk.beginColumn, nb));
        }
    }
}

class NodeGraphics : Graphics
{
    public override void AddKey(string key, string value, Token tk)
    {
        if (key.Equals("shape"))
        {
            graphics.Append(String.Format("shape: %s;", value));
        }
        else if (key.Equals("ic"))
        {
            graphics.Append(String.Format("fill-color: %s;", value));
        }
        else if (key.Equals("bc"))
        {
            graphics.Append(String.Format("stroke-color: %s; stroke-mode: plain;", value));
        }
        else if (key.Equals("bw"))
        {
            graphics.Append(String.Format(Locale.US, "stroke-width: %fpx;", GetReal(value, tk)));
        }
        else if (key.Equals("s_size"))
        {
            graphics.Append(String.Format(Locale.US, "size: %fpx;", GetReal(value, tk)));
        }
        else if (key.Equals("lc"))
        {
            graphics.Append(String.Format("text-color: %s;", value));
        }
        else if (key.Equals("fos"))
        {
            graphics.Append(String.Format("text-size: %d;", GetInt(value, tk)));
        }
        else if (key.Equals("font"))
        {
            graphics.Append(String.Format("text-font: %s;", value));
        }
    }
}

class EdgeGraphics : Graphics
{
    public override void AddKey(string key, string value, Token tk)
    {
        if (key.Equals("w"))
        {
            graphics.Append(String.Format(Locale.US, "size: %fpx;", GetReal(value, tk)));
        }
        else if (key.Equals("c"))
        {
            graphics.Append(String.Format("fill-color: %s;", value));
        }
        else if (key.Equals("s"))
        {
            double s = GetReal(value, tk);
            graphics.Append(String.Format("arrow-size: %spx, %spx;", s * 5, s * 3));
        }
        else if (key.Equals("l"))
        {
        }
        else if (key.Equals("p"))
        {
        }
        else if (key.Equals("lc"))
        {
            graphics.Append(String.Format("text-color: %s;", value));
        }
        else if (key.Equals("fos"))
        {
            graphics.Append(String.Format("text-size: %d;", GetInt(value, tk)));
        }
        else if (key.Equals("font"))
        {
            graphics.Append(String.Format("text-font: %s;", value));
        }
    }
}

class EdgeMatrix
{
    protected bool[, ] mat;
    protected int curLine = 0;
    public EdgeMatrix(int size)
    {
        mat = new bool[size, size];
    }

    public virtual int Size()
    {
        return mat.Length;
    }

    public virtual bool HasEdge(int line, int col)
    {
        return mat[line][col];
    }

    public virtual void Set(int line, int col, bool value)
    {
        mat[line][col] = value;
    }

    public virtual void AddLine(List<string> line)
    {
        if (curLine < mat.Length)
        {
            if (line.Count == mat.Length)
            {
                for (int i = 0; i < mat.Length; i++)
                {
                    mat[curLine][i] = line[i].Equals("1");
                }

                curLine++;
            }
            else if (line.Count == mat.Length * mat.Length)
            {
                int n = mat.Length * mat.Length;
                curLine = -1;
                for (int i = 0; i < n; i++)
                {
                    if (i % mat.Length == 0)
                        curLine++;
                    mat[curLine][i - (curLine * mat.Length)] = line[i].Equals("1");
                }
            }
        }
    }

    public virtual string ToString()
    {
        StringBuffer buffer = new StringBuffer();
        for (int line = 0; line < mat.Length; line++)
        {
            for (int col = 0; col < mat.Length; col++)
            {
                buffer.Append(String.Format("%s ", mat[line][col] ? "1" : "0"));
            }

            buffer.Append(String.Format("%n"));
        }

        return buffer.ToString();
    }
}
