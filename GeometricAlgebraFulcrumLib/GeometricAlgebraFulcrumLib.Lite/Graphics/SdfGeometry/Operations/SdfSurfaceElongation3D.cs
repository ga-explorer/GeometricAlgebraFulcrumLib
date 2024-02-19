using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Operations;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfSurfaceElongation3D : SdfUnaryOperation
{
    public Float64Vector3D Direction { get; set; }
        = Float64Vector3D.Create(0, 0, 1);


    public override double GetScalarDistance(IFloat64Vector3D point)
    {
        var q = point.ComponentsAbs() - Direction;

        return Surface.GetScalarDistance(q.ComponentsMax(0)) - Math.Min(q.ComponentsMax(), 0);
    }
}