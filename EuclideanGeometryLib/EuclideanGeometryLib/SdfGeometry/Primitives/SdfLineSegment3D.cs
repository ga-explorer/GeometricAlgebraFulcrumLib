using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfLineSegment3D : SignedDistanceFunction
    {
        public Tuple3D Point1 { get; set; }
            = new Tuple3D(0, 0, 0);
            
        public Tuple3D Point2 { get; set; }
            = new Tuple3D(1, 0, 0);

        public double Radius { get; set; }
            = 0.1d;

        public Tuple3D Direction 
            => Point2 - Point1;


        public override double ComputeSdf(Tuple3D point)
        {
            var pa = point - Point1; 
            var ba = Direction;

            var h = (pa.DotProduct(ba) / ba.LengthSquared()).ClampToUnit();

            return (pa - ba * h).Length() - Radius;
        }
    }
}
