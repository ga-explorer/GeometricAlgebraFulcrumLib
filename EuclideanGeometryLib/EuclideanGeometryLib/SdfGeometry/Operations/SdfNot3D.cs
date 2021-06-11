using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfNot3D : SdfUnaryOperation
    {
        public override double ComputeSdf(Tuple3D point)
        {
            return -Surface.ComputeSdf(point);
        }
    }
}
