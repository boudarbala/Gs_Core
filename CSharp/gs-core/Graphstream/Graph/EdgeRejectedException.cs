using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public class EdgeRejectedException : Exception
{
    private static readonly long serialVersionUID = 4952910935083960955;
    public EdgeRejectedException() : base("Edge rejected")
    {
    }

    public EdgeRejectedException(string message) : base(message)
    {
    }
}
