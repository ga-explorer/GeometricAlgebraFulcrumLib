using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Outermorphisms
{
    public interface IGaStorageOutermorphismSparse<T> :
        IGaStorageOutermorphism<T>
    {
        public IGaGridEven<T> IdScalarDictionary { get; }
    }
}