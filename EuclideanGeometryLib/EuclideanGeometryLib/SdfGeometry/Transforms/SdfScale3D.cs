using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfScale3D : SdfUnaryOperation
    {
        public double Factor { get; set; }
            = 1.0d;

        public override double ComputeSdf(Tuple3D point)
        {
            return Factor * Surface.ComputeSdf(point / Factor);
        }
    }
}
