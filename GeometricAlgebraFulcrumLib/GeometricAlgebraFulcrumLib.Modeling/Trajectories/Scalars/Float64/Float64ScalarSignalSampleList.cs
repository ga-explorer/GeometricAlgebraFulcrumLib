using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

public sealed record Float64ScalarSignalSampleList :
    IReadOnlyList<double>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarSignalSampleList Create(Float64ScalarSignal baseSignal, int sampleCount)
    {
        return new Float64ScalarSignalSampleList(baseSignal, sampleCount);
    }


    public Float64ScalarSignal BaseSignal { get; }

    public Float64SamplingSpecs SamplingSpecs { get; }
    
    public Float64ScalarRange TimeRange 
        => BaseSignal.TimeRange;

    public int Count 
        => SamplingSpecs.SampleCount;

    public double this[int index] 
        => BaseSignal.GetValue(
            TimeRange.MinValue + SamplingSpecs.GetSampleTime(index)
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarSignalSampleList(Float64ScalarSignal baseSignal, int sampleCount)
    {
        if (sampleCount < 2)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        SamplingSpecs = Float64SamplingSpecs.CreateFromTimeLength(sampleCount, baseSignal.TimeRangeLength);
        BaseSignal = baseSignal;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
    {
        var minTime = BaseSignal.TimeRange.MinValue;

        return SamplingSpecs
            .SampleTimes
            .Select(t => BaseSignal.GetValue(t + minTime))
            .GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}