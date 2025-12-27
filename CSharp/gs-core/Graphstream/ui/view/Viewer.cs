using Java.Util;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.Thread;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.GraphicGraph;
using Gs_Core.Graphstream.Ui.Layout;
using Gs_Core.Graphstream.Ui.View.Camera;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.View.ElementType;
using static Org.Graphstream.Ui.View.EventType;
using static Org.Graphstream.Ui.View.AttributeChangeEvent;
using static Org.Graphstream.Ui.View.Mode;
using static Org.Graphstream.Ui.View.What;
using static Org.Graphstream.Ui.View.TimeFormat;
using static Org.Graphstream.Ui.View.OutputType;
using static Org.Graphstream.Ui.View.OutputPolicy;
using static Org.Graphstream.Ui.View.LayoutPolicy;
using static Org.Graphstream.Ui.View.Quality;
using static Org.Graphstream.Ui.View.Option;
using static Org.Graphstream.Ui.View.AttributeType;
using static Org.Graphstream.Ui.View.Balise;
using static Org.Graphstream.Ui.View.GEXFAttribute;
using static Org.Graphstream.Ui.View.METAAttribute;
using static Org.Graphstream.Ui.View.GRAPHAttribute;
using static Org.Graphstream.Ui.View.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.View.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.View.NODESAttribute;
using static Org.Graphstream.Ui.View.NODEAttribute;
using static Org.Graphstream.Ui.View.ATTVALUEAttribute;
using static Org.Graphstream.Ui.View.PARENTAttribute;
using static Org.Graphstream.Ui.View.EDGESAttribute;
using static Org.Graphstream.Ui.View.SPELLAttribute;
using static Org.Graphstream.Ui.View.COLORAttribute;
using static Org.Graphstream.Ui.View.POSITIONAttribute;
using static Org.Graphstream.Ui.View.SIZEAttribute;
using static Org.Graphstream.Ui.View.NODESHAPEAttribute;
using static Org.Graphstream.Ui.View.EDGEAttribute;
using static Org.Graphstream.Ui.View.THICKNESSAttribute;
using static Org.Graphstream.Ui.View.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.View.IDType;
using static Org.Graphstream.Ui.View.ModeType;
using static Org.Graphstream.Ui.View.WeightType;
using static Org.Graphstream.Ui.View.EdgeType;
using static Org.Graphstream.Ui.View.NodeShapeType;
using static Org.Graphstream.Ui.View.EdgeShapeType;
using static Org.Graphstream.Ui.View.ClassType;
using static Org.Graphstream.Ui.View.TimeFormatType;
using static Org.Graphstream.Ui.View.GPXAttribute;
using static Org.Graphstream.Ui.View.WPTAttribute;
using static Org.Graphstream.Ui.View.LINKAttribute;
using static Org.Graphstream.Ui.View.EMAILAttribute;
using static Org.Graphstream.Ui.View.PTAttribute;
using static Org.Graphstream.Ui.View.BOUNDSAttribute;
using static Org.Graphstream.Ui.View.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.View.FixType;
using static Org.Graphstream.Ui.View.GraphAttribute;
using static Org.Graphstream.Ui.View.LocatorAttribute;
using static Org.Graphstream.Ui.View.NodeAttribute;
using static Org.Graphstream.Ui.View.EdgeAttribute;
using static Org.Graphstream.Ui.View.DataAttribute;
using static Org.Graphstream.Ui.View.PortAttribute;
using static Org.Graphstream.Ui.View.EndPointAttribute;
using static Org.Graphstream.Ui.View.EndPointType;
using static Org.Graphstream.Ui.View.HyperEdgeAttribute;
using static Org.Graphstream.Ui.View.KeyAttribute;
using static Org.Graphstream.Ui.View.KeyDomain;
using static Org.Graphstream.Ui.View.KeyAttrType;
using static Org.Graphstream.Ui.View.GraphEvents;
using static Org.Graphstream.Ui.View.ThreadingModel;
using static Org.Graphstream.Ui.View.CloseFramePolicy;

namespace Gs_Core.Graphstream.Ui.View;

public abstract class Viewer
{
    public enum ThreadingModel
    {
        GRAPH_IN_GUI_THREAD,
        GRAPH_IN_ANOTHER_THREAD,
        GRAPH_ON_NETWORK
    }

    public abstract string GetDefaultID();
    protected bool graphInAnotherThread = true;
    protected GraphicGraph graph;
    protected ProxyPipe pumpPipe;
    protected Source sourceInSameThread;
    protected readonly Dictionary<string, View> views = new TreeMap<string, View>();
    protected CloseFramePolicy closeFramePolicy = CloseFramePolicy.EXIT;
    protected LayoutRunner optLayout = null;
    protected ProxyPipe layoutPipeIn = null;
    public enum CloseFramePolicy
    {
        CLOSE_VIEWER,
        HIDE_ONLY,
        EXIT
    }

    public virtual string NewGGId()
    {
        return String.Format("GraphicGraph_%d", (int)(Math.Random() * 10000));
    }

    public abstract void Init(GraphicGraph graph, ProxyPipe ppipe, Source source);
    public abstract void Dispose();
    public virtual CloseFramePolicy GetCloseFramePolicy()
    {
        return closeFramePolicy;
    }

    public virtual ProxyPipe NewThreadProxyOnGraphicGraph()
    {
        ThreadProxyPipe tpp = new ThreadProxyPipe();
        tpp.Init(graph);
        return tpp;
    }

    public virtual ViewerPipe NewViewerPipe()
    {
        ThreadProxyPipe tpp = new ThreadProxyPipe();
        tpp.Init(graph, false);
        EnableXYZfeedback(true);
        return new ViewerPipe(String.Format("viewer_%d", (int)(Math.Random() * 10000)), tpp);
    }

    public virtual GraphicGraph GetGraphicGraph()
    {
        return graph;
    }

    public virtual View GetView(string id)
    {
        lock (views)
        {
            return views[id];
        }
    }

    public virtual View GetDefaultView()
    {
        return GetView(GetDefaultID());
    }

    public abstract GraphRenderer<?, ?> NewDefaultGraphRenderer();
    public virtual View AddDefaultView(bool openInAFrame)
    {
        lock (views)
        {
            GraphRenderer<?, ?> renderer = NewDefaultGraphRenderer();
            View view = renderer.CreateDefaultView(this, GetDefaultID());
            AddView(view);
            if (openInAFrame)
                view.OpenInAFrame(true);
            return view;
        }
    }

    public virtual View AddView(View view)
    {
        lock (views)
        {
            View old = views.Put(view.GetIdView(), view);
            if (old != null && old != view)
                old.Dispose(graph);
            return old;
        }
    }

    public virtual View AddView(string id, GraphRenderer<?, ?> renderer)
    {
        return AddView(id, renderer, true);
    }

    public virtual View AddView(string id, GraphRenderer<?, ?> renderer, bool openInAFrame)
    {
        lock (views)
        {
            View view = renderer.CreateDefaultView(this, id);
            AddView(view);
            if (openInAFrame)
                view.OpenInAFrame(true);
            return view;
        }
    }

    public virtual void RemoveView(string id)
    {
        lock (views)
        {
            views.Remove(id);
        }
    }

    public virtual void ComputeGraphMetrics()
    {
        graph.ComputeBounds();
        lock (views)
        {
            Point3 lo = graph.GetMinPos();
            Point3 hi = graph.GetMaxPos();
            foreach (View view in views.Values())
            {
                Camera camera = view.GetCamera();
                if (camera != null)
                {
                    camera.SetBounds(lo.x, lo.y, lo.z, hi.x, hi.y, hi.z);
                }
            }
        }
    }

    public virtual void SetCloseFramePolicy(CloseFramePolicy policy)
    {
        lock (views)
        {
            closeFramePolicy = policy;
        }
    }

    public virtual void EnableXYZfeedback(bool on)
    {
        lock (views)
        {
            graph.FeedbackXYZ(on);
        }
    }

    public virtual void EnableAutoLayout()
    {
        EnableAutoLayout(Layouts.NewLayoutAlgorithm());
    }

    public virtual void EnableAutoLayout(Layout layoutAlgorithm)
    {
        lock (views)
        {
            if (optLayout == null)
            {
                optLayout = new LayoutRunner(graph, layoutAlgorithm, true, false);
                graph.Replay();
                layoutPipeIn = optLayout.NewLayoutPipe();
                layoutPipeIn.AddAttributeSink(graph);
            }
        }
    }

    public virtual void DisableAutoLayout()
    {
        lock (views)
        {
            if (optLayout != null)
            {
                ((ThreadProxyPipe)layoutPipeIn).UnregisterFromSource();
                layoutPipeIn.RemoveSink(graph);
                layoutPipeIn = null;
                optLayout.Release();
                optLayout = null;
            }
        }
    }

    public virtual void ReplayGraph(Graph graph)
    {
        graph.AttributeKeys().ForEach((key) =>
        {
            this.graph.SetAttribute(key, graph.GetAttribute(key));
        });
        graph.Nodes().ForEach((node) =>
        {
            Node n = this.graph.AddNode(node.GetId());
            node.AttributeKeys().ForEach((key) =>
            {
                n.SetAttribute(key, node.GetAttribute(key));
            });
        });
        graph.Edges().ForEach((edge) =>
        {
            Edge e = this.graph.AddEdge(edge.GetId(), edge.GetSourceNode().GetId(), edge.GetTargetNode().GetId(), edge.IsDirected());
            edge.AttributeKeys().ForEach((key) =>
            {
                e.SetAttribute(key, edge.GetAttribute(key));
            });
        });
    }
}
