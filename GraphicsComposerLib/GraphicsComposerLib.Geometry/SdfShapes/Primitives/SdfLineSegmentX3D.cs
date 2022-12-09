using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfLineSegmentX3D : ScalarDistanceFunction
    {
        public double Length { get; set; }
            = 1.0d;

        public double Radius { get; set; }
            = 0.1d;


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var v = new Float64Tuple3D(
                point.X - point.X.ClampTo(Length),
                point.Y,
                point.Z
            ).GetVectorNorm();

            return v - Radius;
        }
    }
}
