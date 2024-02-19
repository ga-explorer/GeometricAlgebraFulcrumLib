using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;

public interface ILinFloat64AffineMap :
    ILinFloat64UnilinearMap
{
    Float64Vector MapPoint(Float64Vector point);

    Float64Vector MapOrigin();

    Float64Vector MapNormal(Float64Vector normal);

    ILinFloat64AffineMap GetInverseAffineMap();
}