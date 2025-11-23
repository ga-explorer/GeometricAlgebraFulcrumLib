using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.CurveBasis;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline;

public interface IBSplineBasisSet :
    IPolynomialBasisSet
{
    Float64Interval GetSupportInterval(int index);
}