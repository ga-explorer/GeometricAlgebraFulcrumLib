using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using MathNet.Numerics.Interpolation;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Interpolators;

public class VectorFittingInterpolator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static VectorFittingInterpolator Create(double samplingRate)
    {
        return new VectorFittingInterpolator(samplingRate);
    }

        
    //public XGaMetricScalarProcessor<ScalarSignalFloat64> SignalProcessor 
    //    => VectorSamples.Processor;

    public XGaFloat64Processor SampleProcessor { get; }
        = XGaFloat64Processor.Euclidean;

    public double SamplingRate { get; }

    public int InterpolationSamples { get; set; } = 13;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VectorFittingInterpolator(double samplingRate)
    {
        if (samplingRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        SamplingRate = samplingRate;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IReadOnlyList<int> GetInterpolationSampleIndexList(int samplesCount, double t)
    {
        var index1 = 
            (int) Math.Floor(SamplingRate * t) - (InterpolationSamples - 1) / 2;

        if (index1 < 0)
            index1 = 0;

        else if (index1 >= samplesCount - InterpolationSamples) 
            index1 = samplesCount - InterpolationSamples;

        return Enumerable.Range(index1, InterpolationSamples).ToArray();
    }

    private IEnumerable<NevillePolynomialInterpolation> GetInterpolatorList(IReadOnlyList<XGaFloat64Vector> samples, double t)
    {
        var indexList = GetInterpolationSampleIndexList(samples.Count, t);

        var xValues = 
            indexList.Select(i => i / SamplingRate).ToArray();

        var vSpaceDimensions = samples.GetVSpaceDimensions();

        for (var j = 0; j < vSpaceDimensions; j++)
        {
            var yValues =
                indexList.Select(i => samples[i].Scalar(j)).ToArray();

            //yield return Fit.Polynomial(xValues, yValues, 3, DirectRegressionMethod.QR);
            yield return NevillePolynomialInterpolation.InterpolateSorted(xValues, yValues);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVector(IReadOnlyList<XGaFloat64Vector> samples, double t)
    {
        var signalTime = (samples.Count - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(samples, t)
            .Select(p => p.Interpolate(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt1(IReadOnlyList<XGaFloat64Vector> samples, double t)
    {
        var signalTime = (samples.Count - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(samples, t)
            .Select(p => p.Differentiate(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt2(IReadOnlyList<XGaFloat64Vector> samples, double t)
    {
        var signalTime = (samples.Count - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(samples, t)
            .Select(p => p.Differentiate2(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaFloat64Vector> GetVectorsDt1(IReadOnlyList<XGaFloat64Vector> samples)
    {
        return Enumerable
            .Range(0, samples.Count)
            .Select(i => GetVectorDt1(samples, i / SamplingRate))
            .ToArray();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaFloat64Vector> GetVectorsDt2(IReadOnlyList<XGaFloat64Vector> samples)
    {
        return Enumerable
            .Range(0, samples.Count)
            .Select(i => GetVectorDt2(samples, i / SamplingRate))
            .ToArray();
    }
        

}