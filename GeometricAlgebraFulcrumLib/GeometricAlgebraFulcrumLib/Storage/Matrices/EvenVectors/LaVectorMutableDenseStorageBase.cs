namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public abstract class LaVectorMutableDenseStorageBase<T> :
        LaVectorDenseStorageBase<T>, 
        ILaVectorMutableDenseEvenStorage<T>
    {
        public abstract T this[int index] { get; set; }

        public abstract T this[ulong index] { get; set; }
    }
}