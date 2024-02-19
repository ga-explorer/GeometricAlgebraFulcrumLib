using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Transforms;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfTranslate3D : SdfUnaryOperation
{
    public Float64Vector3D Direction { get; set; }
        = Float64Vector3D.Create(0, 0, 0);

    public override double GetScalarDistance(IFloat64Vector3D point)
    {
        return Surface.GetScalarDistance(point - Direction);
    }
}