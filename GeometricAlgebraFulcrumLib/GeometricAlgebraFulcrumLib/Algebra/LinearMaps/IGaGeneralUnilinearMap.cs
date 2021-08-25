using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public interface IGaGeneralUnilinearMap<T> :
        IGaUnilinearMap<T>
    {
        IGaStorageMultivector<T> MapBasisBlade(ulong id);

        IGaStorageMultivector<T> MapBasisBlade(uint grade, ulong index);
    }
}