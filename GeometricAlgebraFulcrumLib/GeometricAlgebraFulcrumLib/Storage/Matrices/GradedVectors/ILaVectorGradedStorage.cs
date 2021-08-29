using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors
{
    public interface ILaVectorGradedStorage<T> :
        ILaVectorStorage<T>, 
        ILaGradedStorage<T>
    {
        IEnumerable<ILaVectorEvenStorage<T>> GetEvenStorages();

        IEnumerable<GradeVectorStorageRecord<T>> GetGradeStorageRecords();

        IEnumerable<GradeIndexRecord> GetGradeIndexRecords();

        IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords();

        ILaVectorEvenStorage<T> GetEvenStorage(uint grade);

        T GetScalar(uint grade, ulong index);

        T GetScalar(GradeIndexRecord gradeIndex);
        
        bool ContainsIndex(uint grade, ulong index);

        bool TryGetEvenStorage(uint grade, out ILaVectorEvenStorage<T> storage);

        bool TryGetScalar(uint grade, ulong index, out T scalar);
        
        ILaVectorGradedStorage<T> GetCopy();

        ILaVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping);

        ILaVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexScalarMapping);

        ILaVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        ILaVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter);

        ILaVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter);

        ILaVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeIndexFilter);

        ILaVectorGradedStorage<T> FilterByScalar(Func<T, bool> scalarFilter);

        ILaVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeScalarFilter);

        ILaVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexScalarFilter);

        ILaVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeIndexScalarFilter);
        
        bool TryGetCompactStorage(out ILaVectorGradedStorage<T> storage);

        ILaVectorEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeIndexToEvenIndexMapping);
    }
}