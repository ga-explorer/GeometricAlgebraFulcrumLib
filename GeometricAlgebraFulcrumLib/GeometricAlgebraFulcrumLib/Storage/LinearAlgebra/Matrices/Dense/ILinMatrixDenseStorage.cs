using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
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

        ILinMatrixDenseStorage<T> GetDensePermutation(Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping);

        ILinMatrixDenseStorage<T> GetDensePermutation(Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping);

        IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList);

        IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList);
    }
}