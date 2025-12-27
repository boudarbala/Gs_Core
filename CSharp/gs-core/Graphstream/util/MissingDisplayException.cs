using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.ElementType;

namespace Gs_Core.Graphstream.Util;

public class MissingDisplayException : Exception
{
    public MissingDisplayException(string message) : base(message)
    {
    }
}
