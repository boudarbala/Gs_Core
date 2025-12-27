using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public interface ElementSink
{
    void NodeAdded(string sourceId, long timeId, string nodeId);
    void NodeRemoved(string sourceId, long timeId, string nodeId);
    void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed);
    void EdgeRemoved(string sourceId, long timeId, string edgeId);
    void GraphCleared(string sourceId, long timeId);
    void StepBegins(string sourceId, long timeId, double step);
}
