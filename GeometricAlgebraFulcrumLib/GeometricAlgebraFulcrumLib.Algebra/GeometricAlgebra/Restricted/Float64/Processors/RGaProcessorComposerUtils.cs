using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

public static class RGaProcessorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Processor CreateEuclideanRGaFloat64Processor(this IRGaFloat64ProcessorContainer scalarProcessor)
    {
        var processor = RGaFloat64Processor.Euclidean;

        scalarProcessor.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Processor CreateProjectiveRGaFloat64Processor(this IRGaFloat64ProcessorContainer scalarProcessor)
    {
        var processor = RGaFloat64Processor.Projective;

        scalarProcessor.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Processor CreateConformalRGaFloat64Processor(this IRGaFloat64ProcessorContainer scalarProcessor)
    {
        var processor = RGaFloat64Processor.Conformal;

        scalarProcessor.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Processor CreateRGaFloat64Processor(this IRGaFloat64ProcessorContainer scalarProcessor, RGaMetric metric)
    {
        var processor = RGaFloat64Processor.Create(metric.NegativeSignatureBasisCount, metric.ZeroSignatureBasisCount);

        scalarProcessor.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Processor CreateRGaFloat64Processor(this IRGaFloat64ProcessorContainer scalarProcessor, int negativeCount, int zeroCount)
    {
        var processor = RGaFloat64Processor.Create(negativeCount, zeroCount);

        scalarProcessor.AttachRGaProcessor(processor);

        return processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Processor CreateProcessor(this RGaMetric metric, IRGaFloat64ProcessorContainer scalarProcessor)
    {
        var processor = RGaFloat64Processor.Create(metric.NegativeSignatureBasisCount, metric.ZeroSignatureBasisCount);

        scalarProcessor.AttachRGaProcessor(processor);

        return processor;
    }
}