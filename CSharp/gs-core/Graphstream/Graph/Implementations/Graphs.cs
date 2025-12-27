using gs_core.Graphstream.Graph;
using gs_core.Graphstream.Stream;
using gs_core.Graphstream.Stream.File;
using gs_core.Graphstream.Ui.View;
using Java.Io;
using Java.Lang.Reflect;
using Java.Util;
using Java.Util.Concurrent.Locks;
using Java.Util.Logging;
using Java.Util.Stream;
using Microsoft.VisualBasic;
using Gs_Core.Graphstream.Graph.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;
using static gs_core.Graphstream.Graph.Implementations.AttributeChangeEvent;
using static gs_core.Graphstream.Graph.Implementations.ElementType;
using static gs_core.Graphstream.Graph.Implementations.EventType;

namespace Gs_Core.Graphstream.Graph.Implementations
{

    public class Graphs
    {
        private static readonly Logger logger = Logger.GetLogger(typeof(Graphs).GetSimpleName());
        public static Graph UnmutableGraph(Graph g)
        {
            return null;
        }

        public static Graph SynchronizedGraph(Graph g)
        {
            return new SynchronizedGraph(g);
        }

        public static Graph Merge(params Graph[] graphs)
        {
            if (graphs == null)
                return new DefaultGraph("void-merge");
            string id = "merge";
            foreach (Graph g in graphs)
                id += "-" + g.GetId();
            Graph result;
            try
            {
                Class<TWildcardTodoGraph> cls = graphs[0].GetType();
                result = cls.GetConstructor(typeof(string)).NewInstance(id);
            }
            catch (Exception e)
            {
                logger.Warning(String.Format("Cannot create a graph of %s.", graphs[0].GetType().GetName()));
                result = new MultiGraph(id);
            }

            MergeIn(result, graphs);
            return result;
        }

        public static void MergeIn(Graph result, params Graph[] graphs)
        {
            bool strict = result.IsStrict();
            GraphReplay replay = new GraphReplay(String.Format("replay-%x", System.NanoTime()));
            replay.AddSink(result);
            result.SetStrict(false);
            if (graphs != null)
                foreach (Graph g in graphs)
                    replay.Replay(g);
            replay.RemoveSink(result);
            result.SetStrict(strict);
        }

        public static Graph Clone(Graph g)
        {
            Graph copy;
            try
            {
                Class<TWildcardTodoGraph> cls = g.GetType();
                copy = cls.GetConstructor(typeof(string)).NewInstance(g.GetId());
            }
            catch (Exception e)
            {
                logger.Warning(String.Format("Cannot create a graph of %s.", g.GetType().GetName()));
                copy = new AdjacencyListGraph(g.GetId());
            }

            CopyAttributes(g, copy);
            for (int i = 0; i < g.GetNodeCount(); i++)
            {
                Node source = g.GetNode(i);
                Node target = copy.AddNode(source.GetId());
                CopyAttributes(source, target);
            }

            for (int i = 0; i < g.GetEdgeCount(); i++)
            {
                Edge source = g.GetEdge(i);
                Edge target = copy.AddEdge(source.GetId(), source.GetSourceNode().GetId(), source.GetTargetNode().GetId(), source.IsDirected());
                CopyAttributes(source, target);
            }

            return copy;
        }

        public static void CopyAttributes(Element source, Element target)
        {
            source.AttributeKeys().ForEach((key) =>
            {
                object value = source.GetAttribute(key);
                value = CheckedArrayOrCollectionCopy(value);
                target.SetAttribute(key, value);
            });
        }

        private static object CheckedArrayOrCollectionCopy(object o)
        {
            if (o == null)
                return null;
            if (o.GetType().IsArray())
            {
                object c = Array.NewInstance(o.GetType().GetComponentType(), Array.GetLength(o));
                for (int i = 0; i < Array.GetLength(o); i++)
                {
                    object t = CheckedArrayOrCollectionCopy(Array.Get(o, i));
                    Array.Set(c, i, t);
                }

                return c;
            }

            if (typeof(Collection).IsAssignableFrom(o.GetType()))
            {
                Collection<TWildcardTodo> t;
                try
                {
                    t = (Collection<TWildcardTodo>)o.GetType().NewInstance();
                    t.AddAll((Collection)o);
                    return t;
                }
                catch (Exception e)
                {
                    e.PrintStackTrace();
                }
            }

            return o;
        }

        class SynchronizedElement<U> : Element where U : Element
        {
            private static readonly ReentrantLock attributeLock = new ReentrantLock();
            protected readonly U wrappedElement;
            SynchronizedElement(U e)
            {
                this.wrappedElement = e;
            }

            public virtual void SetAttribute(string attribute, params object[] values)
            {
                attributeLock.Lock();
                try
                {
                    wrappedElement.SetAttribute(attribute, values);
                }
                finally
                {
                    attributeLock.Unlock();
                }
            }

            public virtual void SetAttributes(Dictionary<string, object> attributes)
            {
                attributeLock.Lock();
                try
                {
                    wrappedElement.SetAttributes(attributes);
                }
                finally
                {
                    attributeLock.Unlock();
                }
            }

            public virtual void ClearAttributes()
            {
                attributeLock.Lock();
                try
                {
                    wrappedElement.ClearAttributes();
                }
                finally
                {
                    attributeLock.Unlock();
                }
            }

            public virtual Object[] GetArray(string key)
            {
                object[] o;
                attributeLock.Lock();
                try
                {
                    o = wrappedElement.GetArray(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return o;
            }

            public virtual object GetAttribute(string key)
            {
                object o;
                attributeLock.Lock();
                try
                {
                    o = wrappedElement.GetAttribute(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return o;
            }

            public virtual T GetAttribute<T>(string key, Class<T> clazz)
            {
                T o;
                attributeLock.Lock();
                try
                {
                    o = wrappedElement.GetAttribute(key, clazz);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return o;
            }

            public virtual int GetAttributeCount()
            {
                int c;
                attributeLock.Lock();
                try
                {
                    c = wrappedElement.GetAttributeCount();
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return c;
            }

            public virtual Stream<string> AttributeKeys()
            {
                Stream<string> s = null;
                attributeLock.Lock();
                try
                {
                    s = wrappedElement.AttributeKeys();
                    if (!s.Spliterator().HasCharacteristics(Spliterator.CONCURRENT))
                        s = s.Collect(Collectors.ToList()).Stream();
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return s;
            }

            public virtual object GetFirstAttributeOf(params string[] keys)
            {
                object o;
                attributeLock.Lock();
                try
                {
                    o = wrappedElement.GetFirstAttributeOf(keys);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return o;
            }

            public virtual T GetFirstAttributeOf<T>(Class<T> clazz, params string[] keys)
            {
                T o;
                attributeLock.Lock();
                try
                {
                    o = wrappedElement.GetFirstAttributeOf(clazz, keys);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return o;
            }

            public virtual Map<?, ?> GetMap(string key)
            {
                Map <?, ?> o;
                attributeLock.Lock();
                try
                {
                    o = wrappedElement.GetMap(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return o;
            }

            public virtual string GetId()
            {
                return wrappedElement.GetId();
            }

            public virtual int GetIndex()
            {
                return wrappedElement.GetIndex();
            }

            public virtual CharSequence GetLabel(string key)
            {
                CharSequence o;
                attributeLock.Lock();
                try
                {
                    o = wrappedElement.GetLabel(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return o;
            }

            public virtual double GetNumber(string key)
            {
                double o;
                attributeLock.Lock();
                try
                {
                    o = wrappedElement.GetNumber(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return o;
            }

            public virtual IList<TWildcardTodoNumber> GetVector(string key)
            {
                IList<TWildcardTodoNumber> o;
                attributeLock.Lock();
                try
                {
                    o = wrappedElement.GetVector(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return o;
            }

            public virtual bool HasArray(string key)
            {
                bool b;
                attributeLock.Lock();
                try
                {
                    b = wrappedElement.HasArray(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return b;
            }

            public virtual bool HasAttribute(string key)
            {
                bool b;
                attributeLock.Lock();
                try
                {
                    b = wrappedElement.HasAttribute(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return b;
            }

            public virtual bool HasAttribute(string key, Class<TWildcardTodo> clazz)
            {
                bool b;
                attributeLock.Lock();
                try
                {
                    b = wrappedElement.HasAttribute(key, clazz);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return b;
            }

            public virtual bool HasMap(string key)
            {
                bool b;
                attributeLock.Lock();
                try
                {
                    b = wrappedElement.HasMap(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return b;
            }

            public virtual bool HasLabel(string key)
            {
                bool b;
                attributeLock.Lock();
                try
                {
                    b = wrappedElement.HasLabel(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return b;
            }

            public virtual bool HasNumber(string key)
            {
                bool b;
                attributeLock.Lock();
                try
                {
                    b = wrappedElement.HasNumber(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return b;
            }

            public virtual bool HasVector(string key)
            {
                bool b;
                attributeLock.Lock();
                try
                {
                    b = wrappedElement.HasVector(key);
                }
                finally
                {
                    attributeLock.Unlock();
                }

                return b;
            }

            public virtual void RemoveAttribute(string attribute)
            {
                attributeLock.Lock();
                try
                {
                    wrappedElement.RemoveAttribute(attribute);
                }
                finally
                {
                    attributeLock.Unlock();
                }
            }
        }

        class SynchronizedGraph : SynchronizedElement<Graph>, Graph
        {
            readonly ReentrantLock elementLock;
            readonly Dictionary<string, Node> synchronizedNodes;
            readonly Dictionary<string, Edge> synchronizedEdges;
            SynchronizedGraph(Graph g) : base(g)
            {
                elementLock = new ReentrantLock();
                synchronizedNodes = g.Nodes().Collect(Collectors.ToMap(Node.GetId(), (n) => new SynchronizedNode(this, n)));
                synchronizedEdges = g.Edges().Collect(Collectors.ToMap(Edge.GetId(), (e) => new SynchronizedEdge(this, e)));
            }

            public override Stream<Node> Nodes()
            {
                Collection<Node> nodes;
                elementLock.Lock();
                try
                {
                    nodes = new Vector(synchronizedNodes.Values());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return nodes.Stream();
            }

            public override Stream<Edge> Edges()
            {
                Collection<Edge> edges;
                elementLock.Lock();
                try
                {
                    edges = new Vector(synchronizedEdges.Values());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return edges.Stream();
            }

            public override Edge AddEdge(string id, string node1, string node2)
            {
                Edge e;
                Edge se;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.AddEdge(id, node1, node2);
                    se = new SynchronizedEdge(this, e);
                    synchronizedEdges.Put(id, se);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge AddEdge(string id, string from, string to, bool directed)
            {
                Edge e;
                Edge se;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.AddEdge(id, from, to, directed);
                    se = new SynchronizedEdge(this, e);
                    synchronizedEdges.Put(id, se);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge AddEdge(string id, int index1, int index2)
            {
                Edge e;
                Edge se;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.AddEdge(id, index1, index2);
                    se = new SynchronizedEdge(this, e);
                    synchronizedEdges.Put(id, se);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge AddEdge(string id, int fromIndex, int toIndex, bool directed)
            {
                Edge e;
                Edge se;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.AddEdge(id, fromIndex, toIndex, directed);
                    se = new SynchronizedEdge(this, e);
                    synchronizedEdges.Put(id, se);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge AddEdge(string id, Node node1, Node node2)
            {
                Edge e;
                Edge se;
                Node unsyncNode1, unsyncNode2;
                unsyncNode1 = ((SynchronizedElement<Node>)node1).wrappedElement;
                unsyncNode2 = ((SynchronizedElement<Node>)node2).wrappedElement;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.AddEdge(id, unsyncNode1, unsyncNode2);
                    se = new SynchronizedEdge(this, e);
                    synchronizedEdges.Put(id, se);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge AddEdge(string id, Node from, Node to, bool directed)
            {
                Edge e;
                Edge se;
                Node unsyncFrom, unsyncTo;
                unsyncFrom = ((SynchronizedElement<Node>)from).wrappedElement;
                unsyncTo = ((SynchronizedElement<Node>)to).wrappedElement;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.AddEdge(id, unsyncFrom, unsyncTo, directed);
                    se = new SynchronizedEdge(this, e);
                    synchronizedEdges.Put(id, se);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Node AddNode(string id)
            {
                Node n;
                Node sn;
                elementLock.Lock();
                try
                {
                    n = wrappedElement.AddNode(id);
                    sn = new SynchronizedNode(this, n);
                    synchronizedNodes.Put(id, sn);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return sn;
            }

            public override Iterable<AttributeSink> AttributeSinks()
            {
                LinkedList<AttributeSink> sinks = new LinkedList<AttributeSink>();
                elementLock.Lock();
                try
                {
                    foreach (AttributeSink as in wrappedElement.AttributeSinks())
                        sinks.Add(@as);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return sinks;
            }

            public override void Clear()
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.Clear();
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override EdgeFactory<TWildcardTodoEdge> EdgeFactory()
            {
                return wrappedElement.EdgeFactory();
            }

            public override Iterable<ElementSink> ElementSinks()
            {
                LinkedList<ElementSink> sinks = new LinkedList<ElementSink>();
                elementLock.Lock();
                try
                {
                    foreach (ElementSink es in wrappedElement.ElementSinks())
                        sinks.Add(es);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return sinks;
            }

            public override Edge GetEdge(string id)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = synchronizedEdges[id];
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdge(int index)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.GetEdge(index);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e == null ? null : GetEdge(e.GetId());
            }

            public override int GetEdgeCount()
            {
                int c;
                elementLock.Lock();
                try
                {
                    c = synchronizedEdges.Count;
                }
                finally
                {
                    elementLock.Unlock();
                }

                return c;
            }

            public override Node GetNode(string id)
            {
                Node n;
                elementLock.Lock();
                try
                {
                    n = synchronizedNodes[id];
                }
                finally
                {
                    elementLock.Unlock();
                }

                return n;
            }

            public override Node GetNode(int index)
            {
                Node n;
                elementLock.Lock();
                try
                {
                    n = wrappedElement.GetNode(index);
                }
                finally
                {
                    elementLock.Unlock();
                }

                return n == null ? null : GetNode(n.GetId());
            }

            public override int GetNodeCount()
            {
                int c;
                elementLock.Lock();
                try
                {
                    c = synchronizedNodes.Count;
                }
                finally
                {
                    elementLock.Unlock();
                }

                return c;
            }

            public override double GetStep()
            {
                double s;
                elementLock.Lock();
                try
                {
                    s = wrappedElement.GetStep();
                }
                finally
                {
                    elementLock.Unlock();
                }

                return s;
            }

            public override bool IsAutoCreationEnabled()
            {
                return wrappedElement.IsAutoCreationEnabled();
            }

            public virtual Viewer Display()
            {
                return wrappedElement.Display();
            }

            public virtual Viewer Display(bool autoLayout)
            {
                return wrappedElement.Display(autoLayout);
            }

            public override bool IsStrict()
            {
                return wrappedElement.IsStrict();
            }

            public override NodeFactory<TWildcardTodoNode> NodeFactory()
            {
                return wrappedElement.NodeFactory();
            }

            public override void Read(string filename)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.Read(filename);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void Read(FileSource input, string filename)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.Read(input, filename);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override Edge RemoveEdge(string from, string to)
            {
                Edge e;
                Edge se;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.RemoveEdge(from, to);
                    se = synchronizedEdges.Remove(e.GetId());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge RemoveEdge(string id)
            {
                Edge e;
                Edge se;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.RemoveEdge(id);
                    se = synchronizedEdges.Remove(e.GetId());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge RemoveEdge(int index)
            {
                Edge e;
                Edge se;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.RemoveEdge(index);
                    se = synchronizedEdges.Remove(e.GetId());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge RemoveEdge(int fromIndex, int toIndex)
            {
                Edge e;
                Edge se;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.RemoveEdge(fromIndex, toIndex);
                    se = synchronizedEdges.Remove(e.GetId());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge RemoveEdge(Node node1, Node node2)
            {
                Edge e;
                Edge se;
                if (node1 is SynchronizedNode)
                    node1 = ((SynchronizedNode)node1).wrappedElement;
                if (node2 is SynchronizedNode)
                    node2 = ((SynchronizedNode)node1).wrappedElement;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.RemoveEdge(node1, node2);
                    se = synchronizedEdges.Remove(e.GetId());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Edge RemoveEdge(Edge edge)
            {
                Edge e;
                Edge se;
                if (edge is SynchronizedEdge)
                    edge = ((SynchronizedEdge)edge).wrappedElement;
                elementLock.Lock();
                try
                {
                    e = wrappedElement.RemoveEdge(edge);
                    se = synchronizedEdges.Remove(e.GetId());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return se;
            }

            public override Node RemoveNode(string id)
            {
                Node n;
                Node sn;
                elementLock.Lock();
                try
                {
                    n = wrappedElement.RemoveNode(id);
                    sn = synchronizedNodes.Remove(n.GetId());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return sn;
            }

            public override Node RemoveNode(int index)
            {
                Node n;
                Node sn;
                elementLock.Lock();
                try
                {
                    n = wrappedElement.RemoveNode(index);
                    sn = synchronizedNodes.Remove(n.GetId());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return sn;
            }

            public override Node RemoveNode(Node node)
            {
                Node n;
                Node sn;
                if (node is SynchronizedNode)
                    node = ((SynchronizedNode)node).wrappedElement;
                elementLock.Lock();
                try
                {
                    n = wrappedElement.RemoveNode(node);
                    sn = synchronizedNodes.Remove(n.GetId());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return sn;
            }

            public override void SetAutoCreate(bool on)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.SetAutoCreate(on);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void SetEdgeFactory(EdgeFactory<TWildcardTodoEdge> ef)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.SetEdgeFactory(ef);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void SetNodeFactory(NodeFactory<TWildcardTodoNode> nf)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.SetNodeFactory(nf);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void SetStrict(bool on)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.SetStrict(on);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void StepBegins(double time)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.StepBegins(time);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void Write(string filename)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.Write(filename);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void Write(FileSink output, string filename)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.Write(output, filename);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void AddAttributeSink(AttributeSink sink)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.AddAttributeSink(sink);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void AddElementSink(ElementSink sink)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.AddElementSink(sink);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void AddSink(Sink sink)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.AddSink(sink);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void ClearAttributeSinks()
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.ClearAttributeSinks();
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void ClearElementSinks()
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.ClearElementSinks();
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void ClearSinks()
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.ClearSinks();
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void RemoveAttributeSink(AttributeSink sink)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.RemoveAttributeSink(sink);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void RemoveElementSink(ElementSink sink)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.RemoveElementSink(sink);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void RemoveSink(Sink sink)
            {
                elementLock.Lock();
                try
                {
                    wrappedElement.RemoveSink(sink);
                }
                finally
                {
                    elementLock.Unlock();
                }
            }

            public override void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
            {
                wrappedElement.EdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
            }

            public override void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
            {
                wrappedElement.EdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
            }

            public override void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
            {
                wrappedElement.EdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
            }

            public override void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
            {
                wrappedElement.GraphAttributeAdded(sourceId, timeId, attribute, value);
            }

            public override void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
            {
                wrappedElement.GraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
            }

            public override void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
            {
                wrappedElement.GraphAttributeRemoved(sourceId, timeId, attribute);
            }

            public override void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
            {
                wrappedElement.NodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
            }

            public override void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
            {
                wrappedElement.NodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
            }

            public override void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
            {
                wrappedElement.NodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
            }

            public override void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
            {
                wrappedElement.EdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
            }

            public override void EdgeRemoved(string sourceId, long timeId, string edgeId)
            {
                wrappedElement.EdgeRemoved(sourceId, timeId, edgeId);
            }

            public override void GraphCleared(string sourceId, long timeId)
            {
                wrappedElement.GraphCleared(sourceId, timeId);
            }

            public override void NodeAdded(string sourceId, long timeId, string nodeId)
            {
                wrappedElement.NodeAdded(sourceId, timeId, nodeId);
            }

            public override void NodeRemoved(string sourceId, long timeId, string nodeId)
            {
                wrappedElement.NodeRemoved(sourceId, timeId, nodeId);
            }

            public override void StepBegins(string sourceId, long timeId, double step)
            {
                wrappedElement.StepBegins(sourceId, timeId, step);
            }

            public override IEnumerator<Node> Iterator()
            {
                return Nodes().Iterator();
            }
        }

        class SynchronizedNode : SynchronizedElement<Node>, Node
        {
            private readonly SynchronizedGraph sg;
            private readonly ReentrantLock elementLock;
            SynchronizedNode(SynchronizedGraph sg, Node n) : base(n)
            {
                this.sg = sg;
                this.elementLock = new ReentrantLock();
            }

            public override Stream<Node> NeighborNodes()
            {
                IList<Node> nodes;
                elementLock.Lock();
                sg.elementLock.Lock();
                try
                {
                    nodes = wrappedElement.NeighborNodes().Map((n) => sg.GetNode(n.GetIndex())).Collect(Collectors.ToList());
                }
                finally
                {
                    sg.elementLock.Unlock();
                    elementLock.Unlock();
                }

                return nodes.Stream();
            }

            public override Stream<Edge> Edges()
            {
                IList<Edge> edges;
                elementLock.Lock();
                sg.elementLock.Lock();
                try
                {
                    edges = wrappedElement.Edges().Map((e) => sg.GetEdge(e.GetIndex())).Collect(Collectors.ToList());
                }
                finally
                {
                    sg.elementLock.Unlock();
                    elementLock.Unlock();
                }

                return edges.Stream();
            }

            public override Stream<Edge> LeavingEdges()
            {
                IList<Edge> edges;
                elementLock.Lock();
                sg.elementLock.Lock();
                try
                {
                    edges = wrappedElement.LeavingEdges().Map((e) => sg.GetEdge(e.GetIndex())).Collect(Collectors.ToList());
                }
                finally
                {
                    sg.elementLock.Unlock();
                    elementLock.Unlock();
                }

                return edges.Stream();
            }

            public override Stream<Edge> EnteringEdges()
            {
                IList<Edge> edges;
                elementLock.Lock();
                sg.elementLock.Lock();
                try
                {
                    edges = wrappedElement.EnteringEdges().Map((e) => sg.GetEdge(e.GetIndex())).Collect(Collectors.ToList());
                }
                finally
                {
                    sg.elementLock.Unlock();
                    elementLock.Unlock();
                }

                return edges.Stream();
            }

            public override IEnumerator<Node> GetBreadthFirstIterator()
            {
                return GetBreadthFirstIterator(false);
            }

            public override IEnumerator<Node> GetBreadthFirstIterator(bool directed)
            {
                LinkedList<Node> l = new LinkedList<Node>();
                IEnumerator<Node> it;
                elementLock.Lock();
                sg.elementLock.Lock();
                try
                {
                    it = wrappedElement.GetBreadthFirstIterator(directed);
                    while (it.HasNext())
                        l.Add(sg.GetNode(it.Next().GetIndex()));
                }
                finally
                {
                    sg.elementLock.Unlock();
                    elementLock.Unlock();
                }

                return l.Iterator();
            }

            public override int GetDegree()
            {
                int d;
                elementLock.Lock();
                try
                {
                    d = wrappedElement.GetDegree();
                }
                finally
                {
                    elementLock.Unlock();
                }

                return d;
            }

            public override IEnumerator<Node> GetDepthFirstIterator()
            {
                return GetDepthFirstIterator(false);
            }

            public override IEnumerator<Node> GetDepthFirstIterator(bool directed)
            {
                LinkedList<Node> l = new LinkedList<Node>();
                IEnumerator<Node> it;
                elementLock.Lock();
                sg.elementLock.Lock();
                try
                {
                    it = wrappedElement.GetDepthFirstIterator();
                    while (it.HasNext())
                        l.Add(sg.GetNode(it.Next().GetIndex()));
                }
                finally
                {
                    sg.elementLock.Unlock();
                    elementLock.Unlock();
                }

                return l.Iterator();
            }

            public override Edge GetEdge(int i)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdge(i).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEnteringEdge(int i)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEnteringEdge(i).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetLeavingEdge(int i)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetLeavingEdge(i).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdgeBetween(string id)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdgeBetween(id).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdgeBetween(Node n)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdgeBetween(n).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdgeBetween(int index)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdgeBetween(index).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdgeFrom(string id)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdgeFrom(id).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdgeFrom(Node n)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdgeFrom(n).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdgeFrom(int index)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdgeFrom(index).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdgeToward(string id)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdgeToward(id).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdgeToward(Node n)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdgeToward(n).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Edge GetEdgeToward(int index)
            {
                Edge e;
                elementLock.Lock();
                try
                {
                    e = sg.GetEdge(wrappedElement.GetEdgeToward(index).GetIndex());
                }
                finally
                {
                    elementLock.Unlock();
                }

                return e;
            }

            public override Graph GetGraph()
            {
                return sg;
            }

            public override int GetInDegree()
            {
                int d;
                elementLock.Lock();
                try
                {
                    d = wrappedElement.GetInDegree();
                }
                finally
                {
                    elementLock.Unlock();
                }

                return d;
            }

            public override int GetOutDegree()
            {
                int d;
                elementLock.Lock();
                try
                {
                    d = wrappedElement.GetOutDegree();
                }
                finally
                {
                    elementLock.Unlock();
                }

                return d;
            }

            public override IEnumerator<Edge> Iterator()
            {
                return Edges().Iterator();
            }
        }

        class SynchronizedEdge : SynchronizedElement<Edge>, Edge
        {
            readonly SynchronizedGraph sg;
            SynchronizedEdge(SynchronizedGraph sg, Edge e) : base(e)
            {
                this.sg = sg;
            }

            public override Node GetNode0()
            {
                Node n;
                sg.elementLock.Lock();
                try
                {
                    n = sg.GetNode(wrappedElement.GetNode0().GetIndex());
                }
                finally
                {
                    sg.elementLock.Unlock();
                }

                return n;
            }

            public override Node GetNode1()
            {
                Node n;
                sg.elementLock.Lock();
                try
                {
                    n = sg.GetNode(wrappedElement.GetNode1().GetIndex());
                }
                finally
                {
                    sg.elementLock.Unlock();
                }

                return n;
            }

            public override Node GetOpposite(Node node)
            {
                Node n;
                if (node is SynchronizedNode)
                    node = ((SynchronizedNode)node).wrappedElement;
                sg.elementLock.Lock();
                try
                {
                    n = sg.GetNode(wrappedElement.GetOpposite(node).GetIndex());
                }
                finally
                {
                    sg.elementLock.Unlock();
                }

                return n;
            }

            public override Node GetSourceNode()
            {
                Node n;
                sg.elementLock.Lock();
                try
                {
                    n = sg.GetNode(wrappedElement.GetSourceNode().GetIndex());
                }
                finally
                {
                    sg.elementLock.Unlock();
                }

                return n;
            }

            public override Node GetTargetNode()
            {
                Node n;
                sg.elementLock.Lock();
                try
                {
                    n = sg.GetNode(wrappedElement.GetTargetNode().GetIndex());
                }
                finally
                {
                    sg.elementLock.Unlock();
                }

                return n;
            }

            public override bool IsDirected()
            {
                return wrappedElement.IsDirected();
            }

            public override bool IsLoop()
            {
                return wrappedElement.IsLoop();
            }
        }
    }



}
