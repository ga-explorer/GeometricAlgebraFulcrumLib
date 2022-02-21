using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.Polynomials.CurveBasis;

namespace NumericalGeometryLib.Polynomials.BSplineCurveBasis
{
    public interface IBSplineBasisSet :
        IPolynomialBasisSet
    {
        Float64Interval GetSupportInterval(int index);
    }
}