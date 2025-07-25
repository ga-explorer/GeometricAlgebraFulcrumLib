﻿namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

public static class XGaFloat64ProcessorComposerUtils
{
    
    public static XGaFloat64Processor CreateEuclideanXGaFloat64Processor(this IXGaFloat64ProcessorContainer scalarProcessor)
    {
        var processor = XGaFloat64Processor.Euclidean;

        scalarProcessor.AttachXGaProcessor(processor);

        return processor;
    }

    
    public static XGaFloat64Processor CreateProjectiveXGaFloat64Processor(this IXGaFloat64ProcessorContainer scalarProcessor)
    {
        var processor = XGaFloat64Processor.Projective;

        scalarProcessor.AttachXGaProcessor(processor);

        return processor;
    }

    
    public static XGaFloat64Processor CreateConformalXGaFloat64Processor(this IXGaFloat64ProcessorContainer scalarProcessor)
    {
        var processor = XGaFloat64Processor.Conformal;

        scalarProcessor.AttachXGaProcessor(processor);

        return processor;
    }

    
    public static XGaFloat64Processor CreateXGaFloat64Processor(this IXGaFloat64ProcessorContainer scalarProcessor, XGaMetric metric)
    {
        var processor = XGaFloat64Processor.Create(metric.NegativeSignatureBasisCount, metric.ZeroSignatureBasisCount);

        scalarProcessor.AttachXGaProcessor(processor);

        return processor;
    }

    
    public static XGaFloat64Processor CreateXGaFloat64Processor(this IXGaFloat64ProcessorContainer scalarProcessor, int negativeCount, int zeroCount)
    {
        var processor = XGaFloat64Processor.Create(negativeCount, zeroCount);

        scalarProcessor.AttachXGaProcessor(processor);

        return processor;
    }

}