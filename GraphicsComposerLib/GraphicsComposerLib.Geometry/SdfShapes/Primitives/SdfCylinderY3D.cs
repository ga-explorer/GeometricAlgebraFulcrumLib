using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderY3D : ScalarDistanceFunction
    {
        public Float64Tuple2D CenterXz { get; set; }
            = new Float64Tuple2D(0, 0);

        public double Radius { get; set; }


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return (point.XzToTuple2D() - CenterXz).GetVectorNorm() - Radius;
        }
    }
}
