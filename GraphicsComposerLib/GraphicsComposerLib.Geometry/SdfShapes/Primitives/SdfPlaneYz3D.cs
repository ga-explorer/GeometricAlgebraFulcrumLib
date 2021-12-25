using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfPlaneYz3D : ScalarDistanceFunction
    {
        public override double GetScalarDistance(ITuple3D point)
        {
            return point.X;
        }
    }
}
