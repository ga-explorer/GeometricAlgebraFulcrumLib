using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.PolynomialAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.MathBase.SignalAlgebra.Interpolators;

public class RGaVectorPolynomialInterpolator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaVectorPolynomialInterpolator Create(RGaVector<Float64Signal> scalarSamples)
    {
        return new RGaVectorPolynomialInterpolator(scalarSamples);
    }


    public RGaVector<Float64Signal> VectorSamples { get; }

    public RGaProcessor<Float64Signal> SignalProcessor 
        => VectorSamples.Processor;

    public RGaFloat64Processor SampleProcessor { get; }
        = RGaFloat64Processor.Euclidean;
        
    public int VSpaceDimensions { get; }

    public double SamplingRate { get; }

    public int InterpolationSamples { get; set; } = 128;

    public int PolynomialOrder { get; set; } = 13;

    public int SampleCount { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RGaVectorPolynomialInterpolator(RGaVector<Float64Signal> scalarSamples)
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
                VectorSamples[j].ScalarValue;

            var yValues =
                indexList.Select(i => samplesArray[i]).ToArray();

            yield return PolynomialFunction<double>.Create(
                ScalarProcessorOfFloat64.DefaultProcessor,
                Fit.Polynomial(xValues, yValues, PolynomialOrder)
            );
        }
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetVector(double t)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(t)
            .Select(p => p.GetValue(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetVectorDt1(double t)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(t)
            .Select(p => p.GetValueDt1(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetVectorDt2(double t)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(t)
            .Select(p => p.GetValueDt2(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GetVectorDt(double t, int degree = 1)
    {
        var signalTime = (SampleCount - 1) / SamplingRate;
        if (t < 0 || t > signalTime)
            return SampleProcessor.CreateZeroVector();

        var scalarArray = GetInterpolatorList(t)
            .Select(p => p.GetDerivative(degree).GetValue(t))
            .ToArray();
            
        return SampleProcessor.CreateVector(scalarArray);
    }

        
    public RGaVector<Float64Signal> GetVectors()
    {
        var vectorList = Enumerable
            .Range(0, SampleCount)
            .Select(i => GetVector(i / SamplingRate))
            .ToArray();

        var columnVectorArray = new Float64Signal[VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = Float64Signal.Create(SamplingRate, SampleCount);

            for (var i = 0; i < SampleCount; i++)
                columnVector[i] = vectorList[i][j];

            columnVectorArray[j] = columnVector;
        }

        return VectorSamples.Processor.CreateVector(columnVectorArray);
    }
        
    public RGaVector<Float64Signal> GetVectorsDt1()
    {
        var vectorList = Enumerable
            .Range(0, SampleCount)
            .Select(i => GetVectorDt1(i / SamplingRate))
            .ToArray();

        var columnVectorArray = new Float64Signal[VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = Float64Signal.Create(SamplingRate, SampleCount);

            for (var i = 0; i < SampleCount; i++)
                columnVector[i] = vectorList[i][j];

            columnVectorArray[j] = columnVector;
        }

        return VectorSamples.Processor.CreateVector(columnVectorArray);
    }
        
    public RGaVector<Float64Signal> GetVectorsDt2()
    {
        var vectorList = Enumerable
            .Range(0, SampleCount)
            .Select(i => GetVectorDt2(i / SamplingRate))
            .ToArray();

        var columnVectorArray = new Float64Signal[VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = Float64Signal.Create(SamplingRate, SampleCount);

            for (var i = 0; i < SampleCount; i++)
                columnVector[i] = vectorList[i][j];

            columnVectorArray[j] = columnVector;
        }

        return VectorSamples.Processor.CreateVector(columnVectorArray);
    }
        
    public RGaVector<Float64Signal> GetVectorsDt(int degree = 1)
    {
        var vectorList = Enumerable
            .Range(0, SampleCount)
            .Select(i => GetVectorDt(i / SamplingRate, degree))
            .ToArray();

        var columnVectorArray = new Float64Signal[VSpaceDimensions];

        for (var j = 0; j < VSpaceDimensions; j++)
        {
            var columnVector = Float64Signal.Create(SamplingRate, SampleCount);

            for (var i = 0; i < SampleCount; i++)
                columnVector[i] = vectorList[i][j];

            columnVectorArray[j] = columnVector;
        }

        return VectorSamples.Processor.CreateVector(columnVectorArray);
    }

}