using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtFdpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult Fdp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.FdpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static IGaStorageMultivector<double> Fdp(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1, IGaStorageMultivector<double> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorFloat64(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2.GetKeyValueRecords())
                composer.AddFdpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Fdp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.EFdp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.FdpSignature);
        }
    }
}