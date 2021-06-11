using System;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRotateX3D : SdfUnaryOperation
    {
        public double Angle { get; set; }
            = 0;
        
        public override double ComputeSdf(Tuple3D point)
        {
            var cosAngle = Math.Cos(-Angle);
            var sinAngle = Math.Sin(-Angle);

            var q = new Tuple3D(
                point.X,
                point.Y * cosAngle - point.Z * sinAngle,
                point.Z * cosAngle + point.Y * sinAngle
            );

            return Surface.ComputeSdf(q);
        }
    }
}
