using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using MathNet.Numerics.Interpolation;

namespace GeometricAlgebraFulcrumLib.Algebra.Signals.Interpolators;

public class VectorBarycentricInterpolator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static VectorBarycentricInterpolator Create(int vSpaceDimensions, double samplingRate)
    {
        return new VectorBarycentricInterpolator(vSpaceDimensions, samplingRate);
    }

        
    //public XGaMetricScalarProcessor<ScalarSignalFloat64> SignalProcessor 
    //    => VectorSamples.Processor;

    public XGaFloat64Processor SampleProcessor { get; }
        = XGaFloat64Processor.Euclidean;

    public int VSpaceDimensions { get; }

    public double SamplingRate { get; }

    public int InterpolationSamples { get; set; } = 13;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VectorBarycentricInterpolator(int vSpaceDimensions, double samplingRate)
    {
        if (samplingRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(samplingRate));

        if (vSpaceDimensions < 1)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        VSpaceDimensions = vSpaceDimensions;
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

    private IEnumerable<Barycentric> GetInterpolatorList(IReadOnlyList<XGaFloat64Vector> samples, double t)
    {
        var indexList = GetInterpolationSampleIndexList(samples.Count, t);

        var xValues = 
            indexList.Select(i => i / SamplingRate).ToArray();
            
        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var yValues =
                indexList.Select(i => samples[i].Scalar(j)).ToArray();

            yield return Barycentric.InterpolatePolynomialEquidistantSorted(xValues, yValues);
        }
    }
        
    private IEnumerable<Barycentric> GetInterpolatorList(XGaVector<IReadOnlyList<double>> samples, int sampleCount, double t)
    {
        var indexList = GetInterpolationSampleIndexList(sampleCount, t);

        var xValues = 
            indexList.Select(i => i / SamplingRate).ToArray();
            
        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var samplesArray = samples.Scalar(j).ScalarValue;

            var yValues =
                indexList.Select(i => samplesArray[i]).ToArray();

            yield return Barycentric.InterpolatePolynomialEquidistantSorted(xValues, yValues);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVector(IReadOnlyList<XGaFloat64Vector> samples, double t)
    {
        var signalTime = (samples.Count - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(samples, t)
            .Select(p => p.Interpolate(t))
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVector(XGaVector<IReadOnlyList<double>> samples, int sampleCount, double t)
    {
        var signalTime = (sampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(samples, sampleCount, t)
            .Select(p => p.Interpolate(t))
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt1(IReadOnlyList<XGaFloat64Vector> samples, double t)
    {
        var signalTime = (samples.Count - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(samples, t)
            .Select(p => (p.Interpolate(t + 1e-15) - p.Interpolate(t)) / 1e-15)
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt1(XGaVector<IReadOnlyList<double>> samples, int sampleCount, double t)
    {
        var signalTime = (sampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(samples, sampleCount, t)
            .Select(p => (p.Interpolate(t + 1e-15) - p.Interpolate(t)) / 1e-15)
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaFloat64Vector> GetVectorsDt1(IReadOnlyList<XGaFloat64Vector> samples)
    {
        return Enumerable
            .Range(0, samples.Count)
            .Select(i => GetVectorDt1(samples, i / SamplingRate))
            .ToArray();
    }
        
    public XGaVector<IReadOnlyList<double>> GetVectorsDt1(XGaVector<IReadOnlyList<double>> samples, int sampleCount)
    {
        var processor = samples.Processor;

        var vectorList = Enumerable
            .Range(0, sampleCount)
            .Select(i => GetVectorDt1(samples, sampleCount, i / SamplingRate))
            .ToArray();

        var columnVectorArray = new IReadOnlyList<double>[VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = new double[sampleCount];

            for (var i = 0; i < sampleCount; i++)
                columnVector[i] = vectorList[i].Scalar(j);

            columnVectorArray[j] = columnVector;
        }

        return processor.Vector(columnVectorArray);
    }
}