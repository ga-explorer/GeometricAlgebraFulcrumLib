namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public interface ILaMatrixSingleIndexEvenStorage<T> :
        ILaMatrixSparseEvenStorage<T>
    {
        public ulong Index1 { get; }

        public ulong Index2 { get; }

        public IndexPairRecord Index { get; }

        public T Scalar { get; set; }
    }
}