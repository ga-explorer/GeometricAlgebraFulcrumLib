namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public interface ILaVectorImmutableDenseEvenStorage<T> :
        ILaVectorDenseEvenStorage<T>
    {
        public T this[int index] { get; }

        public T this[ulong index] { get; }
    }
}