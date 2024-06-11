using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfPlane3D : ScalarDistanceFunction
{
    public LinFloat64Vector3D Point { get; set; }
        = LinFloat64Vector3D.Create(0, 0, 0);

    public LinFloat64Vector3D UnitNormal { get; set; }
        = LinFloat64Vector3D.Create(0, 0, 1);

    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        return point.VectorESp(UnitNormal) - Point.VectorESp(UnitNormal);
    }
}