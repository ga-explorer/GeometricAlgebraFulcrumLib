using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products.Orthonormal
{
    public static class GaProductOrtAcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult Acp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.AcpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static IGasMultivector<double> Acp(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1, IGasMultivector<double> mv2)
        {
            var composer = 
                new GaMultivectorFloat64Composer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddAcpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Acp<T>(this IGaSignature basisSignature, IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? mv1.EAcp(mv2)
                : mv1.BilinearProduct(mv2, basisSignature.AcpSignature);
        }
    }
}