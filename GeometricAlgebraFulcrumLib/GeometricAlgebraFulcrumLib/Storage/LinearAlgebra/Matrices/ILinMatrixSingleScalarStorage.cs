using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public interface ILinMatrixSingleScalarStorage<T> :
        ILinMatrixStorage<T>
    {
        public ulong Index1 { get; }

        public ulong Index2 { get; }

        public IndexPairRecord Index { get; }

        public T Scalar { get; set; }
    }
}