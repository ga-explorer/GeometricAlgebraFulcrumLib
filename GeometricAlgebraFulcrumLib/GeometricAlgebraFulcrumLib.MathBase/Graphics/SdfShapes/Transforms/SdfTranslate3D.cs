using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfShapes.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfTranslate3D : SdfUnaryOperation
    {
        public Float64Vector3D Direction { get; set; }
            = Float64Vector3D.Create(0, 0, 0);

        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return Surface.GetScalarDistance(point - Direction);
        }
    }
}
