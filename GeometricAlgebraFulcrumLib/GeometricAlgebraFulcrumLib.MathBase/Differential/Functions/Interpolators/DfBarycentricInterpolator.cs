using System.Runtime.CompilerServices;
using MathNet.Numerics;
using MathNet.Numerics.Interpolation;

namespace GeometricAlgebraFulcrumLib.MathBase.Differential.Functions.Interpolators
{
    public class DfBarycentricInterpolator :
        DifferentialInterpolatorFunction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfBarycentricInterpolator Create(IEnumerable<double> xValues, IEnumerable<double> yValues)
        {
            var interpolator =
                Barycentric.InterpolatePolynomialEquidistant(
                    xValues.ToArray(),
                    yValues.ToArray()
                );

            return new DfBarycentricInterpolator(interpolator);
        }

    
        public Barycentric Interpolator { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfBarycentricInterpolator(Barycentric interpolator)
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
                GetDerivative2Value
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
                _ => throw new ArgumentOutOfRangeException(nameof(order))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivativeN(int order)
        {
            return order switch
            {
                0 => this,
                1 => DfComputedFunction.Create(GetDerivative1Value, GetDerivative2Value),
                2 => DfComputedFunction.Create(GetDerivative2Value),
                _ => throw new ArgumentOutOfRangeException(nameof(order))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDerivative1Value(double t)
        {
            return Differentiate.FirstDerivative(GetValue, t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDerivative2Value(double t)
        {
            return Differentiate.SecondDerivative(GetValue, t);
        }
    }
}