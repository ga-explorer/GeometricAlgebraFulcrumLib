using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorCliffordConjugateUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, GaScalarStorage<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, GaVectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, GaBivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, GaKVectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeCliffordConjugate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeCliffordConjugate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeCliffordConjugate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeCliffordConjugate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorSparseStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeCliffordConjugate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeCliffordConjugate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> CliffordConjugate<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeCliffordConjugate);
        }
    }
}