using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters;

public static class GaProcessorConverterUtils
{
    public static XGaFloat64Processor ToXGaProcessor(this RGaMetric metric)
    {
        if (metric.IsEuclidean)
            return XGaFloat64Processor.Euclidean;

        if (metric.IsProjective)
            return XGaFloat64Processor.Projective;

        if (metric.IsConformal)
            return XGaFloat64Processor.Conformal;

        return XGaFloat64Processor.Create(
            metric.NegativeSignatureBasisCount,
            metric.ZeroSignatureBasisCount
        );
    }
    
    public static RGaFloat64Processor ToRGaProcessor(this XGaMetric metric)
    {
        if (metric.IsEuclidean)
            return RGaFloat64Processor.Euclidean;

        if (metric.IsProjective)
            return RGaFloat64Processor.Projective;

        if (metric.IsConformal)
            return RGaFloat64Processor.Conformal;

        return RGaFloat64Processor.Create(
            metric.NegativeSignatureBasisCount,
            metric.ZeroSignatureBasisCount
        );
    }


    public static XGaProcessor<T> ToXGaProcessor<T>(this RGaMetric metric, IScalarProcessor<T> scalarProcessor)
    {
        if (metric.IsEuclidean)
            return XGaProcessor<T>.CreateEuclidean(scalarProcessor);

        if (metric.IsProjective)
            return XGaProcessor<T>.CreateProjective(scalarProcessor);

        if (metric.IsConformal)
            return XGaProcessor<T>.CreateConformal(scalarProcessor);

        return XGaProcessor<T>.Create(
            scalarProcessor,
            metric.NegativeSignatureBasisCount,
            metric.ZeroSignatureBasisCount
        );
    }

    public static RGaProcessor<T> ToRGaProcessor<T>(this XGaMetric metric, IScalarProcessor<T> scalarProcessor)
    {
        if (metric.IsEuclidean)
            return RGaProcessor<T>.CreateEuclidean(scalarProcessor);

        if (metric.IsProjective)
            return RGaProcessor<T>.CreateProjective(scalarProcessor);

        if (metric.IsConformal)
            return RGaProcessor<T>.CreateConformal(scalarProcessor);

        return RGaProcessor<T>.Create(
            scalarProcessor,
            metric.NegativeSignatureBasisCount,
            metric.ZeroSignatureBasisCount
        );
    }

}