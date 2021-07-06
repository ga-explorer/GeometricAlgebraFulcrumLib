using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Factories
{
    public sealed class SymbolicComputedVariablesFactory :
        SymbolicAtomicExpressionsFactoryBase
    {
        internal SymbolicComputedVariablesFactory(SymbolicContext context) 
            : base(context)
        {
        }


        public GaEuclideanSimpleRotor<ISymbolicExpressionAtomic> CreateEuclideanSimpleRotor(IGaVectorStorage<ISymbolicExpressionAtomic> sourceVector, IGaVectorStorage<ISymbolicExpressionAtomic> targetVector)
        {
            return GaEuclideanSimpleRotor<ISymbolicExpressionAtomic>.Create(
                sourceVector,
                targetVector
            );
        }
    }
}