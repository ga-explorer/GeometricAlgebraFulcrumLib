namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public interface ILaMatrixImmutableDenseEvenStorage<T> :
        ILaMatrixDenseEvenStorage<T>
    {
        public T this[int index1, int index2] { get; }

        public T this[ulong index1, ulong index2] { get; }
    }
}