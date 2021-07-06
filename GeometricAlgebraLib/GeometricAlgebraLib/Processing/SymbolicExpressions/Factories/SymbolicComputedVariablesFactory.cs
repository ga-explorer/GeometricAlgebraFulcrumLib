using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Processing.SymbolicExpressions.Factories
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