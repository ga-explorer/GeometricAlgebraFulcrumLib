using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.Borders.Space1D;

namespace NumericalGeometryLib.BasicShapes.Lines.Immutable
{
    public sealed class LimitedLine2D : ILimitedLine2D
    {
        public static LimitedLine2D Create(double originX, double originY, double directionX, double directionY, double minParamValue, double maxParamValue)
        {
            return new LimitedLine2D(
                originX,
                originY,
                directionX,
                directionY,
                minParamValue,
                maxParamValue
            );
        }

        public static LimitedLine2D Create(IFloat64Tuple2D origin, IFloat64Tuple2D direction, IBoundingBox1D parameterLimits)
        {
            return new LimitedLine2D(
                origin.X,
                origin.Y,
                direction.X,
                direction.Y,
                parameterLimits.MinValue,
                parameterLimits.MaxValue
            );
        }


        public double OriginX { get; }

        public double OriginY { get; }


        public double DirectionX { get; }

        public double DirectionY { get; }


        public double ParameterMinValue { get; }

        public double ParameterMaxValue { get; }


        public bool IsValid()
        {
            return !double.IsNaN(OriginX) &&
                   !double.IsNaN(OriginY) &&
                   !double.IsNaN(DirectionX) &&
                   !double.IsNaN(DirectionY) &&
                   !double.IsNaN(ParameterMinValue) &&
                   !double.IsNaN(ParameterMaxValue);
        }


        internal LimitedLine2D(double originX, double originY, double directionX, double directionY, double minParamValue, double maxParamValue)
        {
            OriginX = originX;
            OriginY = originY;

            DirectionX = directionX;
            DirectionY = directionY;

            ParameterMinValue = minParamValue;
            ParameterMaxValue = maxParamValue;
        }


        public Line2D ToLine()
        {
            return new Line2D(
                OriginX,
                OriginY,
                DirectionX,
                DirectionY
            );
        }
    }
}