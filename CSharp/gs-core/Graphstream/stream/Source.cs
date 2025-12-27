using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public interface Source
{
    void AddSink(Sink sink);
    void RemoveSink(Sink sink);
    void AddAttributeSink(AttributeSink sink);
    void RemoveAttributeSink(AttributeSink sink);
    void AddElementSink(ElementSink sink);
    void RemoveElementSink(ElementSink sink);
    void ClearElementSinks();
    void ClearAttributeSinks();
    void ClearSinks();
}
