using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfPlane3D : SignedDistanceFunction
    {
        public Tuple3D Point { get; set; }
            = new Tuple3D(0, 0, 0);

        public Tuple3D UnitNormal { get; set; }
            = new Tuple3D(0, 0, 1);

        public override double ComputeSdf(Tuple3D point)
        {
            return point.DotProduct(UnitNormal) - Point.DotProduct(UnitNormal);
        }
    }
}
