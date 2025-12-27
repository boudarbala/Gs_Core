using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Ui.Geom;
using Java.Util.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.GraphicGraph.ElementType;
using static Org.Graphstream.Ui.GraphicGraph.EventType;
using static Org.Graphstream.Ui.GraphicGraph.AttributeChangeEvent;
using static Org.Graphstream.Ui.GraphicGraph.Mode;
using static Org.Graphstream.Ui.GraphicGraph.What;
using static Org.Graphstream.Ui.GraphicGraph.TimeFormat;
using static Org.Graphstream.Ui.GraphicGraph.OutputType;
using static Org.Graphstream.Ui.GraphicGraph.OutputPolicy;
using static Org.Graphstream.Ui.GraphicGraph.LayoutPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Quality;
using static Org.Graphstream.Ui.GraphicGraph.Option;
using static Org.Graphstream.Ui.GraphicGraph.AttributeType;
using static Org.Graphstream.Ui.GraphicGraph.Balise;
using static Org.Graphstream.Ui.GraphicGraph.GEXFAttribute;
using static Org.Graphstream.Ui.GraphicGraph.METAAttribute;
using static Org.Graphstream.Ui.GraphicGraph.GRAPHAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTVALUEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PARENTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.SPELLAttribute;
using static Org.Graphstream.Ui.GraphicGraph.COLORAttribute;
using static Org.Graphstream.Ui.GraphicGraph.POSITIONAttribute;
using static Org.Graphstream.Ui.GraphicGraph.SIZEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.THICKNESSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.IDType;
using static Org.Graphstream.Ui.GraphicGraph.ModeType;
using static Org.Graphstream.Ui.GraphicGraph.WeightType;
using static Org.Graphstream.Ui.GraphicGraph.EdgeType;
using static Org.Graphstream.Ui.GraphicGraph.NodeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.EdgeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.ClassType;
using static Org.Graphstream.Ui.GraphicGraph.TimeFormatType;
using static Org.Graphstream.Ui.GraphicGraph.GPXAttribute;
using static Org.Graphstream.Ui.GraphicGraph.WPTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.LINKAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EMAILAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.BOUNDSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.FixType;
using static Org.Graphstream.Ui.GraphicGraph.GraphAttribute;
using static Org.Graphstream.Ui.GraphicGraph.LocatorAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NodeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.DataAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PortAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EndPointAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EndPointType;
using static Org.Graphstream.Ui.GraphicGraph.HyperEdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.KeyAttribute;
using static Org.Graphstream.Ui.GraphicGraph.KeyDomain;
using static Org.Graphstream.Ui.GraphicGraph.KeyAttrType;
using static Org.Graphstream.Ui.GraphicGraph.GraphEvents;

namespace Gs_Core.Graphstream.Ui.GraphicGraph;

public class GraphPosLengthUtils
{
    private static readonly Logger logger = Logger.GetLogger(typeof(GraphPosLengthUtils).GetSimpleName());
    public static double[] NodePosition(Graph graph, string id)
    {
        Node node = graph.GetNode(id);
        if (node != null)
            return NodePosition(node);
        return null;
    }

    public static Point3 NodePointPosition(Graph graph, string id)
    {
        Node node = graph.GetNode(id);
        if (node != null)
            return NodePointPosition(node);
        return null;
    }

    public static double[] NodePosition(Node node)
    {
        double[] xyz = new double[3];
        NodePosition(node, xyz);
        return xyz;
    }

    public static Point3 NodePointPosition(Node node)
    {
        return NodePosition(node, new Point3());
    }

    public static void NodePosition(Graph graph, string id, double[] xyz)
    {
        Node node = graph.GetNode(id);
        if (node != null)
            NodePosition(node, xyz);
        throw new Exception("node '" + id + "' does not exist");
    }

    public static Point3 NodePosition(Graph graph, string id, Point3 pos)
    {
        Node node = graph.GetNode(id);
        if (node != null)
            return NodePosition(node, pos);
        throw new Exception("node '" + id + "' does not exist");
    }

    public static void NodePosition(Node node, double[] xyz)
    {
        if (xyz.Length < 3)
            return;
        if (node.HasAttribute("xyz") || node.HasAttribute("xy"))
        {
            object o = node.GetAttribute("xyz");
            if (o == null)
                o = node.GetAttribute("xy");
            if (o != null)
            {
                PositionFromObject(o, xyz);
            }
        }
        else if (node.HasAttribute("x"))
        {
            xyz[0] = (double)node.GetNumber("x");
            if (node.HasAttribute("y"))
                xyz[1] = (double)node.GetNumber("y");
            if (node.HasAttribute("z"))
                xyz[2] = (double)node.GetNumber("z");
        }
    }

    public static Point3 NodePosition(Node node, Point3 pos)
    {
        if (node.HasAttribute("xyz") || node.HasAttribute("xy"))
        {
            object o = node.GetAttribute("xyz");
            if (o == null)
                o = node.GetAttribute("xy");
            if (o != null)
            {
                return PositionFromObject(o, pos);
            }
        }
        else if (node.HasAttribute("x"))
        {
            double x = node.GetNumber("x");
            double y;
            double z;
            if (node.HasAttribute("y"))
                y = node.GetNumber("y");
            else
                y = pos.y;
            if (node.HasAttribute("z"))
                z = node.GetNumber("z");
            else
                z = pos.z;
            return new Point3(x, y, z);
        }

        return pos;
    }

    public static void PositionFromObject(object o, double[] xyz)
    {
        if (o is Object[])
        {
            object[] oo = (Object[])o;
            if (oo.Length > 0 && oo[0] is Number)
            {
                xyz[0] = ((Number)oo[0]).DoubleValue();
                if (oo.Length > 1)
                    xyz[1] = ((Number)oo[1]).DoubleValue();
                if (oo.Length > 2)
                    xyz[2] = ((Number)oo[2]).DoubleValue();
            }
        }
        else if (o is Double[])
        {
            Double[] oo = (Double[])o;
            if (oo.Length > 0)
                xyz[0] = oo[0];
            if (oo.Length > 1)
                xyz[1] = oo[1];
            if (oo.Length > 2)
                xyz[2] = oo[2];
        }
        else if (o is Float[])
        {
            float[] oo = (Float[])o;
            if (oo.Length > 0)
                xyz[0] = oo[0];
            if (oo.Length > 1)
                xyz[1] = oo[1];
            if (oo.Length > 2)
                xyz[2] = oo[2];
        }
        else if (o is Integer[])
        {
            int[] oo = (Integer[])o;
            if (oo.Length > 0)
                xyz[0] = oo[0];
            if (oo.Length > 1)
                xyz[1] = oo[1];
            if (oo.Length > 2)
                xyz[2] = oo[2];
        }
        else if (o is double[])
        {
            double[] oo = (double[])o;
            if (oo.Length > 0)
                xyz[0] = oo[0];
            if (oo.Length > 1)
                xyz[1] = oo[1];
            if (oo.Length > 2)
                xyz[2] = oo[2];
        }
        else if (o is float[])
        {
            float[] oo = (float[])o;
            if (oo.Length > 0)
                xyz[0] = oo[0];
            if (oo.Length > 1)
                xyz[1] = oo[1];
            if (oo.Length > 2)
                xyz[2] = oo[2];
        }
        else if (o is int[])
        {
            int[] oo = (int[])o;
            if (oo.Length > 0)
                xyz[0] = oo[0];
            if (oo.Length > 1)
                xyz[1] = oo[1];
            if (oo.Length > 2)
                xyz[2] = oo[2];
        }
        else if (o is Number[])
        {
            Number[] oo = (Number[])o;
            if (oo.Length > 0)
                xyz[0] = oo[0].DoubleValue();
            if (oo.Length > 1)
                xyz[1] = oo[1].DoubleValue();
            if (oo.Length > 2)
                xyz[2] = oo[2].DoubleValue();
        }
        else if (o is Point3)
        {
            Point3 oo = (Point3)o;
            xyz[0] = oo.x;
            xyz[1] = oo.y;
            xyz[2] = oo.z;
        }
        else if (o is Point2)
        {
            Point2 oo = (Point2)o;
            xyz[0] = oo.x;
            xyz[1] = oo.y;
            xyz[2] = 0;
        }
        else
        {
            logger.Warning(String.Format("Do not know how to handle xyz attribute %s.", o.GetType().GetName()));
        }
    }

    public static Point3 PositionFromObject(object o, Point3 pos)
    {
        double x = pos.x, y = pos.y, z = pos.z;
        if (o is Object[])
        {
            object[] oo = (Object[])o;
            if (oo.Length > 0 && oo[0] is Number)
            {
                x = ((Number)oo[0]).DoubleValue();
                if (oo.Length > 1)
                    y = ((Number)oo[1]).DoubleValue();
                if (oo.Length > 2)
                    z = ((Number)oo[2]).DoubleValue();
            }
        }
        else if (o is Double[])
        {
            Double[] oo = (Double[])o;
            if (oo.Length > 0)
                x = oo[0];
            if (oo.Length > 1)
                y = oo[1];
            if (oo.Length > 2)
                z = oo[2];
        }
        else if (o is Float[])
        {
            float[] oo = (Float[])o;
            if (oo.Length > 0)
                x = oo[0];
            if (oo.Length > 1)
                y = oo[1];
            if (oo.Length > 2)
                z = oo[2];
        }
        else if (o is Integer[])
        {
            int[] oo = (Integer[])o;
            if (oo.Length > 0)
                x = oo[0];
            if (oo.Length > 1)
                y = oo[1];
            if (oo.Length > 2)
                z = oo[2];
        }
        else if (o is double[])
        {
            double[] oo = (double[])o;
            if (oo.Length > 0)
                x = oo[0];
            if (oo.Length > 1)
                y = oo[1];
            if (oo.Length > 2)
                z = oo[2];
        }
        else if (o is float[])
        {
            float[] oo = (float[])o;
            if (oo.Length > 0)
                x = oo[0];
            if (oo.Length > 1)
                y = oo[1];
            if (oo.Length > 2)
                z = oo[2];
        }
        else if (o is int[])
        {
            int[] oo = (int[])o;
            if (oo.Length > 0)
                x = oo[0];
            if (oo.Length > 1)
                y = oo[1];
            if (oo.Length > 2)
                z = oo[2];
        }
        else if (o is Number[])
        {
            Number[] oo = (Number[])o;
            if (oo.Length > 0)
                x = oo[0].DoubleValue();
            if (oo.Length > 1)
                y = oo[1].DoubleValue();
            if (oo.Length > 2)
                z = oo[2].DoubleValue();
        }
        else if (o is Point3)
        {
            Point3 oo = (Point3)o;
            x = oo.x;
            y = oo.y;
            z = oo.z;
        }
        else if (o is Point2)
        {
            Point2 oo = (Point2)o;
            x = oo.x;
            y = oo.y;
            z = 0;
        }
        else
        {
            logger.Warning(String.Format("Do not know how to handle xyz attribute %s%n", o.GetType().GetName()));
        }

        return new Point3(x, y, z);
    }

    public static double EdgeLength(Graph graph, string id)
    {
        Edge edge = graph.GetEdge(id);
        if (edge != null)
            return EdgeLength(edge);
        throw new Exception("edge '" + id + "' cannot be found");
    }

    public static double EdgeLength(Edge edge)
    {
        double[] xyz0 = NodePosition(edge.GetNode0());
        double[] xyz1 = NodePosition(edge.GetNode1());
        if (xyz0 == null || xyz1 == null)
            return -1;
        xyz0[0] = xyz1[0] - xyz0[0];
        xyz0[1] = xyz1[1] - xyz0[1];
        xyz0[2] = xyz1[2] - xyz0[2];
        return Math.Sqrt(xyz0[0] * xyz0[0] + xyz0[1] * xyz0[1] + xyz0[2] * xyz0[2]);
    }
}
