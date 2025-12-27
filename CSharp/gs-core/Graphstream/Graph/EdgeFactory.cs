using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph
{

    public interface EdgeFactory<T>
    {
        T NewInstance(string id, Node src, Node dst, bool directed);
    }


}

