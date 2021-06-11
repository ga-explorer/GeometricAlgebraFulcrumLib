using System;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRotate3D : SdfUnaryOperation
    {
        public double Angle { get; set; }
            = 0;

        public Tuple3D AxisOrigin { get; set; }
            = new Tuple3D(0, 0, 0);

        public Tuple3D AxisUnitDirection { get; set; }
            = new Tuple3D(1, 0, 0);

        public override double ComputeSdf(Tuple3D point)
        {
            var cosAngle = Math.Cos(-Angle);
            var sinAngle = Math.Sin(-Angle);
            var v = point - AxisOrigin;
            var q = 
                AxisOrigin + 
                cosAngle * v +
                (1 - cosAngle) * AxisUnitDirection.DotProduct(v) * AxisUnitDirection +
                sinAngle * AxisUnitDirection.CrossProduct(v);

            return Surface.ComputeSdf(q);
        }
    }
}
