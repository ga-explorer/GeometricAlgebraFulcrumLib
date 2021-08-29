namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public interface ILaMatrixDenseEvenStorage<T> :
        ILaMatrixEvenStorage<T>
    {
        int Count1 { get; }

        int Count2 { get; }

        int Count { get; }
    }
}