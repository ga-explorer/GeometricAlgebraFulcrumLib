using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorTimesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, GaScalarStorage<T> mv, T scalar)
        {
            return mv.MapScalar(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, GaVectorStorage<T> mv, T scalar)
        {
            return mv.MapVectorScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, GaBivectorStorage<T> mv, T scalar)
        {
            return mv.MapBivectorScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, GaKVectorStorage<T> mv, T scalar)
        {
            return mv.MapKVectorScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorSparseStorage<T> mv, T scalar)
        {
            return mv.MapSparseMultivectorScalars(s => scalarProcessor.Times(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, GaScalarStorage<T> mv)
        {
            return mv.MapScalar(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IGaScalarStorage<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IGaVectorStorage<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IGaBivectorStorage<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, GaVectorStorage<T> mv)
        {
            return mv.MapVectorScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, GaBivectorStorage<T> mv)
        {
            return mv.MapBivectorScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, GaKVectorStorage<T> mv)
        {
            return mv.MapKVectorScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, GaMultivectorGradedStorage<T> mv)
        {
            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, GaMultivectorSparseStorage<T> mv)
        {
            return mv.MapSparseMultivectorScalars(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IGaKVectorStorage<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IGaMultivectorGradedStorage<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IGaMultivectorSparseStorage<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IGaMultivectorStorage<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
   }
}