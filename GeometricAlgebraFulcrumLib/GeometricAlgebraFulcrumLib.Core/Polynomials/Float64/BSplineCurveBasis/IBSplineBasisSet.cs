using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Core.Polynomials.Float64.CurveBasis;

namespace GeometricAlgebraFulcrumLib.Core.Polynomials.Float64.BSplineCurveBasis;

public interface IBSplineBasisSet :
    IPolynomialBasisSet
{
    Float64Interval GetSupportInterval(int index);
}