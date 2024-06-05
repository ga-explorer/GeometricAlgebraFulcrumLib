using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfTorusX3D : ScalarDistanceFunction
{
    public double InnerRadius { get; set; }
        = 0.5d;
        
    public double OuterRadius { get; set; }
        = 1.0d;


    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var q = LinFloat64Vector2D.Create(point.VectorLengthYz() - InnerRadius, point.X);

        return q.VectorENorm() - OuterRadius;
    }
}