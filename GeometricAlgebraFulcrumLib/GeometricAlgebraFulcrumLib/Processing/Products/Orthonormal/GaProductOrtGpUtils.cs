using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal
{
    public static class GaProductOrtGpUtils
    {
        public static IGasMultivector<double> Gp(this GaSignatureLookup basisSignature, IGasMultivector<double> mv)
        {
            var composer = 
                new GaMultivectorFloat64Composer(basisSignature);

            var idScalarPairs = 
                mv.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs)
            foreach (var (id2, scalar2) in idScalarPairs)
                composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGasMultivector<double> GpReverse(this GaSignatureLookup basisSignature, IGasMultivector<double> mv)
        {
            var composer = 
                new GaMultivectorFloat64Composer(basisSignature);

            var idScalarPairs = 
                mv.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs)
            foreach (var (id2, scalar2) in idScalarPairs)
                composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGasMultivector<double> Gp(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1, IGasMultivector<double> mv2)
        {
            var composer = 
                new GaMultivectorFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddGpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGasMultivector<double> GpReverse(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1, IGasMultivector<double> mv2)
        {
            var composer = 
                new GaMultivectorFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddGpReverseTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaSignature basisSignature, IGasMultivector<T> mv)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv.EGp()
                : mv.BilinearProduct(basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaSignature basisSignature, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv1.EGp(mv2)
                : mv1.BilinearProduct(mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaSignature basisSignature, IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGasMultivector<T> mv3)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv1.EGp(mv2).EGp(mv3)
                : mv1.BilinearProduct(mv2, basisSignature.GpSignature).BilinearProduct(mv3, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaSignature basisSignature, params IGasMultivector<T>[] mvsList)
        {
            return mvsList.Skip(1).Aggregate(
                mvsList[0], 
                basisSignature.Gp
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGaSignature basisSignature, IGasMultivector<T> mv)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv.EGpReverse()
                : mv.BilinearProduct(basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGaSignature basisSignature, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv1.EGpReverse(mv2)
                : mv1.BilinearProduct(mv2, basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> Gp<T>(this IGaSignature basisSignature, IGasKVector<T> mv)
        {
            //TODO: Add the Euclidean case
            return mv.BilinearProduct(basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> Gp<T>(this IGaSignature basisSignature, IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            return mv1.BilinearProduct(mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGaSignature basisSignature, IGasKVector<T> mv1, IGasKVector<T> mv2, IGasKVector<T> mv3)
        {
            return mv1.BilinearProduct(mv2, basisSignature.GpSignature).BilinearProduct(mv3, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGasMultivector<T> mv1, IGaSignature basisSignature)
        {
            return mv1.BilinearProduct(basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Gp<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv1.EGp(mv2)
                : mv1.BilinearProduct(mv2, basisSignature.GpSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGasMultivector<T> mv1, IGaSignature basisSignature)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv1.EGpReverse()
                : mv1.BilinearProduct(basisSignature.GpReverseSignature);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> GpReverse<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2, IGaSignature basisSignature)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv1.EGpReverse(mv2)
                : mv1.BilinearProduct(mv2, basisSignature.GpReverseSignature);
        }
    }
}
