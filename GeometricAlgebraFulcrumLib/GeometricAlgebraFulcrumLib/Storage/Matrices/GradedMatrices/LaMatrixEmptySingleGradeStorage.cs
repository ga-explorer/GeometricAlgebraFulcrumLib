using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices
{
    public sealed record LaMatrixEmptySingleGradeStorage<T> :
        LaMatrixSingleGradeStorageBase<T>
    {
        public override ILaMatrixEvenStorage<T> EvenStorage
            => LaMatrixEmptyStorage<T>.EmptyMatrix;


        internal LaMatrixEmptySingleGradeStorage(uint grade) 
            : base(grade)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, ulong index1, ulong index2)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(GradeIndexPairRecord gradeKey)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, IndexPairRecord key)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index1, ulong index2)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, IndexPairRecord key)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, IndexPairRecord key, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords()
        {
            return Enumerable.Empty<GradeIndexPairRecord>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return Enumerable.Empty<GradeIndexPairScalarRecord<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaMatrixSingleGradeStorage<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return new LaMatrixSingleGradeStorage<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LaMatrixSingleGradeStorage<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> keyFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return LaMatrixEmptyStorage<T>.EmptyMatrix;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping)
        {
            return LaMatrixEmptyStorage<T>.EmptyMatrix;
        }
    }
}