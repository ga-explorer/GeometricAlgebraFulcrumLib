using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.Differential.Functions;

namespace GeometricAlgebraFulcrumLib.MathBase.Signals
{
    public static class SignalProcessingFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateSignal(this Func<double, double> scalarFunction, double tMin, double tMax, int sampleCount, bool periodicRange)
        {
            var tValues =
                tMin.GetLinearRange(tMax, sampleCount, periodicRange).ToImmutableArray();

            var samplingRate =
                (sampleCount - 1) / (tValues[^1] - tValues[0]);

            return tValues
                .Select(scalarFunction)
                .CreateSignal(samplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateSignal(this DifferentialFunction scalarFunction, double tMin, double tMax, int sampleCount, bool periodicRange)
        {
            var tValues =
                tMin.GetLinearRange(tMax, sampleCount, periodicRange).ToImmutableArray();

            var samplingRate =
                (sampleCount - 1) / (tValues[^1] - tValues[0]);

            return tValues
                .Select(scalarFunction.GetValue)
                .CreateSignal(samplingRate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 CreateSignal(this IEnumerable<double> signalSamples, double samplingRate)
        {
            return ScalarSignalFloat64.Create(samplingRate, signalSamples, false);
        }

    }
}
