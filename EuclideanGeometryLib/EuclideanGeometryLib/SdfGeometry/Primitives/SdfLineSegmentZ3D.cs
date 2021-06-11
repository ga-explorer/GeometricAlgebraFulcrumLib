using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfLineSegmentZ3D : SignedDistanceFunction
    {
        public double Length { get; set; }
            = 1.0d;

        public double Radius { get; set; }
            = 0.1d;


        public override double ComputeSdf(Tuple3D point)
        {
            var v = new Tuple3D(
                point.X,
                point.Y,
                point.Z - point.Z.ClampTo(Length)
            ).Length();

            return v - Radius;
        }
    }
}
