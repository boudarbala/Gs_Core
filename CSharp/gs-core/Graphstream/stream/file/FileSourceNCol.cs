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
using static Org.Graphstream.Stream.File.Balise;
using static Org.Graphstream.Stream.File.GEXFAttribute;
using static Org.Graphstream.Stream.File.METAAttribute;
using static Org.Graphstream.Stream.File.GRAPHAttribute;
using static Org.Graphstream.Stream.File.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.NODESAttribute;
using static Org.Graphstream.Stream.File.NODEAttribute;
using static Org.Graphstream.Stream.File.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.PARENTAttribute;
using static Org.Graphstream.Stream.File.EDGESAttribute;
using static Org.Graphstream.Stream.File.SPELLAttribute;
using static Org.Graphstream.Stream.File.COLORAttribute;
using static Org.Graphstream.Stream.File.POSITIONAttribute;
using static Org.Graphstream.Stream.File.SIZEAttribute;
using static Org.Graphstream.Stream.File.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.EDGEAttribute;
using static Org.Graphstream.Stream.File.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.IDType;
using static Org.Graphstream.Stream.File.ModeType;
using static Org.Graphstream.Stream.File.WeightType;
using static Org.Graphstream.Stream.File.EdgeType;
using static Org.Graphstream.Stream.File.NodeShapeType;
using static Org.Graphstream.Stream.File.EdgeShapeType;
using static Org.Graphstream.Stream.File.ClassType;
using static Org.Graphstream.Stream.File.TimeFormatType;
using static Org.Graphstream.Stream.File.GPXAttribute;
using static Org.Graphstream.Stream.File.WPTAttribute;
using static Org.Graphstream.Stream.File.LINKAttribute;
using static Org.Graphstream.Stream.File.EMAILAttribute;
using static Org.Graphstream.Stream.File.PTAttribute;
using static Org.Graphstream.Stream.File.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.FixType;
using static Org.Graphstream.Stream.File.GraphAttribute;
using static Org.Graphstream.Stream.File.LocatorAttribute;
using static Org.Graphstream.Stream.File.NodeAttribute;
using static Org.Graphstream.Stream.File.EdgeAttribute;
using static Org.Graphstream.Stream.File.DataAttribute;
using static Org.Graphstream.Stream.File.PortAttribute;
using static Org.Graphstream.Stream.File.EndPointAttribute;
using static Org.Graphstream.Stream.File.EndPointType;
using static Org.Graphstream.Stream.File.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.KeyAttribute;
using static Org.Graphstream.Stream.File.KeyDomain;
using static Org.Graphstream.Stream.File.KeyAttrType;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSourceNCol : FileSourceBase
{
    protected int edgeid = 0;
    protected HashSet<string> nodes;
    protected string graphName = "NCOL_";
    public FileSourceNCol() : this(false)
    {
    }

    public FileSourceNCol(bool declareNodes)
    {
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
            if (!id2.Equals("EOL") && !id2.Equals("EOF"))
            {
                if (!id1.Equals(id2))
                {
                    string weight = GetWordOrNumberOrStringOrEolOrEof();
                    double w = 0;
                    if (weight.Equals("EOL") || weight.Equals("EOF"))
                    {
                        weight = null;
                        PushBack();
                    }
                    else
                    {
                        try
                        {
                            w = Double.ParseDouble(weight);
                        }
                        catch (Exception e)
                        {
                            throw new IOException(String.Format("cannot transform weight %s into a number", weight));
                        }
                    }

                    string edgeId = Integer.ToString(edgeid++);
                    DeclareNode(id2);
                    SendEdgeAdded(graphName, edgeId, id1, id2, false);
                    if (weight != null)
                        SendEdgeAttributeAdded(graphName, edgeId, "weight", (Double)w);
                }
            }
            else
            {
                throw new IOException("unexpected EOL or EOF");
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
