using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfTorusX3D : SignedDistanceFunction
    {
        public double InnerRadius { get; set; }
            = 0.5d;
        
        public double OuterRadius { get; set; }
            = 1.0d;


        public override double ComputeSdf(Tuple3D point)
        {
            var q = new Tuple2D(point.LengthYz() - InnerRadius, point.X);

            return q.Length() - OuterRadius;
        }
    }
}
