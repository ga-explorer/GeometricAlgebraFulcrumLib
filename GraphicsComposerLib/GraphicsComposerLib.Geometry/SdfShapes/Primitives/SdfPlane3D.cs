using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfPlane3D : ScalarDistanceFunction
    {
        public Tuple3D Point { get; set; }
            = new Tuple3D(0, 0, 0);

        public Tuple3D UnitNormal { get; set; }
            = new Tuple3D(0, 0, 1);

        public override double GetScalarDistance(ITuple3D point)
        {
            return point.VectorDot(UnitNormal) - Point.VectorDot(UnitNormal);
        }
    }
}
