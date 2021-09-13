using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices
{
    public interface ILinMatrixStorage<T> :
        ILinArrayStorage2D<T>
    {
        IEnumerable<ulong> GetIndices1();

        IEnumerable<ulong> GetIndices2();

        IEnumerable<IndexPairRecord> GetIndices();

        IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords();
        
        IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxCount1, ulong maxCount2);

        IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxCountPair);

        T GetScalar(ulong index1, ulong index2);

        T GetScalar(IndexPairRecord index);

        bool ContainsIndex(ulong index1, ulong index2);

        bool ContainsIndex(IndexPairRecord index);
        
        ulong GetMinIndex1();

        ulong GetMinIndex2();

        IndexPairRecord GetMinIndex();

        ulong GetMaxIndex1();

        ulong GetMaxIndex2();

        IndexPairRecord GetMaxIndex();

        bool TryGetScalar(ulong index1, ulong index2, out T scalar);

        bool TryGetScalar(IndexPairRecord index, out T scalar);

        ILinMatrixStorage<T> GetCopy();
        
        ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, IndexPairRecord> indexMapping);

        ILinMatrixStorage<T> GetPermutation(Func<IndexPairRecord, IndexPairRecord> indexMapping);

        ILinMatrixStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping);

        ILinMatrixStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexScalarMapping);

        ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter);

        ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexScalarFilter);

        ILinMatrixStorage<T> FilterByScalar(Func<T, bool> scalarFilter);

        ILinMatrixStorage<T> GetTranspose();
        
        bool TryGetCompactStorage(out ILinMatrixStorage<T> matrixStorage);
        
        ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> indexToGradeIndexMapping);
        
        ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, T, GradeIndexPairScalarRecord<T>> indexScalarToGradeIndexScalarMapping);

        ILinVectorStorage<T> GetRow(ulong index1);

        ILinVectorStorage<T> GetColumn(ulong index2);

        IEnumerable<IndexLinVectorStorageRecord<T>> GetRows();

        IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns();

        IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter);

        ILinVectorStorage<T> CombineRows(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc);

        ILinVectorStorage<T> CombineRows(IEnumerable<IndexScalarRecord<T>> rowIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc);

        ILinVectorStorage<T> CombineColumns(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc);

        ILinVectorStorage<T> CombineColumns(IEnumerable<IndexScalarRecord<T>> columnIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc);
    }
}