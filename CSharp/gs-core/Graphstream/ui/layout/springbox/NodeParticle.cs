using Java.Io;
using Java.Util;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Miv.Pherd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.Layout.Springbox.ElementType;
using static Org.Graphstream.Ui.Layout.Springbox.EventType;
using static Org.Graphstream.Ui.Layout.Springbox.AttributeChangeEvent;
using static Org.Graphstream.Ui.Layout.Springbox.Mode;
using static Org.Graphstream.Ui.Layout.Springbox.What;
using static Org.Graphstream.Ui.Layout.Springbox.TimeFormat;
using static Org.Graphstream.Ui.Layout.Springbox.OutputType;
using static Org.Graphstream.Ui.Layout.Springbox.OutputPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.LayoutPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Quality;
using static Org.Graphstream.Ui.Layout.Springbox.Option;
using static Org.Graphstream.Ui.Layout.Springbox.AttributeType;
using static Org.Graphstream.Ui.Layout.Springbox.Balise;
using static Org.Graphstream.Ui.Layout.Springbox.GEXFAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.METAAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.GRAPHAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NODESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NODEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.ATTVALUEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.PARENTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EDGESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.SPELLAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.COLORAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.POSITIONAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.SIZEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NODESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EDGEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.THICKNESSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.IDType;
using static Org.Graphstream.Ui.Layout.Springbox.ModeType;
using static Org.Graphstream.Ui.Layout.Springbox.WeightType;
using static Org.Graphstream.Ui.Layout.Springbox.EdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.NodeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.EdgeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.ClassType;
using static Org.Graphstream.Ui.Layout.Springbox.TimeFormatType;
using static Org.Graphstream.Ui.Layout.Springbox.GPXAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.WPTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.LINKAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EMAILAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.PTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.BOUNDSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.FixType;
using static Org.Graphstream.Ui.Layout.Springbox.GraphAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.LocatorAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NodeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.DataAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.PortAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EndPointAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EndPointType;
using static Org.Graphstream.Ui.Layout.Springbox.HyperEdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.KeyAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.KeyDomain;
using static Org.Graphstream.Ui.Layout.Springbox.KeyAttrType;
using static Org.Graphstream.Ui.Layout.Springbox.GraphEvents;
using static Org.Graphstream.Ui.Layout.Springbox.ThreadingModel;
using static Org.Graphstream.Ui.Layout.Springbox.CloseFramePolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Measures;
using static Org.Graphstream.Ui.Layout.Springbox.Token;
using static Org.Graphstream.Ui.Layout.Springbox.Extension;
using static Org.Graphstream.Ui.Layout.Springbox.DefaultEdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.AttrType;
using static Org.Graphstream.Ui.Layout.Springbox.Resolutions;
using static Org.Graphstream.Ui.Layout.Springbox.PropertyType;
using static Org.Graphstream.Ui.Layout.Springbox.Type;
using static Org.Graphstream.Ui.Layout.Springbox.Units;
using static Org.Graphstream.Ui.Layout.Springbox.FillMode;
using static Org.Graphstream.Ui.Layout.Springbox.StrokeMode;
using static Org.Graphstream.Ui.Layout.Springbox.ShadowMode;
using static Org.Graphstream.Ui.Layout.Springbox.VisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextVisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextStyle;
using static Org.Graphstream.Ui.Layout.Springbox.IconMode;
using static Org.Graphstream.Ui.Layout.Springbox.SizeMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextAlignment;
using static Org.Graphstream.Ui.Layout.Springbox.TextBackgroundMode;
using static Org.Graphstream.Ui.Layout.Springbox.ShapeKind;
using static Org.Graphstream.Ui.Layout.Springbox.Shape;
using static Org.Graphstream.Ui.Layout.Springbox.SpriteOrientation;
using static Org.Graphstream.Ui.Layout.Springbox.ArrowShape;
using static Org.Graphstream.Ui.Layout.Springbox.JComponents;

namespace Gs_Core.Graphstream.Ui.Layout.Springbox;

public abstract class NodeParticle : Particle
{
    public List<EdgeSpring> neighbours = new List<EdgeSpring>();
    public bool frozen = false;
    public Vector3 disp;
    public double len;
    public double attE;
    public double repE;
    public PrintStream out;
    protected BarnesHutLayout box;
    public NodeParticle(BarnesHutLayout box, string id) : this(box, id, box.RandomXInsideBounds(), box.RandomYInsideBounds(), box.is3D ? box.RandomZInsideBounds() : 0)
    {
        this.box = box;
    }

    public NodeParticle(BarnesHutLayout box, string id, double x, double y, double z) : base(id, x, y, box.is3D ? z : 0)
    {
        this.box = box;
        disp = new Vector3();
        CreateDebug();
    }

    protected virtual void CreateDebug()
    {
        if (box.outputNodeStats)
        {
            try
            {
                @out = new PrintStream(new FileOutputStream("out" + GetId() + ".data"));
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
                System.Exit(1);
            }
        }
    }

    public virtual Collection<EdgeSpring> GetEdges()
    {
        return neighbours;
    }

    public override void Move(int time)
    {
        if (!frozen)
        {
            disp.Fill(0);
            Vector3 delta = new Vector3();
            repE = 0;
            attE = 0;
            if (box.viewZone < 0)
                RepulsionN2(delta);
            else
                RepulsionNLogN(delta);
            Attraction(delta);
            if (box.gravity != 0)
                Gravity(delta);
            disp.ScalarMult(box.force);
            len = disp.Length;
            if (len > (box.area / 2))
            {
                disp.ScalarMult((box.area / 2) / len);
                len = box.area / 2;
            }

            box.avgLength += len;
            if (len > box.maxMoveLength)
                box.maxMoveLength = len;
        }
    }

    public override void NextStep(int time)
    {
        if (!frozen)
        {
            nextPos.x = pos.x + disp.data[0];
            nextPos.y = pos.y + disp.data[1];
            if (box.is3D)
                nextPos.z = pos.z + disp.data[2];
            box.nodeMoveCount++;
            moved = true;
        }
        else
        {
            nextPos.x = pos.x;
            nextPos.y = pos.y;
            if (box.is3D)
                nextPos.z = pos.z;
        }

        if (@out != null)
        {
            @out.Printf(Locale.US, "%s %f %f %f%n", GetId(), len, attE, repE);
            @out.Flush();
        }

        base.NextStep(time);
    }

    public virtual void MoveOf(double dx, double dy, double dz)
    {
        pos.Set(pos.x + dx, pos.y + dy, pos.z + dz);
    }

    public virtual void MoveTo(double x, double y, double z)
    {
        pos.Set(x, y, z);
        moved = true;
    }

    protected abstract void RepulsionN2(Vector3 delta);
    protected abstract void RepulsionNLogN(Vector3 delta);
    protected abstract void Attraction(Vector3 delta);
    protected abstract void Gravity(Vector3 delta);
    public virtual void RegisterEdge(EdgeSpring e)
    {
        neighbours.Add(e);
    }

    public virtual void UnregisterEdge(EdgeSpring e)
    {
        int i = neighbours.IndexOf(e);
        if (i >= 0)
        {
            neighbours.Remove(i);
        }
    }

    public virtual void RemoveNeighborEdges()
    {
        List<EdgeSpring> edges = new List<EdgeSpring>(neighbours);
        foreach (EdgeSpring edge in edges)
            box.RemoveEdge(box.GetLayoutAlgorithmName(), edge.id);
        neighbours.Clear();
    }

    public override void Inserted()
    {
    }

    public override void Removed()
    {
    }
}
