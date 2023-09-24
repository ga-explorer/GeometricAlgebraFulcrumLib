using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public interface ILinMatrixSingleScalarStorage<T> :
        ILinMatrixStorage<T>
    {
        public ulong Index1 { get; }

        public ulong Index2 { get; }

        public RGaKvIndexPairRecord Index { get; }

        public T Scalar { get; set; }
    }
}