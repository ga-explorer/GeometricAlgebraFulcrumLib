using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageGpOrtUtils
    {
        public static IMultivectorStorage<double> Gp(this BasisBladeSet basisSet, IMultivectorStorage<double> mv)
        {
            var composer =
                new MultivectorFloat64StorageComposer(basisSet);

            var idScalarPairs =
                mv.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
                foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
                    composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IMultivectorStorage<double> GpReverse(this BasisBladeSet basisSet, IMultivectorStorage<double> mv)
        {
            var composer =
                new MultivectorFloat64StorageComposer(basisSet);

            var idScalarPairs =
                mv.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
                foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
                    composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IMultivectorStorage<double> Gp(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1, IMultivectorStorage<double> mv2)
        {
            var composer =
                new MultivectorFloat64StorageComposer(basisSet);

            var idScalarPairs1 =
                mv1.GetIdScalarRecords();

            var idScalarPairs2 =
                mv2.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs1)
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                    composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IMultivectorStorage<double> GpReverse(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1, IMultivectorStorage<double> mv2)
        {
            var composer =
                new MultivectorFloat64StorageComposer(basisSet);

            var idScalarPairs1 =
                mv1.GetIdScalarRecords();

            var idScalarPairs2 =
                mv2.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs1)
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                    composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv)
        {
            return basisSet.IsEuclidean
                ? scalarProcessor.EGpSquared(mv)
                : scalarProcessor.BilinearProduct(mv, basisSet.GpSign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return basisSet.IsEuclidean
                ? scalarProcessor.EGp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSet.GpSign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IMultivectorStorage<T> mv3)
        {
            return basisSet.IsEuclidean
                ? scalarProcessor.EGp(mv1, mv2, mv3)
                : scalarProcessor.BilinearProduct(
                    scalarProcessor.BilinearProduct(mv1, mv2, basisSet.GpSign),
                    mv3, basisSet.GpSign
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, params IMultivectorStorage<T>[] mvsList)
        {
            return mvsList.Skip(1).Aggregate(
                mvsList[0],
                (mv1, mv2) =>
                    scalarProcessor.Gp(mv1, mv2, basisSet)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv)
        {
            return basisSet.IsEuclidean
                ? scalarProcessor.EGpReverse(mv)
                : scalarProcessor.BilinearProduct(mv, basisSet.GpReverseSign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return basisSet.IsEuclidean
                ? scalarProcessor.EGpReverse(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSet.GpReverseSign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv)
        {
            //TODO: Add the Euclidean case
            return scalarProcessor.BilinearProduct(mv, basisSet.GpSign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, basisSet.GpSign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, KVectorStorage<T> mv1, KVectorStorage<T> mv2, KVectorStorage<T> mv3)
        {
            return scalarProcessor.BilinearProduct(
                scalarProcessor.BilinearProduct(mv1, mv2, basisSet.GpSign),
                mv3, basisSet.GpSign
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, BasisBladeSet basisSet)
        {
            return scalarProcessor.BilinearProduct(mv1, basisSet.GpSign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, BasisBladeSet basisSet)
        {
            return basisSet.IsEuclidean
                ? scalarProcessor.EGp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSet.GpSign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, BasisBladeSet basisSet)
        {
            return basisSet.IsEuclidean
                ? scalarProcessor.EGpReverse(mv1)
                : scalarProcessor.BilinearProduct(mv1, basisSet.GpReverseSign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, BasisBladeSet basisSet)
        {
            return basisSet.IsEuclidean
                ? scalarProcessor.EGpReverse(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSet.GpReverseSign);
        }
    }
}
