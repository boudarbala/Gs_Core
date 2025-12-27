using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public interface AttributeSink
{
    void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value);
    void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue);
    void GraphAttributeRemoved(string sourceId, long timeId, string attribute);
    void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value);
    void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue);
    void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute);
    void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value);
    void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue);
    void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute);
}
