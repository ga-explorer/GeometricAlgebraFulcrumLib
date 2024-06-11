using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Operations;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfOr3D : SdfAggregation
{
    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        return Surfaces.Min(s => s.GetScalarDistance(point));
    }
}