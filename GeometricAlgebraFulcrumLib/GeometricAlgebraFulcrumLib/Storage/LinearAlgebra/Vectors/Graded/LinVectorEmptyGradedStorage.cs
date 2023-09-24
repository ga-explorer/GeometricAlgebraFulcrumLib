using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public sealed record LinVectorEmptyGradedStorage<T> :
        ILinVectorGradedStorage<T>
    {
        public static LinVectorEmptyGradedStorage<T> EmptyStorage { get; }
            = new LinVectorEmptyGradedStorage<T>();


        public int GradesCount 
            => 0;


        private LinVectorEmptyGradedStorage()
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return Enumerable.Empty<uint>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetEmptyGrades(uint vSpaceDimensions)
        {
            return (1 + vSpaceDimensions).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILinVectorStorage<T>> GetVectorStorages()
        {
            return Enumerable.Empty<ILinVectorStorage<T>>();
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
        public ILinVectorStorage<T> GetVectorStorage(uint grade)
        {
            return LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(uint grade, ulong index)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(RGaGradeKvIndexRecord gradeKey)
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
        public bool TryGetVectorStorage(uint grade, out ILinVectorStorage<T> vectorStorage)
        {
            vectorStorage = null;
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
        public IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords()
        {
            return Enumerable.Empty<RGaGradeKvIndexRecord>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return Enumerable.Empty<RGaGradeKvIndexScalarRecord<T>>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> GetCopy()
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return LinVectorEmptyGradedStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return LinVectorEmptyGradedStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return LinVectorEmptyGradedStorage<T2>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> ToVectorStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            vectorGradedStorage = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaGradeLinVectorStorageRecord<T>> GetGradeStorageRecords()
        {
            return Enumerable.Empty<GaGradeLinVectorStorageRecord<T>>();
        }
    }
}