using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public interface NodeFactory<T>
{
    T NewInstance(string id, Graph graph);
}
