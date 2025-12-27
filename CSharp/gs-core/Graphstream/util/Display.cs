using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Ui.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.ElementType;

namespace Gs_Core.Graphstream.Util;

public interface Display
{
    static Display GetDefault()
    {
        string uiModule = System.GetProperty("org.graphstream.ui");
        if (uiModule == null)
        {
            throw new MissingDisplayException("No UI package detected! " + "Please use System.setProperty(\"org.graphstream.ui\") for the selected package.");
        }
        else
        {
            Display display = null;
            string[] candidates = new[]
            {
                uiModule,
                uiModule + ".util.Display",
                "org.graphstream.ui." + uiModule + ".util.Display"
            };
            foreach (string candidate in candidates)
            {
                try
                {
                    Class<TWildcardTodo> clazz = Class.ForName(candidate);
                    object object = clazz.NewInstance();
                    if (@object is Display)
                    {
                        display = (Display)@object;
                        break;
                    }
                }
                catch (ClassNotFoundException e)
                {
                    continue;
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to create object", e);
                }
            }

            if (display == null)
            {
                throw new MissingDisplayException("No valid display found. " + "Please check your System.setProperty(\"org.graphstream.ui\") statement.");
            }
            else
            {
                return display;
            }
        }
    }

    Viewer Display(Graph graph, bool autoLayout);
}
