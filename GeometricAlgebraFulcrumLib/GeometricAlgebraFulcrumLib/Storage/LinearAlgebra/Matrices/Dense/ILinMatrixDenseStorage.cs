using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public interface ILinMatrixDenseStorage<T> :
        ILinMatrixStorage<T>
    {
        int Count1 { get; }

        int Count2 { get; }

        int Count { get; }

        bool IsSquare { get; }

        ILinMatrixDenseStorage<T> GetDensePermutation(Func<ulong, ulong, IndexPairRecord> indexMapping);

        ILinMatrixDenseStorage<T> GetDensePermutation(Func<IndexPairRecord, IndexPairRecord> indexMapping);

        IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList);
    }
}