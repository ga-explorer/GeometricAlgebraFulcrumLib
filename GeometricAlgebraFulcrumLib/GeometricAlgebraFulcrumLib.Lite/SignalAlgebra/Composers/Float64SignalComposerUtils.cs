using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Composers;

public static class Float64SignalComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Signal CreateSignal(this Func<double, double> scalarFunction, double tMin, double tMax, int sampleCount, bool periodicRange)
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
    public static Float64Signal CreateSignal(this DifferentialFunction scalarFunction, double tMin, double tMax, int sampleCount, bool periodicRange)
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
    public static Float64Signal CreateSignal(this IEnumerable<double> signalSamples, double samplingRate)
    {
        return Float64Signal.Create(samplingRate, signalSamples, false);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Signal CreateSignal(this IEnumerable<Float64Scalar> signalSamples, double samplingRate)
    {
        return Float64Signal.Create(samplingRate, signalSamples, false);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Signal CreateSignal(this Scalar<IReadOnlyList<double>> signalSamples, double samplingRate)
    {
        return Float64Signal.Create(samplingRate, signalSamples.ScalarValue, false);
    }

        
}