namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public interface ILaMatrixMutableDenseEvenStorage<T> :
        ILaMatrixDenseEvenStorage<T>
    {
        public T this[int index1, int index2] { get; set; }

        public T this[ulong index1, ulong index2] { get; set; }
    }
}