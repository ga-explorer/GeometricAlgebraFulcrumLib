using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtGpUtils
    {
        public static IGaStorageMultivector<double> Gp(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv)
        {
            var composer = 
                new GaStorageComposerMultivectorFloat64(basisSignature);

            var idScalarPairs = 
                mv.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetKeyValueRecords())
            foreach (var (id2, scalar2) in idScalarPairs.GetKeyValueRecords())
                composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaStorageMultivector<double> GpReverse(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv)
        {
            var composer = 
                new GaStorageComposerMultivectorFloat64(basisSignature);

            var idScalarPairs = 
                mv.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs.GetKeyValueRecords())
            foreach (var (id2, scalar2) in idScalarPairs.GetKeyValueRecords())
                composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaStorageMultivector<double> Gp(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1, IGaStorageMultivector<double> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorFloat64(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2.GetKeyValueRecords())
                composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaStorageMultivector<double> GpReverse(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1, IGaStorageMultivector<double> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorFloat64(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2.GetKeyValueRecords())
                composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageMultivector<T> mv)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGp(mv)
                : scalarProcessor.BilinearProduct(mv, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaStorageMultivector<T> mv3)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGp(mv1, mv2, mv3)
                : scalarProcessor.BilinearProduct(
                    scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature), 
                    mv3, basisSignature.GpSignature
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, params IGaStorageMultivector<T>[] mvsList)
        {
            return mvsList.Skip(1).Aggregate(
                mvsList[0], 
                (mv1, mv2) => 
                    scalarProcessor.Gp(mv1, mv2, basisSignature)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageMultivector<T> mv)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv)
                : scalarProcessor.BilinearProduct(mv, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageKVector<T> mv)
        {
            //TODO: Add the Euclidean case
            return scalarProcessor.BilinearProduct(mv, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            return scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2, IGaStorageKVector<T> mv3)
        {
            return scalarProcessor.BilinearProduct(
                scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature), 
                mv3, basisSignature.GpSignature
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaSignature basisSignature)
        {
            return scalarProcessor.BilinearProduct(mv1, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Gp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaSignature basisSignature)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv1)
                : scalarProcessor.BilinearProduct(mv1, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> GpReverse<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EGpReverse(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.GpReverseSignature);
        }
    }
}
