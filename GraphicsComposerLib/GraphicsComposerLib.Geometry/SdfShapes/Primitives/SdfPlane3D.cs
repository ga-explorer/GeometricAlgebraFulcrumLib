using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfPlane3D : ScalarDistanceFunction
    {
        public Float64Tuple3D Point { get; set; }
            = new Float64Tuple3D(0, 0, 0);

        public Float64Tuple3D UnitNormal { get; set; }
            = new Float64Tuple3D(0, 0, 1);

        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return point.VectorDot(UnitNormal) - Point.VectorDot(UnitNormal);
        }
    }
}
