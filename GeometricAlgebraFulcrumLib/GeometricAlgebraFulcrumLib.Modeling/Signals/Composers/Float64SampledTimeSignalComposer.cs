using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.IntegralTransforms;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;

public sealed class Float64SampledTimeSignalComposer :
    IReadOnlyList<double>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer Create(double samplingRate)
    {
        return new Float64SampledTimeSignalComposer(samplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer Create(double samplingRate, int sampleCount)
    {
        return new Float64SampledTimeSignalComposer(samplingRate, sampleCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer Create(double samplingRate, IEnumerable<double> sampleList)
    {
        return new Float64SampledTimeSignalComposer(samplingRate, sampleList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer Create(double samplingRate, IEnumerable<Float64Scalar> sampleList)
    {
        return new Float64SampledTimeSignalComposer(samplingRate, sampleList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreateConstant(double samplingRate, int sampleCount, double value)
    {
        return new Float64SampledTimeSignalComposer(
            samplingRate,
            Enumerable.Repeat(value, sampleCount)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreateNonPeriodic(int sampleCount, double tMin, double tMax, Func<double, double> scalarFunc)
    {
        var sampleList =
            tMin.GetLinearRange(tMax, sampleCount, false).Select(scalarFunc);

        var samplingRate = sampleCount / (tMax - tMin);

        return new Float64SampledTimeSignalComposer(samplingRate, sampleList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreatePeriodic(int sampleCount, double periodTime, Func<double, double> scalarFunc)
    {
        var sampleList =
            0d.GetLinearRange(periodTime, sampleCount, true).Select(scalarFunc);

        return new Float64SampledTimeSignalComposer(sampleCount / periodTime, sampleList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreatePeriodic(double samplingRate, int sampleCount, double periodTime, Func<double, double> scalarFunc)
    {
        var sampleList =
            0d.GetLinearRange(periodTime, sampleCount, true).Select(scalarFunc);

        return new Float64SampledTimeSignalComposer(samplingRate, sampleList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreateConcat(double samplingRate, IEnumerable<double> samples1, IEnumerable<double> samples2)
    {
        return Create(samplingRate, samples1.Concat(samples2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreateRandomUniform(double samplingRate, int sampleCount, Random randomGenerator)
    {
        var sampleList =
            sampleCount.GetRange().Select(_ => randomGenerator.NextDouble());

        return new Float64SampledTimeSignalComposer(
            samplingRate,
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreateRandomUniform(double samplingRate, int sampleCount, Random randomGenerator, double minValue, double maxValue)
    {
        var sampleList =
            sampleCount.GetRange().Select(_ => randomGenerator.NextDouble(minValue, maxValue));

        return new Float64SampledTimeSignalComposer(
            samplingRate,
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreateRandomGaussian(double samplingRate, int sampleCount, Random randomGenerator)
    {
        var sampleList =
            sampleCount.GetRange().Select(_ => randomGenerator.NextGaussian());

        return new Float64SampledTimeSignalComposer(
            samplingRate,
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignalComposer CreateRandomGaussian(double samplingRate, int sampleCount, Random randomGenerator, double mu, double sigma)
    {
        var sampleList =
            sampleCount.GetRange().Select(_ => sigma * randomGenerator.NextGaussian() + mu);

        return new Float64SampledTimeSignalComposer(
            samplingRate,
            sampleList
        );
    }


    private readonly List<double> _sampleList;


    public double SamplingRate { get; }

    public int Count
        => _sampleList.Count;

    public Float64SamplingSpecs SamplingSpecs
        => Float64SamplingSpecs.CreateFromSamplingRate(Count, SamplingRate);

    public double this[int index]
    {
        get => _sampleList[index];
        set => _sampleList[index] = value.NaNToZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SampledTimeSignalComposer(double samplingRate)
    {
        if (double.IsNaN(samplingRate) || double.IsInfinity(samplingRate) || samplingRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        _sampleList = new List<double>();
        SamplingRate = samplingRate;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SampledTimeSignalComposer(double samplingRate, int sampleCount)
    {
        if (double.IsNaN(samplingRate) || double.IsInfinity(samplingRate) || samplingRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        _sampleList = new List<double>(Enumerable.Repeat(0d, sampleCount));
        SamplingRate = samplingRate;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SampledTimeSignalComposer(double samplingRate, IEnumerable<double> sampleList)
    {
        if (double.IsNaN(samplingRate) || double.IsInfinity(samplingRate) || samplingRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        _sampleList = new List<double>(sampleList.Select(s => s.NaNToZero()));
        SamplingRate = samplingRate;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SampledTimeSignalComposer(double samplingRate, IEnumerable<Float64Scalar> sampleList)
    {
        if (double.IsNaN(samplingRate) || double.IsInfinity(samplingRate) || samplingRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        _sampleList = new List<double>(sampleList.Select(s => s.ScalarValue));
        SamplingRate = samplingRate;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return _sampleList.All(s => s.IsZero());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return _sampleList.All(s => s.IsNearZero(zeroEpsilon));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetSampleIndexFromTime(double t)
    {
        //TODO: Is this correct?
        if (t < 0)
            return new Pair<int>(0, 0);

        if (t > SamplingSpecs.MaxTime)
            return new Pair<int>(Count - 1, Count - 1);

        //if (t < 0 || t > SamplingSpecs.MaxTime)
        //    throw new ArgumentOutOfRangeException(nameof(t));

        var index = t / SamplingSpecs.TimeResolution;

        return new Pair<int>(
            (int)Math.Floor(index),
            (int)Math.Ceiling(index)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignalComposer AppendSample(double sampleValue)
    {
        _sampleList.Add(sampleValue.NaNToZero());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignalComposer AppendSamples(IEnumerable<double> sampleValues)
    {
        _sampleList.AddRange(sampleValues.Select(s => s.NaNToZero()));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignalComposer AppendSamples(params double[] sampleValues)
    {
        _sampleList.AddRange(sampleValues.Select(s => s.NaNToZero()));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignalComposer PrependSample(double sampleValue)
    {
        _sampleList.Insert(0, sampleValue.NaNToZero());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignalComposer PrependSamples(IEnumerable<double> sampleValues)
    {
        _sampleList.InsertRange(0, sampleValues.Select(s => s.NaNToZero()));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignalComposer PrependSamples(params double[] sampleValues)
    {
        _sampleList.InsertRange(0, sampleValues.Select(s => s.NaNToZero()));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignalComposer InsertSample(int index, double sampleValue)
    {
        _sampleList.Insert(index, sampleValue.NaNToZero());

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignalComposer InsertSamples(int index, IEnumerable<double> sampleValues)
    {
        _sampleList.InsertRange(index, sampleValues.Select(s => s.NaNToZero()));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignalComposer InsertSamples(int index, params double[] sampleValues)
    {
        _sampleList.InsertRange(index, sampleValues.Select(s => s.NaNToZero()));

        return this;
    }


    public Pair<double> GetMinMaxValues()
    {
        var minValue = double.PositiveInfinity;
        var maxValue = double.NegativeInfinity;

        foreach (var value in _sampleList)
        {
            if (minValue > value) minValue = value;
            if (maxValue < value) maxValue = value;
        }

        return new Pair<double>(minValue, maxValue);
    }


    public double EnergyFft()
    {
        // Compute FFT
        var real = _sampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
        var sampleCount = real.Length;

        Fourier.Forward(real, imaginary);

        // Compute AC energy
        var energy = 0d;
        for (var freqIndex = 0; freqIndex < sampleCount; freqIndex++)
            energy += (real[freqIndex].Square() + imaginary[freqIndex].Square()) / Math.Tau;

        return energy;
    }

    public double EnergyDcFft()
    {
        // Compute FFT
        var real = _sampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();

        Fourier.Forward(real, imaginary);

        // Compute DC energy
        return (real[0].Square() + imaginary[0].Square()) / Math.Tau;
    }

    public double EnergyAcFft()
    {
        // Compute FFT
        var real = _sampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
        var sampleCount = real.Length;

        Fourier.Forward(real, imaginary);

        // Compute AC energy
        var energy = 0d;
        for (var freqIndex = 1; freqIndex < sampleCount; freqIndex++)
            energy += (real[freqIndex].Square() + imaginary[freqIndex].Square()) / Math.Tau;

        return energy;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Energy()
    {
        var energy1 =
            _sampleList.Sum(s => s * s) / Math.Tau;

        var energy2 = EnergyFft();
        Debug.Assert(
            (energy1 - energy2).IsNearZero() ||
            ((energy1 - energy2) / energy1).IsNearZero()
        );

        return energy1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EnergyDc()
    {
        // The DC energy is the mean square value of the signal
        var energy1 =
            _sampleList.Sum().Square() / (Math.Tau * Count);
        //Mean().Square() * Count / (Math.Tau);

        var energy2 = EnergyDcFft();
        Debug.Assert(
            (energy1 - energy2).IsNearZero() ||
            ((energy1 - energy2) / energy1).IsNearZero()
        );

        return energy1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double EnergyAc()
    {
        var energy1 = Energy() - EnergyDc();

        var energy2 = EnergyAcFft();

        //Debug.Assert(
        //    (energy1 - energy2).IsNearZero() ||
        //    ((energy1 - energy2) / energy1).IsNearZero()
        //);

        return energy1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double SumOfSquares()
    {
        return _sampleList.Sum(s => s * s);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampledTimeSignal()
    {
        return SamplingSpecs.GetSampleTimeSignal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampledTimeSignal(int sampleCount)
    {
        return SamplingSpecs.GetSampleTimeSignal(sampleCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetSampledTimeSignal(int firstSampleIndex, int sampleCount)
    {
        return SamplingSpecs.GetSampleTimeSignal(firstSampleIndex, sampleCount);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetSampledTimeArray()
    {
        return SamplingSpecs.GetSampleTimeArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetSampledTimeArray(int sampleCount)
    {
        return SamplingSpecs.GetSampleTimeArray(sampleCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetSampledTimeArray(int firstSampleIndex, int sampleCount)
    {
        return SamplingSpecs.GetSampleTimeArray(firstSampleIndex, sampleCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetArray()
    {
        return _sampleList.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetArray(int sampleCount)
    {
        return _sampleList.Take(sampleCount).ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetArray(int firstSampleIndex, int sampleCount)
    {
        return _sampleList.Skip(firstSampleIndex).Take(sampleCount).ToArray();
    }


    public Float64SampledTimeSignal GetLinearPaddedSignal()
    {
        var sampleCount = Count;

        var paddedSignalSamples = new List<double>(_sampleList);

        // The padded signal always has an odd number of samples
        var u1 = _sampleList[^1];
        var u2 = _sampleList[0];
        for (var i = 0; i < sampleCount - 1; i++)
        {
            var t = (i + 1) / (double)sampleCount;
            var u = (1 - t) * u1 + t * u2;

            paddedSignalSamples.Add(u);
        }

        return paddedSignalSamples.CreateSignal(SamplingRate);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal FilterSpikes(double histogramTrimPercentage = 1e-2d)
    {
        if (histogramTrimPercentage == 0d)
            return GetFiniteSignal();

        return Float64SignalLog2Histogram
            .Create(this)
            .Trim(histogramTrimPercentage)
            .FilterSignal(GetFiniteSignal());
    }

    public Float64SampledTimeSignalComposer Repeat(int count)
    {
        var sampleList = new List<double>(Count * count);

        for (var i = 0; i < count; i++)
            sampleList.AddRange(_sampleList);

        return new Float64SampledTimeSignalComposer(SamplingRate, sampleList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetRunningAverageSignal()
    {
        return GetRunningAverageSignal(Count, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetRunningAverageSignal(int sampleCount)
    {
        return GetRunningAverageSignal(sampleCount, 0);
    }

    public Float64SampledTimeSignal GetRunningAverageSignal(int pastSampleCount, int futureSampleCount)
    {
        if (pastSampleCount < 0)
            throw new ArgumentOutOfRangeException(nameof(pastSampleCount));

        if (futureSampleCount < 0)
            throw new ArgumentOutOfRangeException(nameof(futureSampleCount));

        var signal = new Float64SampledTimeSignalComposer(SamplingRate, Count);

        for (var index = 0; index < Count; index++)
        {
            var index1 = Math.Max(index - pastSampleCount, 0);
            var index2 = Math.Min(index + futureSampleCount, Count - 1);
            var count = index2 - index1 + 1;

            var average = 0d;
            for (var i = index1; i <= index2; i++)
                average += this[i];

            average /= count;

            signal[index] = average;
        }

        return signal.GetFiniteSignal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetRunningAverageSignal(IEnumerable<int> sampleCountArray)
    {
        return sampleCountArray.Aggregate(
            GetFiniteSignal(),
            (signal, sampleCount) =>
                signal.GetRunningAverageSignal(sampleCount)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetFiniteSignal()
    {
        return Float64SampledTimeSignal.Create(
            SamplingSpecs,
            _sampleList.ToImmutableArray(),
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal GetPeriodicSignal()
    {
        return Float64SampledTimeSignal.Create(
            SamplingSpecs,
            _sampleList.ToImmutableArray(),
            true
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
    {
        return _sampleList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    public override string ToString()
    {
        return _sampleList
            .Select(v => v.ToString("G"))
            .ConcatenateText(", ", "{", "}");
    }
}