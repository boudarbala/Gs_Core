using Java.Io;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.File;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.ElementType;

namespace Gs_Core.Graphstream.Util;

public class StepCounter : SinkAdapter
{
    public static int CountStepInFile(string path)
    {
        StepCounter counter = new StepCounter();
        FileSource source = FileSourceFactory.SourceFor(path);
        source.AddElementSink(counter);
        source.ReadAll(path);
        return counter.GetStepCount();
    }

    protected int step;
    public StepCounter()
    {
        Reset();
    }

    public virtual void Reset()
    {
        step = 0;
    }

    public virtual int GetStepCount()
    {
        return step;
    }

    public virtual void StepBegins(string sourceId, long timeId, double time)
    {
        step++;
    }
}
