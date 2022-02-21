using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageConjugateEucUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> EConjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> EConjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> EConjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> EConjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> EConjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }
            
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> EConjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> EConjugate<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return scalarProcessor.NegativeByGrade(mv, BasisBladeUtils.ReverseIsNegativeOfGrade);
        }
    }
}