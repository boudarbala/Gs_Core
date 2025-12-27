using Java.Io;
using Java.Net;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;
using static Org.Graphstream.Stream.File.What;
using static Org.Graphstream.Stream.File.TimeFormat;
using static Org.Graphstream.Stream.File.OutputType;
using static Org.Graphstream.Stream.File.OutputPolicy;
using static Org.Graphstream.Stream.File.LayoutPolicy;
using static Org.Graphstream.Stream.File.Quality;
using static Org.Graphstream.Stream.File.Option;
using static Org.Graphstream.Stream.File.AttributeType;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSourceEdge : FileSourceBase
{
    protected int edgeid = 0;
    protected bool directed = false;
    protected HashSet<string> nodes;
    protected string graphName = "EDGE_";
    public FileSourceEdge() : this(false)
    {
    }

    public FileSourceEdge(bool edgesAreDirected) : this(edgesAreDirected, true)
    {
    }

    public FileSourceEdge(bool edgesAreDirected, bool declareNodes)
    {
        directed = edgesAreDirected;
        nodes = declareNodes ? new HashSet<string>() : null;
    }

    protected override void ContinueParsingInInclude()
    {
    }

    public override bool NextEvents()
    {
        string id1 = GetWordOrNumberOrStringOrEolOrEof();
        if (id1.Equals("EOL"))
        {
        }
        else if (id1.Equals("EOF"))
        {
            return false;
        }
        else
        {
            DeclareNode(id1);
            string id2 = GetWordOrNumberOrStringOrEolOrEof();
            while (!id2.Equals("EOL"))
            {
                if (!id1.Equals(id2))
                {
                    string edgeId = Integer.ToString(edgeid++);
                    DeclareNode(id2);
                    SendEdgeAdded(graphName, edgeId, id1, id2, directed);
                }

                id2 = GetWordOrNumberOrStringOrEolOrEof();
            }
        }

        return true;
    }

    protected virtual void DeclareNode(string id)
    {
        if (nodes != null)
        {
            if (!nodes.Contains(id))
            {
                SendNodeAdded(graphName, id);
                nodes.Add(id);
            }
        }
    }

    public override void Begin(string filename)
    {
        base.Begin(filename);
        Init();
    }

    public override void Begin(URL url)
    {
        base.Begin(url);
        Init();
    }

    public override void Begin(InputStream stream)
    {
        base.Begin(stream);
        Init();
    }

    public override void Begin(Reader reader)
    {
        base.Begin(reader);
        Init();
    }

    protected virtual void Init()
    {
        st.EolIsSignificant(true);
        st.CommentChar('#');
        graphName = String.Format("%s_%d", graphName, System.CurrentTimeMillis() + ((long)Math.Random() * 10));
    }

    public virtual bool NextStep()
    {
        return NextEvents();
    }

    public override void End()
    {
        base.End();
    }
}
