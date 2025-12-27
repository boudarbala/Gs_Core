using Java.Util.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public interface Structure
{
    int GetNodeCount();
    int GetEdgeCount();
    Stream<Node> Nodes();
    Stream<Edge> Edges();
}
