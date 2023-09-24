using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

public static class RGaProcessorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaEuclideanProcessor<T> CreateEuclideanRGaProcessor<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var processor = RGaProcessor<T>.CreateEuclidean(scalarProcessor);

        if (scalarProcessor is IRGaProcessorContainer<T> processorContainer)
            processorContainer.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaProjectiveProcessor<T> CreateProjectiveRGaProcessor<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var processor = RGaProcessor<T>.CreateProjective(scalarProcessor);

        if (scalarProcessor is IRGaProcessorContainer<T> processorContainer)
            processorContainer.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalProcessor<T> CreateConformalRGaProcessor<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var processor = RGaProcessor<T>.CreateConformal(scalarProcessor);

        if (scalarProcessor is IRGaProcessorContainer<T> processorContainer)
            processorContainer.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaProcessor<T> CreateRGaProcessor<T>(this IScalarProcessor<T> scalarProcessor, RGaMetric metric)
    {
        var processor = RGaProcessor<T>.Create(scalarProcessor, metric);

        if (scalarProcessor is IRGaProcessorContainer<T> processorContainer)
            processorContainer.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaProcessor<T> CreateRGaProcessor<T>(this IScalarProcessor<T> scalarProcessor, int negativeCount, int zeroCount)
    {
        var processor = RGaProcessor<T>.Create(scalarProcessor, negativeCount, zeroCount);

        if (scalarProcessor is IRGaProcessorContainer<T> processorContainer)
            processorContainer.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaProcessor<T> CreateProcessor<T>(this RGaMetric metric, IScalarProcessor<T> scalarProcessor)
    {
        var processor = RGaProcessor<T>.Create(scalarProcessor, metric);

        if (scalarProcessor is IRGaProcessorContainer<T> processorContainer)
            processorContainer.AttachRGaProcessor(processor);

        return processor;
    }
}