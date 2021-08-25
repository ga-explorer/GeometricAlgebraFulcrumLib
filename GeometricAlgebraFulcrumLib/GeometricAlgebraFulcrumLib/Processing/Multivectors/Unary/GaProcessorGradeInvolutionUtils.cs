using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary
{
    public static class GaProcessorGradeInvolutionUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageScalar<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageVector<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv)
        {
            return scalarProcessor.Negative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageBivector<T> mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv)
        {
            return mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageKVector<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeGradeInvolution);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeGradeInvolution);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorGraded<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeGradeInvolution);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeGradeInvolution);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorSparse<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeGradeInvolution);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeGradeInvolution);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GradeInvolution<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return scalarProcessor.Negative(mv, GaBasisBladeUtils.GradeHasNegativeGradeInvolution);
        }
    }
}