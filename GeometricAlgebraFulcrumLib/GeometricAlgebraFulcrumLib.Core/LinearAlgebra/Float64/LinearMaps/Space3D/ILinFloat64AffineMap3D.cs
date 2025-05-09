using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.Space3D;

public interface ILinFloat64AffineMap3D :
    ILinFloat64UnilinearMap3D
{
    LinFloat64Vector3D MapPoint(LinFloat64Vector3D point);

    LinFloat64Vector3D MapOrigin();

    LinFloat64Vector3D MapNormal(LinFloat64Vector3D normal);

    ILinFloat64AffineMap3D GetInverseAffineMap();
}