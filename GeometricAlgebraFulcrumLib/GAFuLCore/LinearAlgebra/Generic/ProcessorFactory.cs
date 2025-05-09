using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic;

public static class ProcessorFactory
{
    
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinearProcessor<T> CreateLinearAlgebraProcessor<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new LinearProcessor<T>(scalarProcessor);
    }

        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GeometricAlgebraEuclideanProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraEuclideanProcessor(this MetaContext context, uint vSpaceDimensions)
    //{
    //    var processor = new GeometricAlgebraEuclideanProcessor<IMetaExpressionAtomic>(
    //        context,
    //        vSpaceDimensions
    //    );

    //    context.XGaProcessor = processor;

    //    return processor;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GeometricAlgebraConformalProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraConformalProcessor(this MetaContext context, uint vSpaceDimensions)
    //{
    //    var processor = new GeometricAlgebraConformalProcessor<IMetaExpressionAtomic>(
    //        context,
    //        vSpaceDimensions
    //    );

    //    context.XGaProcessor = processor;

    //    return processor;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GeometricAlgebraProjectiveProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraProjectiveProcessor(this MetaContext context, uint vSpaceDimensions)
    //{
    //    var processor = new GeometricAlgebraProjectiveProcessor<IMetaExpressionAtomic>(
    //        context,
    //        vSpaceDimensions
    //    );

    //    context.XGaProcessor = processor;

    //    return processor;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraOrthonormalProcessor(this MetaContext context, uint positiveCount, uint negativeCount)
    //{
    //    var processor = new GeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic>(
    //        context,
    //        GaBasisSet.Create(positiveCount, negativeCount)
    //    );

    //    context.XGaProcessor = processor;

    //    return processor;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static GeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic> CreateGeometricAlgebraOrthonormalProcessor(this MetaContext context, uint positiveCount, uint negativeCount, uint zeroCount)
    //{
    //    var processor = new GeometricAlgebraOrthonormalProcessor<IMetaExpressionAtomic>(
    //        context,
    //        GaBasisSet.Create(positiveCount, negativeCount, zeroCount)
    //    );

    //    context.XGaProcessor = processor;

    //    return processor;
    //}
}