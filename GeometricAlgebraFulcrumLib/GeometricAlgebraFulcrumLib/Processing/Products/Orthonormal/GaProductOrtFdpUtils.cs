using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal
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

        public static IGasMultivector<double> Fdp(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1, IGasMultivector<double> mv2)
        {
            var composer = 
                new GaMultivectorFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddFdpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Fdp<T>(this IGaSignature basisSignature, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv1.EFdp(mv2)
                : mv1.BilinearProduct(mv2, basisSignature.FdpSignature);
        }
    }
}