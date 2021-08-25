using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary
{
    public static class GaProcessorDivideUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageScalar<T> mv, T scalar)
        {
            return mv.MapScalar(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageVector<T> mv, T scalar)
        {
            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageBivector<T> mv, T scalar)
        {
            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageKVector<T> mv, T scalar)
        {
            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorGraded<T> mv, T scalar)
        {
            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorSparse<T> mv, T scalar)
        {
            return mv.MapSparseMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Divide<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, T scalar)
        {
            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageScalar<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalar(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageVector<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageBivector<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageKVector<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorGraded<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorSparse<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapSparseMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> DivideByENorm<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            var scalar = scalarProcessor.ENorm(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageScalar<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalar(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageVector<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageBivector<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapBivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageKVector<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapKVectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorGraded<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapGradedMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorSparse<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapSparseMultivectorScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> DivideByENormSquared<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            var scalar = scalarProcessor.ENormSquared(mv);

            return mv.MapScalars(s => scalarProcessor.Divide(s, scalar));
        }
    }
}