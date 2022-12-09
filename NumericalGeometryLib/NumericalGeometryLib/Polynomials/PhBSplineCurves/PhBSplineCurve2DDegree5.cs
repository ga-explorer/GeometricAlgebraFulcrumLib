using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.Polynomials.BSplineCurveBasis;

namespace NumericalGeometryLib.Polynomials.PhBSplineCurves
{
    public class PhBSplineCurve2DDegree5
    {
        public static PhBSplineCurve2DDegree5 Create(double tMin, double tMax, params Float64Tuple2D[] controlPointsArray)
        {
            const int n = 2;
            var q = controlPointsArray.Length - 1;

            //var knotVector = BSplineKnotVector.Create(tMin, tMax, , ,);

            return null;
        }


        private readonly BSplineBasisPairProductSet _basisPairProductSet;
        private readonly BSplineBasisPairProductIntegralSet _basisPairProductIntegralSet;

        public BasisBladeSet BasisBladeSet { get; }

        public BSplineBasisSet BasisSet { get; }



    }
}
