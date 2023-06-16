using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D
{
    public interface ILinFloat64AffineMap3D :
        ILinFloat64UnilinearMap3D
    {
        Float64Vector3D MapPoint(Float64Vector3D point);

        Float64Vector3D MapOrigin();

        Float64Vector3D MapNormal(Float64Vector3D normal);

        ILinFloat64AffineMap3D GetInverseAffineMap();
    }
}