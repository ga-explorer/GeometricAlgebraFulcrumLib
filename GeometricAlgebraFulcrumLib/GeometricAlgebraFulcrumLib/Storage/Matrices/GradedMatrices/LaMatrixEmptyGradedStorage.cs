using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices
{
    public sealed record LaMatrixEmptyGradedStorage<T> :
        ILaMatrixGradedStorage<T>
    {
        public static LaMatrixEmptyGradedStorage<T> EmptyGrid { get; }
            = new LaMatrixEmptyGradedStorage<T>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 0;
        }

        public int GradesCount 
            => 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return Enumerable.Empty<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return Enumerable.Empty<uint>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILaMatrixEvenStorage<T>> GetGrids()
        {
            return Enumerable.Empty<ILaMatrixEvenStorage<T>>();
        }


        private LaMatrixEmptyGradedStorage()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> GetEvenStorage(uint grade)
        {
            return LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, ulong index1, ulong index2)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(GradeIndexPairRecord gradeKey)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, IndexPairRecord key)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, ulong index1, ulong index2)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(uint grade, IndexPairRecord key)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetStorage(uint grade, out ILaMatrixEvenStorage<T> evenGrid)
        {
            evenGrid = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, IndexPairRecord key, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint key, out ILaMatrixEvenStorage<T> value)
        {
            value = null;
            return false;
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
        public IEnumerable<GradeArrayStorageRecord<T>> GetGradeStorageRecords()
        {
            return Enumerable.Empty<GradeArrayStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords()
        {
            return Enumerable.Empty<GradeIndexPairRecord>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return Enumerable.Empty<GradeIndexPairScalarRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> GetCopy()
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return LaMatrixEmptyGradedStorage<T2>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return LaMatrixEmptyGradedStorage<T2>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return LaMatrixEmptyGradedStorage<T2>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> keyFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return EmptyGrid;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping)
        {
            return LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILaMatrixGradedStorage<T> gradedGrid)
        {
            gradedGrid = this;
            return false;
        }
    }
}