using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Operations;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfAnd3D : SdfAggregation
{
    public override double GetScalarDistance(IFloat64Vector3D point)
    {
        return Surfaces.Max(s => s.GetScalarDistance(point));
    }
}