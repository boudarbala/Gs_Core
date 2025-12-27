using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public class SourceAdapter : Source
{
    public virtual void AddAttributeSink(AttributeSink sink)
    {
    }

    public virtual void AddElementSink(ElementSink sink)
    {
    }

    public virtual void AddSink(Sink sink)
    {
    }

    public virtual void RemoveAttributeSink(AttributeSink sink)
    {
    }

    public virtual void RemoveElementSink(ElementSink sink)
    {
    }

    public virtual void RemoveSink(Sink sink)
    {
    }

    public virtual void ClearAttributeSinks()
    {
    }

    public virtual void ClearElementSinks()
    {
    }

    public virtual void ClearSinks()
    {
    }
}
