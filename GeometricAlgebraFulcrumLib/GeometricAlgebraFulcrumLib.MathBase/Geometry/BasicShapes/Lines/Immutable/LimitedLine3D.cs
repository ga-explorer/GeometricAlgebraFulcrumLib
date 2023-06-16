using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines.Immutable
{
    public sealed class LimitedLine3D : ILimitedLine3D
    {
        public static LimitedLine3D Create(double originX, double originY, double originZ, double directionX, double directionY, double directionZ, double minParamValue, double maxParamValue)
        {
            return new LimitedLine3D(
                originX,
                originY,
                originZ,
                directionX,
                directionY,
                directionZ,
                minParamValue,
                maxParamValue
            );
        }

        public static LimitedLine3D Create(IFloat64Tuple3D origin, IFloat64Tuple3D direction, Float64Range1D parameterLimits)
        {
            return new LimitedLine3D(
                origin.X,
                origin.Y,
                origin.Z,
                direction.X,
                direction.Y,
                direction.Z,
                parameterLimits.MinValue,
                parameterLimits.MaxValue
            );
        }


        public double OriginX { get; }

        public double OriginY { get; }

        public double OriginZ { get; }


        public double DirectionX { get; }

        public double DirectionY { get; }

        public double DirectionZ { get; }


        public double ParameterMinValue { get; }

        public double ParameterMaxValue { get; }


        public bool IsValid()
        {
            return !double.IsNaN(OriginX) &&
                   !double.IsNaN(OriginY) &&
                   !double.IsNaN(OriginZ) &&
                   !double.IsNaN(DirectionX) &&
                   !double.IsNaN(DirectionY) &&
                   !double.IsNaN(DirectionZ) &&
                   !double.IsNaN(ParameterMinValue) &&
                   !double.IsNaN(ParameterMaxValue);
        }


        internal LimitedLine3D(double originX, double originY, double originZ, double directionX, double directionY, double directionZ, double minParamValue, double maxParamValue)
        {
            OriginX = originX;
            OriginY = originY;
            OriginZ = originZ;

            DirectionX = directionX;
            DirectionY = directionY;
            DirectionZ = directionZ;

            ParameterMinValue = minParamValue;
            ParameterMaxValue = maxParamValue;
        }


        public Line3D ToLine()
        {
            return new Line3D(
                OriginX,
                OriginY,
                OriginZ,
                DirectionX,
                DirectionY,
                DirectionZ
            );
        }
    }
}