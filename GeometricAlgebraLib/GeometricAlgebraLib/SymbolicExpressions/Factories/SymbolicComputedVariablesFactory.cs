using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.SymbolicExpressions.Factories
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