using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.Signals;

public static class Float64VectorSignalUtils
{
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

            mappedVectorSignal[sampleIndex] = metric.Vector(scalarArray);
        }

        return mappedVectorSignal;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<XGaFloat64Vector> WienerFilter1D(this IReadOnlyList<XGaFloat64Vector> vectorSamples, double samplingRate, int order)
    {
        return vectorSamples.MapComponentSignals(
            c => c.WienerFilter(samplingRate, order)
        );
    }

        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static double SignalToNoiseRatio(this XGaVector<ScalarSignalFloat64> vectorSignal, XGaVector<ScalarSignalFloat64> noiseSignal)
    //{
    //    return vectorSignal.NormSquared().ScalarValue.Average() / 
    //           noiseSignal.NormSquared().ScalarValue.Average();
    //}
}