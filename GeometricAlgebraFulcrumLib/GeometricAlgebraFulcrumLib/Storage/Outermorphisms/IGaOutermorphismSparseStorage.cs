using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Outermorphisms
{
    public interface IGaOutermorphismSparseStorage<T> :
        IGaOutermorphismStorage<T>
    {
        public ILaMatrixEvenStorage<T> IdScalarDictionary { get; }
    }
}