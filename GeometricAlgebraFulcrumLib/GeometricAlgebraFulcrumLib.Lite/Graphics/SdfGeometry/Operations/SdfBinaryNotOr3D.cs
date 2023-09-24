using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfBinaryNotOr3D : SdfBinaryOperation
    {
        public override double GetScalarDistance(IFloat64Vector3D point)
        {
            return Math.Min(
                -Surface1.GetScalarDistance(point),
                Surface2.GetScalarDistance(point)
            );
        }
    }
}
