using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfPlaneYz3D : SignedDistanceFunction
    {
        public override double ComputeSdf(Tuple3D point)
        {
            return point.X;
        }
    }
}
