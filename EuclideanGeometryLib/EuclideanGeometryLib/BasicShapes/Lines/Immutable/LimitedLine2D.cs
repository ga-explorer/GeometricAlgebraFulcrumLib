using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.Borders.Space1D;

namespace EuclideanGeometryLib.BasicShapes.Lines.Immutable
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

        public static LimitedLine2D Create(ITuple2D origin, ITuple2D direction, IBoundingBox1D parameterLimits)
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


        public bool IsValid
            => !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(DirectionX) &&
               !double.IsNaN(DirectionY) &&
               !double.IsNaN(ParameterMinValue) &&
               !double.IsNaN(ParameterMaxValue);

        public bool IsInvalid
            => double.IsNaN(OriginX) || 
               double.IsNaN(OriginY) ||
               double.IsNaN(DirectionX) || 
               double.IsNaN(DirectionY) ||
               double.IsNaN(ParameterMinValue) || 
               double.IsNaN(ParameterMaxValue);


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
            => new Line2D(
                OriginX,
                OriginY,
                DirectionX,
                DirectionY
            );
    }
}