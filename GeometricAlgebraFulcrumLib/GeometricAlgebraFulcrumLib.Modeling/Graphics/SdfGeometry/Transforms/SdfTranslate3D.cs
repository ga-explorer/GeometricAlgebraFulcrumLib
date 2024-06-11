using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Transforms;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfTranslate3D : SdfUnaryOperation
{
    public LinFloat64Vector3D Direction { get; set; }
        = LinFloat64Vector3D.Create(0, 0, 0);

    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        return Surface.GetScalarDistance(point - Direction);
    }
}