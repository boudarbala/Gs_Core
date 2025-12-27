using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public class AttributePipe : PipeBase
{
    protected AttributePredicate globalPredicate = new FalsePredicate();
    protected AttributePredicate graphPredicate = new FalsePredicate();
    protected AttributePredicate nodePredicate = new FalsePredicate();
    protected AttributePredicate edgePredicate = new FalsePredicate();
    public virtual void SetGlobalAttributeFilter(AttributePredicate filter)
    {
        if (filter == null)
            globalPredicate = new FalsePredicate();
        else
            globalPredicate = filter;
    }

    public virtual void SetGraphAttributeFilter(AttributePredicate filter)
    {
        if (filter == null)
            graphPredicate = new FalsePredicate();
        else
            graphPredicate = filter;
    }

    public virtual void SetNodeAttributeFilter(AttributePredicate filter)
    {
        if (filter == null)
            nodePredicate = new FalsePredicate();
        else
            nodePredicate = filter;
    }

    public virtual void SetEdgeAttributeFilter(AttributePredicate filter)
    {
        if (filter == null)
            edgePredicate = new FalsePredicate();
        else
            edgePredicate = filter;
    }

    public virtual AttributePredicate GetGlobalAttributeFilter()
    {
        return globalPredicate;
    }

    public virtual AttributePredicate GetGraphAttributeFilter()
    {
        return graphPredicate;
    }

    public virtual AttributePredicate GetNodeAttributeFilter()
    {
        return nodePredicate;
    }

    public virtual AttributePredicate GetEdgeAttributeFilter()
    {
        return edgePredicate;
    }

    public override void EdgeAttributeAdded(string graphId, long timeId, string edgeId, string attribute, object value)
    {
        if (!edgePredicate.Matches(attribute, value))
        {
            if (!globalPredicate.Matches(attribute, value))
            {
                SendEdgeAttributeAdded(graphId, timeId, edgeId, attribute, value);
            }
        }
    }

    public override void EdgeAttributeChanged(string graphId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        if (!edgePredicate.Matches(attribute, newValue))
        {
            if (!globalPredicate.Matches(attribute, newValue))
            {
                SendEdgeAttributeChanged(graphId, timeId, edgeId, attribute, oldValue, newValue);
            }
        }
    }

    public override void EdgeAttributeRemoved(string graphId, long timeId, string edgeId, string attribute)
    {
        if (!edgePredicate.Matches(attribute, null))
        {
            if (!globalPredicate.Matches(attribute, null))
            {
                SendEdgeAttributeRemoved(graphId, timeId, edgeId, attribute);
            }
        }
    }

    public override void GraphAttributeAdded(string graphId, long timeId, string attribute, object value)
    {
        if (!graphPredicate.Matches(attribute, value))
        {
            if (!globalPredicate.Matches(attribute, value))
            {
                SendGraphAttributeAdded(graphId, timeId, attribute, value);
            }
        }
    }

    public override void GraphAttributeChanged(string graphId, long timeId, string attribute, object oldValue, object newValue)
    {
        if (!graphPredicate.Matches(attribute, newValue))
        {
            if (!globalPredicate.Matches(attribute, newValue))
            {
                SendGraphAttributeChanged(graphId, timeId, attribute, oldValue, newValue);
            }
        }
    }

    public override void GraphAttributeRemoved(string graphId, long timeId, string attribute)
    {
        if (!graphPredicate.Matches(attribute, null))
        {
            if (!globalPredicate.Matches(attribute, null))
            {
                SendGraphAttributeRemoved(graphId, timeId, attribute);
            }
        }
    }

    public override void NodeAttributeAdded(string graphId, long timeId, string nodeId, string attribute, object value)
    {
        if (!nodePredicate.Matches(attribute, value))
        {
            if (!globalPredicate.Matches(attribute, value))
            {
                SendNodeAttributeAdded(graphId, timeId, nodeId, attribute, value);
            }
        }
    }

    public override void NodeAttributeChanged(string graphId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        if (!nodePredicate.Matches(attribute, newValue))
        {
            if (!globalPredicate.Matches(attribute, newValue))
            {
                SendNodeAttributeChanged(graphId, timeId, nodeId, attribute, oldValue, newValue);
            }
        }
    }

    public override void NodeAttributeRemoved(string graphId, long timeId, string nodeId, string attribute)
    {
        if (!nodePredicate.Matches(attribute, null))
        {
            if (!globalPredicate.Matches(attribute, null))
            {
                SendNodeAttributeRemoved(graphId, timeId, nodeId, attribute);
            }
        }
    }

    protected class FalsePredicate : AttributePredicate
    {
        public virtual bool Matches(string attributeName, object attributeValue)
        {
            return false;
        }
    }
}
