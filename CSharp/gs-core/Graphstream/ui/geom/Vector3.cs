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

public class Vector3 : Vector2
{
    private static readonly long serialVersionUID = 8839258036865851454;
    public Vector3()
    {
        data = new double[3];
        data[0] = 0;
        data[1] = 0;
        data[2] = 0;
    }

    public Vector3(double x, double y, double z)
    {
        data = new double[3];
        data[0] = x;
        data[1] = y;
        data[2] = z;
    }

    public Vector3(Vector3 other)
    {
        data = new double[3];
        Copy(other);
    }

    public Vector3(Point3 point)
    {
        data = new double[3];
        Copy(point);
    }

    public override bool IsZero()
    {
        return (data[0] == 0 && data[1] == 0 && data[2] == 0);
    }

    public override bool Equals(object other)
    {
        Vector3 v;
        if (!(other is Vector3))
        {
            return false;
        }

        v = (Vector3)other;
        return (data[0] == v.data[0] && data[1] == v.data[1] && data[2] == v.data[2]);
    }

    public override bool ValidComponent(int i)
    {
        return (i >= 0 && i < 3);
    }

    public override object Clone()
    {
        return new Vector3(this);
    }

    public virtual double DotProduct(double ox, double oy, double oz)
    {
        return ((data[0] * ox) + (data[1] * oy) + (data[2] * oz));
    }

    public virtual double DotProduct(Vector3 other)
    {
        return ((data[0] * other.data[0]) + (data[1] * other.data[1]) + (data[2] * other.data[2]));
    }

    public override double Length()
    {
        return Math.Sqrt((data[0] * data[0]) + (data[1] * data[1]) + (data[2] * data[2]));
    }

    public virtual double Z()
    {
        return data[2];
    }

    public override void Fill(double value)
    {
        data[0] = data[1] = data[2] = value;
    }

    public override void Set(int i, double value)
    {
        data[i] = value;
    }

    public virtual void Set(double x, double y, double z)
    {
        data[0] = x;
        data[1] = y;
        data[2] = z;
    }

    public virtual void Add(Vector3 other)
    {
        data[0] += other.data[0];
        data[1] += other.data[1];
        data[2] += other.data[2];
    }

    public virtual void Sub(Vector3 other)
    {
        data[0] -= other.data[0];
        data[1] -= other.data[1];
        data[2] -= other.data[2];
    }

    public virtual void Mult(Vector3 other)
    {
        data[0] *= other.data[0];
        data[1] *= other.data[1];
        data[2] *= other.data[2];
    }

    public override void ScalarAdd(double value)
    {
        data[0] += value;
        data[1] += value;
        data[2] += value;
    }

    public override void ScalarSub(double value)
    {
        data[0] -= value;
        data[1] -= value;
        data[2] -= value;
    }

    public override void ScalarMult(double value)
    {
        data[0] *= value;
        data[1] *= value;
        data[2] *= value;
    }

    public override void ScalarDiv(double value)
    {
        data[0] /= value;
        data[1] /= value;
        data[2] /= value;
    }

    public virtual void CrossProduct(Vector3 other)
    {
        double x;
        double y;
        x = (data[1] * other.data[2]) - (data[2] * other.data[1]);
        y = (data[2] * other.data[0]) - (data[0] * other.data[2]);
        data[2] = (data[0] * other.data[1]) - (data[1] * other.data[0]);
        data[0] = x;
        data[1] = y;
    }

    public virtual void CrossProduct(Vector3 A, Vector3 B)
    {
        data[0] = (A.data[1] * B.data[2]) - (A.data[2] * B.data[1]);
        data[1] = (A.data[2] * B.data[0]) - (A.data[0] * B.data[2]);
        data[2] = (A.data[0] * B.data[1]) - (A.data[1] * B.data[0]);
    }

    public override double Normalize()
    {
        double len = Length();
        if (len != 0)
        {
            data[0] /= len;
            data[1] /= len;
            data[2] /= len;
        }

        return len;
    }

    public virtual void Copy(Vector3 other)
    {
        data[0] = other.data[0];
        data[1] = other.data[1];
        data[2] = other.data[2];
    }

    public virtual void Copy(Point3 point)
    {
        data[0] = point.x;
        data[1] = point.y;
        data[2] = point.z;
    }

    public override string ToString()
    {
        StringBuffer sb = new StringBuffer("[");
        sb.Append(data[0]);
        sb.Append('|');
        sb.Append(data[1]);
        sb.Append('|');
        sb.Append(data[2]);
        sb.Append(']');
        return sb.ToString();
    }
}
