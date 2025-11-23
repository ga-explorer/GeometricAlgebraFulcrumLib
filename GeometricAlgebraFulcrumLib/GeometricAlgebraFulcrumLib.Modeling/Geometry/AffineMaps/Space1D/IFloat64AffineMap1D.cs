using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;

public interface IFloat64AffineMap1D :
    IFloat64AffineMap
{
    Float64Scalar MapPoint(Float64Scalar point);

    Float64Scalar MapVector(Float64Scalar vector);

    SquareMatrix2 GetSquareMatrix2();

    IFloat64AffineMap1D GetInverseAffineMap();
}