using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Transforms;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfRotateX3D : SdfUnaryOperation
{
    public double Angle { get; set; }
        = 0;
        
    public override double GetScalarDistance(IFloat64Vector3D point)
    {
        var cosAngle = Math.Cos(-Angle);
        var sinAngle = Math.Sin(-Angle);

        var q = Float64Vector3D.Create(point.X,
            point.Y * cosAngle - point.Z * sinAngle,
            point.Z * cosAngle + point.Y * sinAngle);

        return Surface.GetScalarDistance(q);
    }
}