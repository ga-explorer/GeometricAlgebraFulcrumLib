﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using MathNet.Numerics.Interpolation;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Interpolators;

public class RGaVectorNevilleInterpolator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaVectorNevilleInterpolator Create(double samplingRate)
    {
        return new RGaVectorNevilleInterpolator(samplingRate);
    }

        
    public RGaFloat64Processor SampleProcessor { get; }
        = RGaFloat64Processor.Euclidean;

    public double SamplingRate { get; }

    public int InterpolationSamples { get; set; } = 13;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaVectorNevilleInterpolator(double samplingRate)
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

    private IEnumerable<NevillePolynomialInterpolation> GetInterpolatorList(IReadOnlyList<RGaFloat64Vector> samples, double t)
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
        
    private IEnumerable<NevillePolynomialInterpolation> GetInterpolatorList(RGaVector<Float64Signal> samples, int sampleCount, double t)
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
    public RGaFloat64Vector GetVector(IReadOnlyList<RGaFloat64Vector> samples, double t)
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
    public RGaFloat64Vector GetVector(RGaVector<Float64Signal> samples, int sampleCount, double t)
    {
        var signalTime = (sampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(samples, sampleCount, t)
            .Select(p => p.Interpolate(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetVectorDt1(IReadOnlyList<RGaFloat64Vector> samples, double t)
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
    public RGaFloat64Vector GetVectorDt1(RGaVector<Float64Signal> samples, int sampleCount, double t)
    {
        var signalTime = (sampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(samples, sampleCount, t)
            .Select(p => p.Differentiate(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetVectorDt2(IReadOnlyList<RGaFloat64Vector> samples, double t)
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
    public RGaFloat64Vector GetVectorDt2(RGaVector<Float64Signal> samples, int sampleCount, double t)
    {
        var signalTime = (sampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(samples, sampleCount, t)
            .Select(p => p.Differentiate2(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaFloat64Vector> GetVectorsDt1(IReadOnlyList<RGaFloat64Vector> samples)
    {
        return Enumerable
            .Range(0, samples.Count)
            .Select(i => GetVectorDt1(samples, i / SamplingRate))
            .ToArray();
    }
        
    public RGaVector<Float64Signal> GetVectorsDt1(RGaVector<Float64Signal> samples, int sampleCount)
    {
        var processor = samples.Processor;

        var vectorList = Enumerable
            .Range(0, sampleCount)
            .Select(i => GetVectorDt1(samples, sampleCount, i / SamplingRate))
            .ToArray();

        var vSpaceDimensions = samples.VSpaceDimensions;

        var columnVectorArray = new Float64Signal[vSpaceDimensions];

        for (var j = 0; j < vSpaceDimensions; j++)
        {
            var columnVector = Float64Signal.Create(SamplingRate, sampleCount);

            for (var i = 0; i < sampleCount; i++)
                columnVector[i] = vectorList[i].Scalar(j);

            columnVectorArray[j] = columnVector;
        }

        return processor.CreateVector(columnVectorArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<RGaFloat64Vector> GetVectorsDt2(IReadOnlyList<RGaFloat64Vector> samples)
    {
        return Enumerable
            .Range(0, samples.Count)
            .Select(i => GetVectorDt2(samples, i / SamplingRate))
            .ToArray();
    }
        
    public RGaVector<Float64Signal> GetVectorsDt2(RGaVector<Float64Signal> samples, int sampleCount)
    {
        var processor = samples.Processor;

        var vectorList = Enumerable
            .Range(0, sampleCount)
            .Select(i => GetVectorDt2(samples, sampleCount, i / SamplingRate))
            .ToArray();

        var vSpaceDimensions = samples.VSpaceDimensions;

        var columnVectorArray = new Float64Signal[vSpaceDimensions];

        for (var j = 0; j < vSpaceDimensions; j++)
        {
            var columnVector = Float64Signal.Create(SamplingRate, sampleCount);

            for (var i = 0; i < sampleCount; i++)
                columnVector[i] = vectorList[i].Scalar(j);

            columnVectorArray[j] = columnVector;
        }

        return processor.CreateVector(columnVectorArray);
    }

}