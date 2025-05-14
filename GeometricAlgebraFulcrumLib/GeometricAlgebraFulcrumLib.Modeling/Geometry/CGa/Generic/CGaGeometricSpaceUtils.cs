using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;

public static class CGaGeometricSpaceUtils
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaGeometricSpace4D<T> CreateCGaGeometricSpace4D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var space = CGaGeometricSpace4D<T>.Create(scalarProcessor);
        var processor = space.ConformalProcessor;

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return space;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaGeometricSpace5D<T> CreateCGaGeometricSpace5D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var space = CGaGeometricSpace5D<T>.Create(scalarProcessor);
        var processor = space.ConformalProcessor;

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return space;
    }
}