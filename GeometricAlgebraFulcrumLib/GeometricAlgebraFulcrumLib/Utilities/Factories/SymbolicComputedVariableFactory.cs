using GeometricAlgebraFulcrumLib.Processors.SymbolicAlgebra.Context;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public sealed class SymbolicComputedVariableFactory :
        SymbolicAtomicExpressionFactoryBase
    {
        internal SymbolicComputedVariableFactory(SymbolicContext context) 
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
}