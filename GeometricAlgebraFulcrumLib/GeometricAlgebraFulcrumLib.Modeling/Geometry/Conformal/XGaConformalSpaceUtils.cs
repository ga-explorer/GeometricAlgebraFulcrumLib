using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal;

public static class XGaConformalSpaceUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalSpace4D<T> CreateConformalXGaSpace4D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var space = XGaConformalSpace4D<T>.Create(scalarProcessor);
        var processor = space.ConformalProcessor;

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return space;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalSpace5D<T> CreateConformalXGaSpace5D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var space = XGaConformalSpace5D<T>.Create(scalarProcessor);
        var processor = space.ConformalProcessor;

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return space;
    }
}