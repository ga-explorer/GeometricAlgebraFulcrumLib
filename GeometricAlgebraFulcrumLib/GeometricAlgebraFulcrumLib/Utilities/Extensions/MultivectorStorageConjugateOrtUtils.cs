using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageConjugateOrtUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Conjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, VectorStorage<T> mv)
        {
            return scalarProcessor.NegativeById(mv, basisSet.ConjugateIsNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Conjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, BivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeById(mv, basisSet.ConjugateIsNegative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Conjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv)
        {
            return scalarProcessor.NegativeById(mv, basisSet.ConjugateIsNegative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> Conjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, MultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeById(mv, basisSet.ConjugateIsNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Conjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, MultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeById(mv, basisSet.ConjugateIsNegative);
        }
            
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Conjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeById(mv, basisSet.ConjugateIsNegative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Conjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeById(mv, basisSet.ConjugateIsNegative);
        }
    }
}