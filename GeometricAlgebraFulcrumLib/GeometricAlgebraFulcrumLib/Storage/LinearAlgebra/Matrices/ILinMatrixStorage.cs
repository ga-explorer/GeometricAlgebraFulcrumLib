using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
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

        IEnumerable<RGaKvIndexPairRecord> GetIndices();

        IEnumerable<RGaKvIndexPairScalarRecord<T>> GetIndexScalarRecords();
        
        IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(ulong maxCount1, ulong maxCount2);

        IEnumerable<RGaKvIndexPairRecord> GetEmptyIndices(RGaKvIndexPairRecord maxCountPair);

        T GetScalar(ulong index1, ulong index2);

        T GetScalar(RGaKvIndexPairRecord index);

        bool ContainsIndex(ulong index1, ulong index2);

        bool ContainsIndex(RGaKvIndexPairRecord index);
        
        ulong GetMinIndex1();

        ulong GetMinIndex2();

        RGaKvIndexPairRecord GetMinIndex();

        ulong GetMaxIndex1();

        ulong GetMaxIndex2();

        RGaKvIndexPairRecord GetMaxIndex();

        bool TryGetScalar(ulong index1, ulong index2, out T scalar);

        bool TryGetScalar(RGaKvIndexPairRecord index, out T scalar);

        ILinMatrixStorage<T> GetCopy();
        
        ILinMatrixStorage<T> GetPermutation(Func<ulong, ulong, RGaKvIndexPairRecord> indexMapping);

        ILinMatrixStorage<T> GetPermutation(Func<RGaKvIndexPairRecord, RGaKvIndexPairRecord> indexMapping);

        ILinMatrixStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping);

        ILinMatrixStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexScalarMapping);

        ILinMatrixStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter);

        ILinMatrixStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexScalarFilter);

        ILinMatrixStorage<T> FilterByScalar(Func<T, bool> scalarFilter);

        ILinMatrixStorage<T> GetTranspose();
        
        bool TryGetCompactStorage(out ILinMatrixStorage<T> matrixStorage);
        
        ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, RGaGradeKvIndexPairRecord> indexToGradeIndexMapping);
        
        ILinMatrixGradedStorage<T> ToMatrixGradedStorage(Func<ulong, ulong, T, RGaGradeKvIndexPairScalarRecord<T>> indexScalarToGradeIndexScalarMapping);

        ILinVectorStorage<T> GetRow(ulong index1);

        ILinVectorStorage<T> GetColumn(ulong index2);

        IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows();

        IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows(Func<ulong, bool> rowIndexFilter);

        IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns();

        IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns(Func<ulong, bool> columnIndexFilter);

        ILinVectorStorage<T> CombineRows(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc);

        ILinVectorStorage<T> CombineRows(IEnumerable<RGaKvIndexScalarRecord<T>> rowIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc);

        ILinVectorStorage<T> CombineColumns(IReadOnlyList<T> scalarList, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc);

        ILinVectorStorage<T> CombineColumns(IEnumerable<RGaKvIndexScalarRecord<T>> columnIndexScalarRecords, Func<T, ILinVectorStorage<T>, ILinVectorStorage<T>> scalingFunc, Func<ILinVectorStorage<T>, ILinVectorStorage<T>, ILinVectorStorage<T>> reducingFunc);
    }
}