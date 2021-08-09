using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Factories
{
    public sealed class SymbolicComputedVariablesFactory :
        SymbolicAtomicExpressionsFactoryBase
    {
        internal SymbolicComputedVariablesFactory(SymbolicContext context) 
            : base(context)
        {
        }


        //public GaEuclideanSimpleRotor<ISymbolicExpressionAtomic> CreateEuclideanSimpleRotor(IGaProcessor<ISymbolicExpressionAtomic> processor, IGasVector<ISymbolicExpressionAtomic> sourceVector, IGasVector<ISymbolicExpressionAtomic> targetVector)
        //{
        //    return processor.CreateEuclideanSimpleRotor(
        //        sourceVector,
        //        targetVector
        //    );
        //}
    }
}