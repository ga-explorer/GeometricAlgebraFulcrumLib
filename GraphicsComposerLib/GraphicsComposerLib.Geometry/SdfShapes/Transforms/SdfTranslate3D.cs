using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfTranslate3D : SdfUnaryOperation
    {
        public Float64Tuple3D Direction { get; set; }
            = new Float64Tuple3D(0, 0, 0);

        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return Surface.GetScalarDistance(point - Direction);
        }
    }
}
