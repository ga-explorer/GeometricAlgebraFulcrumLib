using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfLineSegmentY3D : ScalarDistanceFunction
{
    public double Length { get; set; }
        = 1.0d;

    public double Radius { get; set; }
        = 0.1d;


    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var v = LinFloat64Vector3DUtils.L2Norm(LinFloat64Vector3D.Create(point.X,
            point.Y - point.Y.ClampTo(Length),
            point.Z));

        return v - Radius;
    }
}