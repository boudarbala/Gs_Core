using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.Sync;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.GraphicGraph;
using Gs_Core.Graphstream.Ui.Layout;
using Gs_Core.Miv.Pherd;
using Gs_Core.Miv.Pherd.Ntree;
using Java.Io;
using Java.Util;
using Java.Util.Logging;
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

public abstract class BarnesHutLayout : SourceBase, Layout, ParticleBoxListener
{
    private static readonly Logger logger = Logger.GetLogger(typeof(BarnesHutLayout).GetName());
    protected ParticleBox nodes;
    protected HashMap<string, EdgeSpring> edges = new HashMap<string, EdgeSpring>();
    protected int lastElementCount = 0;
    protected Random random;
    protected Point3 lo = new Point3(0, 0, 0);
    protected Point3 hi = new Point3(1, 1, 1);
    protected Point3 center = new Point3(0.5, 0.5, 0.5);
    protected PrintStream statsOut;
    protected Energies energies = new Energies();
    protected double force = 1F;
    protected double viewZone = 5F;
    protected double theta = 0.7F;
    protected double quality = 1;
    protected int nodesPerCell = 10;
    protected double area = 1;
    protected double stabilizationLimit = 0.9;
    protected int time;
    protected long lastStepTime;
    protected double maxMoveLength;
    protected double avgLength;
    protected int nodeMoveCount;
    protected bool is3D = false;
    protected double gravity = 0;
    protected bool sendNodeInfos = false;
    protected bool outputStats = false;
    protected bool outputNodeStats = false;
    protected int sendMoveEventsEvery = 1;
    protected SinkTime sinkTime;
    public BarnesHutLayout() : this(false)
    {
    }

    public BarnesHutLayout(bool is3D) : this(is3D, new Random(System.CurrentTimeMillis()))
    {
    }

    public BarnesHutLayout(bool is3D, Random randomNumberGenerator)
    {
        CellSpace space;
        this.is3D = is3D;
        this.random = randomNumberGenerator;
        if (is3D)
        {
            space = new OctreeCellSpace(new Anchor(-1, -1, -1), new Anchor(1, 1, 1));
        }
        else
        {
            space = new QuadtreeCellSpace(new Anchor(-1, -1, -0.01F), new Anchor(1, 1, 0.01F));
        }

        this.nodes = new ParticleBox(nodesPerCell, space, new GraphCellData());
        nodes.AddParticleBoxListener(this);
        SetQuality(quality);
        sinkTime = new SinkTime();
        sourceTime.SetSinkTime(sinkTime);
    }

    public virtual Point3 GetLowPoint()
    {
        org.miv.pherd.geom.Point3 p = nodes.GetNTree().GetLowestPoint();
        lo = new Point3(p.x, p.y, p.z);
        return lo;
    }

    public virtual Point3 GetHiPoint()
    {
        org.miv.pherd.geom.Point3 p = nodes.GetNTree().GetHighestPoint();
        hi = new Point3(p.x, p.y, p.z);
        return hi;
    }

    public virtual double RandomXInsideBounds()
    {
        org.miv.pherd.geom.Point3 c = ((GraphCellData)nodes.GetNTree().GetRootCell().GetData()).center;
        return c.x + (random.NextDouble() * 2 - 1);
    }

    public virtual double RandomYInsideBounds()
    {
        org.miv.pherd.geom.Point3 c = ((GraphCellData)nodes.GetNTree().GetRootCell().GetData()).center;
        return c.y + (random.NextDouble() * 2 - 1);
    }

    public virtual double RandomZInsideBounds()
    {
        org.miv.pherd.geom.Point3 c = ((GraphCellData)nodes.GetNTree().GetRootCell().GetData()).center;
        return c.z + (random.NextDouble() * 2 - 1);
    }

    public virtual Point3 GetCenterPoint()
    {
        return center;
    }

    public virtual double GetGravityFactor()
    {
        return gravity;
    }

    public virtual void SetGravityFactor(double value)
    {
        gravity = value;
    }

    public virtual ParticleBox GetSpatialIndex()
    {
        return nodes;
    }

    public virtual long GetLastStepTime()
    {
        return lastStepTime;
    }

    public abstract string GetLayoutAlgorithmName();
    public virtual int GetNodeMovedCount()
    {
        return nodeMoveCount;
    }

    public virtual double GetStabilization()
    {
        if (lastElementCount == nodes.GetParticleCount() + edges.Count)
        {
            if (time > energies.GetBufferSize())
                return energies.GetStabilization();
        }

        lastElementCount = nodes.GetParticleCount() + edges.Count;
        return 0;
    }

    public virtual double GetStabilizationLimit()
    {
        return stabilizationLimit;
    }

    public virtual int GetSteps()
    {
        return time;
    }

    public virtual double GetQuality()
    {
        return quality;
    }

    public virtual bool Is3D()
    {
        return is3D;
    }

    public virtual double GetForce()
    {
        return force;
    }

    public virtual Random GetRandom()
    {
        return random;
    }

    public virtual Energies GetEnergies()
    {
        return energies;
    }

    public virtual double GetBarnesHutTheta()
    {
        return theta;
    }

    public virtual double GetViewZone()
    {
        return viewZone;
    }

    public virtual void SetSendNodeInfos(bool on)
    {
        sendNodeInfos = on;
    }

    public virtual void SetBarnesHutTheta(double theta)
    {
        if (theta > 0 && theta < 1)
        {
            this.theta = theta;
        }
    }

    public virtual void SetForce(double value)
    {
        this.force = value;
    }

    public virtual void SetStabilizationLimit(double value)
    {
        this.stabilizationLimit = value;
    }

    public virtual void SetQuality(double qualityLevel)
    {
        if (qualityLevel > 1)
            qualityLevel = 1;
        else if (qualityLevel < 0)
            qualityLevel = 0;
        quality = qualityLevel;
    }

    public virtual void Clear()
    {
        energies.ClearEnergies();
        nodes.RemoveAllParticles();
        edges.Clear();
        nodeMoveCount = 0;
        lastStepTime = 0;
    }

    public virtual void Compute()
    {
        long t1;
        ComputeArea();
        maxMoveLength = Double.MIN_VALUE;
        t1 = System.CurrentTimeMillis();
        nodeMoveCount = 0;
        avgLength = 0;
        nodes.Step();
        if (nodeMoveCount > 0)
            avgLength /= nodeMoveCount;
        GetLowPoint();
        GetHiPoint();
        center = new Point3(lo.x + (hi.x - lo.x) / 2, lo.y + (hi.y - lo.y) / 2, lo.z + (hi.z - lo.z) / 2);
        energies.StoreEnergy();
        PrintStats();
        time++;
        lastStepTime = System.CurrentTimeMillis() - t1;
    }

    protected virtual void PrintStats()
    {
        if (outputStats)
        {
            if (statsOut == null)
            {
                try
                {
                    statsOut = new PrintStream("springBox.dat");
                    statsOut.Printf("# stabilization nodeMoveCount energy energyDiff maxMoveLength avgLength area%n");
                    statsOut.Flush();
                }
                catch (FileNotFoundException e)
                {
                    e.PrintStackTrace();
                }
            }

            if (statsOut != null)
            {
                double energyDiff = energies.GetEnergy() - energies.GetPreviousEnergyValue(30);
                statsOut.Printf(Locale.US, "%f %d %f %f %f %f%n", GetStabilization(), nodeMoveCount, energies.GetEnergy(), energyDiff, maxMoveLength, avgLength, area);
                statsOut.Flush();
            }
        }
    }

    protected virtual void ComputeArea()
    {
        area = GetHiPoint().Distance(GetLowPoint());
    }

    public virtual void Shake()
    {
        energies.ClearEnergies();
    }

    protected virtual NodeParticle AddNode(string sourceId, string id)
    {
        NodeParticle np = NewNodeParticle(id);
        nodes.AddParticle(np);
        return np;
    }

    public virtual void MoveNode(string id, double x, double y, double z)
    {
        NodeParticle node = (NodeParticle)nodes.GetParticle(id);
        if (node != null)
        {
            node.MoveTo(x, y, z);
            energies.ClearEnergies();
        }
    }

    public virtual void FreezeNode(string id, bool on)
    {
        NodeParticle node = (NodeParticle)nodes.GetParticle(id);
        if (node != null)
        {
            node.frozen = on;
        }
    }

    protected virtual void SetNodeWeight(string id, double weight)
    {
        NodeParticle node = (NodeParticle)nodes.GetParticle(id);
        if (node != null)
            node.SetWeight(weight);
    }

    protected virtual void RemoveNode(string sourceId, string id)
    {
        NodeParticle node = (NodeParticle)nodes.RemoveParticle(id);
        if (node != null)
        {
            node.RemoveNeighborEdges();
        }
        else
        {
            logger.Warning(String.Format("layout %s: cannot remove non existing node %s%n", GetLayoutAlgorithmName(), id));
        }
    }

    protected virtual void AddEdge(string sourceId, string id, string from, string to, bool directed)
    {
        NodeParticle n0 = (NodeParticle)nodes.GetParticle(from);
        NodeParticle n1 = (NodeParticle)nodes.GetParticle(to);
        if (n0 != null && n1 != null)
        {
            EdgeSpring e = new EdgeSpring(id, n0, n1);
            EdgeSpring o = edges.Put(id, e);
            if (o != null)
            {
                logger.Warning(String.Format("layout %s: edge '%s' already exists.", GetLayoutAlgorithmName(), id));
            }
            else
            {
                n0.RegisterEdge(e);
                n1.RegisterEdge(e);
            }

            ChooseNodePosition(n0, n1);
        }
        else
        {
            if (n0 == null)
                logger.Warning(String.Format("layout %s: node '%s' does not exist, cannot create edge %s.", GetLayoutAlgorithmName(), from, id));
            if (n1 == null)
                logger.Warning(String.Format("layout %s: node '%s' does not exist, cannot create edge %s.", GetLayoutAlgorithmName(), to, id));
        }
    }

    protected abstract void ChooseNodePosition(NodeParticle n0, NodeParticle n1);
    protected virtual void AddEdgeBreakPoint(string edgeId, int points)
    {
        logger.Warning(String.Format("layout %s: edge break points are not handled yet.", GetLayoutAlgorithmName()));
    }

    protected virtual void IgnoreEdge(string edgeId, bool on)
    {
        EdgeSpring edge = edges[edgeId];
        if (edge != null)
        {
            edge.ignored = on;
        }
    }

    protected virtual void SetEdgeWeight(string id, double weight)
    {
        EdgeSpring edge = edges[id];
        if (edge != null)
            edge.weight = weight;
    }

    protected virtual void RemoveEdge(string sourceId, string id)
    {
        EdgeSpring e = edges.Remove(id);
        if (e != null)
        {
            e.node0.UnregisterEdge(e);
            e.node1.UnregisterEdge(e);
        }
        else
        {
            logger.Warning(String.Format("layout %s: cannot remove non existing edge %s%n", GetLayoutAlgorithmName(), id));
        }
    }

    public virtual void ParticleAdded(object id, double x, double y, double z, object mark)
    {
    }

    public virtual void ParticleAdded(object id, double x, double y, double z)
    {
    }

    public virtual void ParticleMarked(object id, object mark)
    {
    }

    public virtual void ParticleMoved(object id, double x, double y, double z)
    {
        if ((time % sendMoveEventsEvery) == 0)
        {
            object[] xyz = new object[3];
            xyz[0] = x;
            xyz[1] = y;
            xyz[2] = z;
            SendNodeAttributeChanged(sourceId, (string)id, "xyz", xyz, xyz);
        }
    }

    public virtual void ParticleRemoved(object id)
    {
    }

    public virtual void StepFinished(int time)
    {
    }

    public virtual void ParticleAttributeChanged(object id, string attribute, object newValue, bool removed)
    {
    }

    public virtual void EdgeAdded(string graphId, long time, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            AddEdge(graphId, edgeId, fromNodeId, toNodeId, directed);
            SendEdgeAdded(graphId, time, edgeId, fromNodeId, toNodeId, directed);
        }
    }

    public virtual void NodeAdded(string graphId, long time, string nodeId)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            NodeParticle np = AddNode(graphId, nodeId);
            SendNodeAdded(graphId, time, nodeId);
        }
    }

    public virtual void EdgeRemoved(string graphId, long time, string edgeId)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            RemoveEdge(graphId, edgeId);
            SendEdgeRemoved(graphId, time, edgeId);
        }
    }

    public virtual void NodeRemoved(string graphId, long time, string nodeId)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            RemoveNode(graphId, nodeId);
            SendNodeRemoved(graphId, time, nodeId);
        }
    }

    public virtual void GraphCleared(string graphId, long time)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            Clear();
            SendGraphCleared(graphId, time);
        }
    }

    public virtual void StepBegins(string graphId, long time, double step)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            SendStepBegins(graphId, time, step);
        }
    }

    public virtual void GraphAttributeAdded(string graphId, long time, string attribute, object value)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            GraphAttributeChanged_(graphId, attribute, null, value);
            SendGraphAttributeAdded(graphId, time, attribute, value);
        }
    }

    public virtual void GraphAttributeChanged(string graphId, long time, string attribute, object oldValue, object newValue)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            GraphAttributeChanged_(graphId, attribute, oldValue, newValue);
            SendGraphAttributeChanged(graphId, time, attribute, oldValue, newValue);
        }
    }

    protected virtual void GraphAttributeChanged_(string graphId, string attribute, object oldValue, object newValue)
    {
        if (attribute.Equals("layout.force"))
        {
            if (newValue is Number)
                SetForce(((Number)newValue).DoubleValue());
            energies.ClearEnergies();
        }
        else if (attribute.Equals("layout.quality"))
        {
            if (newValue is Number)
            {
                int q = ((Number)newValue).IntValue();
                q = q > 4 ? 4 : q;
                q = q < 0 ? 0 : q;
                SetQuality(q);
                logger.Fine(String.Format("layout.%s.quality: %d.", GetLayoutAlgorithmName(), q));
            }

            energies.ClearEnergies();
        }
        else if (attribute.Equals("layout.gravity"))
        {
            if (newValue is Number)
            {
                double value = ((Number)newValue).DoubleValue();
                SetGravityFactor(value);
                logger.Fine(String.Format("layout.%s.gravity: %f.", GetLayoutAlgorithmName(), value));
            }
        }
        else if (attribute.Equals("layout.exact-zone"))
        {
            if (newValue is Number)
            {
                double factor = ((Number)newValue).DoubleValue();
                factor = factor > 1 ? 1 : factor;
                factor = factor < 0 ? 0 : factor;
                viewZone = factor;
                logger.Fine(String.Format("layout.%s.exact-zone: %f of [0..1]%n", GetLayoutAlgorithmName(), viewZone));
                energies.ClearEnergies();
            }
        }
        else if (attribute.Equals("layout.output-stats"))
        {
            if (newValue == null)
                outputStats = false;
            else
                outputStats = true;
            logger.Fine(String.Format("layout.%s.output-stats: %b%n", GetLayoutAlgorithmName(), outputStats));
        }
        else if (attribute.Equals("layout.stabilization-limit"))
        {
            if (newValue is Number)
            {
                stabilizationLimit = ((Number)newValue).DoubleValue();
                if (stabilizationLimit > 1)
                    stabilizationLimit = 1;
                else if (stabilizationLimit < 0)
                    stabilizationLimit = 0;
                energies.ClearEnergies();
            }
        }
    }

    public virtual void GraphAttributeRemoved(string graphId, long time, string attribute)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            SendGraphAttributeRemoved(graphId, time, attribute);
        }
    }

    public virtual void NodeAttributeAdded(string graphId, long time, string nodeId, string attribute, object value)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            NodeAttributeChanged_(graphId, nodeId, attribute, null, value);
            SendNodeAttributeAdded(graphId, time, nodeId, attribute, value);
        }
    }

    public virtual void NodeAttributeChanged(string graphId, long time, string nodeId, string attribute, object oldValue, object newValue)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            NodeAttributeChanged_(graphId, nodeId, attribute, oldValue, newValue);
            SendNodeAttributeChanged(graphId, time, nodeId, attribute, oldValue, newValue);
        }
    }

    protected virtual void NodeAttributeChanged_(string graphId, string nodeId, string attribute, object oldValue, object newValue)
    {
        if (attribute.Equals("layout.weight"))
        {
            if (newValue is Number)
                SetNodeWeight(nodeId, ((Number)newValue).DoubleValue());
            else if (newValue == null)
                SetNodeWeight(nodeId, 1);
            energies.ClearEnergies();
        }
        else if (attribute.Equals("layout.frozen"))
        {
            FreezeNode(nodeId, (newValue != null));
        }
        else if (attribute.Equals("xyz") || attribute.Equals("xy"))
        {
            double[] xyz = new double[3];
            GraphPosLengthUtils.PositionFromObject(newValue, xyz);
            MoveNode(nodeId, xyz[0], xyz[1], xyz[2]);
        }
        else if (attribute.Equals("x") && newValue is Number)
        {
            NodeParticle node = (NodeParticle)nodes.GetParticle(nodeId);
            if (node != null)
            {
                MoveNode(nodeId, ((Number)newValue).DoubleValue(), node.GetPosition().y, node.GetPosition().z);
            }
        }
        else if (attribute.Equals("y") && newValue is Number)
        {
            NodeParticle node = (NodeParticle)nodes.GetParticle(nodeId);
            if (node != null)
            {
                MoveNode(nodeId, node.GetPosition().x, ((Number)newValue).DoubleValue(), node.GetPosition().z);
            }
        }
    }

    public virtual void NodeAttributeRemoved(string graphId, long time, string nodeId, string attribute)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            NodeAttributeChanged_(graphId, nodeId, attribute, null, null);
            SendNodeAttributeRemoved(graphId, time, nodeId, attribute);
        }
    }

    public virtual void EdgeAttributeAdded(string graphId, long time, string edgeId, string attribute, object value)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            EdgeAttributeChanged_(graphId, edgeId, attribute, null, value);
            SendEdgeAttributeAdded(graphId, time, edgeId, attribute, value);
        }
    }

    public virtual void EdgeAttributeChanged(string graphId, long time, string edgeId, string attribute, object oldValue, object newValue)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            EdgeAttributeChanged_(graphId, edgeId, attribute, oldValue, newValue);
            SendEdgeAttributeChanged(graphId, time, edgeId, attribute, oldValue, newValue);
        }
    }

    protected virtual void EdgeAttributeChanged_(string graphId, string edgeId, string attribute, object oldValue, object newValue)
    {
        if (attribute.Equals("layout.weight"))
        {
            if (newValue is Number)
                SetEdgeWeight(edgeId, ((Number)newValue).DoubleValue());
            else if (newValue == null)
                SetEdgeWeight(edgeId, 1);
            energies.ClearEnergies();
        }
        else if (attribute.Equals("layout.ignored"))
        {
            if (newValue is bool)
                IgnoreEdge(edgeId, (bool)newValue);
            energies.ClearEnergies();
        }
    }

    public virtual void EdgeAttributeRemoved(string graphId, long time, string edgeId, string attribute)
    {
        if (sinkTime.IsNewEvent(graphId, time))
        {
            EdgeAttributeChanged_(graphId, edgeId, attribute, null, null);
            SendEdgeAttributeRemoved(attribute, time, edgeId, attribute);
        }
    }

    public abstract NodeParticle NewNodeParticle(string id);
}
