using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded
{
    public sealed record LinMatrixEmptyGradedStorage<T> :
        ILinMatrixGradedStorage<T>
    {
        public static LinMatrixEmptyGradedStorage<T> EmptyStorage { get; }
            = new LinMatrixEmptyGradedStorage<T>();


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

        public IEnumerable<uint> GetEmptyGrades(uint vSpaceDimension)
        {
            return (1U + vSpaceDimension).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILinMatrixStorage<T>> GetMatrixStorages()
        {
            return Enumerable.Empty<ILinMatrixStorage<T>>();
        }


        private LinMatrixEmptyGradedStorage()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> GetMatrixStorage(uint grade)
        {
            return LinMatrixEmptyStorage<T>.EmptyStorage;
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
        public bool TryGetMatrixStorage(uint grade, out ILinMatrixStorage<T> matrixStorage)
        {
            matrixStorage = null;
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
        public bool TryGetValue(uint key, out ILinMatrixStorage<T> value)
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
        public IEnumerable<GradeLinMatrixStorageRecord<T>> GetGradeStorageRecords()
        {
            return Enumerable.Empty<GradeLinMatrixStorageRecord<T>>();
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
        public ILinMatrixGradedStorage<T> GetCopy()
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return LinMatrixEmptyGradedStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return LinMatrixEmptyGradedStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return LinMatrixEmptyGradedStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> keyFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return EmptyStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return LinMatrixEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeIndexToIndexMapping)
        {
            return LinMatrixEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetRows(uint grade)
        {
            return Enumerable.Empty<IndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexLinVectorStorageRecord<T>> GetRows()
        {
            return Enumerable.Empty<GradeIndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IndexLinVectorStorageRecord<T>> GetColumns(uint grade)
        {
            return Enumerable.Empty<IndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GradeIndexLinVectorStorageRecord<T>> GetColumns()
        {
            return Enumerable.Empty<GradeIndexLinVectorStorageRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixGradedStorage<T> GetTranspose()
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            matrixGradedStorage = this;
            return false;
        }
    }
}