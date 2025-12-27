using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public interface Edge : Element
{
    bool IsDirected();
    bool IsLoop();
    Node GetNode0();
    Node GetNode1();
    Node GetSourceNode();
    Node GetTargetNode();
    Node GetOpposite(Node node);
}
