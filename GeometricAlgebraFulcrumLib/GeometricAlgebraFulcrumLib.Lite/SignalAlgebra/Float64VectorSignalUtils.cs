using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra;

public static class Float64VectorSignalUtils
{
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<double> Mean(this IScalarProcessor<double> scalarProcessor, Scalar<Float64Signal> vectorSignal)
    {
        return vectorSignal.ScalarValue.Mean().CreateScalar(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<Float64Signal> GetRunningAverageSignal(this Scalar<Float64Signal> scalarSignal, int averageSampleCount)
    {
        return scalarSignal
            .ScalarValue
            .GetRunningAverageSignal(averageSampleCount)
            .CreateScalar(scalarSignal.ScalarProcessor);
    }
        
    public static IReadOnlyList<XGaFloat64Vector> MapComponentSignals(this IReadOnlyList<XGaFloat64Vector> vectorSamples, Func<IReadOnlyList<double>, IReadOnlyList<double>> componentMapping)
    {
        var sampleCount = vectorSamples.Count;
        var metric = XGaFloat64Processor.Euclidean;
        var vSpaceDimensions = vectorSamples.GetVSpaceDimensions();

        var componentSignals = new List<double[]>(vSpaceDimensions);

        for (var i = 0; i < vSpaceDimensions; i++)
            componentSignals.Add(new double[sampleCount]);

        for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
        {
            for (var i = 0; i < vSpaceDimensions; i++)
                componentSignals[i][sampleIndex] = 
                    vectorSamples[sampleIndex].Scalar(i);
        }

        for (var i = 0; i < vSpaceDimensions; i++)
            componentSignals[i] = componentMapping(componentSignals[i]).ToArray();

        var mappedVectorSignal = new XGaFloat64Vector[sampleCount];

        for (var sampleIndex = 0; sampleIndex < sampleCount; sampleIndex++)
        {
            var scalarArray = new double[vSpaceDimensions];

            for (var i = 0; i < vSpaceDimensions; i++)
                scalarArray[i] = componentSignals[i][sampleIndex];

            mappedVectorSignal[sampleIndex] = metric.CreateVector(scalarArray);
        }

        return mappedVectorSignal;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<Float64Signal> WienerFilter(this Scalar<Float64Signal> scalarSamples, int order)
    {
        return scalarSamples
            .ScalarValue
            .WienerFilter(order)
            .CreateScalar(scalarSamples.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<XGaFloat64Vector> WienerFilter1D(this IReadOnlyList<XGaFloat64Vector> vectorSamples, double samplingRate, int order)
    {
        return vectorSamples.MapComponentSignals(
            c => c.WienerFilter(samplingRate, order)
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SignalToNoiseRatio(this Scalar<Float64Signal> scalarSignal, Scalar<Float64Signal> noiseSignal)
    {
        return scalarSignal.Square().ScalarValue.Average() /
               noiseSignal.Square().ScalarValue.Average();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SignalToNoiseRatioDb(this Scalar<Float64Signal> scalarSignal, Scalar<Float64Signal> noiseSignal)
    {
        return (scalarSignal.Square().ScalarValue.Average() /
                noiseSignal.Square().ScalarValue.Average()).Log10() * 10d;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static double SignalToNoiseRatio(this XGaVector<ScalarSignalFloat64> vectorSignal, XGaVector<ScalarSignalFloat64> noiseSignal)
    //{
    //    return vectorSignal.NormSquared().ScalarValue.Average() / 
    //           noiseSignal.NormSquared().ScalarValue.Average();
    //}

        
}