using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Orthonormal
{
    public static class GaProductOrtCpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult Cp(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                signature.CpSignature(id1, id2), 
                id1 ^ id2
            );
        }
        
        public static IGaMultivectorStorage<double> Cp(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorFloat64StorageComposer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                composer.AddCpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Cp<T>(this IScalarProcessor<T> scalarProcessor, IGaSignature basisSignature, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return basisSignature is GaSignatureEuclidean
                ? scalarProcessor.ECp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSignature.CpSignature);
        }


    }
}