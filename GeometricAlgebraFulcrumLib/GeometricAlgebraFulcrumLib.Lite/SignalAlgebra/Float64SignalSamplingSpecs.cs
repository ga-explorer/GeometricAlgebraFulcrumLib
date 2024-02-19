using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;

public sealed record Float64SignalSamplingSpecs
{
    private const double TwoPi = 2d * Math.PI;


    public int SampleCount { get; }

    public double SamplingRate { get; }

    public double MinTime 
        => 0;

    public double MaxTime 
        => (SampleCount - 1) / SamplingRate;

    public double TimeLength 
        => MaxTime - MinTime;

    public double TimeResolution
        => 1d / SamplingRate;

    public double MinFrequency
        => -FrequencyResolution * 
           (SampleCount.IsOdd() ? (SampleCount - 1) >> 1 : SampleCount >> 1);

    public double MaxFrequency
        => FrequencyResolution * 
           (SampleCount.IsOdd() ? (SampleCount - 1) >> 1 : (SampleCount >> 1) - 1);

    public double MaxFrequencyHz 
        => MaxFrequency / TwoPi;

    public double MinFrequencyHz 
        => MinFrequency / TwoPi;

    public double FrequencyLength 
        => MaxFrequency - MinFrequency;
        
    public double FrequencyLengthHz 
        => MaxFrequencyHz - MinFrequencyHz;

    public double FrequencyResolution
        => TwoPi * FrequencyResolutionHz;
        
    public double FrequencyResolutionHz
        => SamplingRate / (SampleCount - 1);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SignalSamplingSpecs(int sampleCount, double samplingRate)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        if (samplingRate <= 0)
            throw new IndexOutOfRangeException(nameof(samplingRate));

        SampleCount = sampleCount;
        SamplingRate = samplingRate;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSampleIndexDc(int index)
    {
        if (index < 0 || index >= SampleCount)
            index = index.Mod(SampleCount);

        return index == 0;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSampleIndexAc(int index)
    {
        if (index < 0 || index >= SampleCount)
            index = index.Mod(SampleCount);

        return index != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFrequency(int index)
    {
        if (index < 0 || index >= SampleCount)
            index = index.Mod(SampleCount);

        var midIndex = SampleCount.IsOdd() 
            ? (SampleCount - 1) / 2 
            : SampleCount / 2;

        return index <= midIndex
            ? index * FrequencyResolution
            : (SampleCount - index) * -FrequencyResolution;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetFrequencyHz(int index)
    {
        return GetFrequency(index) / (2 * Math.PI);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSampledTimeValue(int sampleIndex)
    {
        return sampleIndex / SamplingRate;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> GetSampledTimeValues(int sampleIndex1, int sampleIndex2)
    {
        return new Pair<double>(
            sampleIndex1 / SamplingRate,
            sampleIndex2 / SamplingRate
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetSampledTimeSignal()
    {
        return 0d
            .GetLinearRange((SampleCount - 1) / SamplingRate, SampleCount)
            .CreateSignal(SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetSampledTimeSignal(int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return 0d
            .GetLinearRange((sampleCount - 1) / SamplingRate, sampleCount)
            .CreateSignal(SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetSampledTimeSignal(int firstSampleIndex, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return (firstSampleIndex / SamplingRate)
            .GetLinearRange((firstSampleIndex + sampleCount - 1) / SamplingRate, sampleCount)
            .CreateSignal(SamplingRate);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetSampledTimeArray()
    {
        return 0d
            .GetLinearRange((SampleCount - 1) / SamplingRate, SampleCount)
            .ToArray();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetSampledTimeArray(int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return 0d
            .GetLinearRange((sampleCount - 1) / SamplingRate, sampleCount)
            .ToArray();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetSampledTimeArray(int firstSampleIndex, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return (firstSampleIndex / SamplingRate)
            .GetLinearRange((firstSampleIndex + sampleCount - 1) / SamplingRate, sampleCount)
            .ToArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetSampledFunctionSignal(Func<double, double> func)
    {
        return 0d
            .GetLinearRange((SampleCount - 1) / SamplingRate, SampleCount)
            .Select(func)
            .CreateSignal(SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetSampledFunctionSignal(Func<double, double> func, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return 0d
            .GetLinearRange((sampleCount - 1) / SamplingRate, sampleCount)
            .Select(func)
            .CreateSignal(SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetSampledFunctionSignal(Func<double, double> func, int firstSampleIndex, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return (firstSampleIndex / SamplingRate)
            .GetLinearRange((firstSampleIndex + sampleCount - 1) / SamplingRate, sampleCount)
            .Select(func)
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetSampledFunctionSignal(DifferentialFunction func)
    {
        return 0d
            .GetLinearRange((SampleCount - 1) / SamplingRate, SampleCount)
            .Select(func.GetValue)
            .CreateSignal(SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetSampledFunctionSignal(DifferentialFunction func, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return 0d
            .GetLinearRange((sampleCount - 1) / SamplingRate, sampleCount)
            .Select(func.GetValue)
            .CreateSignal(SamplingRate);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Signal GetSampledFunctionSignal(DifferentialFunction func, int firstSampleIndex, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return (firstSampleIndex / SamplingRate)
            .GetLinearRange((firstSampleIndex + sampleCount - 1) / SamplingRate, sampleCount)
            .Select(func.GetValue)
            .CreateSignal(SamplingRate);
    }
}