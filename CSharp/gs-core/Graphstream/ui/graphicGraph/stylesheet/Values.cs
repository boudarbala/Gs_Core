using Java.Util;
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

public class Values : Iterable<Double>
{
    public List<Double> values = new List<Double>();
    public Style.Units units;
    public Values(Style.Units units, params double[] values)
    {
        this.units = units;
        foreach (double value in values)
            this.values.Add(value);
    }

    public Values(Values other)
    {
        this.values = new List<Double>(other.values);
        this.units = other.units;
    }

    public Values(Value value)
    {
        this.values = new List<Double>();
        this.units = value.units;
        values.Add(value.value);
    }

    public virtual int Size()
    {
        return values.Count;
    }

    public virtual int GetValueCount()
    {
        return values.Count;
    }

    public virtual double Get(int i)
    {
        if (i < 0)
            return values[0];
        else if (i >= values.Count)
            return values[values.Count - 1];
        else
            return values[i];
    }

    public virtual Style.Units GetUnits()
    {
        return units;
    }

    public virtual bool Equals(object o)
    {
        if (o != this)
        {
            if (!(o is Values))
                return false;
            Values other = (Values)o;
            if (other.units != units)
                return false;
            int n = values.Count;
            if (other.values.Count != n)
                return false;
            for (int i = 0; i < n; i++)
            {
                if (!other.values[i].Equals(values[i]))
                    return false;
            }
        }

        return true;
    }

    public virtual IEnumerator<Double> Iterator()
    {
        return values.Iterator();
    }

    public virtual string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append('(');
        foreach (double value in values)
        {
            builder.Append(' ');
            builder.Append(value);
        }

        builder.Append(" )");
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

    public virtual void Copy(Values values)
    {
        units = values.units;
        this.values.Clear();
        this.values.AddAll(values.values);
    }

    public virtual void AddValues(params double[] values)
    {
        foreach (double value in values)
            this.values.Add(value);
    }

    public virtual void InsertValue(int i, double value)
    {
        values.Add(i, value);
    }

    public virtual void SetValue(int i, double value)
    {
        values[i] = value;
    }

    public virtual void RemoveValue(int i)
    {
        values.Remove(i);
    }

    public virtual void SetUnits(Style.Units units)
    {
        this.units = units;
    }
}
