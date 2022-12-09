using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageGpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpSquared<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EGpSquared(mv1),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Gp(ortProcessor.BasisSet, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Gp(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EGp(mv1, mv2),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Gp(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Gp(mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IMultivectorStorage<T> mv3)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EGp(mv1, mv2, mv3),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.Gp(ortProcessor.BasisSet, mv1, mv2, mv3),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).Gp(mv1, mv2, mv3)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EGpReverse(mv1),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.GpReverse(ortProcessor.BasisSet, mv1),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).GpReverse(mv1)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IGeometricAlgebraProcessor<T> processor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return processor switch
            {
                IGeometricAlgebraEuclideanProcessor<T> =>
                    processor.EGpReverse(mv1, mv2),

                IGeometricAlgebraOrthonormalProcessor<T> ortProcessor =>
                    processor.GpReverse(ortProcessor.BasisSet, mv1, mv2),

                _ =>
                    ((IGeometricAlgebraChangeOfBasisProcessor<T>)processor).GpReverse(mv1, mv2)
            };
        }
    }
}