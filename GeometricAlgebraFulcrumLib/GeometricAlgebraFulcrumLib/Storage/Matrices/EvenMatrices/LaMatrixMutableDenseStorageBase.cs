namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public abstract class LaMatrixMutableDenseStorageBase<T> :
        LaMatrixDenseStorageBase<T>, ILaMatrixMutableDenseEvenStorage<T>
    {
        public abstract T this[int index1, int index2] { get; set; }

        public abstract T this[ulong index1, ulong index2] { get; set; }
    }
}