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

public class Point3 : Point2, Serializable
{
    private static readonly long serialVersionUID = 5971336344439693816;
    public double z;
    public static readonly Point3 NULL_POINT3 = new Point3(0, 0, 0);
    public Point3()
    {
    }

    public Point3(double x, double y)
    {
        Set(x, y, 0);
    }

    public Point3(double x, double y, double z)
    {
        Set(x, y, z);
    }

    public Point3(Point3 other)
    {
        Copy(other);
    }

    public Point3(Vector3 vec)
    {
        Copy(vec);
    }

    public Point3(float[] data) : this(0, data)
    {
    }

    public Point3(double[] data) : this(0, data)
    {
    }

    public Point3(int start, float[] data)
    {
        if (data != null)
        {
            if (data.Length > start + 0)
                x = data[start + 0];
            if (data.Length > start + 1)
                y = data[start + 1];
            if (data.Length > start + 2)
                z = data[start + 2];
        }
    }

    public Point3(int start, double[] data)
    {
        if (data != null)
        {
            if (data.Length > start + 0)
                x = data[start + 0];
            if (data.Length > start + 1)
                y = data[start + 1];
            if (data.Length > start + 2)
                z = data[start + 2];
        }
    }

    public override bool IsZero()
    {
        return (x == 0 && y == 0 && z == 0);
    }

    public virtual Point3 Interpolate(Point3 other, double factor)
    {
        Point3 p = new Point3(x + ((other.x - x) * factor), y + ((other.y - y) * factor), z + ((other.z - z) * factor));
        return p;
    }

    public virtual double Distance(Point3 other)
    {
        double xx = other.x - x;
        double yy = other.y - y;
        double zz = other.z - z;
        return Math.Abs(Math.Sqrt((xx * xx) + (yy * yy) + (zz * zz)));
    }

    public virtual double Distance(double x, double y, double z)
    {
        double xx = x - this.x;
        double yy = y - this.y;
        double zz = z - this.z;
        return Math.Abs(Math.Sqrt((xx * xx) + (yy * yy) + (zz * zz)));
    }

    public virtual void Copy(Point3 other)
    {
        x = other.x;
        y = other.y;
        z = other.z;
    }

    public virtual void Copy(Vector3 vec)
    {
        x = vec.data[0];
        y = vec.data[1];
        z = vec.data[2];
    }

    public virtual void Set(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public virtual void MoveTo(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public virtual void Move(double dx, double dy, double dz)
    {
        this.x += dx;
        this.y += dy;
        this.z += dz;
    }

    public virtual void Move(Point3 p)
    {
        this.x += p.x;
        this.y += p.y;
        this.z += p.z;
    }

    public virtual void Move(Vector3 d)
    {
        this.x += d.data[0];
        this.y += d.data[1];
        this.z += d.data[2];
    }

    public virtual void MoveZ(double dz)
    {
        z += dz;
    }

    public virtual void Scale(double sx, double sy, double sz)
    {
        x *= sx;
        y *= sy;
        z *= sz;
    }

    public virtual void Scale(Point3 s)
    {
        x *= s.x;
        y *= s.y;
        z *= s.z;
    }

    public virtual void Scale(Vector3 s)
    {
        x *= s.data[0];
        y *= s.data[1];
        z *= s.data[2];
    }

    public virtual void Scale(double scalar)
    {
        x *= scalar;
        y *= scalar;
        z *= scalar;
    }

    public virtual void SetZ(double z)
    {
        this.z = z;
    }

    public virtual void Swap(Point3 other)
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
            t = this.z;
            this.z = other.z;
            other.z = t;
        }
    }

    public override string ToString()
    {
        StringBuffer buf;
        buf = new StringBuffer("Point3[");
        buf.Append(x);
        buf.Append('|');
        buf.Append(y);
        buf.Append('|');
        buf.Append(z);
        buf.Append("]");
        return buf.ToString();
    }

    public override bool Equals(object o)
    {
        if (this == o)
        {
            return true;
        }

        if (o == null || GetType() != o.GetType())
        {
            return false;
        }

        if (!base.Equals(o))
        {
            return false;
        }

        Point3 point3 = (Point3)o;
        if (Double.Compare(point3.z, z) != 0)
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        int result = base.GetHashCode();
        long temp;
        temp = Double.DoubleToLongBits(z);
        result = 31 * result + (int)(temp ^ (temp >>> 32));
        return result;
    }
}
