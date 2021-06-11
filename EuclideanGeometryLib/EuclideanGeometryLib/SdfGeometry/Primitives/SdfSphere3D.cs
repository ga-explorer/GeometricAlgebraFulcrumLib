using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfSphere3D : SignedDistanceFunction
    {
        public double Radius { get; set; } = 1;


        public override double ComputeSdf(Tuple3D point)
        {
            var sdf = point.Length() - Radius;
            return SdfAlpha * sdf - SdfDelta;
        }
    }
}
