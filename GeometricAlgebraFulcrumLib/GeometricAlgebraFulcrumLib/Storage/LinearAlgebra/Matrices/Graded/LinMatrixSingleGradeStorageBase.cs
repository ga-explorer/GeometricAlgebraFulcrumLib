using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded
{
    public abstract record LinMatrixSingleGradeStorageBase<T> :
        ILinMatrixSingleGradeStorage<T>
    {
        public uint Grade { get; }

        public abstract ILinMatrixStorage<T> MatrixStorage { get; }
        
        public int GradesCount 
            => 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return MatrixStorage.GetSparseCount1();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return MatrixStorage.GetSparseCount2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return MatrixStorage.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return MatrixStorage.GetScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetEmptyGrades(uint vSpaceDimension)
        {
            for (var grade = 0U; grade < Grade; grade++)
                yield return grade;

            for (var grade = Grade + 1; grade <= vSpaceDimension; grade++)
                yield return grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILinMatrixStorage<T>> GetMatrixStorages()
        {
            yield return MatrixStorage;
        }


        protected LinMatrixSingleGradeStorageBase(uint grade)
        {
            Grade = grade;
        }


        public abstract bool IsEmpty();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetMatrixStorage(uint grade)
        {
            return grade == Grade
                ? MatrixStorage
                : LinMatrixEmptyStorage<T>.EmptyStorage;
        }

        public abstract T GetScalar(uint grade, ulong index1, ulong index2);

        public abstract T GetScalar(GradeIndexPairRecord gradeKey);

        public abstract T GetScalar(uint grade, IndexPairRecord key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade == Grade;
        }

        public abstract bool ContainsIndex(uint grade, ulong index1, ulong index2);

        public abstract bool ContainsIndex(uint grade, IndexPairRecord key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetMatrixStorage(uint grade, out ILinMatrixStorage<T> matrixStorage)
        {
            if (grade == Grade)
            {
                matrixStorage = MatrixStorage;
                return true;
            }

            matrixStorage = null;
            return false;
        }

        public abstract bool TryGetScalar(uint grade, IndexPairRecord key, out T value);

        public abstract bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            return Grade;
        }

        public abstract IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords();

        public abstract IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords();

        public abstract ILinMatrixGradedStorage<T> GetCopy();

        public abstract ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping);

        public abstract ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> keyValueMapping);

        public abstract ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return gradeFilter(Grade)
                ? this
                : LinMatrixEmptyGradedStorage<T>.EmptyStorage;
        }

        public abstract ILinMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> keyFilter);

        public abstract ILinMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter);

        public abstract ILinMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter);

        public abstract ILinMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter);

        public abstract ILinMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> keyValueFilter);

        public abstract ILinMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter);

        public abstract ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping);

        public abstract ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeIndexToIndexMapping);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetRows(uint grade)
        {
            return grade == Grade
                ? MatrixStorage.GetRows()
                : Enumerable.Empty<IndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexLinVectorStorageRecord<T>> GetRows()
        {
            foreach (var (index, vectorStorage) in MatrixStorage.GetRows())
                yield return new GradeIndexLinVectorStorageRecord<T>(Grade, index, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns(uint grade)
        {
            return grade == Grade
                ? MatrixStorage.GetColumns()
                : Enumerable.Empty<IndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexLinVectorStorageRecord<T>> GetColumns()
        {
            foreach (var (index, vectorStorage) in MatrixStorage.GetColumns())
                yield return new GradeIndexLinVectorStorageRecord<T>(Grade, index, vectorStorage);
        }

        public abstract ILinMatrixGradedStorage<T> GetTranspose();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            if (MatrixStorage.TryGetCompactStorage(out var matrixStorage))
            {
                matrixGradedStorage = matrixStorage.CreateLinMatrixSingleGradeStorage(Grade);
                return true;
            }

            matrixGradedStorage = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeLinMatrixStorageRecord<T>> GetGradeStorageRecords()
        {
            yield return new GradeLinMatrixStorageRecord<T>(Grade, MatrixStorage);
        }
    }
}