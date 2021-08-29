using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors
{
    public sealed record LaVectorEmptyGradedStorage<T> :
        ILaVectorGradedStorage<T>
    {
        public static LaVectorEmptyGradedStorage<T> EmptyList { get; }
            = new LaVectorEmptyGradedStorage<T>();


        public int GradesCount 
            => 0;


        private LaVectorEmptyGradedStorage()
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return Enumerable.Empty<uint>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILaVectorEvenStorage<T>> GetEvenStorages()
        {
            return Enumerable.Empty<ILaVectorEvenStorage<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return Enumerable.Empty<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> GetEvenStorage(uint grade)
        {
            return LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, ulong index)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(GradeIndexRecord gradeKey)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, ulong index)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetEvenStorage(uint grade, out ILaVectorEvenStorage<T> evenList)
        {
            evenList = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, ulong index, out T value)
        {
            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            return Enumerable.Empty<GradeIndexRecord>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return Enumerable.Empty<GradeIndexScalarRecord<T>>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> GetCopy()
        {
            return EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return LaVectorEmptyGradedStorage<T2>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return LaVectorEmptyGradedStorage<T2>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return LaVectorEmptyGradedStorage<T2>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter)
        {
            return EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            return EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return LaVectorEmptyStorage<T>.ZeroStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILaVectorGradedStorage<T> gradedList)
        {
            gradedList = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeVectorStorageRecord<T>> GetGradeStorageRecords()
        {
            return Enumerable.Empty<GradeVectorStorageRecord<T>>();
        }
    }
}