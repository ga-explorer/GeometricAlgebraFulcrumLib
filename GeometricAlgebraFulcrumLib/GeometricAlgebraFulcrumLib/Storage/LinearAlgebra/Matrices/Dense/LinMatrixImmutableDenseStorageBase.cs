using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public abstract class LinMatrixImmutableDenseStorageBase<T> :
        LinMatrixDenseStorageBase<T>, ILinMatrixImmutableDenseStorage<T>
    {
        public T this[int index1, int index2] 
            => GetScalar((ulong) index1, (ulong) index2);

        public T this[ulong index1, ulong index2] 
            => GetScalar(index1, index2);

        public bool IsSquare 
            => Count1 == Count2;

        public abstract IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList);

        public abstract IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList);
    }
}