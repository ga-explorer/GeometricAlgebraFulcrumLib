using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public interface ILinVectorGradedStorage<T> :
        ILinArrayStorage1D<T>, 
        ILinArrayGradedStorage<T>
    {
        IEnumerable<ILinVectorStorage<T>> GetVectorStorages();

        IEnumerable<GaGradeLinVectorStorageRecord<T>> GetGradeStorageRecords();

        IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords();

        IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords();

        ILinVectorStorage<T> GetVectorStorage(uint grade);

        T GetScalar(uint grade, ulong index);

        T GetScalar(RGaGradeKvIndexRecord gradeIndex);
        
        bool ContainsIndex(uint grade, ulong index);

        bool TryGetVectorStorage(uint grade, out ILinVectorStorage<T> storage);

        bool TryGetScalar(uint grade, ulong index, out T scalar);
        
        ILinVectorGradedStorage<T> GetCopy();

        ILinVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping);

        ILinVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexScalarMapping);

        ILinVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        ILinVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter);

        ILinVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter);

        ILinVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeIndexFilter);

        ILinVectorGradedStorage<T> FilterByScalar(Func<T, bool> scalarFilter);

        ILinVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeScalarFilter);

        ILinVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexScalarFilter);

        ILinVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeIndexScalarFilter);
        
        bool TryGetCompactStorage(out ILinVectorGradedStorage<T> storage);

        ILinVectorStorage<T> ToVectorStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping);
    }
}