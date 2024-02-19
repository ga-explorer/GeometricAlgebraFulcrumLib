using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

public sealed class MetaExpressionComputedVariableFactory :
    MetaExpressionAtomicFactoryBase
{
    internal MetaExpressionComputedVariableFactory(MetaContext context)
        : base(context)
    {
    }


    //public GeoEuclideanSimpleRotor<ISymbolicExpressionAtomic> CreateEuclideanSimpleRotor(IGeoProcessor<ISymbolicExpressionAtomic> processor, IGeosVector<ISymbolicExpressionAtomic> sourceVector, IGeosVector<ISymbolicExpressionAtomic> targetVector)
    //{
    //    return processor.CreateEuclideanSimpleRotor(
    //        sourceVector,
    //        targetVector
    //    );
    //}
}