using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    internal static class MultivectorStorageConjugateCobUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Conjugate<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, VectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Conjugate<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, BivectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Conjugate<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, KVectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> Conjugate<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, MultivectorGradedStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            ).ToMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Conjugate<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, MultivectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            ).ToMultivectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Conjugate<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, IMultivectorGradedStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Conjugate<T>(this IGeometricAlgebraChangeOfBasisProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Conjugate<T>(this VectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Conjugate<T>(this BivectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Conjugate<T>(this KVectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Conjugate<T>(this MultivectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            ).ToMultivectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> Conjugate<T>(this MultivectorGradedStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            ).ToMultivectorGradedStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Conjugate<T>(this IMultivectorGradedStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Conjugate<T>(this IMultivectorStorage<T> mv1, IGeometricAlgebraChangeOfBasisProcessor<T> processor)
        {
            var s1 = processor.OmTargetToOrthonormal.OmMap(mv1);

            return processor.OmOrthonormalToTarget.OmMap(
                processor.Conjugate(processor.BasisSet, s1)
            );
        }
    }
}