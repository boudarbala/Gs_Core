using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public interface AttributePredicate
{
    bool Matches(string attributeName, object attributeValue);
}
