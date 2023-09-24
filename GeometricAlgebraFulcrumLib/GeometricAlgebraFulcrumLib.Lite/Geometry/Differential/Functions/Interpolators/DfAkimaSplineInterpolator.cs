using System.Runtime.CompilerServices;
using MathNet.Numerics;
using MathNet.Numerics.Interpolation;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Interpolators
{
    public class DfAkimaSplineInterpolator :
        DifferentialInterpolatorFunction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAkimaSplineInterpolator Create(IEnumerable<double> xValues, IEnumerable<double> yValues, bool xSorted = true)
        {
            var interpolator =
                xSorted
                    ? CubicSpline.InterpolateAkimaSorted(xValues.ToArray(), yValues.ToArray())
                    : CubicSpline.InterpolateAkima(xValues, yValues);

            //var interpolator = 
            //    xSorted
            //        ? CubicSpline.InterpolateNatural(xValues.ToArray(), yValues.ToArray())
            //        : CubicSpline.InterpolateNatural(xValues, yValues);

            return new DfAkimaSplineInterpolator(interpolator);
        }

    
        public CubicSpline Interpolator { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfAkimaSplineInterpolator(CubicSpline interpolator)
        {
            Interpolator = interpolator;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double t)
        {
            return Interpolator.Interpolate(t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            return DfComputedFunction.Create(
                GetDerivative1Value,
                GetDerivative2Value,
                GetDerivative3Value
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDerivativeValue(double t, int order)
        {
            return order switch
            {
                0 => GetValue(t),
                1 => GetDerivative1Value(t),
                2 => GetDerivative2Value(t),
                3 => GetDerivative3Value(t),
                _ => throw new ArgumentOutOfRangeException(nameof(order))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivativeN(int order)
        {
            return order switch
            {
                0 => this,
                1 => DfComputedFunction.Create(GetDerivative1Value, GetDerivative2Value, GetDerivative3Value),
                2 => DfComputedFunction.Create(GetDerivative2Value, GetDerivative3Value),
                3 => DfComputedFunction.Create(GetDerivative3Value),
                _ => throw new ArgumentOutOfRangeException(nameof(order))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDerivative1Value(double t)
        {
            return Interpolator.Differentiate(t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDerivative2Value(double t)
        {
            return Interpolator.Differentiate2(t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDerivative3Value(double t)
        {
            return Differentiate.FirstDerivative(Interpolator.Differentiate2, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDerivative4Value(double t)
        {
            return Differentiate.SecondDerivative(Interpolator.Differentiate2, t);
        }
    }
}