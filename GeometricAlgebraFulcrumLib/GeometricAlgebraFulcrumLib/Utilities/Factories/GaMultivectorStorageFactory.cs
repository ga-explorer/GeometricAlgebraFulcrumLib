using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaMultivectorStorageFactory
    {
        public static IGaMultivectorStorage<T> CreateMultivector<T>(this ILaVectorEvenStorage<T> idScalarList)
        {
            idScalarList = idScalarList.GetCompactList();

            if (idScalarList.IsEmpty())
                return GaScalarStorage<T>.ZeroScalar;

            if (idScalarList.GetSparseCount() > 1)
                return GaMultivectorSparseStorage<T>.Create(idScalarList);

            var (id, scalar) = idScalarList.GetMinKeyValueRecord();

            if (id == 0)
                return GaScalarStorage<T>.Create(scalar);

            return GaMultivectorSparseStorage<T>.Create(idScalarList);
        }
    }
}