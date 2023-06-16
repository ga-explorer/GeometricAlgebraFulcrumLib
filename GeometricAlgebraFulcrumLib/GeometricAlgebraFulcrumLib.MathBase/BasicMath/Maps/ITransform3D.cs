using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps
{
    public interface ITransform3D
    {
        Float64Vector3D MapPoint(Float64Vector3D point);
    }
}
