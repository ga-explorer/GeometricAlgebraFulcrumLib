using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageTimesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv, T scalar)
        {
            return mv.MapVectorScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv, T scalar)
        {
            return mv.MapBivectorScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Times(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, T scalar)
        {
            return mv.MapKVectorScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, T scalar)
        {
            return mv.MapMultivectorScalars(s => scalarProcessor.Times(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Times(s, scalar));
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, VectorStorage<T> mv)
        {
            return mv.MapVectorScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, BivectorStorage<T> mv)
        {
            return mv.MapBivectorScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, MultivectorGradedStorage<T> mv)
        {
            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, KVectorStorage<T> mv)
        {
            return mv.MapKVectorScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, IMultivectorGradedStorage<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, MultivectorStorage<T> mv)
        {
            return mv.MapMultivectorScalars(s => scalarProcessor.Times(scalar, s));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Times<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar, IMultivectorStorage<T> mv)
        {
            return mv.MapScalars(s => scalarProcessor.Times(scalar, s));
        }
   }
}