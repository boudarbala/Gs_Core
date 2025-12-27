using Java.Util;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.Layout.Springbox;
using Gs_Core.Miv.Pherd;
using Gs_Core.Miv.Pherd.Ntree;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ElementType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EventType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.AttributeChangeEvent;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Mode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.What;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TimeFormat;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.OutputType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.OutputPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.LayoutPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Quality;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Option;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.AttributeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Balise;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GEXFAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.METAAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GRAPHAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NODESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NODEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ATTVALUEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.PARENTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EDGESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.SPELLAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.COLORAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.POSITIONAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.SIZEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NODESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EDGEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.THICKNESSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.IDType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ModeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.WeightType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NodeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EdgeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ClassType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TimeFormatType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GPXAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.WPTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.LINKAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EMAILAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.PTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.BOUNDSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.FixType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GraphAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.LocatorAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NodeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.DataAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.PortAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EndPointAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EndPointType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.HyperEdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.KeyAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.KeyDomain;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.KeyAttrType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GraphEvents;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ThreadingModel;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.CloseFramePolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Measures;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Token;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Extension;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.DefaultEdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.AttrType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Resolutions;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.PropertyType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Type;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Units;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.FillMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.StrokeMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ShadowMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.VisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextVisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextStyle;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.IconMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.SizeMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextAlignment;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextBackgroundMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ShapeKind;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Shape;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.SpriteOrientation;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ArrowShape;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.JComponents;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.InteractiveElement;

namespace Gs_Core.Graphstream.Ui.Layout.Springbox.Implementations;

public class LinLogNodeParticle : NodeParticle
{
    public LinLogNodeParticle(LinLog box, string id) : this(box, id, (box.GetRandom().NextDouble() * 2 * box.k) - box.k, (box.GetRandom().NextDouble() * 2 * box.k) - box.k, box.Is3D() ? (box.GetRandom().NextDouble() * 2 * box.k) - box.k : 0)
    {
        this.box = box;
    }

    public LinLogNodeParticle(LinLog box, string id, double x, double y, double z) : base(box, id, x, y, z)
    {
    }

    protected override void RepulsionN2(Vector3 delta)
    {
        LinLog box = (LinLog)this.box;
        bool is3D = box.Is3D();
        ParticleBox nodes = box.GetSpatialIndex();
        Energies energies = box.GetEnergies();
        IEnumerator<object> i = nodes.GetParticleIdIterator();
        int deg = neighbours.Count;
        while (i.HasNext())
        {
            LinLogNodeParticle node = (LinLogNodeParticle)nodes.GetParticle(i.Next());
            if (node != this)
            {
                delta.Set(node.pos.x - pos.x, node.pos.y - pos.y, is3D ? node.pos.z - pos.z : 0);
                double len = delta.Length;
                if (len > 0)
                {
                    double degFactor = box.edgeBased ? deg * node.neighbours.Count : 1;
                    double factor = 1;
                    double r = box.r;
                    factor = -degFactor * (Math.Pow(len, r - 2)) * node.weight * weight * box.rFactor;
                    if (factor < -box.maxR)
                    {
                        factor = -box.maxR;
                    }

                    energies.AccumulateEnergy(factor);
                    delta.ScalarMult(factor);
                    disp.Add(delta);
                    repE += factor;
                }
            }
        }
    }

    protected override void RepulsionNLogN(Vector3 delta)
    {
        RecurseRepulsion(box.GetSpatialIndex().GetNTree().GetRootCell(), delta);
    }

    protected virtual void RecurseRepulsion(Cell cell, Vector3 delta)
    {
        LinLog box = (LinLog)this.box;
        bool is3D = box.Is3D();
        Energies energies = box.GetEnergies();
        int deg = neighbours.Count;
        if (Intersection(cell))
        {
            if (cell.IsLeaf())
            {
                IEnumerator<TWildcardTodoParticle> i = cell.GetParticles();
                while (i.HasNext())
                {
                    LinLogNodeParticle node = (LinLogNodeParticle)i.Next();
                    if (node != this)
                    {
                        delta.Set(node.pos.x - pos.x, node.pos.y - pos.y, is3D ? node.pos.z - pos.z : 0);
                        double len = delta.Length;
                        if (len > 0)
                        {
                            double degFactor = box.edgeBased ? deg * node.neighbours.Count : 1;
                            double factor = 1;
                            double r = box.r;
                            factor = -degFactor * (Math.Pow(len, r - 2)) * node.weight * weight * box.rFactor;
                            if (factor < -box.maxR)
                            {
                                factor = -box.maxR;
                            }

                            energies.AccumulateEnergy(factor);
                            delta.ScalarMult(factor);
                            disp.Add(delta);
                            repE += factor;
                        }
                    }
                }
            }
            else
            {
                int div = cell.GetSpace().GetDivisions();
                for (int i = 0; i < div; i++)
                    RecurseRepulsion(cell.GetSub(i), delta);
            }
        }
        else
        {
            if (cell != this.cell)
            {
                GraphCellData bary = (GraphCellData)cell.GetData();
                double dist = bary.DistanceFrom(pos);
                double size = cell.GetSpace().GetSize();
                if ((!cell.IsLeaf()) && ((size / dist) > box.GetBarnesHutTheta()))
                {
                    int div = cell.GetSpace().GetDivisions();
                    for (int i = 0; i < div; i++)
                        RecurseRepulsion(cell.GetSub(i), delta);
                }
                else
                {
                    if (bary.weight != 0)
                    {
                        delta.Set(bary.center.x - pos.x, bary.center.y - pos.y, is3D ? bary.center.z - pos.z : 0);
                        double len = delta.Length;
                        if (len > 0)
                        {
                            double degFactor = box.edgeBased ? deg * bary.degree : 1;
                            double factor = 1;
                            double r = box.r;
                            factor = -degFactor * (Math.Pow(len, r - 2)) * bary.weight * weight * box.rFactor;
                            if (factor < -box.maxR)
                            {
                                factor = -box.maxR;
                            }

                            energies.AccumulateEnergy(factor);
                            delta.ScalarMult(factor);
                            disp.Add(delta);
                            repE += factor;
                        }
                    }
                }
            }
        }
    }

    protected override void Attraction(Vector3 delta)
    {
        LinLog box = (LinLog)this.box;
        bool is3D = box.Is3D();
        Energies energies = box.GetEnergies();
        foreach (EdgeSpring edge in neighbours)
        {
            if (!edge.ignored)
            {
                LinLogNodeParticle other = (LinLogNodeParticle)edge.GetOpposite(this);
                delta.Set(other.pos.x - pos.x, other.pos.y - pos.y, is3D ? other.pos.z - pos.z : 0);
                double len = delta.Length;
                if (len > 0)
                {
                    double factor = 1;
                    double a = box.a;
                    factor = (Math.Pow(len, a - 2)) * edge.weight * box.aFactor;
                    energies.AccumulateEnergy(factor);
                    delta.ScalarMult(factor);
                    disp.Add(delta);
                    attE += factor;
                }
            }
        }
    }

    protected override void Gravity(Vector3 delta)
    {
    }

    protected virtual bool Intersection(Cell cell)
    {
        LinLog box = (LinLog)this.box;
        double k = box.k;
        double vz = box.GetViewZone();
        double x1 = cell.GetSpace().GetLoAnchor().x;
        double y1 = cell.GetSpace().GetLoAnchor().y;
        double z1 = cell.GetSpace().GetLoAnchor().z;
        double x2 = cell.GetSpace().GetHiAnchor().x;
        double y2 = cell.GetSpace().GetHiAnchor().y;
        double z2 = cell.GetSpace().GetHiAnchor().z;
        double X1 = pos.x - (k * vz);
        double Y1 = pos.y - (k * vz);
        double Z1 = pos.z - (k * vz);
        double X2 = pos.x + (k * vz);
        double Y2 = pos.y + (k * vz);
        double Z2 = pos.z + (k * vz);
        if (X2 < x1 || X1 > x2)
            return false;
        if (Y2 < y1 || Y1 > y2)
            return false;
        if (Z2 < z1 || Z1 > z2)
            return false;
        return true;
    }
}
