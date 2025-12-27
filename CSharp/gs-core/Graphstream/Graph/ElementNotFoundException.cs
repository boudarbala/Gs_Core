using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph
{

    public class ElementNotFoundException : Exception
    {
        private static readonly long serialVersionUID = 5089958436773409615;
        public ElementNotFoundException() : base("not found")
        {
        }

        public ElementNotFoundException(string message, params object[] args) : base(String.Format(message, args))
        {
        }
    }
}


