namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public interface ILinMatrixImmutableDenseStorage<T> :
        ILinMatrixDenseStorage<T>
    {
        public T this[int index1, int index2] { get; }

        public T this[ulong index1, ulong index2] { get; }
    }
}