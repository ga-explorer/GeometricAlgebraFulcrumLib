using System;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRotateY3D : SdfUnaryOperation
    {
        public double Angle { get; set; }
            = 0;
        
        public override double ComputeSdf(Tuple3D point)
        {
            var cosAngle = Math.Cos(-Angle);
            var sinAngle = Math.Sin(-Angle);

            var q = new Tuple3D(
                point.X * cosAngle + point.Z * sinAngle,
                point.Y,
                point.Z * cosAngle - point.X * sinAngle
            );

            return Surface.ComputeSdf(q);
        }
    }
}
