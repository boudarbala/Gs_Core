using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.Binary.ElementType;
using static Org.Graphstream.Stream.Binary.EventType;
using static Org.Graphstream.Stream.Binary.AttributeChangeEvent;

namespace Gs_Core.Graphstream.Stream.Binary;

public interface ByteFactory
{
    ByteEncoder CreateByteEncoder();
    ByteDecoder CreateByteDecoder();
}
