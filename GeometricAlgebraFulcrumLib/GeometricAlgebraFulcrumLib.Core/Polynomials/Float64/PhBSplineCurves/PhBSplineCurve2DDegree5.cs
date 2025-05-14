using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Polynomials.Float64.BSplineCurveBasis;

namespace GeometricAlgebraFulcrumLib.Core.Polynomials.Float64.PhBSplineCurves;

public class PhBSplineCurve2DDegree5
{
    public static PhBSplineCurve2DDegree5 Create(double tMin, double tMax, params LinFloat64Vector2D[] controlPointsArray)
    {
        const int n = 2;
        var q = controlPointsArray.Length - 1;

        //var knotVector = BSplineKnotVector.Create(tMin, tMax, , ,);

        return null;
    }


    private readonly BSplineBasisPairProductSet _basisPairProductSet;
    private readonly BSplineBasisPairProductIntegralSet _basisPairProductIntegralSet;

    public XGaFloat64Processor BasisBladeSet { get; }

    public BSplineBasisSet BasisSet { get; }



}