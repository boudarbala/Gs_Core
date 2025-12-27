using Gs_Core.Graphstream.Stream;
using Java.Io;
using Java.Net;
using Java.Nio;
using Java.Util;
using Java.Util.Concurrent.Atomic;
using Java.Util.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.Binary.ElementType;
using static Org.Graphstream.Stream.Binary.EventType;
using static Org.Graphstream.Stream.Binary.AttributeChangeEvent;
using static Org.Graphstream.Stream.Binary.Mode;

namespace Gs_Core.Graphstream.Stream.Binary;

public class ByteProxy : SourceBase, Pipe, Runnable
{
    private static readonly Logger LOGGER = Logger.GetLogger(typeof(ByteProxy).GetName());
    public enum Mode
    {
        SERVER,
        CLIENT
    }

    protected static readonly int BUFFER_INITIAL_SIZE = 8192;
    protected readonly ByteFactory byteFactory;
    protected readonly ByteEncoder encoder;
    protected readonly ByteDecoder decoder;
    protected readonly AtomicBoolean running;
    public readonly Mode mode;
    public readonly InetAddress address;
    public readonly int port;
    protected SelectableChannel mainChannel;
    protected Selector selector;
    protected Thread thread;
    protected Collection<SocketChannel> writableChannels;
    protected Replayable replayable;
    public ByteProxy(ByteFactory factory, int port) : this(factory, Mode.SERVER, InetAddress.GetLocalHost(), port)
    {
    }

    public ByteProxy(ByteFactory factory, Mode mode, InetAddress address, int port)
    {
        running = new AtomicBoolean(false);
        writableChannels = new LinkedList();
        replayable = null;
        thread = null;
        this.mode = mode;
        this.address = address;
        this.port = port;
        byteFactory = factory;
        encoder = factory.CreateByteEncoder();
        decoder = factory.CreateByteDecoder();
        encoder.AddTransport(new AnonymousTransport(this));
        decoder.AddSink(new AnonymousSink(this));
        Init();
    }

    private sealed class AnonymousTransport : Transport
    {
        public AnonymousTransport(Mode parent)
        {
            this.parent = parent;
        }

        private readonly Mode parent;
        public void Send(ByteBuffer buffer)
        {
            DoSend(buffer);
        }
    }

    private sealed class AnonymousSink : Sink
    {
        public AnonymousSink(Mode parent)
        {
            this.parent = parent;
        }

        private readonly Mode parent;
        public void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
        {
            SendGraphAttributeAdded(sourceId, timeId, attribute, value);
        }

        public void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
        {
            SendGraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
        }

        public void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
        {
            SendGraphAttributeRemoved(sourceId, timeId, attribute);
        }

        public void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
        {
            SendNodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
        }

        public void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
        {
            SendNodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
        }

        public void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
        {
            SendNodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
        }

        public void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
        {
            SendEdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
        }

        public void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
        {
            SendEdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
        }

        public void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
        {
            SendEdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
        }

        public void NodeAdded(string sourceId, long timeId, string nodeId)
        {
            SendNodeAdded(sourceId, timeId, nodeId);
        }

        public void NodeRemoved(string sourceId, long timeId, string nodeId)
        {
            SendNodeRemoved(sourceId, timeId, nodeId);
        }

        public void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
        {
            SendEdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
        }

        public void EdgeRemoved(string sourceId, long timeId, string edgeId)
        {
            SendEdgeRemoved(sourceId, timeId, edgeId);
        }

        public void GraphCleared(string sourceId, long timeId)
        {
            SendGraphCleared(sourceId, timeId);
        }

        public void StepBegins(string sourceId, long timeId, double step)
        {
            SendStepBegins(sourceId, timeId, step);
        }
    }

    protected virtual void Init()
    {
        InetSocketAddress isa = new InetSocketAddress(address, port);
        selector = Selector.Open();
        switch (mode)
        {
            case SERVER:
                ServerSocketChannel serverChannel = ServerSocketChannel.Open();
                serverChannel.ConfigureBlocking(false);
                serverChannel.Bind(isa);
                mainChannel = serverChannel;
                mainChannel.Register(selector, SelectionKey.OP_ACCEPT);
                break;
            case CLIENT:
                SocketChannel socketChannel = SocketChannel.Open();
                socketChannel.Connect(isa);
                socketChannel.FinishConnect();
                socketChannel.ConfigureBlocking(false);
                mainChannel = socketChannel;
                mainChannel.Register(selector, SelectionKey.OP_READ + SelectionKey.OP_WRITE);
                writableChannels.Add(socketChannel);
                break;
        }
    }

    public virtual void SetReplayable(Replayable replayable)
    {
        this.replayable = replayable;
    }

    public virtual void Start()
    {
        lock (this)
        {
            if (thread != null)
            {
                LOGGER.Warning("Already started.");
            }
            else
            {
                Thread t = new Thread(this);
                t.Start();
            }
        }
    }

    public virtual void Stop()
    {
        if (thread != null)
        {
            Thread t = thread;
            running.Set(false);
            t.Join();
        }
    }

    public override void Run()
    {
        thread = Thread.CurrentThread();
        running.Set(true);
        LOGGER.Info(String.Format("[%s] started on %s:%d...", mode, address.GetHostName(), port));
        while (running.Get())
        {
            Poll();
        }

        thread = null;
    }

    protected virtual void ProcessSelectedKeys()
    {
        HashSet<TWildcardTodo> readyKeys = selector.SelectedKeys();
        IEnumerator<TWildcardTodo> i = readyKeys.Iterator();
        while (i.HasNext())
        {
            SelectionKey key = (SelectionKey)i.Next();
            i.Remove();
            if (key.IsAcceptable())
            {
                ServerSocketChannel ssocket = (ServerSocketChannel)key.Channel();
                SocketChannel socketChannel = ssocket.Accept();
                LOGGER.Info(String.Format("accepting socket %s:%d", socketChannel.Socket().GetInetAddress(), socketChannel.Socket().GetPort()));
                socketChannel.FinishConnect();
                socketChannel.ConfigureBlocking(false);
                if (decoder != null)
                    socketChannel.Register(selector, SelectionKey.OP_READ);
                Replay(socketChannel);
                writableChannels.Add(socketChannel);
            }
            else if (key.IsReadable())
            {
                ReadDataChunk(key);
            }
            else if (key.IsWritable() && key.Attachment() != null)
            {
                ByteBuffer buffer = (ByteBuffer)key.Attachment();
                WritableByteChannel out = (WritableByteChannel)key.Channel();
                try
                {
                    @out.Write(buffer);
                }
                catch (IOException e)
                {
                    LOGGER.Severe("I/O error while writing to channel.");
                    Dispose(@out);
                }
                finally
                {
                    key.Cancel();
                }
            }
        }
    }

    public virtual void Poll()
    {
        Poll(true);
    }

    public virtual void Poll(bool blocking)
    {
        try
        {
            if (blocking)
            {
                if (selector.Select() > 0)
                {
                    ProcessSelectedKeys();
                }
            }
            else
            {
                if (selector.SelectNow() > 0)
                {
                    ProcessSelectedKeys();
                }
            }
        }
        catch (IOException e)
        {
            LOGGER.Severe(String.Format("I/O error in receiver //:%d thread: aborting: %s", port, e.GetMessage()));
            running.Set(false);
        }
        catch (Throwable e)
        {
            LOGGER.Severe(String.Format("Unknown error: %s", e.GetMessage()));
            e.PrintStackTrace();
            running.Set(false);
        }
    }

    protected virtual void ReadDataChunk(SelectionKey key)
    {
        ByteBuffer buffer = (ByteBuffer)key.Attachment();
        SocketChannel socket = (SocketChannel)key.Channel();
        if (buffer == null)
        {
            buffer = ByteBuffer.Allocate(BUFFER_INITIAL_SIZE);
            key.Attach(buffer);
            LOGGER.Info(String.Format("creating buffer for new connection from %s:%d", socket.Socket().GetInetAddress(), socket.Socket().GetPort()));
        }

        try
        {
            int r = socket.Read(buffer);
            if (r < 0)
            {
                LOGGER.Info("end-of-stream reached. Closing the mainChannel.");
                Dispose(socket);
            }
            else if (r == 0)
            {
                LOGGER.Warning("Strange, no binary read.");
            }
            else
            {
                while (decoder.Validate(buffer))
                {
                    buffer.Flip();
                    decoder.Decode(buffer);
                    buffer.Compact();
                }

                if (!buffer.HasRemaining())
                {
                    ByteBuffer bigger = ByteBuffer.Allocate(buffer.Capacity() + BUFFER_INITIAL_SIZE);
                    bigger.Put(buffer);
                    key.Attach(bigger);
                }
            }
        }
        catch (IOException e)
        {
            LOGGER.Severe(String.Format("receiver //%s:%d cannot read object socket mainChannel (I/O error): %s", address.GetHostName(), port, e.GetMessage()));
            Dispose(key.Channel());
        }
    }

    protected virtual void DoSend(ByteBuffer buffer)
    {
        ByteBuffer sendBuffer = ByteBuffer.Allocate(buffer.Remaining());
        sendBuffer.Put(buffer);
        sendBuffer.Rewind();
        IEnumerator<SocketChannel> channels = writableChannels.Iterator();
        while (channels.HasNext())
        {
            SocketChannel writableChannel = channels.Next();
            try
            {
                try
                {
                    writableChannel.Write(sendBuffer.Duplicate());
                }
                catch (NotYetConnectedException e)
                {
                    writableChannel.Register(selector, SelectionKey.OP_WRITE, sendBuffer.Duplicate());
                }
            }
            catch (IOException e)
            {
                LOGGER.Severe("I/O error while writing to channel : " + e.GetMessage());
                channels.Remove();
                Dispose(writableChannel);
            }
        }
    }

    protected virtual void Replay(SocketChannel channel)
    {
        if (replayable != null)
        {
            Replayable.Controller controller = replayable.GetReplayController();
            ByteEncoder encoder = byteFactory.CreateByteEncoder();
            encoder.AddTransport(new AnonymousTransport1(this));
            controller.AddSink(encoder);
            controller.Replay();
        }
    }

    private sealed class AnonymousTransport1 : Transport
    {
        public AnonymousTransport1(Mode parent)
        {
            this.parent = parent;
        }

        private readonly Mode parent;
        public void Send(ByteBuffer buffer)
        {
            try
            {
                channel.Write(buffer);
            }
            catch (IOException e)
            {
                LOGGER.Severe("Failled to replay : " + e.GetMessage());
                controller.RemoveSink(encoder);
            }
        }
    }

    protected virtual void Dispose(Channel channel)
    {
        writableChannels.Remove(channel);
        if (channel == mainChannel)
        {
            LOGGER.Warning("Closing main channel.");
            if (running.Get())
            {
                try
                {
                    Stop();
                }
                catch (InterruptedException e)
                {
                    LOGGER.Warning("Failed to properly terminate the worker.");
                }
            }
        }

        try
        {
            channel.Dispose();
        }
        catch (IOException e)
        {
            LOGGER.Warning("closing channel: " + e.GetMessage());
        }
    }

    public override void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        encoder.GraphAttributeAdded(sourceId, timeId, attribute, value);
    }

    public override void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        encoder.GraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
    }

    public override void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        encoder.GraphAttributeRemoved(sourceId, timeId, attribute);
    }

    public override void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        encoder.NodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
    }

    public override void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        encoder.NodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
    }

    public override void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        encoder.NodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
    }

    public override void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        encoder.EdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
    }

    public override void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        encoder.EdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
    }

    public override void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        encoder.EdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
    }

    public override void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        encoder.NodeAdded(sourceId, timeId, nodeId);
    }

    public override void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        encoder.NodeRemoved(sourceId, timeId, nodeId);
    }

    public override void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        encoder.EdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
    }

    public override void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        encoder.EdgeRemoved(sourceId, timeId, edgeId);
    }

    public override void GraphCleared(string sourceId, long timeId)
    {
        encoder.GraphCleared(sourceId, timeId);
    }

    public override void StepBegins(string sourceId, long timeId, double step)
    {
        encoder.StepBegins(sourceId, timeId, step);
    }
}
