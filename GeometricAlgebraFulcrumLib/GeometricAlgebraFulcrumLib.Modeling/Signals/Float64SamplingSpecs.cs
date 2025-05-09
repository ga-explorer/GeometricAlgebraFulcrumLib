using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space1D;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Signals;

public sealed record Float64SamplingSpecs :
    IAlgebraicElement
{
    public static Float64SamplingSpecs Static { get; }
        = new Float64SamplingSpecs();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SamplingSpecs Create(int samplingRate, double maxTime)
    {
        if (samplingRate < 1)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        if (!maxTime.IsFinite() || samplingRate * maxTime < 1)
            throw new ArgumentOutOfRangeException(nameof(maxTime));

        var sampleIndex2 = (int) Math.Floor(samplingRate * maxTime);

        if (sampleIndex2 < 1)
            throw new ArgumentOutOfRangeException(nameof(maxTime));

        return new Float64SamplingSpecs(
            sampleIndex2 + 1,
            samplingRate
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SamplingSpecs CreateFromSamplingRate(int sampleCount, double samplingRate)
    {
        return new Float64SamplingSpecs(
            sampleCount, 
            samplingRate
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SamplingSpecs CreateFromTimeLength(int sampleCount, double timeLength)
    {
        var samplingRate = (sampleCount - 1) / timeLength;
        
        return new Float64SamplingSpecs(
            sampleCount, 
            samplingRate
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SamplingSpecs CreateFromTimeResolution(int sampleCount, double timeResolution)
    {
        var samplingRate = 1d / timeResolution;

        return new Float64SamplingSpecs(
            sampleCount, 
            samplingRate
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SamplingSpecs CreateFromFrequencyResolution(int sampleCount, double freqResolution)
    {
        var samplingRate = freqResolution * (sampleCount - 1) / Math.Tau;

        return new Float64SamplingSpecs(
            sampleCount, 
            samplingRate
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SamplingSpecs CreateFromFrequencyResolutionHz(int sampleCount, double freqResolution)
    {
        var samplingRate = freqResolution * (sampleCount - 1);

        return new Float64SamplingSpecs(
            sampleCount, 
            samplingRate
        );
    }

    
    public int SampleCount { get; }

    public double SamplingRate { get; }
    
    public Float64ScalarRange TimeRange { get; }

    public double MinTime
        => TimeRange.MinValue;

    public double MaxTime
        => TimeRange.MaxValue;

    public double TimeLength
        => TimeRange.Length;

    public double TimeResolution
        => 1d / SamplingRate;

    public double MinFrequency
        => -FrequencyResolution *
           (SampleCount.IsOdd() ? SampleCount - 1 >> 1 : SampleCount >> 1);

    public double MaxFrequency
        => FrequencyResolution *
           (SampleCount.IsOdd() ? SampleCount - 1 >> 1 : (SampleCount >> 1) - 1);

    public double MaxFrequencyHz
        => MaxFrequency / Math.Tau;

    public double MinFrequencyHz
        => MinFrequency / Math.Tau;

    public double FrequencyLength
        => MaxFrequency - MinFrequency;

    public double FrequencyLengthHz
        => MaxFrequencyHz - MinFrequencyHz;

    public double FrequencyResolution
        => Math.Tau * FrequencyResolutionHz;

    public double FrequencyResolutionHz
        => SamplingRate / (SampleCount - 1);
    
    public bool IsStatic 
        => SampleCount == 0 && SamplingRate == 0;
    
    //public bool IsAnimated 
    //    => SampleCount > 0 && SamplingRate > 0;

    public Int32Range1D SampleIndexRange 
        => Int32Range1D.Create(0, SampleCount - 1);

    public int MinSampleIndex 
        => 0;
    
    public int MaxSampleIndex 
        => SampleCount - 1;
    
    public IEnumerable<double> SampleTimes
        => SampleCount.MapRange(
            sampleIndex => MinTime + sampleIndex * TimeResolution
        );
    
    public IEnumerable<int> SampleIndices
        => SampleCount.GetRange();

    public IEnumerable<KeyValuePair<int, double>> SampleIndexTimePairs
        => SampleCount.MapRange(sampleIndex => 
            new KeyValuePair<int, double>(
                sampleIndex,
                MinTime + sampleIndex * TimeResolution
            )
        );

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SamplingSpecs()
    {
        SampleCount = 0;
        SamplingRate = 0;
        TimeRange = Float64ScalarRange.Create(0, 0);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SamplingSpecs(int sampleCount, double samplingRate)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        if (samplingRate <= 0)
            throw new IndexOutOfRangeException(nameof(samplingRate));

        SampleCount = sampleCount;
        SamplingRate = samplingRate;
        TimeRange = Float64ScalarRange.Create(0, (sampleCount - 1) / samplingRate);

        Debug.Assert(IsValid());
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        if (!MinTime.IsFinite())
            return false;

        return (SampleCount == 0 && SamplingRate == 0) ||
               (SampleCount > 0 && SamplingRate > 0);
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
        return GetFrequency(index) / (Math.Tau);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSampleIndex(double sampleTime)
    {
        return (sampleTime - MinTime) * SamplingRate;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSampleTimeRelative(int sampleIndex)
    {
        return sampleIndex / (SampleCount - 1d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetSampleTime(int sampleIndex)
    {
        return MinTime + sampleIndex * TimeResolution;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> GetSampleTimePair(int sampleIndex1, int sampleIndex2)
    {
        return new Pair<double>(
            MinTime + sampleIndex1 * TimeResolution,
            MinTime + sampleIndex2 * TimeResolution
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampleTimeSignal()
    {
        return MinTime
            .GetLinearRange(MaxTime, SampleCount)
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampleTimeSignal(int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return MinTime
            .GetLinearRange(MinTime + (sampleCount - 1) * TimeResolution, sampleCount)
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampleTimeSignal(int firstSampleIndex, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return (MinTime + firstSampleIndex * TimeResolution)
            .GetLinearRange(MinTime + (firstSampleIndex + sampleCount - 1) * TimeResolution, sampleCount)
            .CreateSignal(SamplingRate);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetSampleTimeArray()
    {
        return MinTime
            .GetLinearRange(MaxTime, SampleCount)
            .ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetSampleTimeArray(int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return MinTime
            .GetLinearRange(MinTime + (sampleCount - 1) * TimeResolution, sampleCount)
            .ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetSampleTimeArray(int firstSampleIndex, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return (MinTime + firstSampleIndex * TimeResolution)
            .GetLinearRange(MinTime + (firstSampleIndex + sampleCount - 1) * TimeResolution, sampleCount)
            .ToArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampledFunctionSignal(Func<double, double> func)
    {
        return MinTime
            .GetLinearRange(MaxTime, SampleCount)
            .Select(func)
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampledFunctionSignal(Func<double, double> func, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return MinTime
            .GetLinearRange(MinTime + (sampleCount - 1) * TimeResolution, sampleCount)
            .Select(func)
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampledFunctionSignal(Func<double, double> func, int firstSampleIndex, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return (MinTime + firstSampleIndex * TimeResolution)
            .GetLinearRange(MinTime + (firstSampleIndex + sampleCount - 1) * TimeResolution, sampleCount)
            .Select(func)
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampledFunctionSignal(DifferentialFunction func)
    {
        return MinTime
            .GetLinearRange(MaxTime, SampleCount)
            .Select(func.GetValue)
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampledFunctionSignal(DifferentialFunction func, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return MinTime
            .GetLinearRange(MinTime + (sampleCount - 1) * TimeResolution, sampleCount)
            .Select(func.GetValue)
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampledFunctionSignal(DifferentialFunction func, int firstSampleIndex, int sampleCount)
    {
        if (sampleCount < 1)
            throw new ArgumentOutOfRangeException(nameof(sampleCount));

        return (MinTime + firstSampleIndex * TimeResolution)
            .GetLinearRange(MinTime + (firstSampleIndex + sampleCount - 1) * TimeResolution, sampleCount)
            .Select(func.GetValue)
            .CreateSignal(SamplingRate);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int AffineMapSampleIndex(Float64SamplingSpecs targetSamplingSpecs, int sampleIndex)
    {
        return targetSamplingSpecs.GetSampleIndex(
            targetSamplingSpecs.MinTime + 
            targetSamplingSpecs.TimeLength * GetSampleTimeRelative(sampleIndex)
        ).RoundToInt32().ClampInt(targetSamplingSpecs.MaxSampleIndex);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, T>> GetSampleIndexValuePairs<T>(Func<double, T> timeToValueMapping)
    {
        return SampleIndexTimePairs.Select(
            indexTimePair => new KeyValuePair<int, T>(
                indexTimePair.Key,
                timeToValueMapping(indexTimePair.Value)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<double, T>> GetSampleTimeValuePairs<T>(Func<double, T> timeToValueMapping)
    {
        return SampleTimes.Select(
            time => new KeyValuePair<double, T>(
                time,
                timeToValueMapping(time)
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Tuple<int, double, T>> GetSampleIndexTimeValueTuples<T>(Func<double, T> timeToValueMapping)
    {
        return SampleIndexTimePairs.Select(
            indexTimePair => new Tuple<int, double, T>(
                indexTimePair.Key,
                indexTimePair.Value,
                timeToValueMapping(indexTimePair.Value)
            )
        );
    }

    public bool Equals(Float64SamplingSpecs? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        
        return SampleCount.Equals(other.SampleCount) && 
               SamplingRate == other.SamplingRate;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(SampleCount, SamplingRate);
    }
}