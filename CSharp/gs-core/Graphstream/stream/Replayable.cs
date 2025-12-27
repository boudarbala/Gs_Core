using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public interface Replayable
{
    Controller GetReplayController();
    public class Controller : Source
    {
        virtual void Replay();
        virtual void Replay(string sourceId);
    }

    public static void Replay(Replayable source, Sink sink)
    {
        Controller controller = source.GetReplayController();
        controller.AddSink(sink);
        controller.Replay();
        controller.RemoveSink(sink);
    }

    public static void TryReplay(Source source, Sink sink)
    {
        if (source is Replayable)
            Replay((Replayable)source, sink);
    }
}
