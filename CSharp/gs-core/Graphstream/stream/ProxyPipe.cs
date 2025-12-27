using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public interface ProxyPipe : Pipe
{
    void Pump();
    void BlockingPump();
    void BlockingPump(long timeout);
}
