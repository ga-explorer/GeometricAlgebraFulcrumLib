using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Outermorphisms
{
    public interface IGaOutermorphismGradedStorage<T> :
        IGaOutermorphismStorage<T>
    {
        public ILaMatrixGradedStorage<T> IdScalarDictionary { get; }
    }
}