using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfPlane3D : ScalarDistanceFunction
{
    public Float64Vector3D Point { get; set; }
        = Float64Vector3D.Create(0, 0, 0);

    public Float64Vector3D UnitNormal { get; set; }
        = Float64Vector3D.Create(0, 0, 1);

    public override double GetScalarDistance(IFloat64Vector3D point)
    {
        return point.ESp(UnitNormal) - Point.ESp(UnitNormal);
    }
}