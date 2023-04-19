using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public abstract record LinVectorSingleGradeStorageBase<T> :
        ILinVectorSingleGradeStorage<T>
    {
        public uint Grade { get; }
        
        public abstract ILinVectorStorage<T> VectorStorage { get; }
        
        public int GradesCount 
            => 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetEmptyGrades(uint vSpaceDimensions)
        {
            for (var grade = 0U; grade < Grade; grade++)
                yield return grade;

            for (var grade = Grade + 1; grade <= vSpaceDimensions; grade++)
                yield return grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ILinVectorStorage<T>> GetVectorStorages()
        {
            yield return VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return VectorStorage.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return VectorStorage.GetScalars();
        }


        protected LinVectorSingleGradeStorageBase(uint grade)
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
        public ILinVectorStorage<T> GetVectorStorage(uint grade)
        {
            return grade == Grade
                ? VectorStorage
                : LinVectorEmptyStorage<T>.EmptyStorage;
        }

        public abstract T GetScalar(uint grade, ulong index);

        public abstract T GetScalar(RGaGradeKvIndexRecord gradeKey);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade == Grade;
        }

        public abstract bool ContainsIndex(uint grade, ulong index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetVectorStorage(uint grade, out ILinVectorStorage<T> vectorStorage)
        {
            if (grade == Grade)
            {
                vectorStorage = VectorStorage;
                return true;
            }

            vectorStorage = null;
            return false;
        }

        public abstract bool TryGetScalar(uint grade, ulong index, out T value);

        public abstract IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords();

        public abstract IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords();

        public abstract ILinVectorGradedStorage<T> GetCopy();

        public abstract ILinVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping);

        public abstract ILinVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping);

        public abstract ILinVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return gradeFilter(Grade)
                ? this
                : LinVectorEmptyGradedStorage<T>.EmptyStorage;
        }

        public abstract ILinVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter);

        public abstract ILinVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter);

        public abstract ILinVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter);

        public abstract ILinVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter);

        public abstract ILinVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter);

        public abstract ILinVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter);

        public abstract ILinVectorStorage<T> ToVectorStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactStorage(out ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            if (VectorStorage.TryGetCompactStorage(out var vectorStorage))
            {
                vectorGradedStorage = vectorStorage.CreateLinVectorSingleGradeStorage(Grade);
                return true;
            }

            vectorGradedStorage = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaGradeLinVectorStorageRecord<T>> GetGradeStorageRecords()
        {
            yield return new GaGradeLinVectorStorageRecord<T>(Grade, VectorStorage);
        }
    }
}