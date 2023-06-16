using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.PolynomialAlgebra.BSplineCurveBasis;

namespace GeometricAlgebraFulcrumLib.MathBase.PolynomialAlgebra.PhBSplineCurves
{
    public class PhBSplineCurve2DDegree5
    {
        public static PhBSplineCurve2DDegree5 Create(double tMin, double tMax, params Float64Vector2D[] controlPointsArray)
        {
            const int n = 2;
            var q = controlPointsArray.Length - 1;

            //var knotVector = BSplineKnotVector.Create(tMin, tMax, , ,);

            return null;
        }


        private readonly BSplineBasisPairProductSet _basisPairProductSet;
        private readonly BSplineBasisPairProductIntegralSet _basisPairProductIntegralSet;

        public RGaFloat64Processor BasisBladeSet { get; }

        public BSplineBasisSet BasisSet { get; }



    }
}
