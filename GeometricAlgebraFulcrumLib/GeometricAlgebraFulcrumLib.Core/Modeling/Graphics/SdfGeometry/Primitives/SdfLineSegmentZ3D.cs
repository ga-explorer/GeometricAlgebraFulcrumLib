using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfLineSegmentZ3D : ScalarDistanceFunction
{
    public double Length { get; set; }
        = 1.0d;

    public double Radius { get; set; }
        = 0.1d;


    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var v = LinFloat64Vector3D.Create(point.X,
            point.Y,
            point.Z - point.Z.ClampTo(Length)).VectorENorm();

        return v - Radius;
    }
}