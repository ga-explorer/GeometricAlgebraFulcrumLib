using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfPlaneYz3D : ScalarDistanceFunction
{
    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        return point.X;
    }
}