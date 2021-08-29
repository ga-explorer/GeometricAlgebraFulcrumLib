using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices
{
    public abstract record LaMatrixSingleGradeStorageBase<T> :
        ILaMatrixSingleGradeStorage<T>
    {
        public uint Grade { get; }

        public abstract ILaMatrixEvenStorage<T> EvenStorage { get; }
        
        public int GradesCount 
            => 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return EvenStorage.GetSparseCount1();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return EvenStorage.GetSparseCount2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return EvenStorage.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return EvenStorage.GetScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILaMatrixEvenStorage<T>> GetGrids()
        {
            yield return EvenStorage;
        }


        protected LaMatrixSingleGradeStorageBase(uint grade)
        {
            Grade = grade;
        }


        public abstract bool IsEmpty();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> GetEvenStorage(uint grade)
        {
            return grade == Grade
                ? EvenStorage
                : LaMatrixEmptyStorage<T>.EmptyMatrix;
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
        public bool TryGetStorage(uint grade, out ILaMatrixEvenStorage<T> evenGrid)
        {
            if (grade == Grade)
            {
                evenGrid = EvenStorage;
                return true;
            }

            evenGrid = null;
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

        public abstract ILaMatrixGradedStorage<T> GetCopy();

        public abstract ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping);

        public abstract ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> keyValueMapping);

        public abstract ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return gradeFilter(Grade)
                ? this
                : LaMatrixEmptyGradedStorage<T>.EmptyGrid;
        }

        public abstract ILaMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> keyFilter);

        public abstract ILaMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter);

        public abstract ILaMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter);

        public abstract ILaMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter);

        public abstract ILaMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> keyValueFilter);

        public abstract ILaMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter);

        public abstract ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping);

        public abstract ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILaMatrixGradedStorage<T> gradedGrid)
        {
            if (EvenStorage.TryGetCompactStorage(out var evenGrid))
            {
                gradedGrid = evenGrid.CreateLaMatrixSingleGradeStorage(Grade);
                return true;
            }

            gradedGrid = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeArrayStorageRecord<T>> GetGradeStorageRecords()
        {
            yield return new GradeArrayStorageRecord<T>(Grade, EvenStorage);
        }
    }
}