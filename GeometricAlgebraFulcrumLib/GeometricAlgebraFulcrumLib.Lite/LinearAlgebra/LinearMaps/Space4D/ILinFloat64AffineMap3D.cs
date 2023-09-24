using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D
{
    public interface ILinFloat64AffineMap4D :
        ILinFloat64UnilinearMap4D
    {
        Float64Quaternion MapPoint(Float64Quaternion point);

        Float64Quaternion MapOrigin();

        Float64Quaternion MapNormal(Float64Quaternion normal);

        ILinFloat64AffineMap4D GetInverseAffineMap();
    }
}