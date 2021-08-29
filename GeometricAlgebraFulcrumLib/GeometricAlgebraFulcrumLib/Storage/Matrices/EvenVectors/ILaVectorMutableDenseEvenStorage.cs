namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public interface ILaVectorMutableDenseEvenStorage<T> :
        ILaVectorDenseEvenStorage<T>
    {
        public T this[int index] { get; set; }

        public T this[ulong index] { get; set; }
    }
}