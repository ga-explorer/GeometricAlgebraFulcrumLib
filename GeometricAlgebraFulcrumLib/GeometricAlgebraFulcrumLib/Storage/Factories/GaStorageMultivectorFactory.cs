using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageMultivectorFactory
    {
        public static IGaStorageMultivector<T> CreateMultivector<T>(this IGaListEven<T> idScalarList)
        {
            idScalarList = idScalarList.GetCompactList();

            if (idScalarList.IsEmpty())
                return GaStorageScalar<T>.ZeroScalar;

            if (idScalarList.GetSparseCount() > 1)
                return GaStorageMultivectorSparse<T>.Create(idScalarList);

            var (id, scalar) = idScalarList.GetMinKeyValueRecord();

            if (id == 0)
                return GaStorageScalar<T>.Create(scalar);

            return GaStorageMultivectorSparse<T>.Create(idScalarList);
        }
    }
}