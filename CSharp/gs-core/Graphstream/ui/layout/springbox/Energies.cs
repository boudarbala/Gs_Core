using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.Layout.Springbox.ElementType;
using static Org.Graphstream.Ui.Layout.Springbox.EventType;
using static Org.Graphstream.Ui.Layout.Springbox.AttributeChangeEvent;
using static Org.Graphstream.Ui.Layout.Springbox.Mode;
using static Org.Graphstream.Ui.Layout.Springbox.What;
using static Org.Graphstream.Ui.Layout.Springbox.TimeFormat;
using static Org.Graphstream.Ui.Layout.Springbox.OutputType;
using static Org.Graphstream.Ui.Layout.Springbox.OutputPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.LayoutPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Quality;
using static Org.Graphstream.Ui.Layout.Springbox.Option;
using static Org.Graphstream.Ui.Layout.Springbox.AttributeType;
using static Org.Graphstream.Ui.Layout.Springbox.Balise;
using static Org.Graphstream.Ui.Layout.Springbox.GEXFAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.METAAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.GRAPHAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NODESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NODEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.ATTVALUEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.PARENTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EDGESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.SPELLAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.COLORAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.POSITIONAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.SIZEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NODESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EDGEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.THICKNESSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.IDType;
using static Org.Graphstream.Ui.Layout.Springbox.ModeType;
using static Org.Graphstream.Ui.Layout.Springbox.WeightType;
using static Org.Graphstream.Ui.Layout.Springbox.EdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.NodeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.EdgeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.ClassType;
using static Org.Graphstream.Ui.Layout.Springbox.TimeFormatType;
using static Org.Graphstream.Ui.Layout.Springbox.GPXAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.WPTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.LINKAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EMAILAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.PTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.BOUNDSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.FixType;
using static Org.Graphstream.Ui.Layout.Springbox.GraphAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.LocatorAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NodeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.DataAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.PortAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EndPointAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EndPointType;
using static Org.Graphstream.Ui.Layout.Springbox.HyperEdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.KeyAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.KeyDomain;
using static Org.Graphstream.Ui.Layout.Springbox.KeyAttrType;
using static Org.Graphstream.Ui.Layout.Springbox.GraphEvents;
using static Org.Graphstream.Ui.Layout.Springbox.ThreadingModel;
using static Org.Graphstream.Ui.Layout.Springbox.CloseFramePolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Measures;
using static Org.Graphstream.Ui.Layout.Springbox.Token;
using static Org.Graphstream.Ui.Layout.Springbox.Extension;
using static Org.Graphstream.Ui.Layout.Springbox.DefaultEdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.AttrType;
using static Org.Graphstream.Ui.Layout.Springbox.Resolutions;
using static Org.Graphstream.Ui.Layout.Springbox.PropertyType;
using static Org.Graphstream.Ui.Layout.Springbox.Type;
using static Org.Graphstream.Ui.Layout.Springbox.Units;
using static Org.Graphstream.Ui.Layout.Springbox.FillMode;
using static Org.Graphstream.Ui.Layout.Springbox.StrokeMode;
using static Org.Graphstream.Ui.Layout.Springbox.ShadowMode;
using static Org.Graphstream.Ui.Layout.Springbox.VisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextVisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextStyle;
using static Org.Graphstream.Ui.Layout.Springbox.IconMode;
using static Org.Graphstream.Ui.Layout.Springbox.SizeMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextAlignment;
using static Org.Graphstream.Ui.Layout.Springbox.TextBackgroundMode;
using static Org.Graphstream.Ui.Layout.Springbox.ShapeKind;
using static Org.Graphstream.Ui.Layout.Springbox.Shape;
using static Org.Graphstream.Ui.Layout.Springbox.SpriteOrientation;
using static Org.Graphstream.Ui.Layout.Springbox.ArrowShape;
using static Org.Graphstream.Ui.Layout.Springbox.JComponents;

namespace Gs_Core.Graphstream.Ui.Layout.Springbox;

public class Energies
{
    protected double energy;
    protected double lastEnergy;
    protected int energiesBuffer = 256;
    protected double[] energies = new double[energiesBuffer];
    protected int energiesPos = 0;
    protected double energySum = 0;
    public virtual double GetEnergy()
    {
        return lastEnergy;
    }

    public virtual int GetBufferSize()
    {
        return energiesBuffer;
    }

    public virtual double GetStabilization()
    {
        int range = 200;
        double eprev1 = GetPreviousEnergyValue(range);
        double eprev2 = GetPreviousEnergyValue(range - 10);
        double eprev3 = GetPreviousEnergyValue(range - 20);
        double eprev = (eprev1 + eprev2 + eprev3) / 3;
        double diff = Math.Abs(lastEnergy - eprev);
        diff = diff < 1 ? 1 : diff;
        return 1 / diff;
    }

    public virtual double GetAverageEnergy()
    {
        return energySum / energies.Length;
    }

    public virtual double GetPreviousEnergyValue(int stepsBack)
    {
        if (stepsBack >= energies.Length)
            stepsBack = energies.Length - 1;
        int pos = (energies.Length + (energiesPos - stepsBack)) % energies.Length;
        return energies[pos];
    }

    public virtual void AccumulateEnergy(double value)
    {
        energy += value;
    }

    public virtual void StoreEnergy()
    {
        energiesPos = (energiesPos + 1) % energies.Length;
        energySum -= energies[energiesPos];
        energies[energiesPos] = energy;
        energySum += energy;
        lastEnergy = energy;
        energy = 0;
    }

    protected virtual void ClearEnergies()
    {
        for (int i = 0; i < energies.Length; ++i)
            energies[i] = ((Math.Random() * 2000) - 1000);
    }
}
