using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderZ3D : ScalarDistanceFunction
    {
        public Float64Tuple2D CenterXy { get; set; }
            = new Float64Tuple2D(0, 0);

        public double Radius { get; set; }


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return (point.XyToTuple2D() - CenterXy).GetVectorNorm() - Radius;
        }
    }
}
