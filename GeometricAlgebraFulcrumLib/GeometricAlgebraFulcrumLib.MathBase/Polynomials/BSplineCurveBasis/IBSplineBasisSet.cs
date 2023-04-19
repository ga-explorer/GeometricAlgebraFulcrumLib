using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.Polynomials.CurveBasis;

namespace GeometricAlgebraFulcrumLib.MathBase.Polynomials.BSplineCurveBasis
{
    public interface IBSplineBasisSet :
        IPolynomialBasisSet
    {
        Float64Interval GetSupportInterval(int index);
    }
}