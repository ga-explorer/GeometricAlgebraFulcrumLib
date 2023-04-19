using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfLineSegmentY3D : ScalarDistanceFunction
    {
        public double Length { get; set; }
            = 1.0d;

        public double Radius { get; set; }
            = 0.1d;


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var v = new Float64Tuple3D(
                point.X,
                point.Y - point.Y.ClampTo(Length),
                point.Z
            ).GetVectorNorm();

            return v - Radius;
        }
    }
}
