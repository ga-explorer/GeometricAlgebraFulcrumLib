using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Polynomials.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Composers;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals.Interpolators;

public class XGaVectorPolynomialInterpolator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVectorPolynomialInterpolator Create(XGaVector<Float64SampledTimeSignal> scalarSamples)
    {
        return new XGaVectorPolynomialInterpolator(scalarSamples);
    }


    public XGaVector<Float64SampledTimeSignal> VectorSamples { get; }

    public XGaProcessor<Float64SampledTimeSignal> SignalProcessor 
        => VectorSamples.Processor;

    public XGaFloat64Processor SampleProcessor { get; }
        = XGaFloat64Processor.Euclidean;
        
    public int VSpaceDimensions { get; }

    public double SamplingRate { get; }

    public int InterpolationSamples { get; set; } = 128;

    public int PolynomialOrder { get; set; } = 13;

    public int SampleCount { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaVectorPolynomialInterpolator(XGaVector<Float64SampledTimeSignal> scalarSamples)
    {
        VSpaceDimensions = scalarSamples.VSpaceDimensions;
        SamplingRate = scalarSamples.GetSamplingRate();
        VectorSamples = scalarSamples;
        SampleCount = scalarSamples.Scalars.Max(s => s.Count);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IReadOnlyList<int> GetInterpolationSampleIndexList(double t)
    {
        // Only past samples
        var index1 = 
            (int) Math.Floor(SamplingRate * t) - (InterpolationSamples - 1);

        //// past and future samples
        //var index1 =
        //    (int) Math.Floor(SamplingRate * t) - (InterpolationSamples - 1) / 2; 

        if (index1 < 0)
            index1 = 0;

        else if (index1 >= SampleCount - InterpolationSamples) 
            index1 = SampleCount - InterpolationSamples;

        return Enumerable.Range(index1, InterpolationSamples).ToArray();
    }
        
        
    private IEnumerable<PolynomialFunction<double>> GetInterpolatorList(double t)
    {
        var indexList = GetInterpolationSampleIndexList(t);

        var xValues = 
            indexList.Select(i => i / SamplingRate).ToArray();

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var samplesArray = 
                VectorSamples.Scalar(j).ScalarValue;

            var yValues =
                indexList.Select(i => samplesArray[i]).ToArray();

            yield return PolynomialFunction<double>.Create(
                ScalarProcessorOfFloat64.Instance,
                Fit.Polynomial(xValues, yValues, PolynomialOrder)
            );
        }
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVector(double t)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(t)
            .Select(p => p.GetValue(t))
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt1(double t)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(t)
            .Select(p => p.GetValueDt1(t))
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt2(double t)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(t)
            .Select(p => p.GetValueDt2(t))
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GetVectorDt(double t, int degree = 1)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.VectorZero;

        var scalarArray = GetInterpolatorList(t)
            .Select(p => p.GetDerivative(degree).GetValue(t))
            .ToArray();
            
        return SampleProcessor.Vector(scalarArray);
    }

        
    public XGaVector<Float64SampledTimeSignal> GetVectors()
    {
        var vectorList = Enumerable
            .Range(0, SampleCount)
            .Select(i => GetVector(i / SamplingRate))
            .ToArray();

        var columnVectorArray = new Float64SampledTimeSignal[VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = Float64SampledTimeSignalComposer.Create(SamplingRate, SampleCount);

            for (var i = 0; i < SampleCount; i++)
                columnVector[i] = vectorList[i].Scalar(j);

            columnVectorArray[j] = columnVector.GetFiniteSignal();
        }

        return VectorSamples.Processor.Vector(columnVectorArray);
    }
        
    public XGaVector<Float64SampledTimeSignal> GetVectorsDt1()
    {
        var vectorList = Enumerable
            .Range(0, SampleCount)
            .Select(i => GetVectorDt1(i / SamplingRate))
            .ToArray();

        var columnVectorArray = new Float64SampledTimeSignal[VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = Float64SampledTimeSignalComposer.Create(SamplingRate, SampleCount);

            for (var i = 0; i < SampleCount; i++)
                columnVector[i] = vectorList[i].Scalar(j);

            columnVectorArray[j] = columnVector.GetFiniteSignal();
        }

        return VectorSamples.Processor.Vector(columnVectorArray);
    }
        
    public XGaVector<Float64SampledTimeSignal> GetVectorsDt2()
    {
        var vectorList = Enumerable
            .Range(0, SampleCount)
            .Select(i => GetVectorDt2(i / SamplingRate))
            .ToArray();

        var columnVectorArray = new Float64SampledTimeSignal[VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = Float64SampledTimeSignalComposer.Create(SamplingRate, SampleCount);

            for (var i = 0; i < SampleCount; i++)
                columnVector[i] = vectorList[i].Scalar(j);

            columnVectorArray[j] = columnVector.GetFiniteSignal();
        }

        return VectorSamples.Processor.Vector(columnVectorArray);
    }
        
    public XGaVector<Float64SampledTimeSignal> GetVectorsDt(int degree = 1)
    {
        var vectorList = Enumerable
            .Range(0, SampleCount)
            .Select(i => GetVectorDt(i / SamplingRate, degree))
            .ToArray();

        var columnVectorArray = new Float64SampledTimeSignal[VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = Float64SampledTimeSignalComposer.Create(SamplingRate, SampleCount);

            for (var i = 0; i < SampleCount; i++)
                columnVector[i] = vectorList[i].Scalar(j);

            columnVectorArray[j] = columnVector.GetFiniteSignal();
        }

        return VectorSamples.Processor.Vector(columnVectorArray);
    }

}