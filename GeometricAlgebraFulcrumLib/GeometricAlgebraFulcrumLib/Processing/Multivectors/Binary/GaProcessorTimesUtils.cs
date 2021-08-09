using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Utils;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary
{
    public static class GaProcessorTimesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageScalar<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageVector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageBivector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageKVector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageKVector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorGraded<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorGraded<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorSparse<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, GaStorageScalar<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaStorageScalar<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaStorageVector<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaStorageBivector<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, GaStorageVector<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, GaStorageBivector<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageKVector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, GaStorageKVector<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorGraded<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, GaStorageMultivectorGraded<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, GaStorageMultivectorSparse<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaStorageKVector<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaStorageMultivectorGraded<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaStorageMultivectorSparse<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Times<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar, IGaStorageMultivector<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
   }
}