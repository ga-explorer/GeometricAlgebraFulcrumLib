using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.BSplines;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class BSplineFactory
    {
        public static BSplineBasisSet<T> CreateBSplineBasisSet<T>(this IScalarProcessor<T> scalarProcessor, int degree, params T[] knotValues)
        {
            var knotVector = new BSplineKnotVector<T>(scalarProcessor);

            foreach (var value in knotValues)
                knotVector.AppendKnot(value);

            return knotVector.CreateBSplineBasisSet(degree);
        }

        public static BSplineBasisSet<T> CreateBSplineBasisSetClamped<T>(this IScalarProcessor<T> scalarProcessor, int degree, params T[] knotValues)
        {
            var knotVector = new BSplineKnotVector<T>(scalarProcessor);

            knotVector.AppendKnot(knotValues[0], degree + 1);
            
            for (var i = 0; i < knotValues.Length - 1; i++)
                knotVector.AppendKnot(knotValues[i]);

            knotVector.AppendKnot(knotValues[^1], degree + 1);

            return knotVector.CreateBSplineBasisSet(degree);
        }
    }
}
