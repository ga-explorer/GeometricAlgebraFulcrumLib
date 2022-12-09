using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfPlaneXy3D : ScalarDistanceFunction
    {
        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return point.Z;
        }
    }
}
