using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public sealed record LinVectorEmptySingleGradeStorage<T> :
        LinVectorSingleGradeStorageBase<T>
    {
        public override ILinVectorStorage<T> VectorStorage 
            => LinVectorEmptyStorage<T>.EmptyStorage;

        
        internal LinVectorEmptySingleGradeStorage(uint grade) 
            : base(grade)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, ulong index)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(GradeIndexRecord gradeKey)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            return Enumerable.Empty<GradeIndexRecord>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return Enumerable.Empty<GradeIndexScalarRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinVectorEmptySingleGradeStorage<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return new LinVectorEmptySingleGradeStorage<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LinVectorEmptySingleGradeStorage<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<T> ToVectorStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return LinVectorEmptyStorage<T>.EmptyStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            return this;
        }
    }
}