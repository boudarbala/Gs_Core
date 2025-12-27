using Java.Awt.Image;
using Java.Io;
using Java.Util;
using Java.Util.Logging;
using Java.Util.Regex;
using Javax.Imageio;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.File;
using Gs_Core.Graphstream.Stream.File.Images.Filters;
using Gs_Core.Graphstream.Stream.Thread;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.GraphicGraph;
using Gs_Core.Graphstream.Ui.Layout;
using Gs_Core.Graphstream.Ui.View.Camera;
using Gs_Core.Graphstream.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;
using static Org.Graphstream.Stream.File.What;
using static Org.Graphstream.Stream.File.TimeFormat;
using static Org.Graphstream.Stream.File.OutputType;
using static Org.Graphstream.Stream.File.OutputPolicy;
using static Org.Graphstream.Stream.File.LayoutPolicy;
using static Org.Graphstream.Stream.File.Quality;
using static Org.Graphstream.Stream.File.Option;

namespace Gs_Core.Graphstream.Stream.File;

public abstract class FileSinkImages : FileSink
{
    public static FileSinkImages CreateDefault()
    {
        try
        {
            Display display = Display.GetDefault();
            if (display is FileSinkImagesFactory)
            {
                return ((FileSinkImagesFactory)display).CreateFileSinkImages();
            }
            else
            {
                LOGGER.Warning("Default UI module does not provide a FileSinkImages implementation");
            }
        }
        catch (MissingDisplayException e)
        {
            LOGGER.Warning("No valid UI module specified in \"org.graphstream.ui\" system property");
        }

        return null;
    }

    public enum OutputType
    {
        PNG,
        JPG,
        png,
        jpg
    }

    public enum OutputPolicy
    {
        BY_EVENT,
        BY_ELEMENT_EVENT,
        BY_ATTRIBUTE_EVENT,
        BY_NODE_EVENT,
        BY_EDGE_EVENT,
        BY_GRAPH_EVENT,
        BY_STEP,
        BY_NODE_ADDED_REMOVED,
        BY_EDGE_ADDED_REMOVED,
        BY_NODE_ATTRIBUTE,
        BY_EDGE_ATTRIBUTE,
        BY_GRAPH_ATTRIBUTE,
        BY_LAYOUT_STEP,
        BY_NODE_MOVED,
        ON_RUNNER,
        NONE
    }

    public enum LayoutPolicy
    {
        NO_LAYOUT,
        COMPUTED_IN_LAYOUT_RUNNER,
        COMPUTED_ONCE_AT_NEW_IMAGE,
        COMPUTED_FULLY_AT_NEW_IMAGE
    }

    public enum Quality
    {
        LOW,
        MEDIUM,
        HIGH
    }

    private static readonly Logger LOGGER = Logger.GetLogger(typeof(FileSinkImages).GetName());
    protected Resolution resolution;
    protected OutputType outputType;
    protected string filePrefix;
    protected readonly GraphicGraph gg;
    protected Sink sink;
    protected int counter;
    protected OutputPolicy outputPolicy;
    protected LinkedList<Filter> filters;
    protected LayoutPolicy layoutPolicy;
    protected LayoutRunner optLayout;
    protected ProxyPipe layoutPipeIn;
    protected Layout layout;
    protected float layoutStabilizationLimit = 0.9F;
    protected int layoutStepAfterStabilization = 10;
    protected int layoutStepPerFrame = 4;
    protected int layoutStepWithoutFrame = 0;
    protected long outputRunnerDelay = 10;
    protected bool outputRunnerAlive = false;
    protected OutputRunner outputRunner;
    protected ThreadProxyPipe outputRunnerProxy;
    protected bool clearImageBeforeOutput = false;
    protected bool hasBegun = false;
    protected bool autofit = true;
    protected string styleSheet = null;
    protected FileSinkImages() : this(OutputType.PNG, Resolutions.HD720)
    {
    }

    protected FileSinkImages(OutputType type, Resolution resolution) : this(type, resolution, OutputPolicy.NONE)
    {
    }

    protected FileSinkImages(OutputType type, Resolution resolution, OutputPolicy outputPolicy)
    {
        this.resolution = resolution;
        this.outputType = type;
        this.filePrefix = "frame_";
        this.counter = 0;
        this.gg = new GraphicGraph(String.Format("images-%x", System.CurrentTimeMillis()));
        this.filters = new LinkedList<Filter>();
        this.layoutPolicy = LayoutPolicy.NO_LAYOUT;
        this.layout = null;
        this.optLayout = null;
        this.layoutPipeIn = null;
        this.sink = gg;
        SetOutputPolicy(outputPolicy);
    }

    protected abstract Camera GetCamera();
    protected abstract void Render();
    protected abstract BufferedImage GetRenderedImage();
    protected abstract void InitImage();
    protected abstract void ClearImage(int color);
    public virtual void SetQuality(Quality q)
    {
        switch (q)
        {
            case LOW:
                if (gg.HasAttribute("ui.quality"))
                    gg.RemoveAttribute("ui.quality");
                if (gg.HasAttribute("ui.antialias"))
                    gg.RemoveAttribute("ui.antialias");
                break;
            case MEDIUM:
                if (!gg.HasAttribute("ui.quality"))
                    gg.SetAttribute("ui.quality");
                if (gg.HasAttribute("ui.antialias"))
                    gg.RemoveAttribute("ui.antialias");
                break;
            case HIGH:
                if (!gg.HasAttribute("ui.quality"))
                    gg.SetAttribute("ui.quality");
                if (!gg.HasAttribute("ui.antialias"))
                    gg.SetAttribute("ui.antialias");
                break;
        }
    }

    public virtual void SetStyleSheet(string styleSheet)
    {
        this.styleSheet = styleSheet;
        gg.SetAttribute("ui.stylesheet", styleSheet);
    }

    public virtual void SetResolution(Resolution r)
    {
        if (r != resolution)
        {
            resolution = r;
            InitImage();
        }
    }

    public virtual void SetResolution(int width, int height)
    {
        if (resolution == null || resolution.GetWidth() != width || resolution.GetHeight() != height)
        {
            SetResolution(new CustomResolution(width, height));
        }
    }

    public virtual void SetOutputPolicy(OutputPolicy policy)
    {
        this.outputPolicy = policy;
    }

    public virtual void SetOutputType(OutputType outputType)
    {
        if (outputType != this.outputType)
        {
            this.outputType = outputType;
            InitImage();
        }
    }

    public virtual void SetLayoutPolicy(LayoutPolicy policy)
    {
        lock (this)
        {
            if (policy != layoutPolicy)
            {
                switch (layoutPolicy)
                {
                    case COMPUTED_IN_LAYOUT_RUNNER:
                        optLayout.Release();
                        optLayout = null;
                        layoutPipeIn.RemoveAttributeSink(gg);
                        layoutPipeIn = null;
                        layout = null;
                        break;
                    case COMPUTED_ONCE_AT_NEW_IMAGE:
                        gg.RemoveSink(layout);
                        layout.RemoveAttributeSink(gg);
                        layout = null;
                        break;
                    default:
                        break;
                }

                switch (policy)
                {
                    case COMPUTED_IN_LAYOUT_RUNNER:
                        layout = Layouts.NewLayoutAlgorithm();
                        optLayout = new InnerLayoutRunner();
                        break;
                    case COMPUTED_FULLY_AT_NEW_IMAGE:
                    case COMPUTED_ONCE_AT_NEW_IMAGE:
                        layout = Layouts.NewLayoutAlgorithm();
                        gg.AddSink(layout);
                        layout.AddAttributeSink(gg);
                        break;
                    default:
                        break;
                }

                layoutPolicy = policy;
            }
        }
    }

    public virtual void SetLayoutStepPerFrame(int spf)
    {
        this.layoutStepPerFrame = spf;
    }

    public virtual void SetLayoutStepAfterStabilization(int sas)
    {
        this.layoutStepAfterStabilization = sas;
    }

    public virtual void SetLayoutStabilizationLimit(double limit)
    {
        if (layout == null)
            throw new NullReferenceException("did you enable layout ?");
        layout.SetStabilizationLimit(limit);
    }

    public virtual void AddFilter(Filter filter)
    {
        filters.Add(filter);
    }

    public virtual void RemoveFilter(Filter filter)
    {
        filters.Remove(filter);
    }

    public virtual void SetOutputRunnerEnabled(bool on)
    {
        lock (this)
        {
            if (!on && outputRunnerAlive)
            {
                outputRunnerAlive = false;
                try
                {
                    if (outputRunner != null)
                        outputRunner.Join();
                }
                catch (InterruptedException e)
                {
                }

                outputRunner = null;
                sink = gg;
                if (outputRunnerProxy != null)
                    outputRunnerProxy.Pump();
            }

            outputRunnerAlive = on;
            if (outputRunnerAlive)
            {
                if (outputRunnerProxy == null)
                {
                    outputRunnerProxy = new ThreadProxyPipe();
                    outputRunnerProxy.Init(gg);
                }

                sink = outputRunnerProxy;
                outputRunner = new OutputRunner();
                outputRunner.Start();
            }
        }
    }

    public virtual void SetOutputRunnerDelay(long delay)
    {
        outputRunnerDelay = delay;
    }

    public virtual void StabilizeLayout(double limit)
    {
        if (layout != null)
        {
            while (layout.GetStabilization() < limit)
                layout.Compute();
        }
    }

    public virtual Point3 GetViewCenter()
    {
        return GetCamera().GetViewCenter();
    }

    public virtual void SetViewCenter(double x, double y)
    {
        GetCamera().SetViewCenter(x, y, 0);
    }

    public virtual double GetViewPercent()
    {
        return GetCamera().GetViewPercent();
    }

    public virtual void SetViewPercent(double zoom)
    {
        GetCamera().SetViewPercent(zoom);
    }

    public virtual void SetGraphViewport(double minx, double miny, double maxx, double maxy)
    {
        GetCamera().SetGraphViewport(minx, miny, maxx, maxy);
    }

    public virtual void SetClearImageBeforeOutputEnabled(bool on)
    {
        clearImageBeforeOutput = on;
    }

    public virtual void SetAutofit(bool on)
    {
        autofit = on;
    }

    protected virtual void ClearGG()
    {
        gg.Clear();
        if (styleSheet != null)
            gg.SetAttribute("ui.stylesheet", styleSheet);
        if (layout != null)
            layout.Clear();
    }

    public virtual void OutputNewImage()
    {
        OutputNewImage(String.Format("%s%06d.%s", filePrefix, counter++, outputType.ext));
    }

    public virtual void OutputNewImage(string filename)
    {
        lock (this)
        {
            switch (layoutPolicy)
            {
                case COMPUTED_IN_LAYOUT_RUNNER:
                    layoutPipeIn.Pump();
                    break;
                case COMPUTED_ONCE_AT_NEW_IMAGE:
                    if (layout != null)
                        layout.Compute();
                    break;
                case COMPUTED_FULLY_AT_NEW_IMAGE:
                    StabilizeLayout(layout.GetStabilizationLimit());
                    break;
                default:
                    break;
            }

            if (clearImageBeforeOutput || gg.GetNodeCount() == 0)
            {
                ClearImage(0x00000000);
            }

            if (gg.GetNodeCount() > 0)
            {
                if (autofit)
                {
                    gg.ComputeBounds();
                    Point3 lo = gg.GetMinPos();
                    Point3 hi = gg.GetMaxPos();
                    GetCamera().SetBounds(lo.x, lo.y, lo.z, hi.x, hi.y, hi.z);
                }

                Render();
            }

            BufferedImage image = GetRenderedImage();
            foreach (Filter action in filters)
                action.Apply(image);
            image.Flush();
            try
            {
                WriteImage(image, filename);
                PrintProgress();
            }
            catch (IOException e)
            {
                LOGGER.Log(Level.WARNING, "Failed to write image", e);
            }
        }
    }

    protected virtual void WriteImage(BufferedImage image, string filename)
    {
        File out = new File(filename);
        if (@out.GetParent() != null && !@out.GetParentFile().Exists())
            @out.GetParentFile().Mkdirs();
        ImageIO.Write(image, outputType.Name(), @out);
    }

    protected virtual void PrintProgress()
    {
        LOGGER.Info(String.Format("\u001b[s\u001b[K%d images written\u001b[u", counter));
    }

    public virtual void Begin(OutputStream stream)
    {
        throw new IOException("not implemented");
    }

    public virtual void Begin(Writer writer)
    {
        throw new IOException("not implemented");
    }

    public virtual void Begin(string prefix)
    {
        InitImage();
        this.filePrefix = prefix;
        this.hasBegun = true;
    }

    public virtual void Flush()
    {
    }

    public virtual void End()
    {
        Flush();
        this.hasBegun = false;
    }

    public virtual void WriteAll(Graph g, OutputStream stream)
    {
        throw new IOException("not implemented");
    }

    public virtual void WriteAll(Graph g, Writer writer)
    {
        throw new IOException("not implemented");
    }

    public virtual void WriteAll(Graph g, string filename)
    {
        lock (this)
        {
            ClearGG();
            GraphReplay replay = new GraphReplay(String.Format("file_sink_image-write_all-replay-%x", System.NanoTime()));
            replay.AddSink(gg);
            replay.Replay(g);
            replay.RemoveSink(gg);
            InitImage();
            OutputNewImage(filename);
            ClearGG();
        }
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        sink.EdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_EDGE_EVENT:
            case BY_EDGE_ATTRIBUTE:
            case BY_ATTRIBUTE_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        sink.EdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_EDGE_EVENT:
            case BY_EDGE_ATTRIBUTE:
            case BY_ATTRIBUTE_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        sink.EdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_EDGE_EVENT:
            case BY_EDGE_ATTRIBUTE:
            case BY_ATTRIBUTE_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        sink.GraphAttributeAdded(sourceId, timeId, attribute, value);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_GRAPH_EVENT:
            case BY_GRAPH_ATTRIBUTE:
            case BY_ATTRIBUTE_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        sink.GraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_GRAPH_EVENT:
            case BY_GRAPH_ATTRIBUTE:
            case BY_ATTRIBUTE_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        sink.GraphAttributeRemoved(sourceId, timeId, attribute);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_GRAPH_EVENT:
            case BY_GRAPH_ATTRIBUTE:
            case BY_ATTRIBUTE_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        sink.NodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_NODE_EVENT:
            case BY_NODE_ATTRIBUTE:
            case BY_ATTRIBUTE_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        sink.NodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_NODE_EVENT:
            case BY_NODE_ATTRIBUTE:
            case BY_ATTRIBUTE_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        sink.NodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_NODE_EVENT:
            case BY_NODE_ATTRIBUTE:
            case BY_ATTRIBUTE_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        sink.EdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_EDGE_EVENT:
            case BY_EDGE_ADDED_REMOVED:
            case BY_ELEMENT_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        sink.EdgeRemoved(sourceId, timeId, edgeId);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_EDGE_EVENT:
            case BY_EDGE_ADDED_REMOVED:
            case BY_ELEMENT_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        sink.GraphCleared(sourceId, timeId);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_GRAPH_EVENT:
            case BY_NODE_ADDED_REMOVED:
            case BY_EDGE_ADDED_REMOVED:
            case BY_ELEMENT_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        sink.NodeAdded(sourceId, timeId, nodeId);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_NODE_EVENT:
            case BY_NODE_ADDED_REMOVED:
            case BY_ELEMENT_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        sink.NodeRemoved(sourceId, timeId, nodeId);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_NODE_EVENT:
            case BY_NODE_ADDED_REMOVED:
            case BY_ELEMENT_EVENT:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        sink.StepBegins(sourceId, timeId, step);
        switch (outputPolicy)
        {
            case BY_EVENT:
            case BY_STEP:
                if (hasBegun)
                    OutputNewImage();
                break;
            default:
                break;
        }
    }

    public enum Option
    {
        IMAGE_PREFIX,
        IMAGE_TYPE,
        IMAGE_RESOLUTION,
        OUTPUT_POLICY,
        LOGO,
        STYLESHEET,
        QUALITY
    }

    protected class InnerLayoutRunner : LayoutRunner
    {
        public InnerLayoutRunner() : base(this.gg, this.layout, true, true)
        {
            this.layoutPipeIn = NewLayoutPipe();
            this.layoutPipeIn.AddAttributeSink(this.gg);
        }

        public virtual void Run()
        {
            int stepAfterStabilization = 0;
            do
            {
                pumpPipe.Pump();
                layout.Compute();
                if (layout.GetStabilization() > layout.GetStabilizationLimit())
                    stepAfterStabilization++;
                else
                    stepAfterStabilization = 0;
                Nap(80);
                if (stepAfterStabilization > layoutStepAfterStabilization)
                    loop = false;
            }
            while (loop);
        }
    }

    protected class OutputRunner : Thread
    {
        public OutputRunner()
        {
            SetDaemon(true);
        }

        public virtual void Run()
        {
            while (outputRunnerAlive && outputPolicy == OutputPolicy.ON_RUNNER)
            {
                outputRunnerProxy.Pump();
                if (hasBegun)
                    OutputNewImage();
                try
                {
                    Thread.Sleep(outputRunnerDelay);
                }
                catch (InterruptedException e)
                {
                    outputRunnerAlive = false;
                }
            }
        }
    }

    public static void Usage()
    {
        LOGGER.Info(String.Format("usage: java %s [options] fichier.dgs%n", typeof(FileSinkImages).GetName()));
        LOGGER.Info(String.Format("where options in:%n"));
        foreach (Option option in Option.Values())
        {
            LOGGER.Info(String.Format("%n --%s%s , -%s %s%n%s%n", option.fullopts, option.valuable ? "=..." : "", option.shortopts, option.valuable ? "..." : "", option.description));
        }
    }

    public static void Main(params string[] args)
    {
        HashMap<Option, string> options = new HashMap<Option, string>();
        LinkedList<string> others = new LinkedList<string>();
        foreach (Option option in Option.Values())
            if (option.defaultValue != null)
                options.Put(option, option.defaultValue);
        if (args != null && args.Length > 0)
        {
            Pattern valueGetter = Pattern.Compile("^--\\w[\\w-]*\\w?(?:=(?:\"([^\"]*)\"|([^\\s]*)))$");
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Matches("^--\\w[\\w-]*\\w?(=(\"[^\"]*\"|[^\\s]*))?$"))
                {
                    bool found = false;
                    foreach (Option option in Option.Values())
                    {
                        if (args[i].StartsWith("--" + option.fullopts + "="))
                        {
                            Matcher m = valueGetter.Matcher(args[i]);
                            if (m.Matches())
                            {
                                options.Put(option, m.Group(1) == null ? m.Group(2) : m.Group(1));
                            }

                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        LOGGER.Severe(String.Format("unknown option: %s%n", args[i].Substring(0, args[i].IndexOf('='))));
                        System.Exit(1);
                    }
                }
                else if (args[i].Matches("^-\\w$"))
                {
                    bool found = false;
                    foreach (Option option in Option.Values())
                    {
                        if (args[i].Equals("-" + option.shortopts))
                        {
                            options.Put(option, args[++i]);
                            break;
                        }
                    }

                    if (!found)
                    {
                        LOGGER.Severe(String.Format("unknown option: %s%n", args[i]));
                        System.Exit(1);
                    }
                }
                else
                {
                    others.AddLast(args[i]);
                }
            }
        }
        else
        {
            Usage();
            System.Exit(0);
        }

        LinkedList<string> errors = new LinkedList<string>();
        if (others.Count == 0)
        {
            errors.Add("dgs file name missing.");
        }

        string imagePrefix;
        OutputType outputType = null;
        OutputPolicy outputPolicy = null;
        Resolution resolution = null;
        Quality quality = null;
        string logo;
        string stylesheet;
        imagePrefix = options[Option.IMAGE_PREFIX];
        try
        {
            outputType = OutputType.ValueOf(options[Option.IMAGE_TYPE]);
        }
        catch (ArgumentException e)
        {
            errors.Add("bad image type: " + options[Option.IMAGE_TYPE]);
        }

        try
        {
            outputPolicy = OutputPolicy.ValueOf(options[Option.OUTPUT_POLICY]);
        }
        catch (ArgumentException e)
        {
            errors.Add("bad output policy: " + options[Option.OUTPUT_POLICY]);
        }

        try
        {
            quality = Quality.ValueOf(options[Option.QUALITY]);
        }
        catch (ArgumentException e)
        {
            errors.Add("bad quality: " + options[Option.QUALITY]);
        }

        logo = options[Option.LOGO];
        stylesheet = options[Option.STYLESHEET];
        try
        {
            resolution = Resolutions.ValueOf(options[Option.IMAGE_RESOLUTION]);
        }
        catch (ArgumentException e)
        {
            Pattern p = Pattern.Compile("^\\s*(\\d+)\\s*x\\s*(\\d+)\\s*$");
            Matcher m = p.Matcher(options[Option.IMAGE_RESOLUTION]);
            if (m.Matches())
            {
                resolution = new CustomResolution(Integer.ParseInt(m.Group(1)), Integer.ParseInt(m.Group(2)));
            }
            else
            {
                errors.Add("bad resolution: " + options[Option.IMAGE_RESOLUTION]);
            }
        }

        if (stylesheet != null && stylesheet.Length < 1024)
        {
            File test = new File(stylesheet);
            if (test.Exists())
            {
                FileReader in = new FileReader(test);
                char[] buffer = new char[128];
                string content = "";
                while (@in.Ready())
                {
                    int c = @in.Read(buffer, 0, 128);
                    content += new string (buffer, 0, c);
                }

                stylesheet = content;
                @in.Dispose();
            }
        }

        {
            File test = new File(others.Peek());
            if (!test.Exists())
                errors.Add(String.Format("file \"%s\" does not exist", others.Peek()));
        }

        if (errors.Count > 0)
        {
            LOGGER.Info(String.Format("error:%n"));
            foreach (string error in errors)
                LOGGER.Info(String.Format("- %s%n", error));
            System.Exit(1);
        }

        FileSourceDGS dgs = new FileSourceDGS();
        FileSinkImages fsi = FileSinkImages.CreateDefault();
        fsi.SetOutputPolicy(outputPolicy);
        fsi.SetResolution(resolution);
        fsi.SetOutputType(outputType);
        dgs.AddSink(fsi);
        if (logo != null)
            fsi.AddFilter(new AddLogoFilter(logo, 0, 0));
        fsi.SetQuality(quality);
        if (stylesheet != null)
            fsi.SetStyleSheet(stylesheet);
        bool next = true;
        dgs.Begin(others[0]);
        while (next)
            next = dgs.NextStep();
        dgs.End();
    }
}
