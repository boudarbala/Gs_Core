using Gs_Core.Graphstream.Stream.Binary;
using Java.Lang.Reflect;
using Java.Nio;
using Java.Nio.Charset;
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

public class NetStreamUtils
{
    private static ByteBuffer NULL_BUFFER = ByteBuffer.Allocate(0);
    private static readonly Logger LOGGER = Logger.GetLogger(typeof(NetStreamUtils).GetName());
    public static ByteFactory GetDefaultNetStreamFactory()
    {
        return new AnonymousByteFactory(this);
    }

    private sealed class AnonymousByteFactory : ByteFactory
    {
        public AnonymousByteFactory(NetStreamUtils parent)
        {
            this.parent = parent;
        }

        private readonly NetStreamUtils parent;
        public ByteEncoder CreateByteEncoder()
        {
            return new NetStreamEncoder();
        }

        public ByteDecoder CreateByteDecoder()
        {
            return new NetStreamDecoder();
        }
    }

    public static int GetType(object value)
    {
        int valueType = NetStreamConstants.TYPE_UNKNOWN;
        if (value == null)
            return NetStreamConstants.TYPE_NULL;
        Class<TWildcardTodo> valueClass = value.GetType();
        bool isArray = valueClass.IsArray();
        if (isArray)
        {
            if (Array.GetLength(value) > 0)
            {
                valueClass = Array.Get(value, 0).GetType();
            }
            else
            {
                return NetStreamConstants.TYPE_ARRAY;
            }
        }

        if (valueClass.Equals(typeof(bool)))
        {
            if (isArray)
            {
                valueType = NetStreamConstants.TYPE_BOOLEAN_ARRAY;
            }
            else
            {
                valueType = NetStreamConstants.TYPE_BOOLEAN;
            }
        }
        else if (valueClass.Equals(typeof(Byte)))
        {
            if (isArray)
            {
                valueType = NetStreamConstants.TYPE_BYTE_ARRAY;
            }
            else
            {
                valueType = NetStreamConstants.TYPE_BYTE;
            }
        }
        else if (valueClass.Equals(typeof(Short)))
        {
            if (isArray)
            {
                valueType = NetStreamConstants.TYPE_SHORT_ARRAY;
            }
            else
            {
                valueType = NetStreamConstants.TYPE_SHORT;
            }
        }
        else if (valueClass.Equals(typeof(int)))
        {
            if (isArray)
            {
                valueType = NetStreamConstants.TYPE_INT_ARRAY;
            }
            else
            {
                valueType = NetStreamConstants.TYPE_INT;
            }
        }
        else if (valueClass.Equals(typeof(long)))
        {
            if (isArray)
            {
                valueType = NetStreamConstants.TYPE_LONG_ARRAY;
            }
            else
            {
                valueType = NetStreamConstants.TYPE_LONG;
            }
        }
        else if (valueClass.Equals(typeof(float)))
        {
            if (isArray)
            {
                valueType = NetStreamConstants.TYPE_FLOAT_ARRAY;
            }
            else
            {
                valueType = NetStreamConstants.TYPE_FLOAT;
            }
        }
        else if (valueClass.Equals(typeof(Double)))
        {
            if (isArray)
            {
                valueType = NetStreamConstants.TYPE_DOUBLE_ARRAY;
            }
            else
            {
                valueType = NetStreamConstants.TYPE_DOUBLE;
            }
        }
        else if (valueClass.Equals(typeof(string)))
        {
            if (isArray)
            {
                valueType = NetStreamConstants.TYPE_STRING_ARRAY;
            }
            else
            {
                valueType = NetStreamConstants.TYPE_STRING;
            }
        }
        else
            LOGGER.Warning(String.Format("can not find type of %s.", valueClass));
        return valueType;
    }

    public static int GetVarintSize(long data)
    {
        if (data < (1 << 7))
        {
            return 1;
        }

        if (data < (1 << 14))
        {
            return 2;
        }

        if (data < (1 << 21))
        {
            return 3;
        }

        if (data < (1 << 28))
        {
            return 4;
        }

        if (data < (1 << 35))
        {
            return 5;
        }

        if (data < (1 << 42))
        {
            return 6;
        }

        if (data < (1 << 49))
        {
            return 7;
        }

        if (data < (1 << 56))
        {
            return 8;
        }

        return 9;
    }

    public static void PutVarint(ByteBuffer buffer, long number, int byteSize)
    {
        for (int i = 0; i < byteSize; i++)
        {
            int head = 128;
            if (i == byteSize - 1)
                head = 0;
            long b = ((number >> (7 * i)) & 127) ^ head;
            buffer.Put((byte)(b & 255));
        }
    }

    public static ByteBuffer EncodeValue(object @in, int valueType)
    {
        if (NetStreamConstants.TYPE_BOOLEAN == valueType)
        {
            return EncodeBoolean(@in);
        }
        else if (NetStreamConstants.TYPE_BOOLEAN_ARRAY == valueType)
        {
            return EncodeBooleanArray(@in);
        }
        else if (NetStreamConstants.TYPE_BYTE == valueType)
        {
            return EncodeByte(@in);
        }
        else if (NetStreamConstants.TYPE_BYTE_ARRAY == valueType)
        {
            return EncodeByteArray(@in);
        }
        else if (NetStreamConstants.TYPE_SHORT == valueType)
        {
            return EncodeShort(@in);
        }
        else if (NetStreamConstants.TYPE_SHORT_ARRAY == valueType)
        {
            return EncodeShortArray(@in);
        }
        else if (NetStreamConstants.TYPE_INT == valueType)
        {
            return EncodeInt(@in);
        }
        else if (NetStreamConstants.TYPE_INT_ARRAY == valueType)
        {
            return EncodeIntArray(@in);
        }
        else if (NetStreamConstants.TYPE_LONG == valueType)
        {
            return EncodeLong(@in);
        }
        else if (NetStreamConstants.TYPE_LONG_ARRAY == valueType)
        {
            return EncodeLongArray(@in);
        }
        else if (NetStreamConstants.TYPE_FLOAT == valueType)
        {
            return EncodeFloat(@in);
        }
        else if (NetStreamConstants.TYPE_FLOAT_ARRAY == valueType)
        {
            return EncodeFloatArray(@in);
        }
        else if (NetStreamConstants.TYPE_DOUBLE == valueType)
        {
            return EncodeDouble(@in);
        }
        else if (NetStreamConstants.TYPE_DOUBLE_ARRAY == valueType)
        {
            return EncodeDoubleArray(@in);
        }
        else if (NetStreamConstants.TYPE_STRING == valueType)
        {
            return EncodeString(@in);
        }
        else if (NetStreamConstants.TYPE_STRING_ARRAY == valueType)
        {
            return EncodeStringArray(@in);
        }
        else if (NetStreamConstants.TYPE_ARRAY == valueType)
        {
            return EncodeArray(@in);
        }
        else if (NetStreamConstants.TYPE_NULL == valueType)
        {
            return NULL_BUFFER;
        }

        return null;
    }

    public static ByteBuffer EncodeUnsignedVarint(object @in)
    {
        long data = ((Number)@in).LongValue();
        int size = GetVarintSize(data);
        ByteBuffer buff = ByteBuffer.Allocate(size);
        for (int i = 0; i < size; i++)
        {
            int head = 128;
            if (i == size - 1)
                head = 0;
            long b = ((data >> (7 * i)) & 127) ^ head;
            buff.Put((byte)(b & 255));
        }

        buff.Rewind();
        return buff;
    }

    public static ByteBuffer EncodeVarint(object @in)
    {
        long data = ((Number)@in).LongValue();
        return EncodeUnsignedVarint(data >= 0 ? (data << 1) : ((Math.Abs(data) << 1) ^ 1));
    }

    public static ByteBuffer EncodeString(object @in)
    {
        string s = (string)@in;
        byte[] data = s.GetBytes(Charset.ForName("UTF-8"));
        ByteBuffer lenBuff = EncodeUnsignedVarint(data.Length);
        ByteBuffer bb = ByteBuffer.Allocate(lenBuff.Capacity() + data.Length);
        bb.Put(lenBuff).Put(data);
        bb.Rewind();
        return bb;
    }

    public static ByteBuffer EncodeArray(object @in)
    {
        return null;
    }

    public static ByteBuffer EncodeDoubleArray(object @in)
    {
        object[] data = (Object[])@in;
        int ssize = GetVarintSize(data.Length);
        ByteBuffer b = ByteBuffer.Allocate(ssize + data.Length * 8);
        PutVarint(b, data.Length, ssize);
        for (int i = 0; i < data.Length; i++)
        {
            b.PutDouble((Double)data[i]);
        }

        b.Rewind();
        return b;
    }

    public static ByteBuffer EncodeStringArray(object @in)
    {
        object[] data = (Object[])@in;
        int ssize = GetVarintSize(data.Length);
        byte[, ] dataArray = new byte[data.Length];
        ByteBuffer[] lenBuffArray = new ByteBuffer[data.Length];
        int bufferSize = 0;
        for (int i = 0; i < data.Length; i++)
        {
            byte[] bs = ((string)data[i]).GetBytes(Charset.ForName("UTF-8"));
            dataArray[i] = bs;
            ByteBuffer lenBuff = EncodeUnsignedVarint(bs.Length);
            lenBuffArray[i] = lenBuff;
            bufferSize += lenBuff.Capacity() + bs.Length;
        }

        ByteBuffer bb = ByteBuffer.Allocate(ssize + bufferSize);
        PutVarint(bb, data.Length, ssize);
        for (int i = 0; i < data.Length; i++)
        {
            bb.Put(lenBuffArray[i]).Put(dataArray[i]);
        }

        bb.Rewind();
        return bb;
    }

    public static ByteBuffer EncodeDouble(object @in)
    {
        ByteBuffer bb = ByteBuffer.Allocate(8).PutDouble((Double)@in);
        bb.Rewind();
        return bb;
    }

    public static ByteBuffer EncodeFloatArray(object @in)
    {
        object[] data = (Object[])@in;
        int ssize = GetVarintSize(data.Length);
        ByteBuffer b = ByteBuffer.Allocate(ssize + data.Length * 4);
        PutVarint(b, data.Length, ssize);
        for (int i = 0; i < data.Length; i++)
        {
            b.PutFloat((float)data[i]);
        }

        b.Rewind();
        return b;
    }

    public static ByteBuffer EncodeFloat(object @in)
    {
        ByteBuffer b = ByteBuffer.Allocate(4);
        b.PutFloat(((float)@in));
        b.Rewind();
        return b;
    }

    public static ByteBuffer EncodeLongArray(object @in)
    {
        return EncodeVarintArray(@in);
    }

    public static ByteBuffer EncodeLong(object @in)
    {
        return EncodeVarint(@in);
    }

    public static ByteBuffer EncodeIntArray(object @in)
    {
        return EncodeVarintArray(@in);
    }

    public static ByteBuffer EncodeInt(object @in)
    {
        return EncodeVarint(@in);
    }

    public static ByteBuffer EncodeShortArray(object @in)
    {
        return EncodeVarintArray(@in);
    }

    public static ByteBuffer EncodeShort(object @in)
    {
        return EncodeVarint(@in);
    }

    public static ByteBuffer EncodeByteArray(object @in)
    {
        object[] data = (Object[])@in;
        int ssize = GetVarintSize(data.Length);
        ByteBuffer b = ByteBuffer.Allocate(ssize + data.Length);
        PutVarint(b, data.Length, ssize);
        for (int i = 0; i < data.Length; i++)
        {
            b.Put((Byte)data[i]);
        }

        b.Rewind();
        return b;
    }

    public static ByteBuffer EncodeByte(object @in)
    {
        ByteBuffer b = ByteBuffer.Allocate(1);
        b.Put(((Byte)@in));
        b.Rewind();
        return b;
    }

    public static ByteBuffer EncodeBooleanArray(object @in)
    {
        object[] data = (Object[])@in;
        int ssize = GetVarintSize(data.Length);
        ByteBuffer b = ByteBuffer.Allocate(ssize + data.Length);
        PutVarint(b, data.Length, ssize);
        for (int i = 0; i < data.Length; i++)
        {
            b.Put((byte)((bool)data[i] == false ? 0 : 1));
        }

        b.Rewind();
        return b;
    }

    public static ByteBuffer EncodeBoolean(object @in)
    {
        ByteBuffer b = ByteBuffer.Allocate(1);
        b.Put((byte)(((bool)@in) == false ? 0 : 1));
        b.Rewind();
        return b;
    }

    public static ByteBuffer EncodeVarintArray(object @in)
    {
        object[] data = (Object[])@in;
        int[] sizes = new int[data.Length];
        long[] zigzags = new long[data.Length];
        int sumsizes = 0;
        for (int i = 0; i < data.Length; i++)
        {
            long datum = ((Number)data[i]).LongValue();
            zigzags[i] = datum > 0 ? (datum << 1) : ((Math.Abs(datum) << 1) ^ 1);
            sizes[i] = GetVarintSize(zigzags[i]);
            sumsizes += sizes[i];
        }

        int ssize = GetVarintSize(data.Length);
        ByteBuffer b = ByteBuffer.Allocate(ssize + sumsizes);
        PutVarint(b, data.Length, ssize);
        for (int i = 0; i < data.Length; i++)
        {
            PutVarint(b, zigzags[i], sizes[i]);
        }

        b.Rewind();
        return b;
    }

    public static int DecodeType(ByteBuffer bb)
    {
        try
        {
            return bb.Get();
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Info("decodeType: could not decode type");
            e.PrintStackTrace();
        }

        return 0;
    }

    public static object DecodeValue(ByteBuffer bb, int valueType)
    {
        if (NetStreamConstants.TYPE_BOOLEAN == valueType)
        {
            return DecodeBoolean(bb);
        }
        else if (NetStreamConstants.TYPE_BOOLEAN_ARRAY == valueType)
        {
            return DecodeBooleanArray(bb);
        }
        else if (NetStreamConstants.TYPE_BYTE == valueType)
        {
            return DecodeByte(bb);
        }
        else if (NetStreamConstants.TYPE_BYTE_ARRAY == valueType)
        {
            return DecodeByteArray(bb);
        }
        else if (NetStreamConstants.TYPE_SHORT == valueType)
        {
            return DecodeShort(bb);
        }
        else if (NetStreamConstants.TYPE_SHORT_ARRAY == valueType)
        {
            return DecodeShortArray(bb);
        }
        else if (NetStreamConstants.TYPE_INT == valueType)
        {
            return DecodeInt(bb);
        }
        else if (NetStreamConstants.TYPE_INT_ARRAY == valueType)
        {
            return DecodeIntArray(bb);
        }
        else if (NetStreamConstants.TYPE_LONG == valueType)
        {
            return DecodeLong(bb);
        }
        else if (NetStreamConstants.TYPE_LONG_ARRAY == valueType)
        {
            return DecodeLongArray(bb);
        }
        else if (NetStreamConstants.TYPE_FLOAT == valueType)
        {
            return DecodeFloat(bb);
        }
        else if (NetStreamConstants.TYPE_FLOAT_ARRAY == valueType)
        {
            return DecodeFloatArray(bb);
        }
        else if (NetStreamConstants.TYPE_DOUBLE == valueType)
        {
            return DecodeDouble(bb);
        }
        else if (NetStreamConstants.TYPE_DOUBLE_ARRAY == valueType)
        {
            return DecodeDoubleArray(bb);
        }
        else if (NetStreamConstants.TYPE_STRING == valueType)
        {
            return DecodeString(bb);
        }
        else if (NetStreamConstants.TYPE_STRING_ARRAY == valueType)
        {
            return DecodeStringArray(bb);
        }
        else if (NetStreamConstants.TYPE_ARRAY == valueType)
        {
            return DecodeArray(bb);
        }

        return null;
    }

    public static Object[] DecodeArray(ByteBuffer bb)
    {
        int len = (int)DecodeUnsignedVarint(bb);
        object[] array = new object[len];
        for (int i = 0; i < len; i++)
        {
            array[i] = DecodeValue(bb, DecodeType(bb));
        }

        return array;
    }

    public static string DecodeString(ByteBuffer bb)
    {
        try
        {
            int len = (int)DecodeUnsignedVarint(bb);
            byte[] data = new byte[len];
            bb[data];
            return new string (data, Charset.ForName("UTF-8"));
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Info("decodeString: could not decode string");
            e.PrintStackTrace();
        }

        return null;
    }

    public static String[] DecodeStringArray(ByteBuffer bb)
    {
        int len = (int)DecodeUnsignedVarint(bb);
        string[] array = new string[len];
        for (int i = 0; i < len; i++)
        {
            array[i] = DecodeString(bb);
        }

        return array;
    }

    public static bool DecodeBoolean(ByteBuffer bb)
    {
        int data = 0;
        try
        {
            data = bb.Get();
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Info("decodeByte: could not decode");
            e.PrintStackTrace();
        }

        return data != 0;
    }

    public static Byte DecodeByte(ByteBuffer bb)
    {
        byte data = 0;
        try
        {
            data = bb.Get();
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Info("decodeByte: could not decode");
            e.PrintStackTrace();
        }

        return data;
    }

    public static long DecodeUnsignedVarint(ByteBuffer bb)
    {
        try
        {
            int size = 0;
            long[] data = new long[9];
            do
            {
                data[size] = bb.Get();
                size++;
            }
            while ((data[size - 1] & 128) == 128);
            long number = 0;
            for (int i = 0; i < size; i++)
            {
                number ^= (data[i] & 127) << (i * 7);
            }

            return number;
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Info("decodeUnsignedVarintFromInteger: could not decode");
            e.PrintStackTrace();
        }

        return 0;
    }

    public static long DecodeVarint(ByteBuffer bb)
    {
        long number = DecodeUnsignedVarint(bb);
        return ((number & 1) == 0) ? number >> 1 : -(number >> 1);
    }

    public static Short DecodeShort(ByteBuffer bb)
    {
        return (short)DecodeVarint(bb);
    }

    public static int DecodeInt(ByteBuffer bb)
    {
        return (int)DecodeVarint(bb);
    }

    public static long DecodeLong(ByteBuffer bb)
    {
        return DecodeVarint(bb);
    }

    public static float DecodeFloat(ByteBuffer bb)
    {
        return bb.GetFloat();
    }

    public static Double DecodeDouble(ByteBuffer bb)
    {
        return bb.GetDouble();
    }

    public static Integer[] DecodeIntArray(ByteBuffer bb)
    {
        int len = (int)DecodeUnsignedVarint(bb);
        int[] res = new int[len];
        for (int i = 0; i < len; i++)
        {
            res[i] = (int)DecodeVarint(bb);
        }

        return res;
    }

    public static Boolean[] DecodeBooleanArray(ByteBuffer bb)
    {
        try
        {
            int len = (int)DecodeUnsignedVarint(bb);
            bool[] res = new bool[len];
            for (int i = 0; i < len; i++)
            {
                byte b = bb.Get();
                res[i] = b != 0;
            }

            return res;
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Info("decodeBooleanArray: could not decode array");
            e.PrintStackTrace();
        }

        return null;
    }

    public static Byte[] DecodeByteArray(ByteBuffer bb)
    {
        try
        {
            int len = (int)DecodeUnsignedVarint(bb);
            Byte[] res = new Byte[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = bb.Get();
            }

            return res;
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Info("decodeBooleanArray: could not decode array");
            e.PrintStackTrace();
        }

        return null;
    }

    public static Double[] DecodeDoubleArray(ByteBuffer bb)
    {
        try
        {
            int len = (int)DecodeUnsignedVarint(bb);
            Double[] res = new Double[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = bb.GetDouble();
            }

            return res;
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Info("decodeDoubleArray: could not decode array");
            e.PrintStackTrace();
        }

        return null;
    }

    public static Float[] DecodeFloatArray(ByteBuffer bb)
    {
        try
        {
            int len = (int)DecodeUnsignedVarint(bb);
            float[] res = new float[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = bb.GetFloat();
            }

            return res;
        }
        catch (BufferUnderflowException e)
        {
            LOGGER.Info("decodeFloatArray: could not decode array");
            e.PrintStackTrace();
        }

        return null;
    }

    public static Long[] DecodeLongArray(ByteBuffer bb)
    {
        int len = (int)DecodeUnsignedVarint(bb);
        long[] res = new long[len];
        for (int i = 0; i < len; i++)
        {
            res[i] = DecodeVarint(bb);
        }

        return res;
    }

    public static Short[] DecodeShortArray(ByteBuffer bb)
    {
        int len = (int)DecodeUnsignedVarint(bb);
        Short[] res = new Short[len];
        for (int i = 0; i < len; i++)
        {
            res[i] = (short)DecodeVarint(bb);
        }

        return res;
    }
}
