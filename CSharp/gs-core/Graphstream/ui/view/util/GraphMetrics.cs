using Java.Util.Logging;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.StyleConstants;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Miv.Pherd.Geom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.View.Util.ElementType;
using static Org.Graphstream.Ui.View.Util.EventType;
using static Org.Graphstream.Ui.View.Util.AttributeChangeEvent;
using static Org.Graphstream.Ui.View.Util.Mode;
using static Org.Graphstream.Ui.View.Util.What;
using static Org.Graphstream.Ui.View.Util.TimeFormat;
using static Org.Graphstream.Ui.View.Util.OutputType;
using static Org.Graphstream.Ui.View.Util.OutputPolicy;
using static Org.Graphstream.Ui.View.Util.LayoutPolicy;
using static Org.Graphstream.Ui.View.Util.Quality;
using static Org.Graphstream.Ui.View.Util.Option;
using static Org.Graphstream.Ui.View.Util.AttributeType;
using static Org.Graphstream.Ui.View.Util.Balise;
using static Org.Graphstream.Ui.View.Util.GEXFAttribute;
using static Org.Graphstream.Ui.View.Util.METAAttribute;
using static Org.Graphstream.Ui.View.Util.GRAPHAttribute;
using static Org.Graphstream.Ui.View.Util.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.View.Util.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.View.Util.NODESAttribute;
using static Org.Graphstream.Ui.View.Util.NODEAttribute;
using static Org.Graphstream.Ui.View.Util.ATTVALUEAttribute;
using static Org.Graphstream.Ui.View.Util.PARENTAttribute;
using static Org.Graphstream.Ui.View.Util.EDGESAttribute;
using static Org.Graphstream.Ui.View.Util.SPELLAttribute;
using static Org.Graphstream.Ui.View.Util.COLORAttribute;
using static Org.Graphstream.Ui.View.Util.POSITIONAttribute;
using static Org.Graphstream.Ui.View.Util.SIZEAttribute;
using static Org.Graphstream.Ui.View.Util.NODESHAPEAttribute;
using static Org.Graphstream.Ui.View.Util.EDGEAttribute;
using static Org.Graphstream.Ui.View.Util.THICKNESSAttribute;
using static Org.Graphstream.Ui.View.Util.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.View.Util.IDType;
using static Org.Graphstream.Ui.View.Util.ModeType;
using static Org.Graphstream.Ui.View.Util.WeightType;
using static Org.Graphstream.Ui.View.Util.EdgeType;
using static Org.Graphstream.Ui.View.Util.NodeShapeType;
using static Org.Graphstream.Ui.View.Util.EdgeShapeType;
using static Org.Graphstream.Ui.View.Util.ClassType;
using static Org.Graphstream.Ui.View.Util.TimeFormatType;
using static Org.Graphstream.Ui.View.Util.GPXAttribute;
using static Org.Graphstream.Ui.View.Util.WPTAttribute;
using static Org.Graphstream.Ui.View.Util.LINKAttribute;
using static Org.Graphstream.Ui.View.Util.EMAILAttribute;
using static Org.Graphstream.Ui.View.Util.PTAttribute;
using static Org.Graphstream.Ui.View.Util.BOUNDSAttribute;
using static Org.Graphstream.Ui.View.Util.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.View.Util.FixType;
using static Org.Graphstream.Ui.View.Util.GraphAttribute;
using static Org.Graphstream.Ui.View.Util.LocatorAttribute;
using static Org.Graphstream.Ui.View.Util.NodeAttribute;
using static Org.Graphstream.Ui.View.Util.EdgeAttribute;
using static Org.Graphstream.Ui.View.Util.DataAttribute;
using static Org.Graphstream.Ui.View.Util.PortAttribute;
using static Org.Graphstream.Ui.View.Util.EndPointAttribute;
using static Org.Graphstream.Ui.View.Util.EndPointType;
using static Org.Graphstream.Ui.View.Util.HyperEdgeAttribute;
using static Org.Graphstream.Ui.View.Util.KeyAttribute;
using static Org.Graphstream.Ui.View.Util.KeyDomain;
using static Org.Graphstream.Ui.View.Util.KeyAttrType;
using static Org.Graphstream.Ui.View.Util.GraphEvents;
using static Org.Graphstream.Ui.View.Util.ThreadingModel;
using static Org.Graphstream.Ui.View.Util.CloseFramePolicy;
using static Org.Graphstream.Ui.View.Util.Measures;
using static Org.Graphstream.Ui.View.Util.Token;
using static Org.Graphstream.Ui.View.Util.Extension;
using static Org.Graphstream.Ui.View.Util.DefaultEdgeType;
using static Org.Graphstream.Ui.View.Util.AttrType;
using static Org.Graphstream.Ui.View.Util.Resolutions;
using static Org.Graphstream.Ui.View.Util.PropertyType;
using static Org.Graphstream.Ui.View.Util.Type;
using static Org.Graphstream.Ui.View.Util.Units;
using static Org.Graphstream.Ui.View.Util.FillMode;
using static Org.Graphstream.Ui.View.Util.StrokeMode;
using static Org.Graphstream.Ui.View.Util.ShadowMode;
using static Org.Graphstream.Ui.View.Util.VisibilityMode;
using static Org.Graphstream.Ui.View.Util.TextMode;
using static Org.Graphstream.Ui.View.Util.TextVisibilityMode;
using static Org.Graphstream.Ui.View.Util.TextStyle;
using static Org.Graphstream.Ui.View.Util.IconMode;
using static Org.Graphstream.Ui.View.Util.SizeMode;
using static Org.Graphstream.Ui.View.Util.TextAlignment;
using static Org.Graphstream.Ui.View.Util.TextBackgroundMode;
using static Org.Graphstream.Ui.View.Util.ShapeKind;
using static Org.Graphstream.Ui.View.Util.Shape;
using static Org.Graphstream.Ui.View.Util.SpriteOrientation;
using static Org.Graphstream.Ui.View.Util.ArrowShape;
using static Org.Graphstream.Ui.View.Util.JComponents;

namespace Gs_Core.Graphstream.Ui.View.Util;

public class GraphMetrics
{
    private static readonly Logger logger = Logger.GetLogger(typeof(GraphMetrics).GetSimpleName());
    public Point3 lo = new Point3();
    public Point3 hi = new Point3();
    public Point3 loVisible = new Point3();
    public Point3 hiVisible = new Point3();
    public Vector3 size = new Vector3();
    public double diagonal = 1;
    public double[] viewport = new double[4];
    public double ratioPx2Gu;
    public double px1;
    public GraphMetrics()
    {
        SetDefaults();
    }

    protected virtual void SetDefaults()
    {
        lo.Set(-1, -1, -1);
        hi.Set(1, 1, 1);
        size.Set(2, 2, 2);
        diagonal = 1;
        ratioPx2Gu = 1;
        px1 = 1;
    }

    public virtual double GetDiagonal()
    {
        return diagonal;
    }

    public virtual Vector3 GetSize()
    {
        return size;
    }

    public virtual Point3 GetLowPoint()
    {
        return lo;
    }

    public virtual Point3 GetHighPoint()
    {
        return hi;
    }

    public virtual double GraphWidthGU()
    {
        return hi.x - lo.x;
    }

    public virtual double GraphHeightGU()
    {
        return hi.y - lo.y;
    }

    public virtual double GraphDepthGU()
    {
        return hi.z - lo.z;
    }

    public virtual double LengthToGu(double value, StyleConstants.Units units)
    {
        switch (units)
        {
            case PX:
                return value / ratioPx2Gu;
            case PERCENTS:
                return (diagonal * value);
            case GU:
            default:
                return value;
                break;
        }
    }

    public virtual double LengthToGu(Value value)
    {
        return LengthToGu(value.value, value.units);
    }

    public virtual double LengthToGu(Values values, int index)
    {
        return LengthToGu(values[index], values.units);
    }

    public virtual double LengthToPx(double value, StyleConstants.Units units)
    {
        switch (units)
        {
            case GU:
                return value * ratioPx2Gu;
            case PERCENTS:
                return (diagonal * value) * ratioPx2Gu;
            case PX:
            default:
                return value;
                break;
        }
    }

    public virtual double LengthToPx(Value value)
    {
        return LengthToPx(value.value, value.units);
    }

    public virtual double LengthToPx(Values values, int index)
    {
        return LengthToPx(values[index], values.units);
    }

    public virtual double PositionPixelToGu(int pixels, int index)
    {
        double l = LengthToGu(pixels, Units.PX);
        switch (index)
        {
            case 0:
                l -= GraphWidthGU() / 2;
                l = (hi.x + lo.x) / 2 + l;
                break;
            case 1:
                l -= GraphHeightGU() / 2;
                l = (hi.y + lo.y) / 2 + l;
                break;
            default:
                throw new ArgumentException();
                break;
        }

        logger.Fine(String.Format("%spixel[%d] %d --> %fgu", this, index, pixels, l));
        return l;
    }

    public virtual string ToString()
    {
        StringBuilder builder = new StringBuilder(String.Format("Graph Metrics :%n"));
        builder.Append(String.Format("        lo         = %s%n", lo));
        builder.Append(String.Format("        hi         = %s%n", hi));
        builder.Append(String.Format("        visible lo = %s%n", loVisible));
        builder.Append(String.Format("        visible hi = %s%n", hiVisible));
        builder.Append(String.Format("        size       = %s%n", size));
        builder.Append(String.Format("        diag       = %f%n", diagonal));
        builder.Append(String.Format("        viewport   = %s%n", viewport));
        builder.Append(String.Format("        ratio      = %fpx = 1gu%n", ratioPx2Gu));
        return builder.ToString();
    }

    public virtual void SetViewport(double viewportX, double viewportY, double viewportWidth, double viewportHeight)
    {
        viewport[0] = viewportX;
        viewport[1] = viewportY;
        viewport[2] = viewportWidth;
        viewport[3] = viewportHeight;
    }

    public virtual void SetRatioPx2Gu(double ratio)
    {
        if (ratio > 0)
        {
            ratioPx2Gu = ratio;
            px1 = 0.95F / ratioPx2Gu;
        }
        else if (ratio == 0)
            throw new Exception("ratio PX to GU cannot be zero");
        else if (ratio < 0)
            throw new Exception(String.Format("ratio PX to GU cannot be negative (%f)", ratio));
    }

    public virtual void SetBounds(double minx, double miny, double minz, double maxx, double maxy, double maxz)
    {
        lo.x = minx;
        lo.y = miny;
        lo.z = minz;
        hi.x = maxx;
        hi.y = maxy;
        hi.z = maxz;
        size.data[0] = hi.x - lo.x;
        size.data[1] = hi.y - lo.y;
        size.data[2] = hi.z - lo.z;
        diagonal = Math.Sqrt(size.data[0] * size.data[0] + size.data[1] * size.data[1] + size.data[2] * size.data[2]);
    }
}
