using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using MathNet.Numerics.Interpolation;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Interpolators;

public class XGaVectorNevilleInterpolator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVectorNevilleInterpolator Create(double samplingRate)
    {
        return new XGaVectorNevilleInterpolator(samplingRate);
    }

        
    public XGaFloat64Processor SampleProcessor { get; }
        = XGaFloat64Processor.Euclidean;

    public double SamplingRate { get; }

    public int InterpolationSamples { get; set; } = 13;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaVectorNevilleInterpolator(double samplingRate)
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

            yield return NevillePolynomialInterpolation.InterpolateSorted(xValues, yValues);
        }
    }
        
    private IEnumerable<NevillePolynomialInterpolation> GetInterpolatorList(XGaVector<Float64SampledTimeSignal> samples, int sampleCount, double t)
    {
        var indexList = GetInterpolationSampleIndexList(sampleCount, t);

        var xValues = 
            indexList.Select(i => i / SamplingRate).ToArray();

        var vSpaceDimensions = samples.VSpaceDimensions;

        for (var j = 0; j < vSpaceDimensions; j++)
        {
            var samplesArray = samples.Scalar(j).ScalarValue;

            var yValues =
                indexList.Select(i => samplesArray[i]).ToArray();

            yield return NevillePolynomialInterpolation.InterpolateSorted(xValues, yValues);
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
    public XGaFloat64Vector GetVector(XGaVector<Float64SampledTimeSignal> samples, int sampleCount, double t)
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
            .Select(p => p.Differentiate(t))
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt1(XGaVector<Float64SampledTimeSignal> samples, int sampleCount, double t)
    {
        var signalTime = (sampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(samples, sampleCount, t)
            .Select(p => p.Differentiate(t))
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt2(IReadOnlyList<XGaFloat64Vector> samples, double t)
    {
        var signalTime = (samples.Count - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(samples, t)
            .Select(p => p.Differentiate2(t))
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt2(XGaVector<Float64SampledTimeSignal> samples, int sampleCount, double t)
    {
        var signalTime = (sampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(samples, sampleCount, t)
            .Select(p => p.Differentiate2(t))
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
        
    public XGaVector<Float64SampledTimeSignal> GetVectorsDt1(XGaVector<Float64SampledTimeSignal> samples, int sampleCount)
    {
        var processor = samples.Processor;

        var vectorList = Enumerable
            .Range(0, sampleCount)
            .Select(i => GetVectorDt1(samples, sampleCount, i / SamplingRate))
            .ToArray();

        var vSpaceDimensions = samples.VSpaceDimensions;

        var columnVectorArray = new Float64SampledTimeSignal[vSpaceDimensions];

        for (var j = 0; j < vSpaceDimensions; j++)
        {
            var columnVector = Float64SampledTimeSignalComposer.Create(SamplingRate, sampleCount);

            for (var i = 0; i < sampleCount; i++)
                columnVector[i] = vectorList[i].Scalar(j);

            columnVectorArray[j] = columnVector.GetFiniteSignal();
        }

        return processor.Vector(columnVectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<XGaFloat64Vector> GetVectorsDt2(IReadOnlyList<XGaFloat64Vector> samples)
    {
        return Enumerable
            .Range(0, samples.Count)
            .Select(i => GetVectorDt2(samples, i / SamplingRate))
            .ToArray();
    }
        
    public XGaVector<Float64SampledTimeSignal> GetVectorsDt2(XGaVector<Float64SampledTimeSignal> samples, int sampleCount)
    {
        var processor = samples.Processor;

        var vectorList = Enumerable
            .Range(0, sampleCount)
            .Select(i => GetVectorDt2(samples, sampleCount, i / SamplingRate))
            .ToArray();

        var vSpaceDimensions = samples.VSpaceDimensions;

        var columnVectorArray = new Float64SampledTimeSignal[vSpaceDimensions];

        for (var j = 0; j < vSpaceDimensions; j++)
        {
            var columnVector = Float64SampledTimeSignalComposer.Create(SamplingRate, sampleCount);

            for (var i = 0; i < sampleCount; i++)
                columnVector[i] = vectorList[i].Scalar(j);

            columnVectorArray[j] = columnVector.GetFiniteSignal();
        }

        return processor.Vector(columnVectorArray);
    }

}