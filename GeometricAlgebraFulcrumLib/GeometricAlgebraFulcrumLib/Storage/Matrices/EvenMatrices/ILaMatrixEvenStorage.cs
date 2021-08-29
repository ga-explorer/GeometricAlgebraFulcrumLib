using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public interface ILaMatrixEvenStorage<T> :
        ILaMatrixStorage<T>
    {
        IEnumerable<ulong> GetIndices1();

        IEnumerable<ulong> GetIndices2();

        IEnumerable<IndexPairRecord> GetIndices();

        IEnumerable<IndexPairScalarRecord<T>> GetIndexScalarRecords();
        
        IEnumerable<IndexPairRecord> GetEmptyIndices(ulong maxIndex1, ulong maxIndex2);

        IEnumerable<IndexPairRecord> GetEmptyIndices(IndexPairRecord maxIndex);

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

        ILaMatrixEvenStorage<T> GetCopy();
        
        ILaMatrixEvenStorage<T> MapIndices(Func<ulong, ulong, IndexPairRecord> indexMapping);

        ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping);

        ILaMatrixEvenStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexScalarMapping);

        ILaMatrixEvenStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter);

        ILaMatrixEvenStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexScalarFilter);

        ILaMatrixEvenStorage<T> FilterByScalar(Func<T, bool> scalarFilter);

        ILaMatrixEvenStorage<T> Transpose();
        
        bool TryGetCompactStorage(out ILaMatrixEvenStorage<T> evenStorage);
        
        ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, GradeIndexPairRecord> evenIndexToGradeIndexMapping);
        
        ILaMatrixGradedStorage<T> ToGradedStorage(Func<ulong, ulong, T, GradeIndexPairScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping);

        ILaVectorEvenStorage<T> GetRow(ulong index1);

        ILaVectorEvenStorage<T> GetColumn(ulong index2);
    }
}