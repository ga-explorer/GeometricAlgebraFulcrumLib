using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.SdfGeometry.Transforms;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfScale3D : SdfUnaryOperation
{
    public double Factor { get; set; }
        = 1.0d;

    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var s = 1d / Factor;
        var p = LinFloat64Vector3D.Create(point.X * s,
            point.Y * s,
            point.Z * s);

        return Factor * Surface.GetScalarDistance(p);
    }
}