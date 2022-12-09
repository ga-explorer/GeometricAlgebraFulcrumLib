using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageAcpOrtUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BasisBilinearProductResult Acp(this BasisBladeSet basisSet, ulong id1, ulong id2)
        {
            return new BasisBilinearProductResult(
                basisSet.AcpSign(id1, id2),
                id1 ^ id2
            );
        }

        public static IMultivectorStorage<double> Acp(this BasisBladeSet basisSet, IMultivectorStorage<double> mv1, IMultivectorStorage<double> mv2)
        {
            var composer =
                new MultivectorFloat64StorageComposer(basisSet);

            var idScalarPairs1 =
                mv1.GetIdScalarRecords();

            var idScalarPairs2 =
                mv2.GetLinVectorIdScalarStorage();

            foreach (var (id1, scalar1) in idScalarPairs1)
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                    composer.AddAcpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Acp<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BasisBladeSet basisSet, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            return basisSet.IsEuclidean
                ? scalarProcessor.EAcp(mv1, mv2)
                : scalarProcessor.BilinearProduct(mv1, mv2, basisSet.AcpSign);
        }
    }
}