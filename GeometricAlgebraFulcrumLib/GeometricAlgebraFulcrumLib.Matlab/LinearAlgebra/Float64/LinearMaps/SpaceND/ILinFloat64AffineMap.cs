using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;

public interface ILinFloat64AffineMap :
    ILinFloat64UnilinearMap
{
    LinFloat64Vector MapPoint(LinFloat64Vector point);

    LinFloat64Vector MapOrigin();

    LinFloat64Vector MapNormal(LinFloat64Vector normal);

    ILinFloat64AffineMap GetInverseAffineMap();
}