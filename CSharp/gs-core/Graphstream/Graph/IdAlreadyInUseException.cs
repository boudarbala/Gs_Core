using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph
{
    public class IdAlreadyInUseException : Exception
    {
        private static readonly long serialVersionUID = -3000770118436738366;
        public IdAlreadyInUseException() : base("singleton exception")
        {
        }

        public IdAlreadyInUseException(string message) : base("singleton exception: " + message)
        {
        }
    }

}


