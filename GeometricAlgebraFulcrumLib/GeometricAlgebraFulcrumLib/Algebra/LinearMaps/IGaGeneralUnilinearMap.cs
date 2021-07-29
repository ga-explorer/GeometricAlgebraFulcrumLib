using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearMaps
{
    public interface IGaGeneralUnilinearMap<T> :
        IGaUnilinearMap<T>
    {
        IGasMultivector<T> MapBasisBlade(ulong id);

        IGasMultivector<T> MapBasisBlade(uint grade, ulong index);
    }
}