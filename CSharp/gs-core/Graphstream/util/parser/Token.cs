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

public class Token : Serializable
{
    private static readonly long serialVersionUID = 1;
    public int kind;
    public int beginLine;
    public int beginColumn;
    public int endLine;
    public int endColumn;
    public string image;
    public Token next;
    public Token specialToken;
    public virtual object GetValue()
    {
        return null;
    }

    public Token()
    {
    }

    public Token(int kind) : this(kind, null)
    {
    }

    public Token(int kind, string image)
    {
        this.kind = kind;
        this.image = image;
    }

    public virtual string ToString()
    {
        return image;
    }

    public static Token NewToken(int ofKind, string image)
    {
        switch (ofKind)
        {
            default:
                return new Token(ofKind, image);
                break;
        }
    }

    public static Token NewToken(int ofKind)
    {
        return NewToken(ofKind, null);
    }
}
