using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtGpUtils
    {
        public static IGaMultivectorStorage<double> Gp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv)
        {
            var composer = 
                new GaMultivectorFloat64StorageComposer(basisSignature);

            var idScalarPairs = 
                mv.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
                composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaMultivectorStorage<double> GpReverse(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv)
        {
            var composer = 
                new GaMultivectorFloat64StorageComposer(basisSignature);

            var idScalarPairs = 
                mv.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetIndexScalarRecords())
            foreach (var (id2, scalar2) in idScalarPairs.GetIndexScalarRecords())
                composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaMultivectorStorage<double> Gp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorFloat64StorageComposer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaMultivectorStorage<double> GpReverse(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorFloat64StorageComposer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Gp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaMultivectorStorage<T> mv)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGp(mv)
                : scalarProcessor.BilinearProduct(mv, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Gp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Gp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorStorage<T> mv3)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGp(mv1, mv2, mv3)
                : scalarProcessor.BilinearProduct(
                    scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature), 
                    mv3, basisSignature.GpSignature
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Gp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, params IGaMultivectorStorage<T>[] mvsList)
        {
            return mvsList.Skip(1).Aggregate(
                mvsList[0], 
                (mv1, mv2) => 
                    scalarProcessor.Gp(mv1, mv2, basisSignature)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> GpReverse<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaMultivectorStorage<T> mv)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv)
                : scalarProcessor.BilinearProduct(mv, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> GpReverse<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Gp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaKVectorStorage<T> mv)
        {
            //TODO: Add the Euclidean case
            return scalarProcessor.BilinearProduct(mv, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Gp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Gp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2, IGaKVectorStorage<T> mv3)
        {
            return scalarProcessor.BilinearProduct(
                scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature), 
                mv3, basisSignature.GpSignature
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Gp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            return scalarProcessor.BilinearProduct(mv1, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Gp<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> GpReverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaSignature basisSignature)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv1)
                : scalarProcessor.BilinearProduct(mv1, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> GpReverse<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpReverseSignature);
        }
    }
}
