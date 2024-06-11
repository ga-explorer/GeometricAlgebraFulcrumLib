using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Transforms;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfRotateZ3D : SdfUnaryOperation
{
    public double Angle { get; set; }
        = 0;
        
    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var cosAngle = Math.Cos(-Angle);
        var sinAngle = Math.Sin(-Angle);

        var q = LinFloat64Vector3D.Create(point.X * cosAngle - point.Y * sinAngle,
            point.Y * cosAngle + point.X * sinAngle,
            point.Z);

        return Surface.GetScalarDistance(q);
    }
}