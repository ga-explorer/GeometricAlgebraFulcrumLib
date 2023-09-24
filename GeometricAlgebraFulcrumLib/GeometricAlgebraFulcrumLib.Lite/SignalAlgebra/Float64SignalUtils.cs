using System.Collections.Immutable;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Interpolators;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Composers;

namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra
{
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


    }
}
