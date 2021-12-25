using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfTranslate3D : SdfUnaryOperation
    {
        public Tuple3D Direction { get; set; }
            = new Tuple3D(0, 0, 0);

        public override double GetScalarDistance(ITuple3D point)
        {
            return Surface.GetScalarDistance(point - Direction);
        }
    }
}
