using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines.Immutable
{
    public sealed class Line3D : ILine3D
    {
        public double OriginX { get; }

        public double OriginY { get; }

        public double OriginZ { get; }


        public double DirectionX { get; }

        public double DirectionY { get; }

        public double DirectionZ { get; }


        public bool IsValid()
        {
            return !double.IsNaN(OriginX) &&
                   !double.IsNaN(OriginY) &&
                   !double.IsNaN(OriginZ) &&
                   !double.IsNaN(DirectionX) &&
                   !double.IsNaN(DirectionY) &&
                   !double.IsNaN(DirectionZ);
        }


        public Line3D(double originX, double originY, double originZ, double directionX, double directionY, double directionZ)
        {
            OriginX = originX;
            OriginY = originY;
            OriginZ = originZ;

            DirectionX = directionX;
            DirectionY = directionY;
            DirectionZ = directionZ;
        }

        public Line3D(IFloat64Tuple3D origin, IFloat64Tuple3D direction)
        {
            OriginX = origin.X;
            OriginY = origin.Y;
            OriginZ = origin.Z;

            DirectionX = direction.X;
            DirectionY = direction.Y;
            DirectionZ = direction.Z;

            Debug.Assert(IsValid());
        }


        public Line3D ToLine()
        {
            return this;
        }
    }
}
