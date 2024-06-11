using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfTorusZ3D : ScalarDistanceFunction
{
    public double InnerRadius { get; set; }
        = 0.5d;
        
    public double OuterRadius { get; set; }
        = 1.0d;


    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var q = LinFloat64Vector2D.Create(point.VectorLengthXy() - InnerRadius, point.Z);

        return q.VectorENorm() - OuterRadius;
    }
}