using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.SdfGeometry.Operations;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfSurfaceElongation3D : SdfUnaryOperation
{
    public LinFloat64Vector3D Direction { get; set; }
        = LinFloat64Vector3D.Create(0, 0, 1);


    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var q = point.ComponentsAbs() - Direction;

        return Surface.GetScalarDistance(q.ComponentsMax(0)) - Math.Min(q.ComponentsMax(), 0);
    }
}