using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Operations;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfNot3D : SdfUnaryOperation
{
    public override double GetScalarDistance(IFloat64Vector3D point)
    {
        return -Surface.GetScalarDistance(point);
    }
}