using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageConjugateUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Conjugate<T>(this IGeometricAlgebraProcessor<T> processor, VectorStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EConjugate(mv1),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Conjugate(ortProcessor.BasisSet, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Conjugate(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Conjugate<T>(this IGeometricAlgebraProcessor<T> processor, BivectorStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EConjugate(mv1),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Conjugate(ortProcessor.BasisSet, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Conjugate(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Conjugate<T>(this IGeometricAlgebraProcessor<T> processor, KVectorStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EConjugate(mv1),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Conjugate(ortProcessor.BasisSet, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Conjugate(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> Conjugate<T>(this IGeometricAlgebraProcessor<T> processor, MultivectorGradedStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EConjugate(mv1),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Conjugate(ortProcessor.BasisSet, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Conjugate(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Conjugate<T>(this IGeometricAlgebraProcessor<T> processor, MultivectorStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EConjugate(mv1),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Conjugate(ortProcessor.BasisSet, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Conjugate(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Conjugate<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorGradedStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EConjugate(mv1),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Conjugate(ortProcessor.BasisSet, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Conjugate(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Conjugate<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EConjugate(mv1),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Conjugate(ortProcessor.BasisSet, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Conjugate(mv1)
            };
        }
    }
}