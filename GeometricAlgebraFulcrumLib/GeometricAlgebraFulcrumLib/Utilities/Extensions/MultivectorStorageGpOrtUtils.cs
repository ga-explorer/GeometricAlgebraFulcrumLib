using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageGpOrtUtils
    {
        public static IMultivectorStorage<double> Gp(this GeometricAlgebraSignatureLookup basisSignature, IMultivectorStorage<double> mv)
        {
            var composer = 
                new MultivectorFloat64StorageComposer(basisSignature);

            var idScalarPairs = 
                mv.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
                composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IMultivectorStorage<double> GpReverse(this GeometricAlgebraSignatureLookup basisSignature, IMultivectorStorage<double> mv)
        {
            var composer = 
                new MultivectorFloat64StorageComposer(basisSignature);

            var idScalarPairs = 
                mv.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
                composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IMultivectorStorage<double> Gp(this GeometricAlgebraSignatureLookup basisSignature, IMultivectorStorage<double> mv1, IMultivectorStorage<double> mv2)
        {
            var composer = 
                new MultivectorFloat64StorageComposer(basisSignature);

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
        
        public static IMultivectorStorage<double> GpReverse(this GeometricAlgebraSignatureLookup basisSignature, IMultivectorStorage<double> mv1, IMultivectorStorage<double> mv2)
        {
            var composer = 
                new MultivectorFloat64StorageComposer(basisSignature);

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
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature basisSignature, IMultivectorStorage<T> mv)
        {
            return basisSignature is GeometricAlgebraSignatureEuclidean
                ? scalarProcessor.EGp(mv)
                : scalarProcessor.BilinearProduct(mv, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature basisSignature, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return basisSignature is GeometricAlgebraSignatureEuclidean
                ? scalarProcessor.EGp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature basisSignature, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IMultivectorStorage<T> mv3)
        {
            return basisSignature is GeometricAlgebraSignatureEuclidean
                ? scalarProcessor.EGp(mv1, mv2, mv3)
                : scalarProcessor.BilinearProduct(
                    scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature), 
                    mv3, basisSignature.GpSignature
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature basisSignature, params IMultivectorStorage<T>[] mvsList)
        {
            return mvsList.Skip(1).Aggregate(
                mvsList[0], 
                (mv1, mv2) => 
                    scalarProcessor.Gp(mv1, mv2, basisSignature)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature basisSignature, IMultivectorStorage<T> mv)
        {
            return basisSignature is GeometricAlgebraSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv)
                : scalarProcessor.BilinearProduct(mv, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature basisSignature, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return basisSignature is GeometricAlgebraSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature basisSignature, KVectorStorage<T> mv)
        {
            //TODO: Add the Euclidean case
            return scalarProcessor.BilinearProduct(mv, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature basisSignature, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IGeometricAlgebraSignature basisSignature, KVectorStorage<T> mv1, KVectorStorage<T> mv2, KVectorStorage<T> mv3)
        {
            return scalarProcessor.BilinearProduct(
                scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature), 
                mv3, basisSignature.GpSignature
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IGeometricAlgebraSignature basisSignature)
        {
            return scalarProcessor.BilinearProduct(mv1, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Gp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IGeometricAlgebraSignature basisSignature)
        {
            return basisSignature is GeometricAlgebraSignatureEuclidean
                ? scalarProcessor.EGp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IGeometricAlgebraSignature basisSignature)
        {
            return basisSignature is GeometricAlgebraSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv1)
                : scalarProcessor.BilinearProduct(mv1, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> GpReverse<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2, IGeometricAlgebraSignature basisSignature)
        {
            return basisSignature is GeometricAlgebraSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpReverseSignature);
        }
    }
}
