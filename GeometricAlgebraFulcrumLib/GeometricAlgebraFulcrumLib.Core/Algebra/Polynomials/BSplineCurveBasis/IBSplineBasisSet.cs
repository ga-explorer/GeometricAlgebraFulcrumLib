using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Core.Algebra.Polynomials.CurveBasis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.Polynomials.BSplineCurveBasis;

public interface IBSplineBasisSet :
    IPolynomialBasisSet
{
    Float64Interval GetSupportInterval(int index);
}