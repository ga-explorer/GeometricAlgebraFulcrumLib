using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.BSplineCurveBasis;

public interface IBSplineBasisSet :
    IPolynomialBasisSet
{
    Float64Interval GetSupportInterval(int index);
}