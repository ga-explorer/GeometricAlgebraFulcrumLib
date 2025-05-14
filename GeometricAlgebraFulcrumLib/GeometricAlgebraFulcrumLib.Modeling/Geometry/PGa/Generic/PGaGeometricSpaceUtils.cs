using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic;

public static class PGaGeometricSpaceUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaGeometricSpace3D<T> CreatePGaGeometricSpace3D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var space = PGaGeometricSpace3D<T>.Create(scalarProcessor);
        var processor = space.ProjectiveProcessor;

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return space;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaGeometricSpace4D<T> CreatePGaGeometricSpace4D<T>(this IScalarProcessor<T> scalarProcessor)
    {
        var space = PGaGeometricSpace4D<T>.Create(scalarProcessor);
        var processor = space.ProjectiveProcessor;

        if (scalarProcessor is IXGaProcessorContainer<T> processorContainer)
            processorContainer.AttachXGaProcessor(processor);

        return space;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaBlade<T> ToBlade<T>(this PGaGeometricSpace<T> pgaSpace, XGaKVector<T> kVector)
    {
        return new PGaBlade<T>(pgaSpace, kVector);
    }
}