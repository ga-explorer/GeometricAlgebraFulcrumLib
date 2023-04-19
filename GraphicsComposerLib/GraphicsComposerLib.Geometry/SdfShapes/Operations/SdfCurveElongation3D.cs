using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCurveElongation3D : SdfUnaryOperation
    {
        public Float64Tuple3D Direction { get; set; }
            = new Float64Tuple3D(0, 0, 1);


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var q = point - point.ToTuple3D().ClampToSymmetric(Direction);

            return Surface.GetScalarDistance(q);
        }
    }
}
