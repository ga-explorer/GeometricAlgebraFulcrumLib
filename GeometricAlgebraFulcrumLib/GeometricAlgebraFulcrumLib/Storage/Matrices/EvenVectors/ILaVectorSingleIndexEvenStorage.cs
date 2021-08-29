namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public interface ILaVectorSingleIndexEvenStorage<T> :
        ILaVectorSparseEvenStorage<T>
    {
        public ulong Index { get; }

        public T Scalar { get; set; }
    }
}