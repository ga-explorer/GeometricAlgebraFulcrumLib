using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Maps
{
    public interface ITransform3D
    {
        Float64Vector3D MapPoint(Float64Vector3D point);
    }
}
