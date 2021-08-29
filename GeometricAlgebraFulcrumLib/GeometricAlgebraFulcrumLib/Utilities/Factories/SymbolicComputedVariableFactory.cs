using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public sealed class SymbolicComputedVariableFactory :
        SymbolicAtomicExpressionFactoryBase
    {
        internal SymbolicComputedVariableFactory(SymbolicContext context) 
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