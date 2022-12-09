using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfTorusX3D : ScalarDistanceFunction
    {
        public double InnerRadius { get; set; }
            = 0.5d;
        
        public double OuterRadius { get; set; }
            = 1.0d;


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var q = new Float64Tuple2D(point.LengthYz() - InnerRadius, point.X);

            return q.GetVectorNorm() - OuterRadius;
        }
    }
}
