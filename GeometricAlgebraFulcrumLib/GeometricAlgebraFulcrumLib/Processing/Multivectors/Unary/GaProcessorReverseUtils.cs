using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary
{
    public static class GaProcessorReverseUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageScalar<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageVector<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageBivector<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageKVector<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorGraded<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorSparse<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Reverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeReverse);
        }
    }
}