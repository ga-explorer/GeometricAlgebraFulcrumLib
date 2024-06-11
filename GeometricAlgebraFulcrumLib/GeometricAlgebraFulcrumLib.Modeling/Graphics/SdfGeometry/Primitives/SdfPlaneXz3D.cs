using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfPlaneXz3D : ScalarDistanceFunction
{
    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        return point.Y;
    }
}