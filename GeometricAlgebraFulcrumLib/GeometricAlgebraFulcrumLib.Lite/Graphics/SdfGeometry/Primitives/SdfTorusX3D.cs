using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfTorusX3D : ScalarDistanceFunction
{
    public double InnerRadius { get; set; }
        = 0.5d;
        
    public double OuterRadius { get; set; }
        = 1.0d;


    public override double GetScalarDistance(IFloat64Vector3D point)
    {
        var q = Float64Vector2D.Create((Float64Scalar)(point.LengthYz() - InnerRadius), point.X);

        return q.ENorm() - OuterRadius;
    }
}