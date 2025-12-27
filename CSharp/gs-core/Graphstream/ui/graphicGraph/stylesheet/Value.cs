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
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Type;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Units;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.FillMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.StrokeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ShadowMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.VisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextVisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextStyle;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.IconMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SizeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextAlignment;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextBackgroundMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ShapeKind;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Shape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SpriteOrientation;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ArrowShape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.JComponents;

namespace Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;

public class Value : Number
{
    private static readonly long serialVersionUID = 1;
    public double value;
    public Style.Units units;
    public Value(Style.Units units, double value)
    {
        this.value = value;
        this.units = units;
    }

    public Value(Value other)
    {
        this.value = other.value;
        this.units = other.units;
    }

    public override float FloatValue()
    {
        return (float)value;
    }

    public override double DoubleValue()
    {
        return value;
    }

    public override int IntValue()
    {
        return (int)Math.Round(value);
    }

    public override long LongValue()
    {
        return Math.Round(value);
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(value);
        switch (units)
        {
            case GU:
                builder.Append("gu");
                break;
            case PX:
                builder.Append("px");
                break;
            case PERCENTS:
                builder.Append("%");
                break;
            default:
                builder.Append("wtf (what's the fuck?)");
                break;
        }

        return builder.ToString();
    }

    public virtual bool Equals(Value o)
    {
        if (o != this)
        {
            if (!(o is Value))
                return false;
            Value other = (Value)o;
            if (other.units != units)
                return false;
            if (other.value != value)
                return false;
        }

        return true;
    }
}
