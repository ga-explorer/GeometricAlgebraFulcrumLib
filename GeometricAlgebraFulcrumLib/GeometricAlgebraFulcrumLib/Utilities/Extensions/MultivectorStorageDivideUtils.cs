using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageDivideUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv, T scalar)
        {
            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv, T scalar)
        {
            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, T scalar)
        {
            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, T scalar)
        {
            return mv.MapMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Divide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> DivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> DivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> DivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> DivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> DivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> DivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> DivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> DivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> DivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> DivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> DivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> DivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> DivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> DivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
    }
}