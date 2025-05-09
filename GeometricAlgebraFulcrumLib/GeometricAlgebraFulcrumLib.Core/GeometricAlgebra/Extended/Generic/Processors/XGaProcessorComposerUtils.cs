using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Processors;

public static class XGaProcessorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaEuclideanProcessor<T> CreateEuclideanXGaProcessor<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var processor = XGaProcessor<T>.CreateEuclidean(scalarProcessor);

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveProcessor<T> CreateProjectiveXGaProcessor<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var processor = XGaProcessor<T>.CreateProjective(scalarProcessor);

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalProcessor<T> CreateConformalXGaProcessor<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var processor = XGaProcessor<T>.CreateConformal(scalarProcessor);

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProcessor<T> CreateXGaProcessor<T>(this IScalarProcessor<T> scalarProcessor, XGaMetric metric)
    {
        var processor = XGaProcessor<T>.Create(scalarProcessor, metric);

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProcessor<T> CreateXGaProcessor<T>(this IScalarProcessor<T> scalarProcessor, int negativeCount, int zeroCount)
    {
        var processor = XGaProcessor<T>.Create(scalarProcessor, negativeCount, zeroCount);

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProcessor<T> CreateProcessor<T>(this XGaMetric metric, IScalarProcessor<T> scalarProcessor)
    {
        var processor = XGaProcessor<T>.Create(scalarProcessor, metric);

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return processor;
    }
    

}