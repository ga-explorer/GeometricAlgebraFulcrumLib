using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfSphere3D : ScalarDistanceFunction
    {
        public double Radius { get; set; } = 0.5d;


        public override double GetScalarDistance(ITuple3D point)
        {
            var sdf = point.GetLength() - Radius;
            return SdfAlpha * sdf - SdfDelta;
        }
    }
}
