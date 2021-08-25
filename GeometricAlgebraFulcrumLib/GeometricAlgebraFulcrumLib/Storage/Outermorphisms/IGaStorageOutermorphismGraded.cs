using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.Outermorphisms
{
    public interface IGaStorageOutermorphismGraded<T> :
        IGaStorageOutermorphism<T>
    {
        public IGaGridGraded<T> IdScalarDictionary { get; }
    }
}