using Gs_Core.Graphstream.Stream;
using Java.Nio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.Binary.ElementType;
using static Org.Graphstream.Stream.Binary.EventType;
using static Org.Graphstream.Stream.Binary.AttributeChangeEvent;

namespace Gs_Core.Graphstream.Stream.Binary;

public interface ByteDecoder : Source
{
    void Decode(ByteBuffer buffer);
    bool Validate(ByteBuffer buffer);
}
