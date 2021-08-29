using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public interface IGaGeneralUnilinearMap<T> :
        IGaUnilinearMap<T>
    {
        IGaMultivectorStorage<T> MapBasisBlade(ulong id);

        IGaMultivectorStorage<T> MapBasisBlade(uint grade, ulong index);
    }
}