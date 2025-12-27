using Java.Util;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Graph.Implementations;
using Gs_Core.Graphstream.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.ElementType;

namespace Gs_Core.Graphstream.Stream;

public class Timeline : Source, Replayable, Iterable<Graph>
{
    public static readonly string TIME_PREFIX = "time";
    private class StepDiff
    {
        double step;
        GraphDiff diff;
        StepDiff(double step, GraphDiff diff)
        {
            this.step = step;
            this.diff = diff;
        }
    }

    LinkedList<StepDiff> diffs;
    protected bool changed;
    protected Graph initialGraph, currentGraph;
    protected GraphDiff currentDiff;
    protected Connector connector;
    protected PipeBase pipe;
    protected int seeker;
    public Timeline()
    {
        this.diffs = new LinkedList<StepDiff>();
        this.changed = false;
        this.connector = new Connector();
        this.currentDiff = null;
        this.pipe = new PipeBase();
    }

    public virtual void Reset()
    {
    }

    public virtual void Play(double from, double to)
    {
        Play(from, to, pipe);
    }

    public virtual void Play(double from, double to, Sink sink)
    {
        if (diffs.Count == 0)
            return;
        if (from > to)
        {
            int i = diffs.Count - 1, j;
            while (i > 0 && diffs[i].step > from)
                i--;
            j = i;
            while (j > 0 && diffs[j].step >= to)
                j--;
            for (int k = i; k >= j; k--)
                diffs[k].diff.Reverse(sink);
        }
        else
        {
            int i = 0, j;
            while (i < diffs.Count - 1 && diffs[i].step < from)
                i++;
            j = i;
            while (j < diffs.Count - 1 && diffs[j].step <= to)
                j++;
            for (int k = i; k <= j; k++)
                diffs[k].diff.Apply(sink);
        }
    }

    public virtual void Play()
    {
        Play(initialGraph.GetStep(), currentGraph.GetStep());
    }

    public virtual void Play(Sink sink)
    {
        Play(initialGraph.GetStep(), currentGraph.GetStep(), sink);
    }

    public virtual void Playback()
    {
        Play(currentGraph.GetStep(), initialGraph.GetStep());
    }

    public virtual void Playback(Sink sink)
    {
        Play(currentGraph.GetStep(), initialGraph.GetStep(), sink);
    }

    public virtual void Seek(int i)
    {
        seeker = i;
    }

    public virtual void SeekStart()
    {
        seeker = 0;
    }

    public virtual void SeekEnd()
    {
        seeker = diffs.Count;
    }

    public virtual bool HasNext()
    {
        return seeker < diffs.Count;
    }

    public virtual void Next()
    {
        if (seeker >= diffs.Count)
            return;
        diffs[seeker++].diff.Apply(pipe);
    }

    public virtual bool HasPrevious()
    {
        return seeker > 0;
    }

    public virtual void Previous()
    {
        if (seeker <= 0)
            return;
        diffs[--seeker].diff.Reverse(pipe);
    }

    public virtual void Begin(Source source)
    {
        initialGraph = new AdjacencyListGraph("initial");
        currentGraph = new AdjacencyListGraph("initial");
        Begin();
    }

    public virtual void Begin(Graph source)
    {
        initialGraph = Graphs.Clone(source);
        currentGraph = source;
        Begin();
    }

    protected virtual void Begin()
    {
        currentGraph.AddSink(connector);
        PushDiff();
    }

    public virtual void End()
    {
        if (currentDiff != null)
        {
            currentDiff.End();
            diffs.Add(new StepDiff(currentGraph.GetStep(), currentDiff));
        }

        currentGraph.RemoveSink(connector);
        currentGraph = Graphs.Clone(currentGraph);
    }

    protected virtual void PushDiff()
    {
        if (currentDiff != null)
        {
            currentDiff.End();
            diffs.Add(new StepDiff(currentGraph.GetStep(), currentDiff));
        }

        currentDiff = new GraphDiff();
        currentDiff.Start(currentGraph);
    }

    public virtual IEnumerator<Graph> Iterator()
    {
        return new TimelineIterator();
    }

    public virtual Controller GetReplayController()
    {
        return new TimelineReplayController();
    }

    protected class Connector : SinkAdapter
    {
        public override void StepBegins(string sourceId, long timeId, double step)
        {
            this.PushDiff();
        }
    }

    protected class TimelineReplayController : PipeBase, Controller
    {
        public virtual void Replay()
        {
            Play(this);
        }

        public virtual void Replay(string sourceId)
        {
            string tmp = this.sourceId;
            this.sourceId = sourceId;
            Play(this);
            this.sourceId = tmp;
        }
    }

    protected class TimelineIterator : IEnumerator<Graph>
    {
        Graph current;
        int idx;
        public TimelineIterator()
        {
            current = Graphs.Clone(initialGraph);
            idx = 0;
        }

        public virtual bool HasNext()
        {
            return idx < diffs.Count;
        }

        public virtual Graph Next()
        {
            if (idx >= diffs.Count)
                return null;
            diffs[idx++].diff.Apply(current);
            return Graphs.Clone(current);
        }

        public virtual void Remove()
        {
        }
    }

    public virtual void AddSink(Sink sink)
    {
        pipe.AddSink(sink);
    }

    public virtual void RemoveSink(Sink sink)
    {
        pipe.RemoveSink(sink);
    }

    public virtual void AddAttributeSink(AttributeSink sink)
    {
        pipe.AddAttributeSink(sink);
    }

    public virtual void RemoveAttributeSink(AttributeSink sink)
    {
        pipe.RemoveAttributeSink(sink);
    }

    public virtual void AddElementSink(ElementSink sink)
    {
        pipe.AddElementSink(sink);
    }

    public virtual void RemoveElementSink(ElementSink sink)
    {
        pipe.RemoveElementSink(sink);
    }

    public virtual void ClearElementSinks()
    {
        pipe.ClearElementSinks();
    }

    public virtual void ClearAttributeSinks()
    {
        pipe.ClearAttributeSinks();
    }

    public virtual void ClearSinks()
    {
        pipe.ClearSinks();
    }

    public static void Main(params string[] strings)
    {
        Graph g = new AdjacencyListGraph("g");
        Timeline timeline = new Timeline();
        timeline.AddSink(new VerboseSink());
        timeline.Begin(g);
        g.StepBegins(0);
        g.AddNode("A");
        g.AddNode("B");
        g.StepBegins(1);
        g.AddNode("C");
        timeline.End();
        System.@out.Printf("############\n");
        System.@out.Printf("# Play :\n");
        timeline.Play();
        System.@out.Printf("############\n");
        System.@out.Printf("# Playback :\n");
        timeline.Playback();
        System.@out.Printf("############\n");
        System.@out.Printf("# Sequence :\n");
        int i = 0;
        foreach (Graph it in timeline)
        {
            System.@out.Printf(" Graph#%d %s\n", i, ToString(it));
        }

        System.@out.Printf("############\n");
    }

    private static string ToString(Graph g)
    {
        StringBuilder buffer = new StringBuilder();
        buffer.Append("id=\"").Append(g.GetId()).Append("\" node={");
        g.Nodes().ForEach((n) => buffer.Append("\"").Append(n.GetId()).Append("\", "));
        buffer.Append("}, edges={");
        g.Edges().ForEach((e) =>
        {
            buffer.Append("\"").Append(e.GetId()).Append("\":\"").Append(e.GetSourceNode().GetId()).Append("\"--\"").Append(e.GetTargetNode().GetId()).Append("\", ");
        });
        buffer.Append("}");
        return buffer.ToString();
    }
}
