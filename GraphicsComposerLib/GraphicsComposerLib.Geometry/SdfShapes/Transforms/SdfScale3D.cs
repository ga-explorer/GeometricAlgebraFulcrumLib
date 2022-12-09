using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfScale3D : SdfUnaryOperation
    {
        public double Factor { get; set; }
            = 1.0d;

        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var s = 1d / Factor;
            var p = new Float64Tuple3D(
                point.X * s,
                point.Y * s,
                point.Z * s
            );

            return Factor * Surface.GetScalarDistance(p);
        }
    }
}
