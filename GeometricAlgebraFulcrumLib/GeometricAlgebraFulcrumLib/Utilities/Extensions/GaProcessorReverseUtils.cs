using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorReverseUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalarStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, GaScalarStorage<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, GaVectorStorage<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, GaBivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, GaKVectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorSparseStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Reverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }
    }
}