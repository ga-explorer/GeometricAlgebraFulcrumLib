using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageDivideUtils
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
        public static VectorStorage<T> NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv, T scalar)
        {
            return mv.MapVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv, T scalar)
        {
            return mv.MapBivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapGradedMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, T scalar)
        {
            return mv.MapKVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, T scalar)
        {
            return mv.MapMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> NegativeDivide<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
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
        public static VectorStorage<T> DivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> DivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> DivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> DivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> DivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> DivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> DivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> NegativeDivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> NegativeDivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> NegativeDivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> NegativeDivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> NegativeDivideByENorm<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> NegativeDivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> NegativeDivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> NegativeDivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> NegativeDivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> NegativeDivideByNorm<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.Norm(mv);

            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> DivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> DivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> DivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> DivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> DivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> DivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> DivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> NegativeDivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> NegativeDivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> NegativeDivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> NegativeDivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> NegativeDivideByENormSquared<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> NegativeDivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> NegativeDivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> NegativeDivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> NegativeDivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> NegativeDivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapMultivectorScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> NegativeDivideByNormSquared<T>(this IGeometricAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var scalar = scalarProcessor.NormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.NegativeDivide(s, scalar));
        }
    }
}