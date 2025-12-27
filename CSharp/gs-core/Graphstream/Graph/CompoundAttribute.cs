using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public interface CompoundAttribute
{
    HashMap<?, ?> ToHashMap();
    string GetKey();
}
