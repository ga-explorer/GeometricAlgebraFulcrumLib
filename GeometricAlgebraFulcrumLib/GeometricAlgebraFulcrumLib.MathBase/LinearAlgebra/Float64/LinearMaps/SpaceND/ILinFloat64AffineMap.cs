using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND
{
    public interface ILinFloat64AffineMap :
        ILinFloat64UnilinearMap
    {
        Float64Vector MapPoint(Float64Vector point);

        Float64Vector MapOrigin();

        Float64Vector MapNormal(Float64Vector normal);

        ILinFloat64AffineMap GetInverseAffineMap();
    }
}