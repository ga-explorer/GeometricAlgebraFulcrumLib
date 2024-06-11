using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfSphere3D : ScalarDistanceFunction
{
    public double Radius { get; set; } = 0.5d;


    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var sdf = point.VectorENorm() - Radius;
        return SdfAlpha * sdf - SdfDelta;
    }
}