using System;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Primitives
{

    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfBox3D : SignedDistanceFunction
    {
        public Tuple3D SideHalfLengths { get; set; }
            = new Tuple3D(1, 1, 1);
            

        public override double ComputeSdf(Tuple3D point)
        {
            var d = point.Abs() - SideHalfLengths;

            var v1 = d.Max(0.0d).Length();
            
            // remove v2 for an only partially signed sdf 
            var v2 = Math.Min(Math.Max(d.X, Math.Max(d.Y, d.Z)), 0.0d);

            return v1 + v2;
        }
    }
}
