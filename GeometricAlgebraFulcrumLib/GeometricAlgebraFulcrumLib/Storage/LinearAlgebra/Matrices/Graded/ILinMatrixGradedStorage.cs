using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded
{
    public interface ILinMatrixGradedStorage<T> :
        ILinArrayStorage2D<T>, 
        ILinArrayGradedStorage<T>
    {
        IEnumerable<ILinMatrixStorage<T>> GetMatrixStorages();

        IEnumerable<GradeLinMatrixStorageRecord<T>> GetGradeStorageRecords();

        IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords();

        IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords();

        ILinMatrixStorage<T> GetMatrixStorage(uint grade);

        T GetScalar(uint grade, ulong index1, ulong index2);

        T GetScalar(GradeIndexPairRecord gradeIndex);

        T GetScalar(uint grade, IndexPairRecord index);
        
        bool ContainsIndex(uint grade, ulong index1, ulong index2);
        
        bool ContainsIndex(uint grade, IndexPairRecord index);

        bool TryGetMatrixStorage(uint grade, out ILinMatrixStorage<T> matrixStorage);

        bool TryGetScalar(uint grade, IndexPairRecord index, out T scalar);

        bool TryGetScalar(uint grade, ulong index1, ulong index2, out T scalar);

        ILinMatrixGradedStorage<T> GetCopy();

        ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping);
        
        ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexScalarMapping);

        ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeIndexScalarMapping);

        ILinMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter);

        ILinMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter);

        ILinMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeIndexFilter);

        ILinMatrixGradedStorage<T> FilterByScalar(Func<T, bool> scalarFilter);

        ILinMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeScalarFilter);

        ILinMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexScalarFilter);

        ILinMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeIndexScalarFilter);
        
        bool TryGetCompactStorage(out ILinMatrixGradedStorage<T> gradedStorage);

        ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping);

        ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeIndexToIndexMapping);

        IEnumerable<IndexLinVectorStorageRecord<T>> GetRows(uint grade);

        IEnumerable<GradeIndexLinVectorStorageRecord<T>> GetRows();

        IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns(uint grade);

        IEnumerable<GradeIndexLinVectorStorageRecord<T>> GetColumns();

        ILinMatrixGradedStorage<T> GetTranspose();
    }
}