namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public interface ILinVectorImmutableDenseStorage<T> :
        ILinVectorDenseStorage<T>
    {
        public T this[int index] { get; }

        public T this[ulong index] { get; }
    }
}