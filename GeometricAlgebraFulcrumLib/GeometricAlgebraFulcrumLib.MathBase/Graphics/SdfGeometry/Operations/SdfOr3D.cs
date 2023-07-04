using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfGeometry.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfOr3D : SdfAggregation
    {
        public override double GetScalarDistance(IFloat64Vector3D point)
        {
            return Surfaces.Min(s => s.GetScalarDistance(point));
        }
    }
}
