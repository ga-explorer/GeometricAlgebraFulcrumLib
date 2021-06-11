using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfTranslate3D : SdfUnaryOperation
    {
        public Tuple3D Direction { get; set; }
            = new Tuple3D(0, 0, 0);

        public override double ComputeSdf(Tuple3D point)
        {
            return Surface.ComputeSdf(point - Direction);
        }
    }
}
