using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors
{
    public abstract record LaVectorSingleGradeStorageBase<T> :
        ILaVectorSingleGradeStorage<T>
    {
        public uint Grade { get; }
        
        public abstract ILaVectorEvenStorage<T> EvenStorage { get; }
        
        public int GradesCount 
            => 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILaVectorEvenStorage<T>> GetEvenStorages()
        {
            yield return EvenStorage;
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


        protected LaVectorSingleGradeStorageBase(uint grade)
        {
            Grade = grade;
        }


        public abstract bool IsEmpty();

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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetEvenStorage(uint grade)
        {
            return grade == Grade
                ? EvenStorage
                : LaVectorEmptyStorage<T>.ZeroStorage;
        }

        public abstract T GetScalar(uint grade, ulong index);

        public abstract T GetScalar(GradeIndexRecord gradeKey);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade == Grade;
        }

        public abstract bool ContainsIndex(uint grade, ulong index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetEvenStorage(uint grade, out ILaVectorEvenStorage<T> evenList)
        {
            if (grade == Grade)
            {
                evenList = EvenStorage;
                return true;
            }

            evenList = null;
            return false;
        }

        public abstract bool TryGetScalar(uint grade, ulong index, out T value);

        public abstract IEnumerable<GradeIndexRecord> GetGradeIndexRecords();

        public abstract IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords();

        public abstract ILaVectorGradedStorage<T> GetCopy();

        public abstract ILaVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping);

        public abstract ILaVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping);

        public abstract ILaVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return gradeFilter(Grade)
                ? this
                : LaVectorEmptyGradedStorage<T>.EmptyList;
        }

        public abstract ILaVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter);

        public abstract ILaVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter);

        public abstract ILaVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter);

        public abstract ILaVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter);

        public abstract ILaVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter);

        public abstract ILaVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter);

        public abstract ILaVectorEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILaVectorGradedStorage<T> gradedList)
        {
            if (EvenStorage.TryGetCompactStorage(out var evenList))
            {
                gradedList = evenList.CreateLaVectorSingleGradeStorage(Grade);
                return true;
            }

            gradedList = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeVectorStorageRecord<T>> GetGradeStorageRecords()
        {
            yield return new GradeVectorStorageRecord<T>(Grade, EvenStorage);
        }
    }
}