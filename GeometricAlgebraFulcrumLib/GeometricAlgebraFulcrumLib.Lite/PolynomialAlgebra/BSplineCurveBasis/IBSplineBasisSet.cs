using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;
using GeometricAlgebraFulcrumLib.Lite.PolynomialAlgebra.CurveBasis;

namespace GeometricAlgebraFulcrumLib.Lite.PolynomialAlgebra.BSplineCurveBasis;

public interface IBSplineBasisSet :
    IPolynomialBasisSet
{
    Float64Interval GetSupportInterval(int index);
}