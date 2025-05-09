using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.Space4D;

public interface ILinFloat64AffineMap4D :
    ILinFloat64UnilinearMap4D
{
    LinFloat64Quaternion MapPoint(LinFloat64Quaternion point);

    LinFloat64Quaternion MapOrigin();

    LinFloat64Quaternion MapNormal(LinFloat64Quaternion normal);

    ILinFloat64AffineMap4D GetInverseAffineMap();
}