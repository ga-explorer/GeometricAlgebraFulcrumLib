using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCurveElongation3D : SdfUnaryOperation
    {
        public Tuple3D Direction { get; set; }
            = new Tuple3D(0, 0, 1);


        public override double GetScalarDistance(ITuple3D point)
        {
            var q = point - point.ToTuple3D().ClampToSymmetric(Direction);

            return Surface.GetScalarDistance(q);
        }
    }
}
