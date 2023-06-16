using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfShapes.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCurveElongation3D : SdfUnaryOperation
    {
        public Float64Vector3D Direction { get; set; }
            = Float64Vector3D.Create(0, 0, 1);


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var q = point - point.ToVector3D().ClampToSymmetric(Direction);

            return Surface.GetScalarDistance(q);
        }
    }
}
