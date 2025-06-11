using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.IntegralTransforms;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Interpolators;

public class VectorFourierInterpolator
{
    /// <summary>
    /// Apply FFT to given real sampled signal and find the frequency indices of the
    /// dominant frequency using a ratio of the total signal energy
    /// </summary>
    /// <param name="signalSamples"></param>
    /// <param name="energyThreshold"></param>
    /// <returns></returns>
    public static IEnumerable<int> GetDominantFrequencyIndexSet(IEnumerable<double> signalSamples, double energyThreshold = 0.998d)
    {
        // Compute FFT
        var real = signalSamples.ToArray();
        var imaginary = Enumerable.Repeat(0d, real.Length).ToArray();
        var sampleCount = real.Length;

        Fourier.Forward(real, imaginary, FourierOptions.Default);

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
                real[freqIndex].Square() +
                imaginary[freqIndex].Square();

            energyDictionary.Add(energy, freqIndex);

            energySum += energy;
        }

        // Find frequencies with most energy, but always include 0 frequency
        var threshold = energyThreshold * energySum;
        var indexSet = new HashSet<int> {0};

        foreach (var (energy, freqIndex) in energyDictionary.Reverse())
        {
            indexSet.Add(freqIndex);

            threshold -= energy;

            if (threshold < 0d)// || indexSet.Count < 2)
                break;
        }
            
        return indexSet;
    }
        
    /// <summary>
    /// Apply FFT to each dimension of the sampled vector signal
    /// </summary>
    /// <param name="signalSamples"></param>
    /// <returns></returns>
    private static List<Complex[]> GetFourierArrays(IReadOnlyList<XGaFloat64Vector> signalSamples)
    {
        var vSpaceDimensions = signalSamples.GetVSpaceDimensions();
        var complexSamples = new List<Complex[]>(vSpaceDimensions);

        for (var i = 0; i < vSpaceDimensions; i++)
        {
            var index = i;

            var complexSamplesArray = signalSamples.Select(
                v => (Complex) v.Scalar(index)
            ).ToArray();

            Fourier.Forward(complexSamplesArray, FourierOptions.Default);

            complexSamples.Add(complexSamplesArray);
        }

        return complexSamples;
    }
        
    /// <summary>
    /// Apply FFT to each dimension of the sampled vector signal
    /// </summary>
    /// <param name="signalSamples"></param>
    /// <returns></returns>
    private static List<Complex[]> GetFourierArrays(XGaVector<Float64SampledTimeSignal> signalSamples)
    {
        var vSpaceDimensions = signalSamples.VSpaceDimensions;
        var complexSamples = new List<Complex[]>(vSpaceDimensions);

        for (var i = 0; i < vSpaceDimensions; i++)
        {
            var complexSamplesArray = 
                signalSamples.Scalar(i)
                    .ScalarValue
                    .Select(v => (Complex) v)
                    .ToArray();

            Fourier.Forward(complexSamplesArray, FourierOptions.Default);

            complexSamples.Add(complexSamplesArray);
        }

        return complexSamples;
    }

    private static IEnumerable<int> NormalizeFrequencyIndexSet(IEnumerable<int> frequencyIndexList, int sampleCount)
    {
        var freqIndexMax = 
            sampleCount.IsOdd()
                ? (sampleCount - 1) / 2
                : (sampleCount - 2) / 2;

        var freqIndexSet = new HashSet<int>(){0};
        foreach (var freqIndex in frequencyIndexList)
        {
            if (freqIndex <= freqIndexMax)
                freqIndexSet.Add(freqIndex);

            else if (freqIndex < sampleCount)
                freqIndexSet.Add(sampleCount - freqIndex);

            else
                throw new InvalidOperationException();
        }

        return freqIndexSet;
    }


    private static VectorFourierInterpolator Create(List<Complex[]> complexSamples, int vSpaceDimensions, double samplingRate, IEnumerable<int> frequencyIndexList)
    {
        var sampleCount = complexSamples[0].Length;
        var scalingFactor = Math.Sqrt(sampleCount);
        var signalTime = (sampleCount - 1) / samplingRate;
        var df = 1d / signalTime;

        //// For validation
        //foreach (var freqIndex in frequencyIndexList)
        //{
        //    var freqHz = freqIndex * df;
        //    var freqRad = Math.Tau * freqHz;
        //    var freqRatio = freqHz / 50d;

        //    Console.WriteLine(
        //        $"Frequency Samples ({freqIndex}, {sampleCount - freqIndex}) = ±{freqHz:G} Hz = ±{freqRatio} * 50 Hz");
        //    Console.WriteLine($@"$f_{{{freqRatio}}} = {freqHz:G} \textrm{{Hz}}$");
        //    Console.WriteLine();
        //}

        var freqIndexSet = 
            NormalizeFrequencyIndexSet(frequencyIndexList, sampleCount)
                .Where(i => i > 0)
                .OrderBy(i => i)
                .ToArray();

        var interpolator = new VectorFourierInterpolator(freqIndexSet);

        {
            // The index for the positive frequency of the spectrum
            var freqIndex = 0;

            // The positive frequency value
            var freqHz = 0d;
            var freqRad = 0d;

            var cosVectorScalars = new double[vSpaceDimensions];
            var sinVectorScalars = new double[vSpaceDimensions];

            for (var scalarIndex = 0; scalarIndex < vSpaceDimensions; scalarIndex++)
            {
                var complexSample = complexSamples[scalarIndex][freqIndex];

                cosVectorScalars[scalarIndex] = 
                    complexSample.Real / scalingFactor;

                sinVectorScalars[scalarIndex] = 
                    -complexSample.Imaginary / scalingFactor;
            }

            var metric = XGaFloat64Processor.Euclidean;

            interpolator.SetTerm(
                freqRad,
                metric.Vector(cosVectorScalars),
                metric.Vector(sinVectorScalars)
            );
        }

        //var signalTime = (signalSamples.Count - 1) / samplingRate;
        //var df = 1d / signalTime;

        foreach (var i in freqIndexSet)
        {
            // The index for the positive frequency of the spectrum
            var freqIndex1 = i;
                
            // The index for the negative frequency of the spectrum
            var freqIndex2 = sampleCount - i;

            // The positive frequency value
            var freqHz = i * df;
            var freqRad = Math.Tau * freqHz;

            var cosVectorScalars = new double[vSpaceDimensions];
            var sinVectorScalars = new double[vSpaceDimensions];
                
            for (var scalarIndex = 0; scalarIndex < vSpaceDimensions; scalarIndex++)
            {
                var complexSample1 = complexSamples[scalarIndex][freqIndex1];
                var complexSample2 = complexSamples[scalarIndex][freqIndex2];

                cosVectorScalars[scalarIndex] = 
                    (complexSample2.Real + complexSample1.Real) / scalingFactor;

                sinVectorScalars[scalarIndex] = 
                    (complexSample2.Imaginary - complexSample1.Imaginary) / scalingFactor;
            }
                
            var metric = XGaFloat64Processor.Euclidean;

            interpolator.SetTerm(
                freqRad,
                metric.Vector(cosVectorScalars),
                metric.Vector(sinVectorScalars)
            );
        }

        return interpolator;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static VectorFourierInterpolator Create(IReadOnlyList<XGaFloat64Vector> signalSamples, double samplingRate, IEnumerable<int> frequencyIndexList)
    {
        var vSpaceDimensions = 
            signalSamples.GetVSpaceDimensions();

        var complexSamples = 
            GetFourierArrays(signalSamples);
            
        return Create(
            complexSamples,
            vSpaceDimensions,
            samplingRate,
            frequencyIndexList
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static VectorFourierInterpolator Create(XGaVector<Float64SampledTimeSignal> signalSamples, IEnumerable<int> frequencyIndexList)
    {
        var vSpaceDimensions = 
            signalSamples.VSpaceDimensions;

        var samplingRate = signalSamples.GetSamplingRate();

        var complexSamples = 
            GetFourierArrays(signalSamples);

        return Create(
            complexSamples,
            vSpaceDimensions,
            samplingRate,
            frequencyIndexList
        );
    }

    /// <summary>
    /// Create a Fourier interpolator based on the given sampled periodic signal
    /// </summary>
    /// <param name="signalSamples"></param>
    /// <param name="samplingRate"></param>
    /// <param name="energyThreshold"></param>
    /// <returns></returns>
    internal static VectorFourierInterpolator Create(IReadOnlyList<XGaFloat64Vector> signalSamples, double samplingRate, double energyThreshold = 0.998d)
    {
        var frequencyIndexSet = GetDominantFrequencyIndexSet(
            signalSamples.Select(v => v.Norm().ScalarValue),
            energyThreshold
        );

        return Create(
            signalSamples, 
            samplingRate, 
            frequencyIndexSet
        );
    }

    internal static VectorFourierInterpolator Create(XGaVector<Float64SampledTimeSignal> signalSamples, double energyThreshold = 0.998d)
    {
        var frequencyIndexSet = GetDominantFrequencyIndexSet(
            signalSamples.Norm().ScalarValue,
            energyThreshold
        );
            
        return Create(
            signalSamples, 
            frequencyIndexSet
        );
    }


    private readonly List<int> _frequencyIndexList;
    private readonly Dictionary<double, VectorFourierInterpolatorTerm> _termsDictionary;
        
        
    //public XGaMetricScalarProcessor<ScalarSignalFloat64> SignalProcessor 
    //    => VectorSamples.Processor;

    public XGaFloat64Processor SampleProcessor { get; }
        = XGaFloat64Processor.Euclidean;


    private VectorFourierInterpolator(IEnumerable<int> frequencyIndexList, Dictionary<double, VectorFourierInterpolatorTerm> termsDictionary)
    {
        _frequencyIndexList = new List<int>(frequencyIndexList);
        _termsDictionary = termsDictionary;
    }

    private VectorFourierInterpolator(IEnumerable<int> frequencyIndexList)
    {
        _frequencyIndexList = new List<int>(frequencyIndexList);
        _termsDictionary = new Dictionary<double, VectorFourierInterpolatorTerm>();
    }

        
    public VectorFourierInterpolator AddTerm(double frequency, XGaFloat64Vector cosVector, XGaFloat64Vector sinVector)
    {
        if (_termsDictionary.TryGetValue(frequency.Abs(), out var term))
        {
            term.AddVectors(
                cosVector, 
                frequency >= 0 ? sinVector : -sinVector
            );
        }
        else
        {
            term = new VectorFourierInterpolatorTerm(cosVector, sinVector, frequency);

            _termsDictionary.Add(frequency, term);
        }

        return this;
    }
        
    public VectorFourierInterpolator SubtractTerm(double frequency, XGaFloat64Vector cosVector, XGaFloat64Vector sinVector)
    {
        if (_termsDictionary.TryGetValue(frequency.Abs(), out var term))
        {
            term.AddVectors(
                -cosVector, 
                frequency >= 0 ? -sinVector : sinVector
            );
        }
        else
        {
            term = new VectorFourierInterpolatorTerm(cosVector, sinVector, frequency);

            _termsDictionary.Add(frequency, term);
        }

        return this;
    }

    public VectorFourierInterpolator SetTerm(double frequency, XGaFloat64Vector cosVector, XGaFloat64Vector sinVector)
    {
        var term = new VectorFourierInterpolatorTerm(cosVector, sinVector, frequency);

        if (_termsDictionary.ContainsKey(frequency))
            _termsDictionary[frequency] = term;
        else
            _termsDictionary.Add(frequency, term);

        return this;
    }
        
    public VectorFourierInterpolator GetInterpolatorDerivative(int degree = 1)
    {
        var termsDictionary = new Dictionary<double, VectorFourierInterpolatorTerm>();

        foreach (var (frequency, term) in _termsDictionary)
            termsDictionary.Add(
                frequency, 
                term.GetTermDerivative(degree)
            );

        return new VectorFourierInterpolator(_frequencyIndexList, termsDictionary);
    }

        
    public XGaFloat64Vector GetVector(double parameterValue)
    {
        return _termsDictionary.Values.Aggregate(
            SampleProcessor.VectorZero, 
            (current, term) => 
                current + term.GetVector(parameterValue)
        );
    }

    public XGaFloat64Vector GetVectorDt(double parameterValue, int degree = 1)
    {
        return _termsDictionary.Values.Aggregate(
            SampleProcessor.VectorZero, 
            (current, term) => 
                current + term.GetVectorDt(parameterValue, degree)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaFloat64Vector> GetVectors(IEnumerable<double> parameterValueList)
    {
        return parameterValueList.Select(GetVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaFloat64Vector> GetVectorsDt(IEnumerable<double> parameterValueList, int degree = 1)
    {
        var interpolator = GetInterpolatorDerivative(degree);

        return parameterValueList.Select(
            t => interpolator.GetVector(t)
        );
    }
        
    public Pair<XGaFloat64Vector> GetLocalFrame2D(double parameterValue)
    {
        var vDt1 = GetVectorDt(parameterValue, 1);
        var vDt2 = GetVectorDt(parameterValue, 2);

        // Apply GS process
        var u1 = vDt1;
        var e1 = u1.DivideByNorm();
            
        var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm();

        return new Pair<XGaFloat64Vector>(e1, e2);
    }

    public Triplet<XGaFloat64Vector> GetLocalFrame3D(double parameterValue)
    {
        var vDt1 = GetVectorDt(parameterValue, 1);
        var vDt2 = GetVectorDt(parameterValue, 2);
        var vDt3 = GetVectorDt(parameterValue, 3);

        // Apply GS process
        var u1 = vDt1;
        var e1 = u1.DivideByNorm();
            
        var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm();
                
        var u3 = vDt3 - vDt3.ProjectOn(u1.ToSubspace()) - vDt3.ProjectOn(u2.ToSubspace());
        var e3 = u3.DivideByNorm();

        return new Triplet<XGaFloat64Vector>(e1, e2, e3);
    }
        
    public Quad<XGaFloat64Vector> GetLocalFrame4D(double parameterValue)
    {
        var vDt1 = GetVectorDt(parameterValue, 1);
        var vDt2 = GetVectorDt(parameterValue, 2);
        var vDt3 = GetVectorDt(parameterValue, 3);
        var vDt4 = GetVectorDt(parameterValue, 4);

        // Apply GS process
        var u1 = vDt1;
        var e1 = u1.DivideByNorm();
            
        var u2 = vDt2 - vDt2.ProjectOn(u1.ToSubspace());
        var e2 = u2.DivideByNorm();
                
        var u3 = vDt3 - vDt3.ProjectOn(u1.ToSubspace()) - vDt3.ProjectOn(u2.ToSubspace());
        var e3 = u3.DivideByNorm();
                
        var u4 = vDt4 - vDt4.ProjectOn(u1.ToSubspace()) - vDt4.ProjectOn(u2.ToSubspace()) - vDt4.ProjectOn(u3.ToSubspace());
        var e4 = u4.DivideByNorm();

        return new Quad<XGaFloat64Vector>(e1, e2, e3, e4);
    }
}