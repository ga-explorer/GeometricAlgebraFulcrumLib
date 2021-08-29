using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorDivideUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, GaScalarStorage<T> mv, T scalar)
        {
            return mv.MapScalar(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, GaVectorStorage<T> mv, T scalar)
        {
            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, GaBivectorStorage<T> mv, T scalar)
        {
            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, GaKVectorStorage<T> mv, T scalar)
        {
            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorSparseStorage<T> mv, T scalar)
        {
            return mv.MapSparseMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, GaScalarStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalar(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, GaVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, GaBivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, GaKVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorSparseStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapSparseMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> DivideByENorm<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, GaScalarStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalar(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, GaVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, GaBivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, GaKVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorSparseStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapSparseMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> DivideByENormSquared<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
    }
}