using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Statistics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Computers;

public static class GeometryComputersUtils
{
    public static EventSummaryCollection GlobalEventCounters { get; }
        = new EventSummaryCollection();


    public static LineTraversalData2D GetLineTraversalData(this IFloat64Line2D line)
    {
        return new LineTraversalData2D(line);
    }

    public static LineTraversalData2D GetLineTraversalData(this IFloat64Line2D line, double lineParamMinValue, double lineParamMaxValue)
    {
        return new LineTraversalData2D(
            line,
            lineParamMinValue,
            lineParamMaxValue
        );
    }

    public static LineTraversalData2D GetLineTraversalData(this IFloat64Line2D line, Float64ScalarRange lineParamRange)
    {
        return new LineTraversalData2D(
            line,
            lineParamRange.MinValue,
            lineParamRange.MaxValue
        );
    }


    public static LineTraversalData3D GetLineTraversalData(this IFloat64Line3D line)
    {
        return new LineTraversalData3D(line);
    }

    public static LineTraversalData3D GetLineTraversalData(this IFloat64Line3D line, double lineParamMinValue, double lineParamMaxValue)
    {
        return new LineTraversalData3D(
            line,
            lineParamMinValue, 
            lineParamMaxValue
        );
    }

    public static LineTraversalData3D GetLineTraversalData(this IFloat64Line3D line, Float64ScalarRange lineParamRange)
    {
        return new LineTraversalData3D(
            line,
            lineParamRange.MinValue,
            lineParamRange.MaxValue
        );
    }
}