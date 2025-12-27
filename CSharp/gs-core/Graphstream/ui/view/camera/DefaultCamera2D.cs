using Java.Util;
using Java.Util.Stream;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.GraphicGraph;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.StyleConstants;
using Gs_Core.Graphstream.Ui.View.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.View.Camera.ElementType;
using static Org.Graphstream.Ui.View.Camera.EventType;
using static Org.Graphstream.Ui.View.Camera.AttributeChangeEvent;
using static Org.Graphstream.Ui.View.Camera.Mode;
using static Org.Graphstream.Ui.View.Camera.What;
using static Org.Graphstream.Ui.View.Camera.TimeFormat;
using static Org.Graphstream.Ui.View.Camera.OutputType;
using static Org.Graphstream.Ui.View.Camera.OutputPolicy;
using static Org.Graphstream.Ui.View.Camera.LayoutPolicy;
using static Org.Graphstream.Ui.View.Camera.Quality;
using static Org.Graphstream.Ui.View.Camera.Option;
using static Org.Graphstream.Ui.View.Camera.AttributeType;
using static Org.Graphstream.Ui.View.Camera.Balise;
using static Org.Graphstream.Ui.View.Camera.GEXFAttribute;
using static Org.Graphstream.Ui.View.Camera.METAAttribute;
using static Org.Graphstream.Ui.View.Camera.GRAPHAttribute;
using static Org.Graphstream.Ui.View.Camera.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.View.Camera.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.View.Camera.NODESAttribute;
using static Org.Graphstream.Ui.View.Camera.NODEAttribute;
using static Org.Graphstream.Ui.View.Camera.ATTVALUEAttribute;
using static Org.Graphstream.Ui.View.Camera.PARENTAttribute;
using static Org.Graphstream.Ui.View.Camera.EDGESAttribute;
using static Org.Graphstream.Ui.View.Camera.SPELLAttribute;
using static Org.Graphstream.Ui.View.Camera.COLORAttribute;
using static Org.Graphstream.Ui.View.Camera.POSITIONAttribute;
using static Org.Graphstream.Ui.View.Camera.SIZEAttribute;
using static Org.Graphstream.Ui.View.Camera.NODESHAPEAttribute;
using static Org.Graphstream.Ui.View.Camera.EDGEAttribute;
using static Org.Graphstream.Ui.View.Camera.THICKNESSAttribute;
using static Org.Graphstream.Ui.View.Camera.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.View.Camera.IDType;
using static Org.Graphstream.Ui.View.Camera.ModeType;
using static Org.Graphstream.Ui.View.Camera.WeightType;
using static Org.Graphstream.Ui.View.Camera.EdgeType;
using static Org.Graphstream.Ui.View.Camera.NodeShapeType;
using static Org.Graphstream.Ui.View.Camera.EdgeShapeType;
using static Org.Graphstream.Ui.View.Camera.ClassType;
using static Org.Graphstream.Ui.View.Camera.TimeFormatType;
using static Org.Graphstream.Ui.View.Camera.GPXAttribute;
using static Org.Graphstream.Ui.View.Camera.WPTAttribute;
using static Org.Graphstream.Ui.View.Camera.LINKAttribute;
using static Org.Graphstream.Ui.View.Camera.EMAILAttribute;
using static Org.Graphstream.Ui.View.Camera.PTAttribute;
using static Org.Graphstream.Ui.View.Camera.BOUNDSAttribute;
using static Org.Graphstream.Ui.View.Camera.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.View.Camera.FixType;
using static Org.Graphstream.Ui.View.Camera.GraphAttribute;
using static Org.Graphstream.Ui.View.Camera.LocatorAttribute;
using static Org.Graphstream.Ui.View.Camera.NodeAttribute;
using static Org.Graphstream.Ui.View.Camera.EdgeAttribute;
using static Org.Graphstream.Ui.View.Camera.DataAttribute;
using static Org.Graphstream.Ui.View.Camera.PortAttribute;
using static Org.Graphstream.Ui.View.Camera.EndPointAttribute;
using static Org.Graphstream.Ui.View.Camera.EndPointType;
using static Org.Graphstream.Ui.View.Camera.HyperEdgeAttribute;
using static Org.Graphstream.Ui.View.Camera.KeyAttribute;
using static Org.Graphstream.Ui.View.Camera.KeyDomain;
using static Org.Graphstream.Ui.View.Camera.KeyAttrType;
using static Org.Graphstream.Ui.View.Camera.GraphEvents;
using static Org.Graphstream.Ui.View.Camera.ThreadingModel;
using static Org.Graphstream.Ui.View.Camera.CloseFramePolicy;
using static Org.Graphstream.Ui.View.Camera.Measures;
using static Org.Graphstream.Ui.View.Camera.Token;
using static Org.Graphstream.Ui.View.Camera.Extension;
using static Org.Graphstream.Ui.View.Camera.DefaultEdgeType;
using static Org.Graphstream.Ui.View.Camera.AttrType;
using static Org.Graphstream.Ui.View.Camera.Resolutions;
using static Org.Graphstream.Ui.View.Camera.PropertyType;
using static Org.Graphstream.Ui.View.Camera.Type;
using static Org.Graphstream.Ui.View.Camera.Units;
using static Org.Graphstream.Ui.View.Camera.FillMode;
using static Org.Graphstream.Ui.View.Camera.StrokeMode;
using static Org.Graphstream.Ui.View.Camera.ShadowMode;
using static Org.Graphstream.Ui.View.Camera.VisibilityMode;
using static Org.Graphstream.Ui.View.Camera.TextMode;
using static Org.Graphstream.Ui.View.Camera.TextVisibilityMode;
using static Org.Graphstream.Ui.View.Camera.TextStyle;
using static Org.Graphstream.Ui.View.Camera.IconMode;
using static Org.Graphstream.Ui.View.Camera.SizeMode;
using static Org.Graphstream.Ui.View.Camera.TextAlignment;
using static Org.Graphstream.Ui.View.Camera.TextBackgroundMode;
using static Org.Graphstream.Ui.View.Camera.ShapeKind;
using static Org.Graphstream.Ui.View.Camera.Shape;
using static Org.Graphstream.Ui.View.Camera.SpriteOrientation;
using static Org.Graphstream.Ui.View.Camera.ArrowShape;
using static Org.Graphstream.Ui.View.Camera.JComponents;

namespace Gs_Core.Graphstream.Ui.View.Camera;

public class DefaultCamera2D : Camera
{
    protected GraphMetrics metrics = new GraphMetrics();
    protected bool autoFit = true;
    protected Point3 center = new Point3();
    protected double zoom = 1;
    protected double rotation = 0;
    protected Values padding = new Values(Units.GU, 0, 0, 0);
    protected Backend bck = null;
    protected HashSet<string> nodeInvisible = new HashSet();
    protected HashSet<string> spriteInvisible = new HashSet();
    protected double[] gviewport = null;
    protected GraphicGraph graph;
    public DefaultCamera2D(GraphicGraph graph)
    {
        this.graph = graph;
    }

    public virtual Point3 GetViewCenter()
    {
        return center;
    }

    public virtual void SetViewCenter(double x, double y, double z)
    {
        SetAutoFitView(false);
        center.Set(x, y, z);
        graph.graphChanged = true;
    }

    public virtual void SetViewCenter(Point3 p)
    {
        SetViewCenter(p.x, p.y, p.z);
    }

    public virtual double GetViewPercent()
    {
        return zoom;
    }

    public virtual void SetViewPercent(double percent)
    {
        SetAutoFitView(false);
        SetZoom(percent);
        graph.graphChanged = true;
    }

    public virtual void SetZoom(double z)
    {
        zoom = z;
        graph.graphChanged = true;
    }

    public virtual double GetViewRotation()
    {
        return rotation;
    }

    public virtual void SetViewRotation(double theta)
    {
        rotation = theta;
        graph.graphChanged = true;
    }

    public virtual void SetViewport(double x, double y, double viewportWidth, double viewportHeight)
    {
        metrics.SetViewport(x, y, viewportWidth, viewportHeight);
    }

    public virtual double GetGraphDimension()
    {
        return metrics.GetDiagonal();
    }

    public virtual bool SpriteContains(GraphicElement elt, double x, double y)
    {
        GraphicSprite sprite = (GraphicSprite)elt;
        Values size = GetNodeOrSpriteSize(elt);
        double w2 = metrics.LengthToPx(size, 0) / 2;
        double h2 = w2;
        if (size.Count > 1)
            h2 = metrics.LengthToPx(size, 1) / 2;
        Point3 dst = SpritePositionPx(sprite);
        double x1 = dst.x - w2;
        double x2 = dst.x + w2;
        double y1 = dst.y - h2;
        double y2 = dst.y + h2;
        if (x < x1)
            return false;
        else if (y < y1)
            return false;
        else if (x > x2)
            return false;
        else if (y > y2)
            return false;
        else
            return true;
    }

    public virtual Point3 SpritePositionPx(GraphicSprite sprite)
    {
        return GetSpritePosition(sprite, new Point3(), Units.PX);
    }

    public virtual Point3 GetSpritePosition(GraphicSprite sprite, Point3 pos, Units units)
    {
        if (sprite.IsAttachedToNode())
            return GetSpritePositionNode(sprite, pos, units);
        else if (sprite.IsAttachedToEdge())
            return GetSpritePositionEdge(sprite, pos, units);
        else
            return GetSpritePositionFree(sprite, pos, units);
    }

    public virtual Point3 GetSpritePositionFree(GraphicSprite sprite, Point3 position, Units units)
    {
        Point3 pos = position;
        if (pos == null)
        {
            pos = new Point3();
        }

        if (sprite.GetUnits() == units)
        {
            pos.x = sprite.GetX();
            pos.y = sprite.GetY();
        }
        else if (units == Units.GU && sprite.GetUnits() == Units.PX)
        {
            pos.x = sprite.GetX();
            pos.y = sprite.GetY();
            bck.InverseTransform(pos);
        }
        else if (units == Units.PX && sprite.GetUnits() == Units.GU)
        {
            pos.x = sprite.GetX();
            pos.y = sprite.GetY();
            bck.Transform(pos);
        }
        else if (units == Units.GU && sprite.GetUnits() == Units.PERCENTS)
        {
            pos.x = metrics.lo.x + (sprite.GetX() / 100F) * metrics.GraphWidthGU();
            pos.y = metrics.lo.y + (sprite.GetY() / 100F) * metrics.GraphHeightGU();
        }
        else if (units == Units.PX && sprite.GetUnits() == Units.PERCENTS)
        {
            pos.x = (sprite.GetX() / 100F) * metrics.viewport[2];
            pos.y = (sprite.GetY() / 100F) * metrics.viewport[3];
        }
        else
        {
            throw new Exception("Unhandled yet sprite positioning convertion " + sprite.GetUnits() + " to " + units + ".");
        }

        return pos;
    }

    public virtual Point3 GetSpritePositionEdge(GraphicSprite sprite, Point3 position, Units units)
    {
        Point3 pos = position;
        if (pos == null)
            pos = new Point3();
        GraphicEdge edge = sprite.GetEdgeAttachment();
        ConnectorSkeleton info = (ConnectorSkeleton)edge.GetAttribute(Skeleton.attributeName);
        if (info != null)
        {
            double o = metrics.LengthToGu(sprite.GetY(), sprite.GetUnits());
            if (o == 0)
            {
                Point3 p = info.PointOnShape(sprite.GetX());
                pos.x = p.x;
                pos.y = p.y;
            }
            else
            {
                Point3 p = info.PointOnShapeAndPerpendicular(sprite.GetX(), o);
                pos.x = p.x;
                pos.y = p.y;
            }
        }
        else
        {
            double x = 0;
            double y = 0;
            double dx = 0;
            double dy = 0;
            double d = sprite.GetX();
            double o = metrics.LengthToGu(sprite.GetY(), sprite.GetUnits());
            x = edge.from.x;
            y = edge.from.y;
            dx = edge.to.x - x;
            dy = edge.to.y - y;
            if (d > 1)
                d = 1;
            if (d < 0)
                d = 0;
            x += dx * d;
            x += dy * d;
            if (o != 0)
            {
                d = Math.Sqrt(dx * dx + dy * dy);
                dx /= d;
                dy /= d;
                x += -dy * o;
                y += dx * o;
            }

            pos.x = x;
            pos.y = y;
        }

        if (units == Units.PX)
            bck.Transform(pos);
        return pos;
    }

    public virtual Point3 GetSpritePositionNode(GraphicSprite sprite, Point3 position, Units units)
    {
        Point3 pos = position;
        if (pos == null)
            pos = new Point3();
        double spriteX = metrics.LengthToGu(sprite.GetX(), sprite.GetUnits());
        double spriteY = metrics.LengthToGu(sprite.GetY(), sprite.GetUnits());
        GraphicNode node = sprite.GetNodeAttachment();
        pos.x = node.x + spriteX;
        pos.y = node.y + spriteY;
        if (units == Units.PX)
            bck.Transform(pos);
        return pos;
    }

    public virtual bool NodeContains(GraphicElement elt, double x, double y)
    {
        Values size = GetNodeOrSpriteSize(elt);
        double w2 = metrics.LengthToPx(size, 0) / 2;
        double h2 = w2;
        if (size.Count > 1)
            h2 = metrics.LengthToPx(size, 1) / 2;
        Point3 dst = bck.Transform(elt.GetX(), elt.GetY(), 0);
        double x1 = (dst.x) - w2;
        double x2 = (dst.x) + w2;
        double y1 = (dst.y) - h2;
        double y2 = (dst.y) + h2;
        if (x < x1)
            return false;
        else if (y < y1)
            return false;
        else if (x > x2)
            return false;
        else if (y > y2)
            return false;
        else
            return true;
    }

    public virtual bool EdgeContains(GraphicElement elt, double x, double y)
    {
        Values size = elt.GetStyle().GetSize();
        double w2 = metrics.LengthToPx(size, 0) / 2;
        double h2 = w2;
        if (size.Count > 1)
            h2 = metrics.LengthToPx(size, 1) / 2;
        Point3 dst = bck.Transform(elt.GetX(), elt.GetY(), 0);
        double x1 = (dst.x) - w2;
        double x2 = (dst.x) + w2;
        double y1 = (dst.y) - h2;
        double y2 = (dst.y) + h2;
        if (x < x1)
            return false;
        else if (y < y1)
            return false;
        else if (x > x2)
            return false;
        else if (y > y2)
            return false;
        else
            return true;
    }

    public virtual Values GetNodeOrSpriteSize(GraphicElement elt)
    {
        AreaSkeleton info = (AreaSkeleton)elt.GetAttribute(Skeleton.attributeName);
        if (info != null)
        {
            return new Values(Units.GU, info.TheSize().x, info.TheSize().y);
        }
        else
        {
            return elt.GetStyle().GetSize();
        }
    }

    public virtual Point3 GetNodeOrSpritePositionGU(GraphicElement elt, Point3 pos)
    {
        Point3 p = pos;
        if (p == null)
            p = new Point3();
        if (elt is GraphicNode)
        {
            p.x = ((GraphicNode)elt).GetX();
            p.y = ((GraphicNode)elt).GetY();
        }
        else if (elt is GraphicSprite)
        {
            p = GetSpritePosition(((GraphicSprite)elt), p, Units.GU);
        }

        return p;
    }

    public virtual void RemoveGraphViewport()
    {
        gviewport = null;
    }

    public virtual void SetGraphViewport(double minx, double miny, double maxx, double maxy)
    {
        gviewport = new double[4];
        gviewport[0] = minx;
        gviewport[1] = miny;
        gviewport[2] = maxx;
        gviewport[3] = maxy;
    }

    public virtual void ResetView()
    {
        SetAutoFitView(true);
        SetViewRotation(0);
    }

    public virtual void PushView(GraphicGraph graph)
    {
        bck.PushTransform();
        SetPadding(graph);
        if (autoFit)
            AutoFitView();
        else
            UserView();
        CheckVisibility(graph);
    }

    public virtual void PopView()
    {
        bck.PopTransform();
    }

    public virtual void CheckVisibility(GraphicGraph graph)
    {
        nodeInvisible.Clear();
        spriteInvisible.Clear();
        if (!autoFit)
        {
            double X = metrics.viewport[0];
            double Y = metrics.viewport[1];
            double W = metrics.viewport[2];
            double H = metrics.viewport[3];
            graph.Nodes().ForEach((node) =>
            {
                GraphicNode n = (GraphicNode)node;
                bool visible = IsNodeIn(n, X, Y, X + W, Y + H) && (!n.hidden) && n.positionned;
                if (!visible)
                {
                    nodeInvisible.Add(node.GetId());
                }
            });
            graph.Sprites().ForEach((sprite) =>
            {
                bool visible = IsSpriteIn(sprite, X, Y, X + W, Y + H) && (!sprite.hidden);
                if (!visible)
                {
                    spriteInvisible.Add(sprite.GetId());
                }
            });
        }
    }

    public virtual bool IsNodeIn(GraphicNode node, double X1, double Y1, double X2, double Y2)
    {
        Values size = GetNodeOrSpriteSize(node);
        double w2 = metrics.LengthToPx(size, 0) / 2;
        double h2 = w2;
        if (size.Count > 1)
            h2 = metrics.LengthToPx(size, 1) / 2;
        Point3 src = new Point3(node.GetX(), node.GetY(), 0);
        bck.Transform(src);
        double x1 = src.x - w2;
        double x2 = src.x + w2;
        double y1 = src.y - h2;
        double y2 = src.y + h2;
        if (x2 < X1)
            return false;
        else if (y2 < Y1)
            return false;
        else if (x1 > X2)
            return false;
        else if (y1 > Y2)
            return false;
        else
            return true;
    }

    public virtual bool IsEdgeIn(GraphicEdge edge, double X1, double Y1, double X2, double Y2)
    {
        Values size = edge.GetStyle().GetSize();
        double w2 = metrics.LengthToPx(size, 0) / 2;
        double h2 = w2;
        if (size.Count > 1)
            h2 = metrics.LengthToPx(size, 1) / 2;
        Point3 src = new Point3(edge.GetX(), edge.GetY(), 0);
        bck.Transform(src);
        double x1 = src.x - w2;
        double x2 = src.x + w2;
        double y1 = src.y - h2;
        double y2 = src.y + h2;
        if (x2 < X1)
            return false;
        else if (y2 < Y1)
            return false;
        else if (x1 > X2)
            return false;
        else if (y1 > Y2)
            return false;
        else
            return true;
    }

    public virtual void SetPadding(GraphicGraph graph)
    {
        padding.Copy(graph.GetStyle().GetPadding());
    }

    public virtual void AutoFitView()
    {
        double sx = 0;
        double sy = 0;
        double tx = 0;
        double ty = 0;
        double padXgu = PaddingXgu() * 2;
        double padYgu = PaddingYgu() * 2;
        double padXpx = PaddingXpx() * 2;
        double padYpx = PaddingYpx() * 2;
        if (padXpx > metrics.viewport[2])
            padXpx = metrics.viewport[2] / 10;
        if (padYpx > metrics.viewport[3])
            padYpx = metrics.viewport[3] / 10;
        sx = (metrics.viewport[2] - padXpx) / (metrics.size.data[0] + padXgu);
        sy = (metrics.viewport[3] - padYpx) / (metrics.size.data[1] + padYgu);
        tx = metrics.lo.x + (metrics.size.data[0] / 2);
        ty = metrics.lo.y + (metrics.size.data[1] / 2);
        if (sx > sy)
            sx = sy;
        else
            sy = sx;
        bck.BeginTransform();
        bck.SetIdentity();
        bck.Translate(metrics.viewport[2] / 2, metrics.viewport[3] / 2, 0);
        if (rotation != 0)
            bck.Rotate(rotation / (180 / Math.PI), 0, 0, 1);
        bck.Scale(sx, -sy, 0);
        bck.Translate(-tx, -ty, 0);
        bck.EndTransform();
        zoom = 1;
        center.Set(tx, ty, 0);
        metrics.ratioPx2Gu = sx;
        metrics.loVisible.Copy(metrics.lo);
        metrics.hiVisible.Copy(metrics.hi);
    }

    public virtual void UserView()
    {
        double sx = 0;
        double sy = 0;
        double tx = 0;
        double ty = 0;
        double padXgu = PaddingXgu() * 2;
        double padYgu = PaddingYgu() * 2;
        double padXpx = PaddingXpx() * 2;
        double padYpx = PaddingYpx() * 2;
        double gw;
        if (gviewport != null)
            gw = gviewport[2] - gviewport[0];
        else
            gw = metrics.size.data[0];
        double gh;
        if (gviewport != null)
            gh = gviewport[3] - gviewport[1];
        else
            gh = metrics.size.data[1];
        if (padXpx > metrics.viewport[2])
            padXpx = metrics.viewport[2] / 10;
        if (padYpx > metrics.viewport[3])
            padYpx = metrics.viewport[3] / 10;
        sx = (metrics.viewport[2] - padXpx) / ((gw + padXgu) * zoom);
        sy = (metrics.viewport[3] - padYpx) / ((gh + padYgu) * zoom);
        tx = center.x;
        ty = center.y;
        if (sx > sy)
            sx = sy;
        else
            sy = sx;
        bck.BeginTransform();
        bck.SetIdentity();
        bck.Translate(metrics.viewport[2] / 2, metrics.viewport[3] / 2, 0);
        if (rotation != 0)
            bck.Rotate(rotation / (180 / Math.PI), 0, 0, 1);
        bck.Scale(sx, -sy, 0);
        bck.Translate(-tx, -ty, 0);
        bck.EndTransform();
        metrics.ratioPx2Gu = sx;
        double w2 = (metrics.viewport[2] / sx) / 2F;
        double h2 = (metrics.viewport[3] / sx) / 2F;
        metrics.loVisible[center.x - w2] = center.y - h2;
        metrics.hiVisible[center.x + w2] = center.y + h2;
    }

    public virtual double PaddingXgu()
    {
        if (padding.units == Units.GU && padding.Count > 0)
            return padding[0];
        else
            return 0;
    }

    public virtual double PaddingYgu()
    {
        if (padding.units == Units.GU && padding.Count > 1)
            return padding[1];
        else
            return PaddingXgu();
    }

    public virtual double PaddingXpx()
    {
        if (padding.units == Units.PX && padding.Count > 0)
            return padding[0];
        else
            return 0;
    }

    public virtual double PaddingYpx()
    {
        if (padding.units == Units.PX && padding.Count > 1)
            return padding[1];
        else
            return PaddingXpx();
    }

    public virtual void SetBounds(double minx, double miny, double minz, double maxx, double maxy, double maxz)
    {
        metrics.SetBounds(minx, miny, minz, maxx, maxy, maxz);
    }

    public virtual void SetBounds(GraphicGraph graph)
    {
        SetBounds(graph.GetMinPos().x, graph.GetMinPos().y, 0, graph.GetMaxPos().x, graph.GetMaxPos().y, 0);
    }

    public virtual GraphMetrics GetMetrics()
    {
        return metrics;
    }

    public virtual void SetAutoFitView(bool on)
    {
        if (autoFit && (!on))
        {
            zoom = 1;
            center.Set(metrics.lo.x + (metrics.size.data[0] / 2), metrics.lo.y + (metrics.size.data[1] / 2), 0);
        }

        autoFit = on;
    }

    public virtual void SetBackend(Backend backend)
    {
        this.bck = backend;
    }

    public virtual Point3 TransformGuToPx(double x, double y, double z)
    {
        return bck.Transform(x, y, 0);
    }

    public virtual Point3 TransformPxToGu(double x, double y)
    {
        return bck.InverseTransform(x, y, 0);
    }

    protected virtual bool StyleVisible(GraphicElement element)
    {
        Values visibility = element.GetStyle().GetVisibility();
        switch (element.GetStyle().GetVisibilityMode())
        {
            case HIDDEN:
                return false;
            case AT_ZOOM:
                return (zoom == visibility[0]);
            case UNDER_ZOOM:
                return (zoom <= visibility[0]);
            case OVER_ZOOM:
                return (zoom >= visibility[0]);
            case ZOOM_RANGE:
                if (visibility.Count > 1)
                    return (zoom >= visibility[0] && zoom <= visibility[1]);
                else
                    return true;
            case ZOOMS:
                return Arrays.AsList(Selector.Type.Values()).Contains(visibility[0]);
            default:
                return true;
                break;
        }
    }

    public virtual bool IsTextVisible(GraphicElement element)
    {
        Values visibility = element.GetStyle().GetTextVisibility();
        switch (element.GetStyle().GetTextVisibilityMode())
        {
            case HIDDEN:
                return false;
            case AT_ZOOM:
                return (zoom == visibility[0]);
            case UNDER_ZOOM:
                return (zoom <= visibility[0]);
            case OVER_ZOOM:
                return (zoom >= visibility[0]);
            case ZOOM_RANGE:
                if (visibility.Count > 1)
                    return (zoom >= visibility[0] && zoom <= visibility[1]);
                else
                    return true;
            case ZOOMS:
                return Arrays.AsList(Selector.Type.Values()).Contains(visibility[0]);
            default:
                return true;
                break;
        }
    }

    public virtual bool IsVisible(GraphicElement element)
    {
        if (autoFit)
        {
            return ((!element.hidden) && (element.style.GetVisibilityMode() != StyleConstants.VisibilityMode.HIDDEN));
        }
        else
        {
            switch (element.GetSelectorType())
            {
                case NODE:
                    return !nodeInvisible.Contains(element.GetId());
                case EDGE:
                    return IsEdgeVisible((GraphicEdge)element);
                case SPRITE:
                    return !spriteInvisible.Contains(element.GetId());
                default:
                    return false;
                    break;
            }
        }
    }

    public virtual bool IsSpriteVisible(GraphicSprite sprite)
    {
        return IsSpriteIn(sprite, 0, 0, metrics.viewport[2], metrics.viewport[3]);
    }

    public virtual bool IsSpriteIn(GraphicSprite sprite, double X1, double Y1, double X2, double Y2)
    {
        Values size = GetNodeOrSpriteSize(sprite);
        double w2 = metrics.LengthToPx(size, 0) / 2;
        double h2 = w2;
        if (size.Count > 1)
            h2 = metrics.LengthToPx(size, 1) / 2;
        Point3 src = SpritePositionPx(sprite);
        double x1 = src.x - w2;
        double x2 = src.x + w2;
        double y1 = src.y - h2;
        double y2 = src.y + h2;
        if (x2 < X1)
            return false;
        else if (y2 < Y1)
            return false;
        else if (x1 > X2)
            return false;
        else if (y1 > Y2)
            return false;
        else
            return true;
    }

    public virtual bool IsEdgeVisible(GraphicEdge edge)
    {
        if (!((GraphicNode)edge.GetNode0()).positionned || !((GraphicNode)edge.GetNode1()).positionned)
        {
            return false;
        }
        else if (edge.hidden)
        {
            return false;
        }
        else
        {
            bool node0Invis = nodeInvisible.Contains(edge.GetNode0().GetId());
            bool node1Invis = nodeInvisible.Contains(edge.GetNode1().GetId());
            return !(node0Invis && node1Invis);
        }
    }

    public virtual GraphicElement FindGraphicElementAt(GraphicGraph graph, EnumSet<InteractiveElement> types, double x, double y)
    {
        double xT = x + metrics.viewport[0];
        double yT = y + metrics.viewport[1];
        if (types.Contains(InteractiveElement.NODE))
        {
            Optional<Node> node = graph.Nodes().Filter((n) => NodeContains((GraphicElement)n, xT, yT)).FindFirst();
            if (node.IsPresent())
            {
                if (IsVisible((GraphicElement)node.Get()))
                {
                    return (GraphicElement)node.Get();
                }
            }
        }

        if (types.Contains(InteractiveElement.EDGE))
        {
            Optional<Edge> edge = graph.Edges().Filter((e) => EdgeContains((GraphicElement)e, xT, yT)).FindFirst();
            if (edge.IsPresent())
            {
                if (IsVisible((GraphicElement)edge.Get()))
                {
                    return (GraphicElement)edge.Get();
                }
            }
        }

        if (types.Contains(InteractiveElement.SPRITE))
        {
            Optional<GraphicSprite> sprite = graph.Sprites().Filter((s) => SpriteContains(s, xT, yT)).FindFirst();
            if (sprite.IsPresent())
            {
                if (IsVisible((GraphicElement)sprite.Get()))
                {
                    return (GraphicElement)sprite.Get();
                }
            }
        }

        return null;
    }

    public virtual double[] GraphViewport()
    {
        return gviewport;
    }

    public virtual Collection<GraphicElement> AllGraphicElementsIn(GraphicGraph graph, EnumSet<InteractiveElement> types, double x1, double y1, double x2, double y2)
    {
        double x1T = x1 + metrics.viewport[0];
        double y1T = y1 + metrics.viewport[1];
        double x2T = x2 + metrics.viewport[0];
        double y2T = y2 + metrics.viewport[1];
        IList<GraphicElement> elts = new List<GraphicElement>();
        Stream nodeStream = null;
        Stream edgeStream = null;
        Stream spriteStream = null;
        if (types.Contains(InteractiveElement.NODE))
        {
            nodeStream = graph.Nodes().Filter((n) => IsNodeIn((GraphicNode)n, x1T, y1T, x2T, y2T));
        }
        else
        {
            nodeStream = Stream.Empty();
        }

        if (types.Contains(InteractiveElement.EDGE))
        {
            edgeStream = graph.Edges().Filter((e) => IsEdgeIn((GraphicEdge)e, x1T, y1T, x2T, y2T));
        }
        else
        {
            edgeStream = Stream.Empty();
        }

        if (types.Contains(InteractiveElement.SPRITE))
        {
            spriteStream = graph.Sprites().Filter((e) => IsSpriteIn((GraphicSprite)e, x1T, y1T, x2T, y2T));
        }
        else
        {
            spriteStream = Stream.Empty();
        }

        Stream<GraphicElement> s = Stream.Concat(nodeStream, Stream.Concat(edgeStream, spriteStream));
        return s.Collect(Collectors.ToList());
    }

    public virtual string ToString()
    {
        StringBuilder builder = new StringBuilder(String.Format("Camera :%n"));
        builder.Append(String.Format("    autoFit  = %b%n", autoFit));
        builder.Append(String.Format("    center   = %s%n", center));
        builder.Append(String.Format("    rotation = %f%n", rotation));
        builder.Append(String.Format("    zoom     = %f%n", zoom));
        builder.Append(String.Format("    padding  = %s%n", padding));
        builder.Append(String.Format("    metrics  = %s%n", metrics));
        return builder.ToString();
    }
}
