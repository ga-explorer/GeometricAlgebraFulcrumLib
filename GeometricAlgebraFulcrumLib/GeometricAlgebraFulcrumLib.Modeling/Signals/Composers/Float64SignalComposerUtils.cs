using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;

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
    public static Float64Signal CreateSignal(this IEnumerable<double> signalSamples, double samplingRate, Func<double, double> scalarMapping)
    {
        return Float64Signal.Create(
            samplingRate, 
            signalSamples.Select(scalarMapping), 
            false
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Signal CreateSignal(this IEnumerable<Float64Scalar> signalSamples, double samplingRate, Func<Float64Scalar, double> scalarMapping)
    {
        return Float64Signal.Create(
            samplingRate, 
            signalSamples.Select(scalarMapping), 
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Signal CreateSignal<T>(this IEnumerable<T> signalSamples, double samplingRate, Func<T, double> scalarMapping)
    {
        return Float64Signal.Create(
            samplingRate, 
            signalSamples.Select(scalarMapping), 
            false
        );
    }

}