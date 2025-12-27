using gs_core.Graphstream.Stream;
using gs_core.Graphstream.Stream.File;
using gs_core.Graphstream.Ui.View;
using Java.Io;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Pipelines;
using System.Linq;

namespace Gs_Core.Graphstream.Graph
{
    public interface Graph : Element, Pipe, Iterable<Node>, Structure
    {
        Node GetNode(string id);
        Edge GetEdge(string id);
        NodeFactory<TWildcardTodoNode> NodeFactory();
        EdgeFactory<TWildcardTodoEdge> EdgeFactory();
        bool IsStrict();
        bool IsAutoCreationEnabled();
        double GetStep();
        void SetNodeFactory(NodeFactory<TWildcardTodoNode> nf);
        void SetEdgeFactory(EdgeFactory<TWildcardTodoEdge> ef);
        void SetStrict(bool on);
        void SetAutoCreate(bool on);
        void Clear();
        Node AddNode(string id);
        Edge AddEdge(string id, string node1, string node2)
        {
            return AddEdge(id, node1, node2, false);
        }

        Edge AddEdge(string id, string from, string to, bool directed)
        {
            Node src = GetNode(from);
            Node dst = GetNode(to);
            if (src == null || dst == null)
            {
                if (IsStrict())
                    throw new ElementNotFoundException("Node '%s'", src == null ? from : to);
                if (!IsAutoCreationEnabled())
                    return null;
                if (src == null)
                    src = AddNode(from);
                if (dst == null)
                    dst = AddNode(to);
            }

            return AddEdge(id, src, dst, directed);
        }

        void StepBegins(double time);
        Iterable<AttributeSink> AttributeSinks();
        Iterable<ElementSink> ElementSinks();
        void Read(string filename)
        {
            FileSource input = FileSourceFactory.SourceFor(filename);
            if (input != null)
            {
                input.AddSink(this);
                Read(input, filename);
                input.RemoveSink(this);
            }
            else
            {
                throw new IOException("No source reader for " + filename);
            }
        }

        void Read(FileSource input, string filename)
        {
            input.ReadAll(filename);
        }

        void Write(string filename)
        {
            FileSink output = FileSinkFactory.SinkFor(filename);
            if (output != null)
            {
                Write(output, filename);
            }
            else
            {
                throw new IOException("No sink writer for " + filename);
            }
        }

        void Write(FileSink output, string filename)
        {
            output.WriteAll(this, filename);
        }

        Viewer Display();
        Viewer Display(bool autoLayout);
        Node GetNode(int index);
        Edge GetEdge(int index);
        Edge AddEdge(string id, int index1, int index2)
        {
            return AddEdge(id, GetNode(index1), GetNode(index2), false);
        }

        Edge AddEdge(string id, int fromIndex, int toIndex, bool directed)
        {
            return AddEdge(id, GetNode(fromIndex), GetNode(toIndex), directed);
        }

        Edge AddEdge(string id, Node node1, Node node2)
        {
            return AddEdge(id, node1, node2, false);
        }

        Edge AddEdge(string id, Node from, Node to, bool directed);
        Edge RemoveEdge(int index)
        {
            Edge edge = GetEdge(index);
            if (edge == null)
            {
                if (IsStrict())
                    throw new ElementNotFoundException("Edge #" + index);
                return null;
            }

            return RemoveEdge(edge);
        }

        Edge RemoveEdge(int fromIndex, int toIndex)
        {
            Node fromNode = GetNode(fromIndex);
            Node toNode = GetNode(toIndex);
            if (fromNode == null || toNode == null)
            {
                if (IsStrict())
                    throw new ElementNotFoundException("Node #%d", fromNode == null ? fromIndex : toIndex);
                return null;
            }

            return RemoveEdge(fromNode, toNode);
        }

        Edge RemoveEdge(Node node1, Node node2);
        Edge RemoveEdge(string from, string to)
        {
            Node fromNode = GetNode(from);
            Node toNode = GetNode(to);
            if (fromNode == null || toNode == null)
            {
                if (IsStrict())
                    throw new ElementNotFoundException("Node \"%s\"", fromNode == null ? from : to);
                return null;
            }

            return RemoveEdge(fromNode, toNode);
        }

        Edge RemoveEdge(string id)
        {
            Edge edge = GetEdge(id);
            if (edge == null)
            {
                if (IsStrict())
                    throw new ElementNotFoundException("Edge \"" + id + "\"");
                return null;
            }

            return RemoveEdge(edge);
        }

        Edge RemoveEdge(Edge edge);
        Node RemoveNode(int index)
        {
            Node node = GetNode(index);
            if (node == null)
            {
                if (IsStrict())
                    throw new ElementNotFoundException("Node #" + index);
                return null;
            }

            return RemoveNode(node);
        }

        Node RemoveNode(string id)
        {
            Node node = GetNode(id);
            if (node == null)
            {
                if (IsStrict())
                    throw new ElementNotFoundException("Node \"" + id + "\"");
                return null;
            }

            return RemoveNode(node);
        }

        Node RemoveNode(Node node);
    }


}

