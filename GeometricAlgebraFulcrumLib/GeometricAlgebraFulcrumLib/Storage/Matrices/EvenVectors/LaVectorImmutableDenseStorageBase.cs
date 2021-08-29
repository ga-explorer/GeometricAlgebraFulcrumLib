namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public abstract class LaVectorImmutableDenseStorageBase<T> :
        LaVectorDenseStorageBase<T>, ILaVectorImmutableDenseEvenStorage<T>
    {
        public T this[int index] 
            => GetScalar((ulong) index);

        public T this[ulong index] 
            => GetScalar(index);
    }
}