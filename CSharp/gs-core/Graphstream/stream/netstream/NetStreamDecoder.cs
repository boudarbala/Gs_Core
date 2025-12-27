using Gs_Core.Graphstream.Stream.Binary;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.Netstream;
using Java.Nio;
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

public class NetStreamDecoder : SourceBase, ByteDecoder
{
    private static readonly Logger LOGGER = Logger.GetLogger(typeof(NetStreamDecoder).GetName());
    public override bool Validate(ByteBuffer buffer)
    {
        if (buffer.Position() >= 4)
        {
            int size = buffer.GetInt(0);
            return buffer.Position() >= size;
        }

        return false;
    }

    public override void Decode(ByteBuffer bb)
    {
        try
        {
            int size = bb.GetInt();
            string streamId = NetStreamUtils.DecodeString(bb);
            int cmd = bb.Get();
            if (cmd == NetStreamConstants.EVENT_ADD_NODE)
            {
                Serve_EVENT_ADD_NODE(bb);
            }
            else if ((cmd & 0xFF) == (NetStreamConstants.EVENT_DEL_NODE & 0xFF))
            {
                Serve_DEL_NODE(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_ADD_EDGE)
            {
                Serve_EVENT_ADD_EDGE(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_DEL_EDGE)
            {
                Serve_EVENT_DEL_EDGE(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_STEP)
            {
                Serve_EVENT_STEP(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_CLEARED)
            {
                Serve_EVENT_CLEARED(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_ADD_GRAPH_ATTR)
            {
                Serve_EVENT_ADD_GRAPH_ATTR(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_CHG_GRAPH_ATTR)
            {
                Serve_EVENT_CHG_GRAPH_ATTR(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_DEL_GRAPH_ATTR)
            {
                Serve_EVENT_DEL_GRAPH_ATTR(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_ADD_NODE_ATTR)
            {
                Serve_EVENT_ADD_NODE_ATTR(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_CHG_NODE_ATTR)
            {
                Serve_EVENT_CHG_NODE_ATTR(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_DEL_NODE_ATTR)
            {
                Serve_EVENT_DEL_NODE_ATTR(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_ADD_EDGE_ATTR)
            {
                Serve_EVENT_ADD_EDGE_ATTR(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_CHG_EDGE_ATTR)
            {
                Serve_EVENT_CHG_EDGE_ATTR(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_DEL_EDGE_ATTR)
            {
                Serve_EVENT_DEL_EDGE_ATTR(bb);
            }
            else if (cmd == NetStreamConstants.EVENT_END)
            {
                LOGGER.Info("NetStreamReceiver : Client properly ended the connection.");
            }
            else
            {
                LOGGER.Warning("NetStreamReceiver: Don't know this command: " + cmd);
            }
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Warning("bad buffer");
        }
    }

    protected virtual void Serve_EVENT_DEL_EDGE_ATTR(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received DEL_EDGE_ATTR command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string edgeId = DecodeString(bb);
        string attrId = DecodeString(bb);
        SendEdgeAttributeRemoved(sourceId, timeId, edgeId, attrId);
    }

    protected virtual void Serve_EVENT_CHG_EDGE_ATTR(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received CHG_EDGE_ATTR command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string edgeId = DecodeString(bb);
        string attrId = DecodeString(bb);
        int oldValueType = DecodeType(bb);
        object oldValue = DecodeValue(bb, oldValueType);
        int newValueType = DecodeType(bb);
        object newValue = DecodeValue(bb, newValueType);
        SendEdgeAttributeChanged(sourceId, timeId, edgeId, attrId, oldValue, newValue);
    }

    protected virtual void Serve_EVENT_ADD_EDGE_ATTR(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received ADD_EDGE_ATTR command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string edgeId = DecodeString(bb);
        string attrId = DecodeString(bb);
        object value = DecodeValue(bb, DecodeType(bb));
        SendEdgeAttributeAdded(sourceId, timeId, edgeId, attrId, value);
    }

    protected virtual void Serve_EVENT_DEL_NODE_ATTR(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received DEL_NODE_ATTR command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string nodeId = DecodeString(bb);
        string attrId = DecodeString(bb);
        SendNodeAttributeRemoved(sourceId, timeId, nodeId, attrId);
    }

    protected virtual void Serve_EVENT_CHG_NODE_ATTR(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received EVENT_CHG_NODE_ATTR command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string nodeId = DecodeString(bb);
        string attrId = DecodeString(bb);
        int oldValueType = DecodeType(bb);
        object oldValue = DecodeValue(bb, oldValueType);
        int newValueType = DecodeType(bb);
        object newValue = DecodeValue(bb, newValueType);
        SendNodeAttributeChanged(sourceId, timeId, nodeId, attrId, oldValue, newValue);
    }

    protected virtual void Serve_EVENT_ADD_NODE_ATTR(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received EVENT_ADD_NODE_ATTR command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string nodeId = DecodeString(bb);
        string attrId = DecodeString(bb);
        object value = DecodeValue(bb, DecodeType(bb));
        SendNodeAttributeAdded(sourceId, timeId, nodeId, attrId, value);
    }

    protected virtual void Serve_EVENT_DEL_GRAPH_ATTR(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received EVENT_DEL_GRAPH_ATTR command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string attrId = DecodeString(bb);
        SendGraphAttributeRemoved(sourceId, timeId, attrId);
    }

    protected virtual void Serve_EVENT_CHG_GRAPH_ATTR(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received EVENT_CHG_GRAPH_ATTR command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string attrId = DecodeString(bb);
        int oldValueType = DecodeType(bb);
        object oldValue = DecodeValue(bb, oldValueType);
        int newValueType = DecodeType(bb);
        object newValue = DecodeValue(bb, newValueType);
        SendGraphAttributeChanged(sourceId, timeId, attrId, oldValue, newValue);
    }

    protected virtual void Serve_EVENT_ADD_GRAPH_ATTR(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received EVENT_ADD_GRAPH_ATTR command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string attrId = DecodeString(bb);
        object value = DecodeValue(bb, DecodeType(bb));
        LOGGER.Finest(String.Format("NetStreamServer | EVENT_ADD_GRAPH_ATTR | %s=%s", attrId, value.ToString()));
        SendGraphAttributeAdded(sourceId, timeId, attrId, value);
    }

    protected virtual void Serve_EVENT_CLEARED(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received EVENT_CLEARED command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        SendGraphCleared(sourceId, timeId);
    }

    protected virtual void Serve_EVENT_STEP(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received EVENT_STEP command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        double time = DecodeDouble(bb);
        SendStepBegins(sourceId, timeId, time);
    }

    protected virtual void Serve_EVENT_DEL_EDGE(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received EVENT_DEL_EDGE command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string edgeId = DecodeString(bb);
        SendEdgeRemoved(sourceId, timeId, edgeId);
    }

    protected virtual void Serve_EVENT_ADD_EDGE(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received ADD_EDGE command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string edgeId = DecodeString(bb);
        string from = DecodeString(bb);
        string to = DecodeString(bb);
        bool directed = DecodeBoolean(bb);
        SendEdgeAdded(sourceId, timeId, edgeId, from, to, directed);
    }

    protected virtual void Serve_DEL_NODE(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received DEL_NODE command.");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string nodeId = DecodeString(bb);
        SendNodeRemoved(sourceId, timeId, nodeId);
    }

    protected virtual void Serve_EVENT_ADD_NODE(ByteBuffer bb)
    {
        LOGGER.Finest("NetStreamServer: Received EVENT_ADD_NODE command");
        string sourceId = DecodeString(bb);
        long timeId = DecodeUnsignedVarint(bb);
        string nodeId = DecodeString(bb);
        SendNodeAdded(sourceId, timeId, nodeId);
    }
}
