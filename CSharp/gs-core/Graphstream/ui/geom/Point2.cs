using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.Geom.ElementType;
using static Org.Graphstream.Ui.Geom.EventType;
using static Org.Graphstream.Ui.Geom.AttributeChangeEvent;
using static Org.Graphstream.Ui.Geom.Mode;
using static Org.Graphstream.Ui.Geom.What;
using static Org.Graphstream.Ui.Geom.TimeFormat;
using static Org.Graphstream.Ui.Geom.OutputType;
using static Org.Graphstream.Ui.Geom.OutputPolicy;
using static Org.Graphstream.Ui.Geom.LayoutPolicy;
using static Org.Graphstream.Ui.Geom.Quality;
using static Org.Graphstream.Ui.Geom.Option;
using static Org.Graphstream.Ui.Geom.AttributeType;
using static Org.Graphstream.Ui.Geom.Balise;
using static Org.Graphstream.Ui.Geom.GEXFAttribute;
using static Org.Graphstream.Ui.Geom.METAAttribute;
using static Org.Graphstream.Ui.Geom.GRAPHAttribute;
using static Org.Graphstream.Ui.Geom.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.Geom.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.Geom.NODESAttribute;
using static Org.Graphstream.Ui.Geom.NODEAttribute;
using static Org.Graphstream.Ui.Geom.ATTVALUEAttribute;
using static Org.Graphstream.Ui.Geom.PARENTAttribute;
using static Org.Graphstream.Ui.Geom.EDGESAttribute;
using static Org.Graphstream.Ui.Geom.SPELLAttribute;
using static Org.Graphstream.Ui.Geom.COLORAttribute;
using static Org.Graphstream.Ui.Geom.POSITIONAttribute;
using static Org.Graphstream.Ui.Geom.SIZEAttribute;
using static Org.Graphstream.Ui.Geom.NODESHAPEAttribute;
using static Org.Graphstream.Ui.Geom.EDGEAttribute;
using static Org.Graphstream.Ui.Geom.THICKNESSAttribute;
using static Org.Graphstream.Ui.Geom.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.Geom.IDType;
using static Org.Graphstream.Ui.Geom.ModeType;
using static Org.Graphstream.Ui.Geom.WeightType;
using static Org.Graphstream.Ui.Geom.EdgeType;
using static Org.Graphstream.Ui.Geom.NodeShapeType;
using static Org.Graphstream.Ui.Geom.EdgeShapeType;
using static Org.Graphstream.Ui.Geom.ClassType;
using static Org.Graphstream.Ui.Geom.TimeFormatType;
using static Org.Graphstream.Ui.Geom.GPXAttribute;
using static Org.Graphstream.Ui.Geom.WPTAttribute;
using static Org.Graphstream.Ui.Geom.LINKAttribute;
using static Org.Graphstream.Ui.Geom.EMAILAttribute;
using static Org.Graphstream.Ui.Geom.PTAttribute;
using static Org.Graphstream.Ui.Geom.BOUNDSAttribute;
using static Org.Graphstream.Ui.Geom.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.Geom.FixType;
using static Org.Graphstream.Ui.Geom.GraphAttribute;
using static Org.Graphstream.Ui.Geom.LocatorAttribute;
using static Org.Graphstream.Ui.Geom.NodeAttribute;
using static Org.Graphstream.Ui.Geom.EdgeAttribute;
using static Org.Graphstream.Ui.Geom.DataAttribute;
using static Org.Graphstream.Ui.Geom.PortAttribute;
using static Org.Graphstream.Ui.Geom.EndPointAttribute;
using static Org.Graphstream.Ui.Geom.EndPointType;
using static Org.Graphstream.Ui.Geom.HyperEdgeAttribute;
using static Org.Graphstream.Ui.Geom.KeyAttribute;
using static Org.Graphstream.Ui.Geom.KeyDomain;
using static Org.Graphstream.Ui.Geom.KeyAttrType;
using static Org.Graphstream.Ui.Geom.GraphEvents;

namespace Gs_Core.Graphstream.Ui.Geom;

public class Point2 : Serializable
{
    private static readonly long serialVersionUID = 965985679540486895;
    public double x;
    public double y;
    public static readonly Point2 NULL_POINT2 = new Point2(0, 0);
    public Point2()
    {
    }

    public Point2(double x, double y)
    {
        Set(x, y);
    }

    public Point2(Point2 other)
    {
        Copy(other);
    }

    public virtual void Make(double x, double y)
    {
        Set(x, y);
    }

    public virtual bool IsZero()
    {
        return (x == 0 && y == 0);
    }

    public virtual Point2 Interpolate(Point2 other, double factor)
    {
        Point2 p = new Point2(x + ((other.x - x) * factor), y + ((other.y - y) * factor));
        return p;
    }

    public virtual double Distance(Point2 other)
    {
        double xx = other.x - x;
        double yy = other.y - y;
        return Math.Abs(Math.Sqrt((xx * xx) + (yy * yy)));
    }

    public virtual void Copy(Point2 other)
    {
        x = other.x;
        y = other.y;
    }

    public virtual void Set(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public virtual void MoveTo(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public virtual void Move(double dx, double dy)
    {
        this.x += dx;
        this.y += dy;
    }

    public virtual void Move(Point2 p)
    {
        this.x += p.x;
        this.y += p.y;
    }

    public virtual void MoveX(double dx)
    {
        x += dx;
    }

    public virtual void MoveY(double dy)
    {
        y += dy;
    }

    public virtual void Scale(double sx, double sy)
    {
        x *= sx;
        y *= sy;
    }

    public virtual void Scale(Point2 s)
    {
        x *= s.x;
        y *= s.y;
    }

    public virtual void SetX(double x)
    {
        this.x = x;
    }

    public virtual void SetY(double y)
    {
        this.y = y;
    }

    public virtual void Swap(Point2 other)
    {
        double t;
        if (other != this)
        {
            t = this.x;
            this.x = other.x;
            other.x = t;
            t = this.y;
            this.y = other.y;
            other.y = t;
        }
    }

    public virtual string ToString()
    {
        StringBuffer buf;
        buf = new StringBuffer("Point2[");
        buf.Append(x);
        buf.Append('|');
        buf.Append(y);
        buf.Append("]");
        return buf.ToString();
    }

    public virtual bool Equals(object o)
    {
        if (this == o)
        {
            return true;
        }

        if (o == null || GetType() != o.GetType())
        {
            return false;
        }

        Point2 point2 = (Point2)o;
        if (Double.Compare(point2.x, x) != 0)
        {
            return false;
        }

        if (Double.Compare(point2.y, y) != 0)
        {
            return false;
        }

        return true;
    }

    public virtual int GetHashCode()
    {
        int result;
        long temp;
        temp = Double.DoubleToLongBits(x);
        result = (int)(temp ^ (temp >>> 32));
        temp = Double.DoubleToLongBits(y);
        result = 31 * result + (int)(temp ^ (temp >>> 32));
        return result;
    }
}
