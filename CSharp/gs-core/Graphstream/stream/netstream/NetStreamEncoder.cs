using Gs_Core.Graphstream.Stream.Binary;
using Gs_Core.Graphstream.Stream.Netstream;
using Java.Nio;
using Java.Util;
using Java.Util.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.Netstream.ElementType;
using static Org.Graphstream.Stream.Netstream.EventType;
using static Org.Graphstream.Stream.Netstream.AttributeChangeEvent;
using static Org.Graphstream.Stream.Netstream.Mode;
using static Org.Graphstream.Stream.Netstream.What;
using static Org.Graphstream.Stream.Netstream.TimeFormat;
using static Org.Graphstream.Stream.Netstream.OutputType;
using static Org.Graphstream.Stream.Netstream.OutputPolicy;
using static Org.Graphstream.Stream.Netstream.LayoutPolicy;
using static Org.Graphstream.Stream.Netstream.Quality;
using static Org.Graphstream.Stream.Netstream.Option;
using static Org.Graphstream.Stream.Netstream.AttributeType;
using static Org.Graphstream.Stream.Netstream.Balise;
using static Org.Graphstream.Stream.Netstream.GEXFAttribute;
using static Org.Graphstream.Stream.Netstream.METAAttribute;
using static Org.Graphstream.Stream.Netstream.GRAPHAttribute;
using static Org.Graphstream.Stream.Netstream.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.Netstream.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.Netstream.NODESAttribute;
using static Org.Graphstream.Stream.Netstream.NODEAttribute;
using static Org.Graphstream.Stream.Netstream.ATTVALUEAttribute;
using static Org.Graphstream.Stream.Netstream.PARENTAttribute;
using static Org.Graphstream.Stream.Netstream.EDGESAttribute;
using static Org.Graphstream.Stream.Netstream.SPELLAttribute;
using static Org.Graphstream.Stream.Netstream.COLORAttribute;
using static Org.Graphstream.Stream.Netstream.POSITIONAttribute;
using static Org.Graphstream.Stream.Netstream.SIZEAttribute;
using static Org.Graphstream.Stream.Netstream.NODESHAPEAttribute;
using static Org.Graphstream.Stream.Netstream.EDGEAttribute;
using static Org.Graphstream.Stream.Netstream.THICKNESSAttribute;
using static Org.Graphstream.Stream.Netstream.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.Netstream.IDType;
using static Org.Graphstream.Stream.Netstream.ModeType;
using static Org.Graphstream.Stream.Netstream.WeightType;
using static Org.Graphstream.Stream.Netstream.EdgeType;
using static Org.Graphstream.Stream.Netstream.NodeShapeType;
using static Org.Graphstream.Stream.Netstream.EdgeShapeType;
using static Org.Graphstream.Stream.Netstream.ClassType;
using static Org.Graphstream.Stream.Netstream.TimeFormatType;
using static Org.Graphstream.Stream.Netstream.GPXAttribute;
using static Org.Graphstream.Stream.Netstream.WPTAttribute;
using static Org.Graphstream.Stream.Netstream.LINKAttribute;
using static Org.Graphstream.Stream.Netstream.EMAILAttribute;
using static Org.Graphstream.Stream.Netstream.PTAttribute;
using static Org.Graphstream.Stream.Netstream.BOUNDSAttribute;
using static Org.Graphstream.Stream.Netstream.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.Netstream.FixType;
using static Org.Graphstream.Stream.Netstream.GraphAttribute;
using static Org.Graphstream.Stream.Netstream.LocatorAttribute;
using static Org.Graphstream.Stream.Netstream.NodeAttribute;
using static Org.Graphstream.Stream.Netstream.EdgeAttribute;
using static Org.Graphstream.Stream.Netstream.DataAttribute;
using static Org.Graphstream.Stream.Netstream.PortAttribute;
using static Org.Graphstream.Stream.Netstream.EndPointAttribute;
using static Org.Graphstream.Stream.Netstream.EndPointType;
using static Org.Graphstream.Stream.Netstream.HyperEdgeAttribute;
using static Org.Graphstream.Stream.Netstream.KeyAttribute;
using static Org.Graphstream.Stream.Netstream.KeyDomain;
using static Org.Graphstream.Stream.Netstream.KeyAttrType;

namespace Gs_Core.Graphstream.Stream.Netstream;

public class NetStreamEncoder : ByteEncoder
{
    private static readonly Logger LOGGER = Logger.GetLogger(typeof(NetStreamEncoder).GetName());
    protected readonly IList<Transport> transportList;
    protected string sourceId;
    protected ByteBuffer sourceIdBuff;
    protected ByteBuffer streamBuffer;
    public NetStreamEncoder(params Transport[] transports) : this("default", transports)
    {
    }

    public NetStreamEncoder(string stream, params Transport[] transports)
    {
        streamBuffer = EncodeString(stream);
        transportList = new LinkedList();
        if (transports != null)
        {
            foreach (Transport transport in transports)
                transportList.Add(transport);
        }
    }

    public virtual void AddTransport(Transport transport)
    {
        transportList.Add(transport);
    }

    public virtual void RemoveTransport(Transport transport)
    {
        transportList.Remove(transport);
    }

    protected virtual ByteBuffer GetEncodedValue(object @in, int valueType)
    {
        ByteBuffer value = EncodeValue(@in, valueType);
        if (value == null)
        {
            LOGGER.Warning(String.Format("unknown value type %d\n", valueType));
        }

        return value;
    }

    protected virtual void DoSend(ByteBuffer @event)
    {
        foreach (Transport transport in transportList)
        {
            @event.Rewind();
            transport.Send(@event);
        }
    }

    protected virtual ByteBuffer GetAndPrepareBuffer(string sourceId, long timeId, int eventType, int messageSize)
    {
        if (!sourceId.Equals(this.sourceId))
        {
            this.sourceId = sourceId;
            sourceIdBuff = EncodeString(sourceId);
        }

        streamBuffer.Rewind();
        sourceIdBuff.Rewind();
        int size = 4 + +streamBuffer.Capacity() + 1 + sourceIdBuff.Capacity() + GetVarintSize(timeId) + messageSize;
        ByteBuffer bb = ByteBuffer.Allocate(size);
        bb.PutInt(size).Put(streamBuffer).Put((byte)eventType).Put(sourceIdBuff).Put(EncodeUnsignedVarint(timeId));
        return bb;
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        ByteBuffer attrBuff = EncodeString(attribute);
        int valueType = GetType(value);
        ByteBuffer valueBuff = GetEncodedValue(value, valueType);
        int innerSize = attrBuff.Capacity() + 1 + valueBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_ADD_GRAPH_ATTR, innerSize);
        buff.Put(attrBuff).Put((byte)valueType).Put(valueBuff);
        DoSend(buff);
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        ByteBuffer attrBuff = EncodeString(attribute);
        int oldValueType = GetType(oldValue);
        int newValueType = GetType(newValue);
        ByteBuffer oldValueBuff = GetEncodedValue(oldValue, oldValueType);
        ByteBuffer newValueBuff = GetEncodedValue(newValue, newValueType);
        int innerSize = attrBuff.Capacity() + 1 + oldValueBuff.Capacity() + 1 + newValueBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_CHG_GRAPH_ATTR, innerSize);
        buff.Put(attrBuff).Put((byte)oldValueType).Put(oldValueBuff).Put((byte)newValueType).Put(newValueBuff);
        DoSend(buff);
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        ByteBuffer attrBuff = EncodeString(attribute);
        int innerSize = attrBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_DEL_GRAPH_ATTR, innerSize);
        buff.Put(attrBuff);
        DoSend(buff);
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        ByteBuffer nodeBuff = EncodeString(nodeId);
        ByteBuffer attrBuff = EncodeString(attribute);
        int valueType = GetType(value);
        ByteBuffer valueBuff = GetEncodedValue(value, valueType);
        int innerSize = nodeBuff.Capacity() + attrBuff.Capacity() + 1 + valueBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_ADD_NODE_ATTR, innerSize);
        buff.Put(nodeBuff).Put(attrBuff).Put((byte)valueType).Put(valueBuff);
        DoSend(buff);
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        ByteBuffer nodeBuff = EncodeString(nodeId);
        ByteBuffer attrBuff = EncodeString(attribute);
        int oldValueType = GetType(oldValue);
        int newValueType = GetType(newValue);
        ByteBuffer oldValueBuff = GetEncodedValue(oldValue, oldValueType);
        ByteBuffer newValueBuff = GetEncodedValue(newValue, newValueType);
        int innerSize = nodeBuff.Capacity() + attrBuff.Capacity() + 1 + oldValueBuff.Capacity() + 1 + newValueBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_CHG_NODE_ATTR, innerSize);
        buff.Put(nodeBuff).Put(attrBuff).Put((byte)oldValueType).Put(oldValueBuff).Put((byte)newValueType).Put(newValueBuff);
        DoSend(buff);
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        ByteBuffer nodeBuff = EncodeString(nodeId);
        ByteBuffer attrBuff = EncodeString(attribute);
        int innerSize = nodeBuff.Capacity() + attrBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_DEL_NODE_ATTR, innerSize);
        buff.Put(nodeBuff).Put(attrBuff);
        DoSend(buff);
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        ByteBuffer edgeBuff = EncodeString(edgeId);
        ByteBuffer attrBuff = EncodeString(attribute);
        int valueType = GetType(value);
        ByteBuffer valueBuff = GetEncodedValue(value, valueType);
        int innerSize = edgeBuff.Capacity() + attrBuff.Capacity() + 1 + valueBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_ADD_EDGE_ATTR, innerSize);
        buff.Put(edgeBuff).Put(attrBuff).Put((byte)valueType).Put(valueBuff);
        DoSend(buff);
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        ByteBuffer edgeBuff = EncodeString(edgeId);
        ByteBuffer attrBuff = EncodeString(attribute);
        int oldValueType = GetType(oldValue);
        int newValueType = GetType(newValue);
        ByteBuffer oldValueBuff = GetEncodedValue(oldValue, oldValueType);
        ByteBuffer newValueBuff = GetEncodedValue(newValue, newValueType);
        int innerSize = edgeBuff.Capacity() + attrBuff.Capacity() + 1 + oldValueBuff.Capacity() + 1 + newValueBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_CHG_EDGE_ATTR, innerSize);
        buff.Put(edgeBuff).Put(attrBuff).Put((byte)oldValueType).Put(oldValueBuff).Put((byte)newValueType).Put(newValueBuff);
        DoSend(buff);
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        ByteBuffer edgeBuff = EncodeString(edgeId);
        ByteBuffer attrBuff = EncodeString(attribute);
        int innerSize = edgeBuff.Capacity() + attrBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_DEL_EDGE_ATTR, innerSize);
        buff.Put(edgeBuff).Put(attrBuff);
        DoSend(buff);
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        ByteBuffer nodeBuff = EncodeString(nodeId);
        int innerSize = nodeBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_ADD_NODE, innerSize);
        buff.Put(nodeBuff);
        DoSend(buff);
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        ByteBuffer nodeBuff = EncodeString(nodeId);
        int innerSize = nodeBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_DEL_NODE, innerSize);
        buff.Put(nodeBuff);
        DoSend(buff);
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        ByteBuffer edgeBuff = EncodeString(edgeId);
        ByteBuffer fromNodeBuff = EncodeString(fromNodeId);
        ByteBuffer toNodeBuff = EncodeString(toNodeId);
        int innerSize = edgeBuff.Capacity() + fromNodeBuff.Capacity() + toNodeBuff.Capacity() + 1;
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_ADD_EDGE, innerSize);
        buff.Put(edgeBuff).Put(fromNodeBuff).Put(toNodeBuff).Put((byte)(!directed ? 0 : 1));
        DoSend(buff);
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        ByteBuffer edgeBuff = EncodeString(edgeId);
        int innerSize = edgeBuff.Capacity();
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_DEL_EDGE, innerSize);
        buff.Put(edgeBuff);
        DoSend(buff);
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_CLEARED, 0);
        DoSend(buff);
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        ByteBuffer buff = GetAndPrepareBuffer(sourceId, timeId, NetStreamConstants.EVENT_STEP, 8);
        buff.PutDouble(step);
        DoSend(buff);
    }
}
