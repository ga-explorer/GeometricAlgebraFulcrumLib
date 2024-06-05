using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64Complex;
using GeometricAlgebraFulcrumLib.Core.Algebra.Signals;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Interpolators;

public class DfFourierSignalInterpolator :
    DifferentialSignalInterpolatorFunction
{
    ///// <summary>
    ///// Apply FFT to given real sampled signal and find the frequency indices of the
    ///// dominant frequency using a ratio of the total signal energy
    ///// </summary>
    ///// <param name="signalSamples"></param>
    ///// <param name="energyThreshold"></param>
    ///// <returns></returns>
    //public static IEnumerable<int> GetDominantFrequencyIndexSet(IEnumerable<double> signalSamples, double energyThreshold = 0.998d)
    //{
    //    // Compute FFT
    //    var real = signalSamples.ToArray();
    //    var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
    //    var sampleCount = real.Length;

    //    Fourier.Forward(real, imaginary, FourierOptions.Default);

    //    //Compute frequency sample energy, and total energy (not including 0 and negative frequencies)
    //    var energyDictionary = new SortedDictionary<double, int>();
    //    var energySum = 0d;

    //    // Ignore negative frequencies from the spectrum,
    //    // they will be added later using the real symmetry of the signal
    //    var freqIndexMax = Int32BitUtils.IsOdd(sampleCount)
    //        ? (sampleCount - 1) / 2
    //        : (sampleCount - 2) / 2;

    //    for (var freqIndex = 1; freqIndex <= freqIndexMax; freqIndex++)
    //    {
    //        var energy =
    //            real[freqIndex].Square() +
    //            imaginary[freqIndex].Square();

    //        energyDictionary.Add(energy, freqIndex);

    //        energySum += energy;
    //    }

    //    // Find frequencies with most energy, but always include 0 frequency
    //    var threshold = energyThreshold * energySum;
    //    var indexSet = new HashSet<int> {0};

    //    foreach (var (energy, freqIndex) in energyDictionary.Reverse())
    //    {
    //        indexSet.Add(freqIndex);

    //        threshold -= energy;

    //        if (threshold < 0d)// || indexSet.Count < 2)
    //            break;
    //    }
        
    //    return indexSet;
    //}
    
    ///// <summary>
    ///// Apply FFT to each dimension of the sampled vector signal
    ///// </summary>
    ///// <param name="signalSamples"></param>
    ///// <returns></returns>
    //private static List<Complex[]> GetFourierArrays(IReadOnlyList<XGaVector<double>> signalSamples)
    //{
    //    var geometricProcessor = signalSamples[0].GeometricProcessor;
    //    var vSpaceDimensions = (int) geometricProcessor.VSpaceDimensions;
    //    var complexSamples = new List<Complex[]>(vSpaceDimensions);

    //    for (var i = 0; i < vSpaceDimensions; i++)
    //    {
    //        var index = i;

    //        var complexSamplesArray = signalSamples.Select(
    //            v => (Complex) v[index].ScalarValue
    //        ).ToArray();

    //        Fourier.Forward(complexSamplesArray, FourierOptions.Default);

    //        complexSamples.Add(complexSamplesArray);
    //    }

    //    return complexSamples;
    //}
    
    ///// <summary>
    ///// Apply FFT to each dimension of the sampled vector signal
    ///// </summary>
    ///// <param name="signalSamples"></param>
    ///// <returns></returns>
    //private static List<Complex[]> GetFourierArrays(XGaVector<ScalarSignalFloat64> signalSamples)
    //{
    //    var geometricProcessor = signalSamples.GeometricProcessor;
    //    var vSpaceDimensions = (int) geometricProcessor.VSpaceDimensions;
    //    var complexSamples = new List<Complex[]>(vSpaceDimensions);

    //    for (var i = 0; i < vSpaceDimensions; i++)
    //    {
    //        var complexSamplesArray = 
    //            signalSamples[i]
    //                .ScalarValue
    //                .Select(v => (Complex) v)
    //                .ToArray();

    //        Fourier.Forward(complexSamplesArray, FourierOptions.Default);

    //        complexSamples.Add(complexSamplesArray);
    //    }

    //    return complexSamples;
    //}

    //private static IEnumerable<int> NormalizeFrequencyIndexSet(IEnumerable<int> frequencyIndexList, int sampleCount)
    //{
    //    var freqIndexMax = 
    //        sampleCount.IsOdd()
    //            ? (sampleCount - 1) / 2
    //            : (sampleCount - 2) / 2;

    //    var freqIndexSet = new HashSet<int>(){0};
    //    foreach (var freqIndex in frequencyIndexList)
    //    {
    //        if (freqIndex <= freqIndexMax)
    //            freqIndexSet.Add(freqIndex);

    //        else if (freqIndex < sampleCount)
    //            freqIndexSet.Add(sampleCount - freqIndex);

    //        else
    //            throw new InvalidOperationException();
    //    }

    //    return freqIndexSet;
    //}
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfFourierSignalInterpolator Create(Float64Signal signal, DfFourierSignalInterpolatorOptions options)
    {
        signal = signal.GetSmoothedSignal(options);

        var vectorSignalSpectrum = options.AssumePeriodic
            ? signal.GetFourierSpectrum(options)
            : signal.GetPeriodicPaddedSignal(
                options.PaddingTrendSampleCount,
                options.PaddingSampleCount,
                true
            ).GetFourierSpectrum(options);
        
        return new DfFourierSignalInterpolator(
            signal.SamplingSpecs, 
            0,
            signal.Count - 1,
            options,
            vectorSignalSpectrum
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DfFourierSignalInterpolator Create(Float64Signal signal, int sampleIndex1, int sampleIndex2, DfFourierSignalInterpolatorOptions options)
    {
        var sampleCount = sampleIndex2 - sampleIndex2 + 1;
        signal = signal.GetSubSignal(sampleIndex1, sampleCount).GetSmoothedSignal(options);

        var vectorSignalSpectrum = options.AssumePeriodic
            ? signal.GetFourierSpectrum(options)
            : signal.GetPeriodicPaddedSignal(
                options.PaddingTrendSampleCount,
                options.PaddingSampleCount,
                true
            ).GetFourierSpectrum(options);
        
        return new DfFourierSignalInterpolator(
            signal.SamplingSpecs, 
            0,
            sampleCount - 1,
            options,
            vectorSignalSpectrum
        );
    }

        
    public DfFourierSignalInterpolatorOptions Options { get; }

    public ComplexSignalSpectrum VectorSignalSpectrum { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfFourierSignalInterpolator(Float64SignalSamplingSpecs samplingSpecs, int sampleIndex1, int sampleIndex2, DfFourierSignalInterpolatorOptions options, ComplexSignalSpectrum signalSpectrum) 
        : base(samplingSpecs, sampleIndex1, sampleIndex2)
    {
        Options = options;
        VectorSignalSpectrum = signalSpectrum;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        var c = VectorSignalSpectrum.GetValue(t);

        //Debug.Assert(
        //    c.Imaginary.IsNearZero(1e-7)
        //);

        return c.Real.Sign() * c.Magnitude;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative1()
    {
        return Create(
            SamplingSpecs.GetSampledFunctionSignal(
                t => VectorSignalSpectrum.GetValueDt1(t).RotateToReal()
            ), 
            Options
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivative2()
    {
        return Create(
            SamplingSpecs.GetSampledFunctionSignal(
                t => VectorSignalSpectrum.GetValueDt2(t).RotateToReal()
            ), 
            Options
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DifferentialFunction GetDerivativeN(int order)
    {
        return order switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(order)),
            0 => this,
            1 => GetDerivative1(),
            2 => GetDerivative2(),
            _ => Create(SamplingSpecs.GetSampledFunctionSignal(t => 
                VectorSignalSpectrum.GetValueDt(order, t).RotateToReal()
            ), Options)
        };

        //order -= 2;
        //var df = GetDerivative2();

        //while (order > 0)
        //{
        //    df = df.GetDerivative1();

        //    order--;
        //}

        //return df;
    }
}