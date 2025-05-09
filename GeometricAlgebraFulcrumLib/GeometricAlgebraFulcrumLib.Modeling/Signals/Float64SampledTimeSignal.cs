using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.IntegralTransforms;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Signals;

public sealed class Float64SampledTimeSignal :
    IReadOnlyList<double>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal FiniteZero(double samplingRate, int sampleCount)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            false,
            new RepeatedItemReadOnlyList<double>(0, sampleCount)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal PeriodicZero(double samplingRate, int sampleCount)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            true,
            new RepeatedItemReadOnlyList<double>(0, sampleCount)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal FiniteConstant(double samplingRate, int sampleCount, double value)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            false,
            new RepeatedItemReadOnlyList<double>(value, sampleCount)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal PeriodicConstant(double samplingRate, int sampleCount, double value)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            true,
            new RepeatedItemReadOnlyList<double>(value, sampleCount)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal Create(double samplingRate, IReadOnlyList<double> sampleList, bool isPeriodic)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            isPeriodic,
            sampleList
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal Create(Float64SamplingSpecs samplingSpecs, IReadOnlyList<double> sampleList, bool isPeriodic)
    {
        return new Float64SampledTimeSignal(
            samplingSpecs, 
            isPeriodic,
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal Finite(double samplingRate, IReadOnlyList<double> sampleList)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            false,
            sampleList
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal Periodic(double samplingRate, IReadOnlyList<double> sampleList)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            true,
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal Finite(double samplingRate, IEnumerable<double> sampleList)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            false,
            sampleList.ToImmutableArray()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal Periodic(double samplingRate, IEnumerable<double> sampleList)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            true,
            sampleList.ToImmutableArray()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal Finite(double samplingRate, IEnumerable<Float64Scalar> sampleList)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            false,
            sampleList.SelectToImmutableArray(s => s.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal Periodic(double samplingRate, IEnumerable<Float64Scalar> sampleList)
    {
        return new Float64SampledTimeSignal(
            samplingRate, 
            true,
            sampleList.SelectToImmutableArray(s => s.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreateNonPeriodic(int sampleCount, double tMin, double tMax, Func<double, double> scalarFunc)
    {
        var sampleList = SampledTimeMapList<double>.Create(
            tMin, 
            tMax, 
            false, 
            scalarFunc, 
            sampleCount
        );

        var samplingRate = sampleCount / (tMax - tMin);

        return new Float64SampledTimeSignal(
            samplingRate, 
            false, 
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreatePeriodic(int sampleCount, double periodTime, Func<double, double> scalarFunc)
    {
        var sampleList = SampledTimeMapList<double>.Create(
            0, 
            periodTime, 
            true, 
            scalarFunc, 
            sampleCount
        );
        
        var samplingRate = sampleCount / periodTime;

        return new Float64SampledTimeSignal(
            samplingRate, 
            true, 
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal CreatePeriodic(double samplingRate, int sampleCount, double periodTime, Func<double, double> scalarFunc)
    {
        var sampleList = SampledTimeMapList<double>.Create(
            0, 
            periodTime, 
            true, 
            scalarFunc, 
            sampleCount
        );
        
        return new Float64SampledTimeSignal(
            samplingRate, 
            true, 
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal FiniteConcat(double samplingRate, IEnumerable<double> samples1, IEnumerable<double> samples2)
    {
        return Finite(samplingRate, samples1.Concat(samples2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal FiniteRandomUniform(double samplingRate, int sampleCount, Random randomGenerator)
    {
        var sampleList =
            sampleCount
                .GetRange()
                .SelectToImmutableArray(_ => randomGenerator.NextDouble());

        return new Float64SampledTimeSignal(
            samplingRate,
            false,
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal FiniteRandomUniform(double samplingRate, int sampleCount, Random randomGenerator, double minValue, double maxValue)
    {
        var sampleList =
            sampleCount
                .GetRange()
                .SelectToImmutableArray(_ => randomGenerator.NextDouble(minValue, maxValue));

        return new Float64SampledTimeSignal(
            samplingRate,
            false,
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal FiniteRandomGaussian(double samplingRate, int sampleCount, Random randomGenerator)
    {
        var sampleList =
            sampleCount
                .GetRange()
                .SelectToImmutableArray(_ => randomGenerator.NextGaussian());

        return new Float64SampledTimeSignal(
            samplingRate,
            false,
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal FiniteRandomGaussian(double samplingRate, int sampleCount, Random randomGenerator, double mu, double sigma)
    {
        var sampleList =
            sampleCount
                .GetRange()
                .SelectToImmutableArray(_ => sigma * randomGenerator.NextGaussian() + mu);

        return new Float64SampledTimeSignal(
            samplingRate,
            false,
            sampleList
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator +(Float64SampledTimeSignal signal)
    {
        return signal.MapSamples(s => s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator -(Float64SampledTimeSignal signal)
    {
        return signal.MapSamples(s => -s);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator +(Float64SampledTimeSignal signal1, double signal2)
    {
        return signal1.MapSamples(s => s + signal2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator +(double signal1, Float64SampledTimeSignal signal2)
    {
        return signal2.MapSamples(s => signal1 + s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator -(Float64SampledTimeSignal signal1, double signal2)
    {
        return signal1.MapSamples(s => s - signal2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator -(double signal1, Float64SampledTimeSignal signal2)
    {
        return signal2.MapSamples(s => signal1 - s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator *(Float64SampledTimeSignal signal1, double signal2)
    {
        return signal1.MapSamples(s => s * signal2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator *(double signal1, Float64SampledTimeSignal signal2)
    {
        return signal2.MapSamples(s => signal1 * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator /(Float64SampledTimeSignal signal1, double signal2)
    {
        return signal1.MapSamples(s => s / signal2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator /(double signal1, Float64SampledTimeSignal signal2)
    {
        return signal2.MapSamples(s => signal1 / s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator +(Float64SampledTimeSignal signal1, Float64SampledTimeSignal signal2)
    {
        if (signal1.SamplingRate != signal2.SamplingRate)
            throw new InvalidOperationException();

        return signal1.MapSamples(signal2, (s1, s2) => s1 + s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator -(Float64SampledTimeSignal signal1, Float64SampledTimeSignal signal2)
    {
        if (signal1.SamplingRate != signal2.SamplingRate)
            throw new InvalidOperationException();

        return signal1.MapSamples(signal2, (s1, s2) => s1 - s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator *(Float64SampledTimeSignal signal1, Float64SampledTimeSignal signal2)
    {
        if (signal1.SamplingRate != signal2.SamplingRate)
            throw new InvalidOperationException();

        return signal1.MapSamples(signal2, (s1, s2) => s1 * s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SampledTimeSignal operator /(Float64SampledTimeSignal signal1, Float64SampledTimeSignal signal2)
    {
        if (signal1.SamplingRate != signal2.SamplingRate)
            throw new InvalidOperationException();

        return signal1.MapSamples(signal2, (s1, s2) => (s1 / s2).NaNToZero());
    }
    

    public bool IsPeriodic { get; }

    public Float64SamplingSpecs SamplingSpecs { get; }

    public IReadOnlyList<double> SampleList { get; }
    
    public Float64ScalarRange TimeRange 
        => SamplingSpecs.TimeRange;

    public double SamplingRate 
        => SamplingSpecs.SamplingRate;

    public int Count
        => SampleList.Count;
    
    public double this[int index]
    {
        get
        {
            if (IsPeriodic) 
                return SampleList[index.Mod(Count)];

            return index >= 0 && index < SampleList.Count
                ? SampleList[index]
                : throw new IndexOutOfRangeException();
        }
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SampledTimeSignal(double samplingRate, bool isPeriodic, IReadOnlyList<double> sampleList)
    {
        if (double.IsNaN(samplingRate) || double.IsInfinity(samplingRate) || samplingRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        var sampleCount = sampleList.Count;

        if (sampleCount < 2)
            throw new ArgumentException(nameof(sampleList));

        IsPeriodic = isPeriodic;
        SamplingSpecs = Float64SamplingSpecs.CreateFromSamplingRate(sampleCount, samplingRate);
        SampleList = sampleList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SampledTimeSignal(Float64SamplingSpecs samplingSpecs, bool isPeriodic, IReadOnlyList<double> sampleList)
    {
        if (samplingSpecs.SampleCount != sampleList.Count || samplingSpecs.SampleCount < 2)
            throw new ArgumentException();

        IsPeriodic = isPeriodic;
        SamplingSpecs = samplingSpecs;
        SampleList = sampleList;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return SampleList.All(s => s.IsZero());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return SampleList.All(s => s.IsNearZero(zeroEpsilon));
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
    public Float64SampledTimeSignal GetSubSignal(int index, int count)
    {
        var sampleList =
            Enumerable
                .Range(index, count)
                .SelectToImmutableArray(i => SampleList[i]);

        return new Float64SampledTimeSignal(
            SamplingSpecs.SamplingRate,
            IsPeriodic,
            sampleList
        );
    }

    public Float64SampledTimeSignal ReSample(int sampleCount)
    {
        var tMin = SamplingSpecs.MinTime;
        var tMax = SamplingSpecs.MaxTime;

        var samplingRate =
            SamplingRate * ((sampleCount - 1d) / (Count - 1d));

        var tValues =
            tMin.GetLinearRange(tMax, sampleCount).ToArray();

        return tValues
            .Select(LinearInterpolation)
            .CreateSignal(samplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal DownSampleByFactor(int factor)
    {
        return DownSampleByFactor(0, factor);
    }

    public Float64SampledTimeSignal DownSampleByFactor(int index, int factor)
    {
        if (factor < 2)
            throw new ArgumentOutOfRangeException(nameof(factor), "Sampling factor must be 2 or more");

        var sampleList = new List<double>();
        for (var i = index; i < SampleList.Count; i += factor)
            sampleList.Add(SampleList[i]);

        return new Float64SampledTimeSignal(
            SamplingRate / factor,
            IsPeriodic,
            sampleList
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Complex[] GetFourierArray(FourierOptions options = FourierOptions.Default)
    {
        var complexSamplesArray =
            SampleList.Select(v => (Complex)v).ToArray();

        Fourier.Forward(complexSamplesArray, options);

        return complexSamplesArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal MapSamples(Func<double, double> sampleMapping)
    {
        return SampleList
            .Select(sampleMapping)
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal MapSamples(Func<int, double, double> sampleMapping)
    {
        return SampleList
            .Select((s, i) => sampleMapping(i, s))
            .CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal MapSamples(IReadOnlyList<double> signal2, Func<double, double, double> sampleMapping)
    {
        var sampleCount = Math.Max(Count, signal2.Count);
        var sampleList = new double[sampleCount];

        for (var i = 0; i < sampleCount; i++)
        {
            var s1 = i < Count ? SampleList[i] : 0d;
            var s2 = i < signal2.Count ? signal2[i] : 0d;

            sampleList[i] = sampleMapping(s1, s2);
        }

        return sampleList.CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal MapSamples(IReadOnlyList<double> signal2, Func<int, double, double, double> sampleMapping)
    {
        var sampleCount = Math.Max(Count, signal2.Count);
        var sampleList = new double[sampleCount];

        for (var i = 0; i < sampleCount; i++)
        {
            var s1 = i < Count ? SampleList[i] : 0d;
            var s2 = i < signal2.Count ? signal2[i] : 0d;

            sampleList[i] = sampleMapping(i, s1, s2);
        }

        return sampleList.CreateSignal(SamplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal AddRandomUniform(Random randomGenerator)
    {
        return MapSamples(
            s => s + randomGenerator.NextDouble()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal AddRandomUniform(Random randomGenerator, double minValue, double maxValue)
    {
        return MapSamples(
            s => s + randomGenerator.NextDouble(minValue, maxValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal AddRandomGaussian(Random randomGenerator)
    {
        return MapSamples(
            s => s + randomGenerator.NextGaussian()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal AddRandomGaussian(Random randomGenerator, double mu, double sigma)
    {
        return MapSamples(
            s => s + sigma * randomGenerator.NextGaussian() + mu
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Sqrt()
    {
        return MapSamples(Math.Sqrt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Cbrt()
    {
        return MapSamples(Math.Cbrt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Square()
    {
        return MapSamples(s => Math.Pow(s, 2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Cube()
    {
        return MapSamples(s => Math.Pow(s, 3));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Power(double powerScalar)
    {
        return MapSamples(s => Math.Pow(s, powerScalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Power(IReadOnlyList<double> powerScalarSignal)
    {
        return MapSamples(powerScalarSignal, Math.Pow);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Exp()
    {
        return MapSamples(Math.Exp);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Log()
    {
        return MapSamples(s => Math.Log(s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Log2()
    {
        return MapSamples(Math.Log2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Log10()
    {
        return MapSamples(Math.Log10);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Log(double baseScalar)
    {
        return MapSamples(s => Math.Log(s, baseScalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Log(IReadOnlyList<double> baseScalarSignal)
    {
        return MapSamples(baseScalarSignal, Math.Log);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Abs()
    {
        return MapSamples(Math.Abs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Floor()
    {
        return MapSamples(Math.Floor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Ceiling()
    {
        return MapSamples(Math.Ceiling);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Truncate()
    {
        return MapSamples(Math.Truncate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Sin()
    {
        return MapSamples(Math.Sin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Cos()
    {
        return MapSamples(Math.Cos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Tan()
    {
        return MapSamples(Math.Tan);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal ArcCos()
    {
        return MapSamples(s => s.ArcCos());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal ArcSin()
    {
        return MapSamples(s => s.ArcSin());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal ArcTan()
    {
        return MapSamples(s => s.ArcTan());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Sinh()
    {
        return MapSamples(Math.Sinh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Cosh()
    {
        return MapSamples(Math.Cosh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Tanh()
    {
        return MapSamples(Math.Tanh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Asinh()
    {
        return MapSamples(Math.Asinh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Acosh()
    {
        return MapSamples(Math.Acosh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal Atanh()
    {
        return MapSamples(Math.Atanh);
    }


    /// <summary>
    /// Use trapezoidal rule for integrating this signal
    /// https://en.wikipedia.org/wiki/Trapezoidal_rule
    /// </summary>
    /// <param name="integrationConstant"></param>
    /// <returns></returns>
    public Float64SampledTimeSignal IntegrateTrapezoidal(double integrationConstant = 0d)
    {
        var sampleList = new double[Count];

        sampleList[0] = integrationConstant;

        var areaSum = integrationConstant;
        var dt = SamplingSpecs.TimeResolution;
        var f0 = SampleList[0];

        for (var i = 1; i < Count; i++)
        {
            var f1 = SampleList[i];

            areaSum += 0.5d * (f0 + f1) * dt;

            sampleList[i] = areaSum;

            f0 = f1;
        }

        return new Float64SampledTimeSignal(
            SamplingRate,
            IsPeriodic,
            sampleList
        );
    }


    public Pair<double> GetMinMaxValues()
    {
        var minValue = double.PositiveInfinity;
        var maxValue = double.NegativeInfinity;

        foreach (var value in SampleList)
        {
            if (minValue > value) minValue = value;
            if (maxValue < value) maxValue = value;
        }

        return new Pair<double>(minValue, maxValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal FlipX()
    {
        return new Float64SampledTimeSignal(
            SamplingSpecs,
            IsPeriodic,
            SampleList.Reverse().ToImmutableArray()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal FlipY()
    {
        var (minValue, maxValue) = GetMinMaxValues();
        var minMaxPlus = minValue + maxValue;

        return MapSamples(s => minMaxPlus - s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal FlipXy()
    {
        var (minValue, maxValue) = GetMinMaxValues();
        var minMaxPlus = minValue + maxValue;

        return new Float64SampledTimeSignal(
            SamplingSpecs,
            IsPeriodic,
            SampleList.Select(s => minMaxPlus - s).Reverse().ToImmutableArray()
        );
    }


    public double EnergyFft()
    {
        // Compute FFT
        var real = SampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
        var sampleCount = real.Length;

        Fourier.Forward(real, imaginary);

        // Compute AC energy
        var energy = 0d;
        for (var freqIndex = 0; freqIndex < sampleCount; freqIndex++)
            energy += (real[freqIndex].Square() + imaginary[freqIndex].Square()) / (Math.Tau);

        return energy;
    }

    public double EnergyDcFft()
    {
        // Compute FFT
        var real = SampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();

        Fourier.Forward(real, imaginary);

        // Compute DC energy
        return (real[0].Square() + imaginary[0].Square()) / (Math.Tau);
    }

    public double EnergyAcFft()
    {
        // Compute FFT
        var real = SampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
        var sampleCount = real.Length;

        Fourier.Forward(real, imaginary);

        // Compute AC energy
        var energy = 0d;
        for (var freqIndex = 1; freqIndex < sampleCount; freqIndex++)
            energy += (real[freqIndex].Square() + imaginary[freqIndex].Square()) / (Math.Tau);

        return energy;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Energy()
    {
        var energy1 =
            SampleList.Sum(s => s * s) / (Math.Tau);

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
            SampleList.Sum().Square() / (Math.Tau * Count);
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
    public double Mean()
    {
        return SampleList.Sum() / SampleList.Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double MeanSquare()
    {
        return SampleList.Sum(d => d * d) / SampleList.Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double RootMeanSquare()
    {
        return Math.Sqrt(SampleList.Sum(s => s * s) / SampleList.Count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Sum()
    {
        return SampleList.Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double SumOfSquares()
    {
        return SampleList.Sum(s => s * s);
    }

    /// <summary>
    /// Compute peak signal-to-noise ratio (PSNR) in Db. This signal is assumed to
    /// be the original signal and the given input is its reconstructed version
    /// https://en.wikipedia.org/wiki/Peak_signal-to-noise_ratio
    /// </summary>
    /// <param name="reconstructedSignal"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double PeakSignalToNoiseRatioDb(Float64SampledTimeSignal reconstructedSignal)
    {
        var (min, max) = GetMinMaxValues();
        var maxMinDiff = max - min;
        var meanSquareError = (this - reconstructedSignal).MeanSquare();

        return 20 * maxMinDiff.Log10() - 10 * meanSquareError.Log10();
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
        return SampleList.ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetArray(int sampleCount)
    {
        return SampleList.Take(sampleCount).ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] GetArray(int firstSampleIndex, int sampleCount)
    {
        return SampleList.Skip(firstSampleIndex).Take(sampleCount).ToArray();
    }


    public Float64SampledTimeSignal GetLinearPaddedSignal()
    {
        var sampleCount = Count;

        var paddedSignalSamples = new List<double>(SampleList);

        // The padded signal always has an odd number of samples
        var u1 = SampleList[^1];
        var u2 = SampleList[0];
        for (var i = 0; i < sampleCount - 1; i++)
        {
            var t = (i + 1) / (double)sampleCount;
            var u = (1 - t) * u1 + t * u2;

            paddedSignalSamples.Add(u);
        }

        return paddedSignalSamples.CreateSignal(SamplingRate);
    }

    /// <summary>
    /// Apply FFT to given real sampled signal and find the frequency indices of the
    /// dominant frequency using a ratio of the total signal energy
    /// </summary>
    /// <param name="energyThreshold"></param>
    /// <param name="freqCountThreshold"></param>
    /// <returns></returns>
    public IEnumerable<int> GetDominantFrequencyIndexSet(double energyThreshold = 0.998d, int freqCountThreshold = int.MaxValue)
    {
        // Compute FFT
        var real = SampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
        var sampleCount = real.Length;

        Fourier.Forward(real, imaginary);

        //Compute frequency sample energy, and total energy (not including 0 and negative frequencies)
        var energyDictionary = new SortedDictionary<double, int>();
        var energySum = 0d;

        // Ignore negative frequencies from the spectrum,
        // they will be added later using the real symmetry of the signal
        var freqIndexMax = sampleCount.IsOdd()
            ? (sampleCount - 1) / 2
            : (sampleCount - 2) / 2;

        for (var freqIndex = 1; freqIndex <= freqIndexMax; freqIndex++)
        {
            var energy =
                (real[freqIndex].Square() + imaginary[freqIndex].Square()) / (Math.Tau);

            energyDictionary.Add(energy, freqIndex);

            energySum += energy;
        }

        // Find frequencies with most energy, but always include 0 frequency
        var threshold = energyThreshold * energySum;
        var indexSet = new HashSet<int> { 0 };

        freqCountThreshold--;
        foreach (var (energy, freqIndex) in energyDictionary.Reverse())
        {
            indexSet.Add(freqIndex);

            freqCountThreshold--;
            threshold -= energy;

            if (threshold < 0d || freqCountThreshold <= 0)// || indexSet.Count < 2)
                break;
        }

        return indexSet;
    }

    /// <summary>
    /// Apply FFT to given real sampled signal and find the frequency indices of the
    /// dominant frequency using a ratio of the total signal energy
    /// </summary>
    /// <param name="energyThreshold"></param>
    /// <returns></returns>
    public IEnumerable<FrequencyDataRecord<double>> GetDominantFrequencyDataRecords(double energyThreshold = 0.998d)
    {
        // Compute FFT
        var real = SampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
        var sampleCount = real.Length;

        Fourier.Forward(real, imaginary);

        //Compute frequency sample energy, and total energy (not including 0 and negative frequencies)
        var energyDictionary = new SortedDictionary<double, int>();
        var energySum = 0d;

        // Ignore negative frequencies from the spectrum,
        // they will be added later using the real symmetry of the signal
        var freqIndexMax = sampleCount.IsOdd()
            ? (sampleCount - 1) / 2
            : (sampleCount - 2) / 2;

        for (var freqIndex = 1; freqIndex <= freqIndexMax; freqIndex++)
        {
            var energy =
                (real[freqIndex].Square() + imaginary[freqIndex].Square()) / (Math.Tau);

            energyDictionary.Add(energy, freqIndex);

            energySum += energy;
        }

        // Find frequencies with most energy, but always include 0 frequency
        var threshold = energyThreshold * energySum;
        var indexSet = new List<FrequencyDataRecord<double>>()
        {
            new FrequencyDataRecord<double>(0, 0, 0)
        };

        var df = SamplingRate / (Count - 1);
        foreach (var (energy, freqIndex) in energyDictionary.Reverse())
        {
            indexSet.Add(new FrequencyDataRecord<double>(
                freqIndex,
                df * freqIndex,
                energy / energySum
            ));

            threshold -= energy;

            if (threshold <= 0d)// || indexSet.Count < 2)
                break;
        }

        return indexSet;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarFourierSeries CreateFourierSeries(double energyThreshold)
    {
        var frequencyIndexSet =
            GetDominantFrequencyIndexSet(energyThreshold);

        return ScalarFourierSeries.Create(SampleList, SamplingRate, frequencyIndexSet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarFourierSeries CreateFourierSeries()
    {
        var indexCount = Count.IsOdd()
            ? (Count + 1) / 2 : Count / 2 + 1;

        var frequencyIndexSet =
            Enumerable.Range(0, indexCount);

        return ScalarFourierSeries.Create(SampleList, SamplingRate, frequencyIndexSet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarFourierSeries CreateFourierInterpolator(double snrThreshold, double energyThreshold)
    {
        var frequencyIndexSet =
            GetDominantFrequencyIndexSet(energyThreshold);

        return ScalarFourierSeries.Create(
            this,
            snrThreshold,
            frequencyIndexSet
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarFourierSeries CreateFourierInterpolator(IEnumerable<int> frequencyIndexSet)
    {
        return ScalarFourierSeries.Create(SampleList, SamplingRate, frequencyIndexSet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ScalarFourierSeries CreateFourierInterpolator(double snrThreshold, IEnumerable<int> frequencyIndexSet)
    {
        return ScalarFourierSeries.Create(
            this,
            snrThreshold,
            frequencyIndexSet
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal FourierInterpolate(double energyThreshold = 0.998)
    {
        var frequencyIndexSet =
            GetDominantFrequencyIndexSet(energyThreshold);

        return FourierInterpolate(frequencyIndexSet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal FourierInterpolate(IEnumerable<int> frequencyIndexSet)
    {
        var interpolator = CreateFourierInterpolator(frequencyIndexSet);

        return 0d
            .GetLinearRange((Count - 1) / SamplingRate, Count)
            .Select(interpolator.GetValue)
            .CreateSignal(SamplingRate);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfLinearSplineSignalInterpolator GetLinearInterpolator(DfLinearSplineSignalInterpolatorOptions options)
    {
        return DfLinearSplineSignalInterpolator.Create(this, options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfCatmullRomSplineSignalInterpolator GetCatmullRomInterpolator(DfCatmullRomSplineSignalInterpolatorOptions options)
    {
        return DfCatmullRomSplineSignalInterpolator.Create(this, options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfChebyshevSignalInterpolator GetChebyshevInterpolator(DfChebyshevSignalInterpolatorOptions options)
    {
        return DfChebyshevSignalInterpolator.Create(this, options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DfFourierSignalInterpolator GetFourierInterpolator(DfFourierSignalInterpolatorOptions options)
    {
        return DfFourierSignalInterpolator.Create(this, options);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DifferentialSignalInterpolatorFunction GetDifferentialInterpolator(DfSignalInterpolatorOptions options)
    {
        return options switch
        {
            DfLinearSplineSignalInterpolatorOptions options1 =>
                DfLinearSplineSignalInterpolator.Create(this, options1),

            DfCatmullRomSplineSignalInterpolatorOptions options1 =>
                DfCatmullRomSplineSignalInterpolator.Create(this, options1),

            DfChebyshevSignalInterpolatorOptions options1 =>
                DfChebyshevSignalInterpolator.Create(this, options1),

            DfFourierSignalInterpolatorOptions options1 =>
                DfFourierSignalInterpolator.Create(this, options1),

            _ =>
                throw new ArgumentException(nameof(options))
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double LinearInterpolation(double t)
    {
        var sampleIndex = SamplingRate * t;

        if (sampleIndex < 0 || sampleIndex > SampleList.Count - 1)
            return 0;

        var i1 = (int)Math.Floor(sampleIndex);
        var i2 = (int)Math.Ceiling(sampleIndex);

        t = sampleIndex - Math.Truncate(sampleIndex);

        return (1 - t) * SampleList[i1] + t * SampleList[i2];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64SampledTimeSignal FilterSpikes(double histogramTrimPercentage = 1e-2d)
    {
        if (histogramTrimPercentage == 0d)
            return this;

        return Float64SignalLog2Histogram
            .Create(this)
            .Trim(histogramTrimPercentage)
            .FilterSignal(this);
    }

    public Float64SampledTimeSignal Repeat(int count)
    {
        var sampleList = new List<double>(Count * count);

        for (var i = 0; i < count; i++)
            sampleList.AddRange(SampleList);

        return new Float64SampledTimeSignal(
            SamplingRate, 
            IsPeriodic, 
            sampleList
        );
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

        var signal = Float64SampledTimeSignalComposer.Create(SamplingRate, Count);

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
            this,
            (signal, sampleCount) =>
                signal.GetRunningAverageSignal(sampleCount)
        );
    }

    public Float64ComplexSignalSpectrum GetFourierSpectrum()
    {
        var scalingFactor = Math.Sqrt(Count);

        // Compute FFT
        var real = SampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
        var sampleCount = real.Length;

        Fourier.Forward(real, imaginary);

        var spectrum = new Float64ComplexSignalSpectrum(sampleCount, SamplingRate);
        for (var index = 0; index < sampleCount; index++)
        {
            var value = new Complex(
                real[index] / scalingFactor,
                imaginary[index] / scalingFactor
            );

            spectrum.Add(index, value);
        }

        return spectrum;
    }

    public Float64SignalSpectrum GetEnergySpectrum()
    {
        // Compute FFT
        var real = SampleList.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
        var sampleCount = real.Length;

        Fourier.Forward(real, imaginary);

        var energySpectrum = new Float64SignalSpectrum(sampleCount, SamplingRate);
        for (var index = 0; index < sampleCount; index++)
        {
            var energy =
                (real[index].Square() + imaginary[index].Square()) / (Math.Tau);

            energySpectrum.Add(index, energy);
        }

        return energySpectrum;
    }

    public Float64ComplexSignalSpectrum GetFourierSpectrum(DfFourierSignalInterpolatorOptions spectrumThresholdSpecs)
    {
        if (spectrumThresholdSpecs.EnergyAcPercentThreshold is <= 0 or > 1d)
            throw new ArgumentOutOfRangeException();

        if (spectrumThresholdSpecs.SignalToNoiseRatioThreshold <= 1d)
            throw new ArgumentOutOfRangeException();

        // Compute complete Fourier spectrum of signal
        var scalarSpectrumFull =
            (Float64ComplexSignalSpectrum)GetFourierSpectrum().RemoveHighFrequencySamples(spectrumThresholdSpecs.FrequencyThreshold);

        // Compute the energy spectrum of signal
        var energySpectrumFull =
            (Float64SignalSpectrum)GetEnergySpectrum().RemoveHighFrequencySamples(spectrumThresholdSpecs.FrequencyThreshold);

        var samplingSpecs = energySpectrumFull.SamplingSpecs;

        // Define time axis values
        var tValues =
            samplingSpecs.GetSampleTimeSignal();

        // Add DC components to final spectrum
        var vectorSpectrum =
            new Float64ComplexSignalSpectrum(samplingSpecs) { scalarSpectrumFull.SamplesDc };

        // Test total AC energy threshold
        var vectorSignalEnergyAc = EnergyAc();
        if (vectorSignalEnergyAc < spectrumThresholdSpecs.EnergyAcThreshold)
        {
            // Add a single frequency to the spectrum
            var (energySample1, energySample2) =
                energySpectrumFull
                    .SamplePairsAc
                    .OrderByDescending(p => energySpectrumFull.GetValueSumAc(p))
                    .First();

            vectorSpectrum.Add(scalarSpectrumFull.GetSample(energySample1.Index));
            vectorSpectrum.Add(scalarSpectrumFull.GetSample(energySample2.Index));

            return vectorSpectrum;
        }

        // Select all energy spectrum AC sample pairs
        var energySamplePairs =
            energySpectrumFull
                .SamplePairsAc
                .OrderByDescending(p =>
                    energySpectrumFull.GetValueSumAc(p)
                ).ToArray();

        // Compute energy threshold for selecting suitable spectrum samples
        var energy = energySpectrumFull.Sum(s => s.Value);
        var energyThreshold = spectrumThresholdSpecs.EnergyAcPercentThreshold * energy;

        // Define initial error signal for gradually computing SNR
        var sumOfSquares = SumOfSquares();
        var errorSignal = this - vectorSpectrum.GetRealSignal(tValues);

        var frequencyCountThreshold = spectrumThresholdSpecs.FrequencyCountThreshold;

        foreach (var (energySample1, energySample2) in energySamplePairs)
        {
            var index1 = energySample1.Index;
            var index2 = energySample2.Index;

            frequencyCountThreshold--;

            if (index1 == index2)
            {
                // Add the selected samples to spectrum
                var sample1 = scalarSpectrumFull.GetSample(index1);

                vectorSpectrum.Add(sample1);

                if (frequencyCountThreshold <= 0)
                    return vectorSpectrum;

                // Update energy threshold
                energyThreshold -= energySpectrumFull.GetValueAc(index1);

                //Console.WriteLine($"Energy = {(1 - energyThreshold / energy):P5}");

                // Test energy threshold stop condition
                if (energyThreshold < 0)
                    return vectorSpectrum;

                // Update error signal
                errorSignal -= scalarSpectrumFull.GetRealSignal(sample1, tValues);
            }
            else
            {
                // Add the selected samples to spectrum
                var sample1 = scalarSpectrumFull.GetSample(index1);
                var sample2 = scalarSpectrumFull.GetSample(index2);

                vectorSpectrum.Add(sample1);
                vectorSpectrum.Add(sample2);

                if (frequencyCountThreshold <= 0)
                    return vectorSpectrum;

                // Update energy threshold
                energyThreshold -= energySpectrumFull.GetValueAc(index1);
                energyThreshold -= energySpectrumFull.GetValueAc(index2);

                //Console.WriteLine($"Energy = {(1 - energyThreshold / energy):P5}");

                // Test energy threshold stop condition
                if (energyThreshold < 0)
                    return vectorSpectrum;

                // Update error signal
                errorSignal -= scalarSpectrumFull.GetRealSignal(sample1, tValues);
                errorSignal -= scalarSpectrumFull.GetRealSignal(sample2, tValues);
            }

            // Test SNR threshold stop condition
            var signalToNoiseRatio =
                sumOfSquares / errorSignal.SumOfSquares();

            //Console.WriteLine($"SNR = {signalToNoiseRatio:G}");

            if (signalToNoiseRatio >= spectrumThresholdSpecs.SignalToNoiseRatioThreshold)
                break;
        }

        Console.WriteLine();

        return vectorSpectrum;
    }

    public Float64ComplexSignalSpectrum GetFourierSpectrum(double energyPercentThreshold, double signalToNoiseRatioThreshold)
    {
        if (energyPercentThreshold is <= 0 or > 1d)
            throw new ArgumentOutOfRangeException(nameof(energyPercentThreshold));

        if (signalToNoiseRatioThreshold <= 1d)
            throw new ArgumentOutOfRangeException(nameof(signalToNoiseRatioThreshold));

        var tValues =
            GetSampledTimeSignal();

        var fullSpectrum =
            GetFourierSpectrum();


        var spectrum = new Float64ComplexSignalSpectrum(SamplingSpecs);

        // Add DC components
        foreach (var spectrumSample in fullSpectrum.SamplesDc)
            spectrum.Add(spectrumSample);

        // Add significant AC components
        var minEnergyAc = energyPercentThreshold * fullSpectrum.GetEnergyAc();
        var sumOfSquares = SumOfSquares();
        var errorSignal = this - spectrum.GetRealSignal(tValues);
        var sampleList =
            fullSpectrum
                .SamplePairsAc
                .OrderByDescending(s => fullSpectrum.GetEnergyAc(s));

        foreach (var (spectrumSample1, spectrumSample2) in sampleList)
        {
            spectrum.Add(spectrumSample1);
            minEnergyAc -= fullSpectrum.GetEnergyAc(spectrumSample1.Index);

            if (spectrumSample1.Index != spectrumSample2.Index)
            {
                spectrum.Add(spectrumSample2);
                minEnergyAc -= fullSpectrum.GetEnergyAc(spectrumSample2.Index);
            }

            if (minEnergyAc <= 0)
                break;

            errorSignal -= fullSpectrum.GetRealSignal(spectrumSample1, tValues);

            if (spectrumSample1.Index != spectrumSample2.Index)
                errorSignal -= fullSpectrum.GetRealSignal(spectrumSample2, tValues);

            var signalToNoiseRatio =
                sumOfSquares / errorSignal.SumOfSquares();

            if (signalToNoiseRatio >= signalToNoiseRatioThreshold)
                break;
        }

        spectrum.RemoveZeroValueSamples();

        return spectrum;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
    {
        return SampleList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    public override string ToString()
    {
        return SampleList
            .Select(v => v.ToString("G"))
            .ConcatenateText(", ", "{", "}");
    }
}