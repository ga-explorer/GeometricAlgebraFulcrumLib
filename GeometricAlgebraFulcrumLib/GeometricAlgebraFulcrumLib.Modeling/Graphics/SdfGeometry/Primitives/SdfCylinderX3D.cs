using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfCylinderX3D : ScalarDistanceFunction
{
    public LinFloat64Vector2D CenterYz { get; set; }
        = LinFloat64Vector2D.Create((Float64Scalar)0, 0);

    public double Radius { get; set; }


    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        return (point.YzToLinVector2D() - CenterYz).VectorENorm() - Radius;
    }
}