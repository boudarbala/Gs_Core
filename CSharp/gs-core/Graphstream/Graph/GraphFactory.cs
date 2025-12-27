using Java.Util.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public class GraphFactory
{
    private static readonly Logger logger = Logger.GetLogger(typeof(GraphFactory).GetSimpleName());
    public GraphFactory()
    {
    }

    public virtual Graph NewInstance(string id, string graphClass)
    {
        try
        {
            string completeGraphClass;
            if (graphClass.Split("[.]").Length < 2)
            {
                completeGraphClass = "org.graphstream.graph.implementations." + graphClass;
            }
            else
            {
                completeGraphClass = graphClass;
            }

            Class<TWildcardTodo> clazz = Class.ForName(completeGraphClass);
            Graph res = (Graph)clazz.GetConstructor(typeof(string)).NewInstance(id);
            return res;
        }
        catch (Exception e)
        {
            logger.Log(Level.SEVERE, "Error executing GraphFactory#newInstance.", e);
        }

        return null;
    }
}
