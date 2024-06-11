using System.Collections.Immutable;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using MathNet.Numerics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals;

public static class Float64SignalUtils
{
    public static Float64Signal GetPeriodicPaddedSignal_NoInterpolator(this Float64Signal signal, int trendSampleCount, int paddingSampleCount = -1)
    {
        if (paddingSampleCount < 0)
            paddingSampleCount = signal.Count;

        if (trendSampleCount < 0 || trendSampleCount >= signal.Count)
            throw new ArgumentOutOfRangeException(nameof(trendSampleCount));

        var samplingSpecs = signal.SamplingSpecs;

        var n = signal.Count;
        var m = paddingSampleCount;
        var k = trendSampleCount;

        var s1 = signal[0];
        var s2 = signal[^1];

        var uValues = new List<double>();

        for (var i = 0; i < n; i++)
            uValues.Add(signal[i]);

        for (var i = 1; i < k; i++)
            uValues.Add(2 * s2 - signal[n - 1 - i]);

        var sList =
            (2 * s2 - signal[n - k - 1]).GetLinearRange(2 * s1 - signal[k], m + 2, false).Skip(1).Take(m).ToArray();

        for (var i = 0; i < m; i++)
            uValues.Add(sList[i]);

        for (var i = 0; i < k - 1; i++)
            uValues.Add(2 * s1 - signal[k - 1 - i]);

        var uSignal =
            uValues.CreateSignal(samplingSpecs.SamplingRate);

        //uSignal.PlotSignal(
        //    uSignal.SamplingSpecs.MinTime,
        //    uSignal.SamplingSpecs.MaxTime,
        //    @"D:\uSignal"
        //);

        return uSignal;
    }

    public static Float64Signal GetPeriodicPaddedSignal_CatmullRom(this Float64Signal signal, int trendSampleCount, int paddingSampleCount = -1)
    {
        if (paddingSampleCount < 0)
            paddingSampleCount = signal.Count;

        if (trendSampleCount < 0 || trendSampleCount >= signal.Count)
            throw new ArgumentOutOfRangeException(nameof(trendSampleCount));

        var samplingSpecs = signal.SamplingSpecs;

        var n = signal.Count;
        var m = paddingSampleCount;
        var k = trendSampleCount;

        var s1 = signal[0];
        var s2 = signal[^1];

        var tValues = new List<double>();
        var uValues = new List<double>();

        var tList =
            samplingSpecs.GetSampledTimeSignal(
                n - k - 1,
                k + 1
            );

        for (var i = 0; i < k; i++)
        {
            tValues.Add(tList[i]);
            uValues.Add(signal[n - k - 1 + i]);
        }

        tList =
            samplingSpecs.GetSampledTimeSignal(
                n - 1,
                k + 1
            );

        for (var i = 1; i < k; i++)
        {
            tValues.Add(tList[i]);
            uValues.Add(2 * s2 - signal[n - 1 - i]);
        }

        tList =
            samplingSpecs.GetSampledTimeSignal(
                n + k,
                m
            );

        var sList =
            (2 * s2 - signal[n - k - 1]).GetLinearRange(2 * s1 - signal[k], m + 2, false).Skip(1).Take(m).ToArray();

        for (var i = 0; i < m; i++)
        {
            tValues.Add(tList[i]);
            uValues.Add(sList[i]);
        }

        tList =
            samplingSpecs.GetSampledTimeSignal(
                n + m + k,
                k + 1
            );

        for (var i = 0; i < k - 1; i++)
        {
            tValues.Add(tList[i]);
            uValues.Add(2 * s1 - signal[k - 1 - i]);
        }

        tList =
            samplingSpecs.GetSampledTimeSignal(
                n + m + 2 * k,
                k + 1
            );

        for (var i = 0; i < k; i++)
        {
            tValues.Add(tList[i]);
            uValues.Add(signal[i]);
        }

        //var uSignal =
        //    uValues.CreateSignal(samplingSpecs.SamplingRate);

        //uSignal.PlotSignal(
        //    uSignal.SamplingSpecs.MinTime,
        //    uSignal.SamplingSpecs.MaxTime,
        //    @"D:\uSignal"
        //);

        //return uSignal;

        var p1 = DfComputedFunction.Create(
            DfCatmullRomSplineInterpolator.CreateFromSortedX(
                tValues.ToArray(),
                uValues.ToArray()
            ).GetValue
        );

        var p1Signal = samplingSpecs.GetSampledFunctionSignal(
            p1.GetValue,
            n - 1,
            m + 2 * k + 2
        );

        var paddedSignalSamples = new List<double>(n + m + 2 * k + 2);

        paddedSignalSamples.AddRange(signal);
        paddedSignalSamples.AddRange(p1Signal.Skip(1).Take(p1Signal.Count - 2));
        paddedSignalSamples.AddRange(signal);

        var paddedSignal = paddedSignalSamples.CreateSignal(signal.SamplingRate);

        //paddedSignal.PlotSignal(
        //    paddedSignal.SamplingSpecs.MinTime,
        //    paddedSignal.SamplingSpecs.MaxTime,
        //    @"D:\paddedSignal"
        //);

        return paddedSignal;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Signal GetPeriodicPaddedSignal(this Float64Signal signal, int trendSampleCount, int paddingSampleCount = -1, bool useCatmullRomInterpolator = true)
    {
        return useCatmullRomInterpolator
            ? signal.GetPeriodicPaddedSignal_CatmullRom(trendSampleCount, paddingSampleCount)
            : signal.GetPeriodicPaddedSignal_NoInterpolator(trendSampleCount, paddingSampleCount);
    }

    public static ComplexSignalSpectrum GetFourierSpectrum(this Float64Signal signal, DfFourierSignalInterpolatorOptions options)
    {
        if (options.EnergyAcPercentThreshold is <= 0 or > 1d)
            throw new ArgumentOutOfRangeException();

        if (options.SignalToNoiseRatioThreshold <= 1d)
            throw new ArgumentOutOfRangeException();

        // Compute complete Fourier spectrum of vector component signals
        var vectorSpectrumFull =
            (ComplexSignalSpectrum)signal
                .GetFourierSpectrum()
                .RemoveHighFrequencySamples(options.FrequencyThreshold);

        // Compute a single joint energy spectrum of vector signal
        var energySpectrumFull =
            (Float64SignalSpectrum)signal
                .GetEnergySpectrum()
                .RemoveHighFrequencySamples(options.FrequencyThreshold);

        var samplingSpecs = energySpectrumFull.SamplingSpecs;

        // Define time axis values
        var tValues =
            samplingSpecs.GetSampledTimeSignal();

        // Add DC components to final vector spectrum
        var vectorSpectrum =
            new ComplexSignalSpectrum(samplingSpecs)
            {
                vectorSpectrumFull.SamplesDc
            };

        // Test total AC energy threshold
        var vectorSignalEnergyAc = signal.EnergyAc();
        if (vectorSignalEnergyAc < options.EnergyAcThreshold)
        {
            // Add a single frequency to the spectrum
            var (energySample1, energySample2) =
                energySpectrumFull
                    .SamplePairsAc
                    .OrderByDescending(p => energySpectrumFull.GetValueSumAc(p))
                    .First();

            vectorSpectrum.Add(vectorSpectrumFull.GetSample(energySample1.Index));
            vectorSpectrum.Add(vectorSpectrumFull.GetSample(energySample2.Index));

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
        var energyThreshold = options.EnergyAcPercentThreshold * energy;

        // Define initial error signal for gradually computing SNR
        var sumOfSquares = signal.SumOfSquares();
        var errorSignal = signal - vectorSpectrum.GetRealSignal(tValues);

        var frequencyCountThreshold = options.FrequencyCountThreshold;

        foreach (var (energySample1, energySample2) in energySamplePairs)
        {
            var index1 = energySample1.Index;
            var index2 = energySample2.Index;

            frequencyCountThreshold--;

            if (index1 == index2)
            {
                // Add the selected samples to vector spectrum
                var sample1 = vectorSpectrumFull.GetSample(index1);

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
                errorSignal -= vectorSpectrumFull.GetRealSignal(sample1, tValues);
            }
            else
            {
                // Add the selected samples to vector spectrum
                var sample1 = vectorSpectrumFull.GetSample(index1);
                var sample2 = vectorSpectrumFull.GetSample(index2);

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
                errorSignal -= vectorSpectrumFull.GetRealSignal(sample1, tValues);
                errorSignal -= vectorSpectrumFull.GetRealSignal(sample2, tValues);
            }

            // Test SNR threshold stop condition
            var signalToNoiseRatio =
                sumOfSquares / errorSignal.SumOfSquares();

            //Console.WriteLine($"SNR = {signalToNoiseRatio:G}");

            if (signalToNoiseRatio >= options.SignalToNoiseRatioThreshold)
                break;
        }

        Console.WriteLine();

        return vectorSpectrum;
    }


    private static IEnumerable<double> CrossCorrelationWiener(IReadOnlyList<double> scalarSignal, int newOrder, int oldOrder)
    {
        var nTaps = scalarSignal.Count;
        var nSize = nTaps > newOrder ? nTaps : newOrder;

        var outData = new List<double>(2 * nTaps + 1);
        for (var i = -nTaps; i < nTaps; ++i)
        {
            var tsSum = 0.0;
            for (var j = 0; j < nSize; ++j)
            {
                if (j + i < nTaps && j < newOrder && j + i > -1)
                    tsSum += scalarSignal[j + i];
            }

            outData.Add(tsSum);
        }

        var outVector = new List<double>(nTaps + 1);

        for (var i = nTaps; i >= 0; i--)
        {
            var index = outData.Count - oldOrder - i;

            outVector.Add(outData[index]);
        }

        return outVector;
    }

    public static Float64Signal WienerFilter(this IReadOnlyList<double> scalarSignal, double samplingRate, int order)
    {
        var newOrder = 2 * order + 1;

        // Estimate the local mean
        var localMean =
            CrossCorrelationWiener(scalarSignal, newOrder, order)
                .Select(value => value / newOrder).ToArray();

        // Estimate the local variance
        var t2Series =
            scalarSignal.Select(value => Math.Pow(value, 2)).ToArray();

        var localVariance =
            CrossCorrelationWiener(t2Series, newOrder, order)
                .Select((value, index) =>
                    value / newOrder - Math.Pow(localMean[index], 2)
                ).ToArray();

        // Estimate the noise power
        var noisePowerEstimate = localVariance.Sum() / localVariance.Length;

        return scalarSignal
            .Select((t, i) =>
                localVariance[i] < noisePowerEstimate
                    ? localMean[i]
                    : (t - localMean[i]) * (1 - noisePowerEstimate / localVariance[i]) + localMean[i]
            )
            .Select(v => double.IsNaN(v) ? 0d : v)
            .CreateSignal(samplingRate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Signal WienerFilter(this Float64Signal scalarSignal, int order)
    {
        return scalarSignal.WienerFilter(scalarSignal.SamplingRate, order);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SignalToNoiseRatio(this IEnumerable<double> vectorSignal, IEnumerable<double> noiseSignal)
    {
        return vectorSignal.Select(s => s.Square()).Average() /
               noiseSignal.Select(s => s.Square()).Average();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SignalToNoiseRatioDb(this IEnumerable<double> vectorSignal, IEnumerable<double> noiseSignal)
    {
        return (vectorSignal.Select(s => s.Square()).Average() /
                noiseSignal.Select(s => s.Square()).Average()).Log10() * 10d;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Signal FourierInterpolate(this Float64Signal scalarSignal, double samplingRate, double energyThreshold = 0.998)
    {
        var normSignal = scalarSignal.CreateSignal(samplingRate);

        var normInterpolator = normSignal.CreateFourierInterpolator(
            normSignal.GetDominantFrequencyIndexSet(energyThreshold)
        );

        var t = 0d.GetLinearRange(
            (scalarSignal.Count - 1) / samplingRate,
            scalarSignal.Count
        );

        return normInterpolator.GetValues(t).CreateSignal(samplingRate);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfChebyshevSignalInterpolator CreateChebyshevInterpolator(this Float64Signal signal, DfChebyshevSignalInterpolatorOptions options)
    {
        return DfChebyshevSignalInterpolator.Create(signal, options);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Signal ReSampleForBezierSmoothing(this Float64Signal signal, int bezierDegree)
    {
        var sampleCount =
            (int)(Math.Ceiling((signal.Count - 1) / (double)bezierDegree) * bezierDegree) + 1;

        return signal.Count == sampleCount
            ? signal
            : signal.ReSample(sampleCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfAkimaSplineInterpolator CreateSmoothedAkimaSplineFunction(this Float64Signal signal, int bezierDegree)
    {
        var reSampledSignal =
            signal.ReSampleForBezierSmoothing(bezierDegree);

        var tSignal =
            reSampledSignal.GetSampledTimeSignal();

        var (xArray, yArray) =
            reSampledSignal.GetBezierSmoothingPairs(
                tSignal,
                bezierDegree,
                true
            );

        var f = yArray.CreateAkimaSplineFunction(xArray, true);

        return f;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<Complex> GetValue(this IReadOnlyList<ComplexSignalSpectrum> vectorSpectrum, IReadOnlyList<ScalarSignalSpectrum<Complex>.SignalSpectrumSample> vectorSpectrumSample, double t)
    {
        var valueArray = new Complex[vectorSpectrumSample.Count];

        for (var i = 0; i < vectorSpectrumSample.Count; i++)
        {
            valueArray[i] = vectorSpectrum[i].GetValue(vectorSpectrumSample[i], t);
        }

        return valueArray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<Float64Signal> GetRealSignal(this IReadOnlyList<ComplexSignalSpectrum> vectorSpectrum, Float64Signal tValues)
    {
        return vectorSpectrum
            .Select(spectrum => spectrum.GetRealSignal(tValues))
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<Float64Signal> GetRealSignalDt(this IReadOnlyList<ComplexSignalSpectrum> vectorSpectrum, int degree, Float64Signal tValues)
    {
        return vectorSpectrum
            .Select(spectrum => spectrum.GetRealSignalDt(degree, tValues))
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<Float64Signal> GetRealSignal(this IReadOnlyList<ComplexSignalSpectrum> vectorSpectrum, IReadOnlyList<ScalarSignalSpectrum<Complex>.SignalSpectrumSample> vectorSpectrumSample, IEnumerable<double> tValues)
    {
        return vectorSpectrum
            .Select((spectrum, i) => spectrum.GetRealSignal(vectorSpectrumSample[i], tValues))
            .ToImmutableArray();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<ScalarSignalSpectrum<Complex>.SignalSpectrumSample> GetSample(this IReadOnlyList<ComplexSignalSpectrum> vectorSpectrum, int index)
    {
        return vectorSpectrum
            .Select(s => s.GetSample(index))
            .ToImmutableArray();
    }

    public static IReadOnlyList<ComplexSignalSpectrum> Add(this IReadOnlyList<ComplexSignalSpectrum> vectorSpectrum, IEnumerable<ScalarSignalSpectrum<Complex>.SignalSpectrumSample> vectorSpectrumSample)
    {
        var i = 0;
        foreach (var sample in vectorSpectrumSample)
        {
            vectorSpectrum[i].Add(sample);

            i++;
        }

        return vectorSpectrum;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<Float64SignalSpectrum> RemoveHighFrequencySamples(this IReadOnlyList<Float64SignalSpectrum> vectorSpectrum, double cutoffFrequency)
    {
        return vectorSpectrum
            .Select(s => (Float64SignalSpectrum)s.RemoveHighFrequencySamples(cutoffFrequency))
            .ToArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<ComplexSignalSpectrum> RemoveHighFrequencySamples(this IReadOnlyList<ComplexSignalSpectrum> vectorSpectrum, double cutoffFrequency)
    {
        return vectorSpectrum
            .Select(s => (ComplexSignalSpectrum)s.RemoveHighFrequencySamples(cutoffFrequency))
            .ToArray();
    }


    public static Float64Signal GetPeriodicPaddedSignal1(this Float64Signal signal, int trendSampleCount, int polynomialDegree)
    {
        var scalarProcessor = ScalarProcessorOfFloat64.Instance;

        var sampleCount = signal.Count;

        var tValues = new List<double>(trendSampleCount * 2 + sampleCount);
        var uValues = new List<double>(trendSampleCount * 2 + sampleCount);
            
        for (var i = 0; i < trendSampleCount; i++)
        {
            var sampleIndex = i + sampleCount - trendSampleCount;

            tValues.Add(sampleIndex / signal.SamplingRate);
            uValues.Add(signal[sampleIndex]);
        }

        var u1 = signal[^1];
        var u2 = signal[0];
        for (var i = 0; i < sampleCount - 1; i++)
        {
            var t = (i + 1) / (double) sampleCount;
            var u = (1 - t) * u1 + t * u2;

            var sampleIndex = i + sampleCount;

            tValues.Add(sampleIndex / signal.SamplingRate);
            uValues.Add(u);
        }
            
        for (var i = 0; i < trendSampleCount; i++)
        {
            var sampleIndex = i + 2 * sampleCount;

            tValues.Add(sampleIndex / signal.SamplingRate);
            uValues.Add(signal[i]);
        }

        var polynomial = PolynomialFunction<double>.Create(
            scalarProcessor,
            Fit.Polynomial(
                tValues.ToArray(), 
                uValues.ToArray(), 
                polynomialDegree
            )
        );

        // The padded signal always has an odd number of samples
        var paddedSignalSamples = new List<double>(signal);

        for (var i = 0; i < sampleCount - 1; i++)
        {
            var tValue = (i + sampleCount) / signal.SamplingRate;
            var uValue = polynomial.GetValue(tValue);

            paddedSignalSamples.Add(uValue);
        }

        return paddedSignalSamples.CreateSignal(signal.SamplingRate);
    }
        
    public static Float64Signal GetPeriodicPaddedSignal2(this Float64Signal signal, int trendSampleCount, int polynomialDegree, int paddingSampleCount = -1)
    {
        if (paddingSampleCount == 0)
            return signal.Concat(signal.Reverse()).CreateSignal(signal.SamplingRate);

        if (paddingSampleCount < 0)
            paddingSampleCount = signal.Count;
            
        if (trendSampleCount < 1 || trendSampleCount > signal.Count)
            throw new ArgumentOutOfRangeException(nameof(trendSampleCount));

        if (polynomialDegree < 0 || polynomialDegree > trendSampleCount / 2)
            throw new ArgumentOutOfRangeException(nameof(polynomialDegree));

        var samplingSpecs = signal.SamplingSpecs;

        var n = signal.Count;
        var m = paddingSampleCount;
        var k = trendSampleCount;

        var tValues = new List<double>(trendSampleCount * 2);
        var uValues = new List<double>(trendSampleCount * 2);

        var t1Signal = samplingSpecs.GetSampledTimeSignal(n - k, k);
        var t2Signal = samplingSpecs.GetSampledTimeSignal(m + n, k);

        for (var i = 0; i < k; i++)
        {
            tValues.Add(t1Signal[i]);
            uValues.Add(signal[n - k + i]);
        }

        var tMeanCount = 10;
        var signalMean = signal.Mean();

        var tMeanList = 
            t1Signal[^1]
                .GetLinearRange(t2Signal[0], tMeanCount + 2, false)
                .Skip(1)
                .Take(tMeanCount);

        foreach (var t in tMeanList)
        {
            tValues.Add(t);
            uValues.Add(signalMean);
        }

        for (var i = 0; i < k; i++)
        {
            tValues.Add(t2Signal[i]);
            uValues.Add(signal[n - 1 - i]);
        }
            
        //var p1 = PolynomialFunction<double>.Create(
        //    scalarProcessor,
        //    Fit.Polynomial(
        //        tValues.ToArray(), 
        //        uValues.ToArray(), 
        //        polynomialDegree
        //    )
        //);

        var p1 = DfComputedFunction.Create(
            MathNet.Numerics.Interpolation.NevillePolynomialInterpolation.InterpolateSorted(
                tValues.ToArray(), 
                uValues.ToArray()
            ).Interpolate
        );

        var p1Signal = samplingSpecs.GetSampledFunctionSignal(
            p1.GetValue, 
            n, 
            m
        );

        tValues.Clear();
        uValues.Clear();

        t1Signal = samplingSpecs.GetSampledTimeSignal(m + 2 * n - k, k);
        t2Signal = samplingSpecs.GetSampledTimeSignal(2 * m + 2 * n, k);

        for (var i = 0; i < k; i++)
        {
            tValues.Add(t1Signal[i]);
            uValues.Add(signal[k - 1 - i]);
        }
            
        tMeanList = t1Signal[^1]
            .GetLinearRange(t2Signal[0], tMeanCount + 2, false)
            .Skip(1)
            .Take(tMeanCount);

        foreach (var t in tMeanList)
        {
            tValues.Add(t);
            uValues.Add(signalMean);
        }

        for (var i = 0; i < k; i++)
        {
            tValues.Add(t2Signal[i]);
            uValues.Add(signal[i]);
        }
            
        //var p2 = PolynomialFunction<double>.Create(
        //    scalarProcessor,
        //    Fit.Polynomial(
        //        tValues.ToArray(), 
        //        uValues.ToArray(), 
        //        polynomialDegree
        //    )
        //);
            
        var p2 = DfComputedFunction.Create(
            MathNet.Numerics.Interpolation.NevillePolynomialInterpolation.InterpolateSorted(
                tValues.ToArray(), 
                uValues.ToArray()
            ).Interpolate
        );

        var p2Signal = samplingSpecs.GetSampledFunctionSignal(
            p2.GetValue, 
            m + 2 * n, 
            m
        );

        var paddedSignalSamples = new List<double>(2 * n + 2 * m);

        paddedSignalSamples.AddRange(signal);
        paddedSignalSamples.AddRange(p1Signal);
        paddedSignalSamples.AddRange(signal.Reverse());
        paddedSignalSamples.AddRange(p2Signal);

        var paddedSignal = paddedSignalSamples.CreateSignal(signal.SamplingRate);

        paddedSignal.PlotSignal(
            paddedSignal.SamplingSpecs.MinTime,
            paddedSignal.SamplingSpecs.MaxTime,
            @"D:\paddedSignal"
        );

        return paddedSignal;
    }
        
    public static void PlotSignal(this Float64Signal scalarSignal, double tMin, double tMax, string plotFileName)
    {
        var samplingSpecs = scalarSignal.SamplingSpecs;

        var pm = new PlotModel
        {
            //Title = "title",
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        var s1 = new FunctionSeries(
            t => scalarSignal.LinearInterpolation(t - tMin),
            tMin,
            tMax,
            1024,
            @$"Signal 2"
        )
        {
            StrokeThickness = 1.5
        };

        pm.Series.Add(s1);
            
        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        PngExporter.Export(pm, $"{plotFileName}.png", samplingSpecs.SampleCount * 2, 750, 200);
    }

    public static void PlotSignal(this Float64Signal scalarSignal1, Float64Signal scalarSignal2, double tMin, double tMax, string plotFileName)
    {
        var samplingSpecs = scalarSignal1.SamplingSpecs;

        var pm = new PlotModel
        {
            //Title = "title",
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        var s1 = new FunctionSeries(
            t => scalarSignal1.LinearInterpolation(t - tMin),
            tMin,
            tMax,
            samplingSpecs.SampleCount,
            @"Signal 1"
        )
        {
            LineStyle = LineStyle.Dot,
            StrokeThickness = 1,
            //MarkerType = MarkerType.Diamond,
            //MarkerStrokeThickness = 1,
            //MarkerSize = 4
        };


        var s2 = new FunctionSeries(
            t => scalarSignal2.LinearInterpolation(t - tMin),
            tMin,
            tMax,
            samplingSpecs.SampleCount * 2,
            @$"Signal 2"
        )
        {
            StrokeThickness = 1.5
        };

        pm.Series.Add(s1);
        pm.Series.Add(s2);

        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        PngExporter.Export(pm, $"{plotFileName}.png", samplingSpecs.SampleCount * 2, 750, 200);
    }

    public static void PlotScalarSignal(this Float64Signal scalarSignal, string title, string filePath)
    {
        filePath = Path.Combine(filePath);

        const int sampleTrim = 0;
        var tMin = sampleTrim / scalarSignal.SamplingRate;
        var tMax = (scalarSignal.Count - 1 - sampleTrim) / scalarSignal.SamplingRate;

        var plotWidth = 
            int.Clamp(scalarSignal.Count * 2, 1920, 7680);

        var plotHeight =
            (plotWidth * 9d / 32d).RoundToInt32();

        var pm = new PlotModel
        {
            Title = title,
            Background = OxyColor.FromRgb(255, 255, 255)
        };

        var s1 = new FunctionSeries(
            scalarSignal.LinearInterpolation,
            tMin,
            tMax,
            plotWidth
        );

        pm.Series.Add(s1);

        //OxyPlot.SkiaSharp.PdfExporter.Export(pm, filePath + ".pdf", 1024, 768);
        PngExporter.Export(pm, filePath + ".png", plotWidth, plotHeight, 200);
    }

        
    public static Image Plot(this Func<double, double> scalarFunction, Float64Signal scalarSignal, double xMin, double xMax)
    {
        var model = new PlotModel
        {
            Background = OxyColors.White
        };

        var dx = (xMax - xMin) / 1024;

        model.Axes.Add(
            new LinearAxis()
            {
                Minimum = xMin - (xMax - xMin) / 20,
                Maximum = xMax + (xMax - xMin) / 20,
                Position = AxisPosition.Bottom
            }
        );

        model.Series.Add(
            new FunctionSeries(scalarFunction, xMin, xMax, dx)
            {
                Color = OxyColors.Blue,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        model.Series.Add(
            new FunctionSeries(scalarSignal.LinearInterpolation, xMin, xMax, dx)
            {
                Color = OxyColors.Black,
                LineStyle = LineStyle.Dot,
                StrokeThickness = 1
            }
        );

        var tSignal = scalarSignal.GetSampledTimeSignal();
        var scatterPoints =
            Enumerable
                .Range(0, scalarSignal.Count)
                .Select(i => new ScatterPoint(tSignal[i], scalarSignal[i]))
                .ToList();

        var scatterSeries = new ScatterSeries
        {
            MarkerSize = 4,
            MarkerStroke = OxyColors.Green,
            MarkerFill = OxyColors.Transparent,
            MarkerStrokeThickness = 1,
            MarkerType = MarkerType.Circle
        };

        scatterSeries.Points.AddRange(scatterPoints);

        model.Series.Add(scatterSeries);

        var renderer = new PngExporter
        {
            //Dpi = 120,
            Width = 1200,
            Height = 600
        };

        using var stream = new MemoryStream();
        renderer.Export(model, stream);

        stream.Position = 0;

        return Image.Load(stream);
    }

    public static Image PlotValue(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax)
    {
        return ((Func<double, double>)scalarFunction.GetValue).Plot(scalarSignal, xMin, xMax);
    }

    public static Image PlotFirstDerivative(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax)
    {
        var func = scalarFunction.GetDerivative1().GetValue;

        return func.Plot(scalarSignal, xMin, xMax);
    }

    public static Image PlotSecondDerivative(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax)
    {
        var func = scalarFunction.GetDerivative2().GetValue;

        return func.Plot(scalarSignal, xMin, xMax);
    }

    public static Image Plot(this Func<double, double> scalarFunction, Float64Signal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var model = new PlotModel
        {
            Background = OxyColors.White
        };

        var dx = (xMax - xMin) / 1024;

        model.Axes.Add(
            new LinearAxis()
            {
                Minimum = xMin - (xMax - xMin) / 20,
                Maximum = xMax + (xMax - xMin) / 20,
                Position = AxisPosition.Bottom
            }
        );

        model.Axes.Add(
            new LinearAxis()
            {
                Minimum = yMin - (yMax - yMin) / 20,
                Maximum = yMax + (yMax - yMin) / 20,
                Position = AxisPosition.Left
            }
        );

        model.Series.Add(
            new FunctionSeries(scalarFunction, xMin, xMax, dx)
            {
                Color = OxyColors.Blue,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            }
        );

        model.Series.Add(
            new FunctionSeries(scalarSignal.LinearInterpolation, xMin, xMax, dx)
            {
                Color = OxyColors.Black,
                LineStyle = LineStyle.LongDash,
                StrokeThickness = 1
            }
        );

        var tSignal = scalarSignal.GetSampledTimeSignal();
        var scatterPoints =
            Enumerable
                .Range(0, scalarSignal.Count)
                .Select(i => new ScatterPoint(tSignal[i], scalarSignal[i]))
                .ToList();

        var scatterSeries = new ScatterSeries
        {
            MarkerSize = 4,
            MarkerStroke = OxyColors.Green,
            MarkerFill = OxyColors.Transparent,
            MarkerStrokeThickness = 1,
            MarkerType = MarkerType.Circle
        };

        scatterSeries.Points.AddRange(scatterPoints);

        model.Series.Add(scatterSeries);

        var renderer = new PngExporter
        {
            //Dpi = 120,
            Width = 1200,
            Height = 600
        };

        using var stream = new MemoryStream();
        renderer.Export(model, stream);

        stream.Position = 0;

        return Image.Load(stream);
    }

    public static Image PlotValue(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var func = scalarFunction.GetValue;

        return func.Plot(scalarSignal, xMin, xMax, yMin, yMax);
    }

    public static Image PlotFirstDerivative(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var func = scalarFunction.GetDerivative1().GetValue;

        return func.Plot(scalarSignal, xMin, xMax, yMin, yMax);
    }

    public static Image PlotSecondDerivative(this DifferentialFunction scalarFunction, Float64Signal scalarSignal, double xMin, double xMax, double yMin, double yMax)
    {
        var func = scalarFunction.GetDerivative2().GetValue;

        return func.Plot(scalarSignal, xMin, xMax, yMin, yMax);
    }
}