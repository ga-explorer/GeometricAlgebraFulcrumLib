using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.PolynomialAlgebra.CurveBasis;

namespace GeometricAlgebraFulcrumLib.MathBase.PolynomialAlgebra.BSplineCurveBasis
{
    public interface IBSplineBasisSet :
        IPolynomialBasisSet
    {
        Float64Interval GetSupportInterval(int index);
    }
}