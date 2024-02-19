using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfSphere3D : ScalarDistanceFunction
{
    public double Radius { get; set; } = 0.5d;


    public override double GetScalarDistance(IFloat64Vector3D point)
    {
        var sdf = point.ENorm() - Radius;
        return SdfAlpha * sdf - SdfDelta;
    }
}