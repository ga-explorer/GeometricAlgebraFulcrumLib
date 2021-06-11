using System;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfSurfaceElongation3D : SdfUnaryOperation
    {
        public Tuple3D Direction { get; set; }
            = new Tuple3D(0, 0, 1);


        public override double ComputeSdf(Tuple3D point)
        {
            var q = point.Abs() - Direction;

            return Surface.ComputeSdf(q.Max(0)) - Math.Min(q.ElementsMax(), 0);
        }
    }
}
