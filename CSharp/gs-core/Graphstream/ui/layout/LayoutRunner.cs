using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.Thread;
using Java.Util.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.Layout.ElementType;
using static Org.Graphstream.Ui.Layout.EventType;
using static Org.Graphstream.Ui.Layout.AttributeChangeEvent;
using static Org.Graphstream.Ui.Layout.Mode;
using static Org.Graphstream.Ui.Layout.What;
using static Org.Graphstream.Ui.Layout.TimeFormat;
using static Org.Graphstream.Ui.Layout.OutputType;
using static Org.Graphstream.Ui.Layout.OutputPolicy;
using static Org.Graphstream.Ui.Layout.LayoutPolicy;
using static Org.Graphstream.Ui.Layout.Quality;
using static Org.Graphstream.Ui.Layout.Option;
using static Org.Graphstream.Ui.Layout.AttributeType;
using static Org.Graphstream.Ui.Layout.Balise;
using static Org.Graphstream.Ui.Layout.GEXFAttribute;
using static Org.Graphstream.Ui.Layout.METAAttribute;
using static Org.Graphstream.Ui.Layout.GRAPHAttribute;
using static Org.Graphstream.Ui.Layout.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.Layout.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.Layout.NODESAttribute;
using static Org.Graphstream.Ui.Layout.NODEAttribute;
using static Org.Graphstream.Ui.Layout.ATTVALUEAttribute;
using static Org.Graphstream.Ui.Layout.PARENTAttribute;
using static Org.Graphstream.Ui.Layout.EDGESAttribute;
using static Org.Graphstream.Ui.Layout.SPELLAttribute;
using static Org.Graphstream.Ui.Layout.COLORAttribute;
using static Org.Graphstream.Ui.Layout.POSITIONAttribute;
using static Org.Graphstream.Ui.Layout.SIZEAttribute;
using static Org.Graphstream.Ui.Layout.NODESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.EDGEAttribute;
using static Org.Graphstream.Ui.Layout.THICKNESSAttribute;
using static Org.Graphstream.Ui.Layout.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.IDType;
using static Org.Graphstream.Ui.Layout.ModeType;
using static Org.Graphstream.Ui.Layout.WeightType;
using static Org.Graphstream.Ui.Layout.EdgeType;
using static Org.Graphstream.Ui.Layout.NodeShapeType;
using static Org.Graphstream.Ui.Layout.EdgeShapeType;
using static Org.Graphstream.Ui.Layout.ClassType;
using static Org.Graphstream.Ui.Layout.TimeFormatType;
using static Org.Graphstream.Ui.Layout.GPXAttribute;
using static Org.Graphstream.Ui.Layout.WPTAttribute;
using static Org.Graphstream.Ui.Layout.LINKAttribute;
using static Org.Graphstream.Ui.Layout.EMAILAttribute;
using static Org.Graphstream.Ui.Layout.PTAttribute;
using static Org.Graphstream.Ui.Layout.BOUNDSAttribute;
using static Org.Graphstream.Ui.Layout.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.Layout.FixType;
using static Org.Graphstream.Ui.Layout.GraphAttribute;
using static Org.Graphstream.Ui.Layout.LocatorAttribute;
using static Org.Graphstream.Ui.Layout.NodeAttribute;
using static Org.Graphstream.Ui.Layout.EdgeAttribute;
using static Org.Graphstream.Ui.Layout.DataAttribute;
using static Org.Graphstream.Ui.Layout.PortAttribute;
using static Org.Graphstream.Ui.Layout.EndPointAttribute;
using static Org.Graphstream.Ui.Layout.EndPointType;
using static Org.Graphstream.Ui.Layout.HyperEdgeAttribute;
using static Org.Graphstream.Ui.Layout.KeyAttribute;
using static Org.Graphstream.Ui.Layout.KeyDomain;
using static Org.Graphstream.Ui.Layout.KeyAttrType;
using static Org.Graphstream.Ui.Layout.GraphEvents;

namespace Gs_Core.Graphstream.Ui.Layout;

public class LayoutRunner : Thread
{
    private static readonly Logger logger = Logger.GetLogger(typeof(LayoutRunner).GetSimpleName());
    protected Layout layout = null;
    protected ThreadProxyPipe pumpPipe = null;
    protected bool loop = true;
    protected long longNap = 80;
    protected long shortNap = 10;
    public LayoutRunner(Source source, Layout layout) : this(source, layout, true)
    {
    }

    public LayoutRunner(Source source, Layout layout, bool start)
    {
        this.layout = layout;
        this.pumpPipe = new ThreadProxyPipe();
        this.pumpPipe.AddSink(layout);
        if (start)
            Start();
        this.pumpPipe.Init(source);
    }

    public LayoutRunner(Graph graph, Layout layout, bool start, bool replay)
    {
        this.layout = layout;
        this.pumpPipe = new ThreadProxyPipe();
        this.pumpPipe.AddSink(layout);
        if (start)
            Start();
        this.pumpPipe.Init(graph, replay);
    }

    public virtual ProxyPipe NewLayoutPipe()
    {
        ThreadProxyPipe tpp = new ThreadProxyPipe();
        tpp.Init(layout);
        return tpp;
    }

    public override void Run()
    {
        string layoutName = layout.GetLayoutAlgorithmName();
        while (loop)
        {
            double limit = layout.GetStabilizationLimit();
            pumpPipe.Pump();
            if (limit > 0)
            {
                if (layout.GetStabilization() > limit)
                {
                    Nap(longNap);
                }
                else
                {
                    layout.Compute();
                    Nap(shortNap);
                }
            }
            else
            {
                layout.Compute();
                Nap(shortNap);
            }
        }

        logger.Info(String.Format("Layout '%s' process stopped.", layoutName));
    }

    public virtual void Release()
    {
        pumpPipe.UnregisterFromSource();
        pumpPipe.RemoveSink(layout);
        pumpPipe = null;
        loop = false;
        if (Thread.CurrentThread() != this)
        {
            try
            {
                this.Join();
            }
            catch (Exception e)
            {
                logger.Log(Level.WARNING, "Unable to stop/release layout.", e);
            }
        }

        layout = null;
    }

    protected virtual void Nap(long ms)
    {
        try
        {
            Thread.Sleep(ms);
        }
        catch (Exception e)
        {
        }
    }

    public virtual void SetNaps(long longNap, long shortNap)
    {
        this.longNap = longNap;
        this.shortNap = shortNap;
    }
}
