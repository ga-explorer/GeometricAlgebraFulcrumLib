namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public abstract class LaMatrixImmutableDenseStorageBase<T> :
        LaMatrixDenseStorageBase<T>, ILaMatrixImmutableDenseEvenStorage<T>
    {
        public T this[int index1, int index2] 
            => GetScalar((ulong) index1, (ulong) index2);

        public T this[ulong index1, ulong index2] 
            => GetScalar(index1, index2);
    }
}