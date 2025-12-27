using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ElementType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EventType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.AttributeChangeEvent;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Mode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.What;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TimeFormat;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.OutputType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.OutputPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.LayoutPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Quality;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Option;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.AttributeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Balise;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GEXFAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.METAAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GRAPHAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NODESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NODEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ATTVALUEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PARENTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EDGESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SPELLAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.COLORAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.POSITIONAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SIZEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NODESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EDGEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.THICKNESSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.IDType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ModeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.WeightType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NodeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EdgeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ClassType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TimeFormatType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GPXAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.WPTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.LINKAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EMAILAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.BOUNDSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.FixType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GraphAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.LocatorAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NodeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.DataAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PortAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EndPointAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EndPointType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.HyperEdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.KeyAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.KeyDomain;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.KeyAttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GraphEvents;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ThreadingModel;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.CloseFramePolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Measures;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Token;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Extension;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.DefaultEdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.AttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Resolutions;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PropertyType;

namespace Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;

public class Color
{
    private int r;
    private int g;
    private int b;
    private int a;
    public static readonly Color white = new Color(255, 255, 255);
    public static readonly Color WHITE = white;
    public static readonly Color lightGray = new Color(192, 192, 192);
    public static readonly Color LIGHT_GRAY = lightGray;
    public static readonly Color gray = new Color(128, 128, 128);
    public static readonly Color GRAY = gray;
    public static readonly Color darkGray = new Color(64, 64, 64);
    public static readonly Color DARK_GRAY = darkGray;
    public static readonly Color black = new Color(0, 0, 0);
    public static readonly Color BLACK = black;
    public static readonly Color red = new Color(255, 0, 0);
    public static readonly Color RED = red;
    public static readonly Color pink = new Color(255, 175, 175);
    public static readonly Color PINK = pink;
    public static readonly Color orange = new Color(255, 200, 0);
    public static readonly Color ORANGE = orange;
    public static readonly Color yellow = new Color(255, 255, 0);
    public static readonly Color YELLOW = yellow;
    public static readonly Color green = new Color(0, 255, 0);
    public static readonly Color GREEN = green;
    public static readonly Color magenta = new Color(255, 0, 255);
    public static readonly Color MAGENTA = magenta;
    public static readonly Color cyan = new Color(0, 255, 255);
    public static readonly Color CYAN = cyan;
    public static readonly Color blue = new Color(0, 0, 255);
    public static readonly Color BLUE = blue;
    public Color(int r, int g, int b, int a) : base()
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public Color(int r, int g, int b) : base()
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = 255;
    }

    public virtual int GetRed()
    {
        return r;
    }

    public virtual void SetRed(int r)
    {
        this.r = r;
    }

    public virtual int GetGreen()
    {
        return g;
    }

    public virtual void SetGreen(int g)
    {
        this.g = g;
    }

    public virtual int GetBlue()
    {
        return b;
    }

    public virtual void SetBlue(int b)
    {
        this.b = b;
    }

    public virtual int GetAlpha()
    {
        return a;
    }

    public virtual void SetAlpha(int a)
    {
        this.a = a;
    }

    public static Color Decode(string nm)
    {
        int intval = Integer.Decode(nm);
        int i = intval.IntValue();
        return new Color((i >> 16) & 0xFF, (i >> 8) & 0xFF, i & 0xFF);
    }

    public virtual bool Equals(object o)
    {
        Color c = (Color)o;
        return (this.r == c.r && this.g == c.g && this.b == c.b && this.a == c.a);
    }
}
