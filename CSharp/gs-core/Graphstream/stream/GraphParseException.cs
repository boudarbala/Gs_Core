using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public class GraphParseException : Exception
{
    private static readonly long serialVersionUID = 8469350631709220693;
    public GraphParseException() : base("graph parse error")
    {
    }

    public GraphParseException(string message) : base(message)
    {
    }
}
