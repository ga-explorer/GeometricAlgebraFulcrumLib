using DataStructuresLib.Statistics;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.Borders.Space1D;

namespace NumericalGeometryLib.Computers
{
    public static class GeometryComputersUtils
    {
        public static EventSummaryCollection GlobalEventCounters { get; }
            = new EventSummaryCollection();


        public static LineTraversalData2D GetLineTraversalData(this ILine2D line)
        {
            return new LineTraversalData2D(line);
        }

        public static LineTraversalData2D GetLineTraversalData(this ILine2D line, double lineParamMinValue, double lineParamMaxValue)
        {
            return new LineTraversalData2D(
                line,
                lineParamMinValue,
                lineParamMaxValue
            );
        }

        public static LineTraversalData2D GetLineTraversalData(this ILine2D line, IBoundingBox1D lineParamRange)
        {
            return new LineTraversalData2D(
                line,
                lineParamRange.MinValue,
                lineParamRange.MaxValue
            );
        }


        public static LineTraversalData3D GetLineTraversalData(this ILine3D line)
        {
            return new LineTraversalData3D(line);
        }

        public static LineTraversalData3D GetLineTraversalData(this ILine3D line, double lineParamMinValue, double lineParamMaxValue)
        {
            return new LineTraversalData3D(
                line,
                lineParamMinValue, 
                lineParamMaxValue
            );
        }

        public static LineTraversalData3D GetLineTraversalData(this ILine3D line, IBoundingBox1D lineParamRange)
        {
            return new LineTraversalData3D(
                line,
                lineParamRange.MinValue,
                lineParamRange.MaxValue
            );
        }
    }
}
