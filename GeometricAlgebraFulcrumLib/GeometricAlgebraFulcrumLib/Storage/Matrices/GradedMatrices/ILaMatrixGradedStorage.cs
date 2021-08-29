using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices
{
    public interface ILaMatrixGradedStorage<T> :
        ILaMatrixStorage<T>, 
        ILaGradedStorage<T>
    {
        IEnumerable<ILaMatrixEvenStorage<T>> GetGrids();

        IEnumerable<GradeArrayStorageRecord<T>> GetGradeStorageRecords();

        IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords();

        IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords();

        ILaMatrixEvenStorage<T> GetEvenStorage(uint grade);

        T GetScalar(uint grade, ulong index1, ulong index2);

        T GetScalar(GradeIndexPairRecord gradeIndex);

        T GetScalar(uint grade, IndexPairRecord index);
        
        bool ContainsIndex(uint grade, ulong index1, ulong index2);
        
        bool ContainsIndex(uint grade, IndexPairRecord index);

        bool TryGetStorage(uint grade, out ILaMatrixEvenStorage<T> evenGrid);

        bool TryGetScalar(uint grade, IndexPairRecord index, out T scalar);

        bool TryGetScalar(uint grade, ulong index1, ulong index2, out T scalar);

        ILaMatrixGradedStorage<T> GetCopy();

        ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping);
        
        ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexScalarMapping);

        ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeIndexScalarMapping);

        ILaMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter);

        ILaMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter);

        ILaMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeIndexFilter);

        ILaMatrixGradedStorage<T> FilterByScalar(Func<T, bool> scalarFilter);

        ILaMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeScalarFilter);

        ILaMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexScalarFilter);

        ILaMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeIndexScalarFilter);
        
        bool TryGetCompactStorage(out ILaMatrixGradedStorage<T> gradedStorage);

        ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeIndexToEvenIndexMapping);

        ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeIndexToEvenIndexMapping);
    }
}