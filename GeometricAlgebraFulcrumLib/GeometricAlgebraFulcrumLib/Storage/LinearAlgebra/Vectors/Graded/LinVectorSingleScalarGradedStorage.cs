using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public sealed record LinVectorSingleScalarGradedStorage<T> :
        LinVectorSingleGradeStorageBase<T>
    {
        public ulong Index 
            => SingleKeyList.Index;

        public T Scalar 
            => SingleKeyList.Scalar;

        public ILinVectorSingleScalarStorage<T> SingleKeyList { get; }

        public override ILinVectorStorage<T> VectorStorage 
            => SingleKeyList;


        internal LinVectorSingleScalarGradedStorage(uint grade, T value) 
            : base(grade)
        {
            SingleKeyList = new LinVectorSingleScalarDenseStorage<T>(value);
        }
        
        internal LinVectorSingleScalarGradedStorage(uint grade, ulong index, T value) 
            : base(grade)
        {
            SingleKeyList = index > 0
                ? new LinVectorSingleScalarSparseStorage<T>(index, value)
                : new LinVectorSingleScalarDenseStorage<T>(value);
        }
        
        internal LinVectorSingleScalarGradedStorage(uint grade, ILinVectorSingleScalarStorage<T> singleScalarVectorStorage) 
            : base(grade)
        {
            SingleKeyList = singleScalarVectorStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, ulong index)
        {
            return grade == Grade && index == Index
                ? Scalar
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(RGaGradeKvIndexRecord gradeKey)
        {
            var (grade, index) = gradeKey;

            return grade == Grade && index == Index
                ? Scalar
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index)
        {
            return grade == Grade && index == Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index, out T value)
        {
            if (grade == Grade && index == Index)
            {
                value = Scalar;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords()
        {
            yield return new RGaGradeKvIndexRecord(Grade, Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            yield return new RGaGradeKvIndexScalarRecord<T>(Grade, Index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> GetCopy()
        {
            return new LinVectorSingleScalarGradedStorage<T>(Grade, Index, Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinVectorSingleScalarGradedStorage<T2>(
                Grade, 
                Index, 
                valueMapping(Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return new LinVectorSingleScalarGradedStorage<T2>(
                Grade, 
                Index, 
                indexValueMapping(Index, Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LinVectorSingleScalarGradedStorage<T2>(
                Grade, 
                Index, 
                gradeKeyValueMapping(Grade, Index, Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return indexFilter(Index)
                ? this
                : new LinVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter)
        {
            return gradeKeyFilter(Grade, Index)
                ? this
                : new LinVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Scalar)
                ? this
                : new LinVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return gradeValueFilter(Grade, Scalar)
                ? this
                : new LinVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(Index, Scalar)
                ? this
                : new LinVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            return gradeKeyValueFilter(Grade, Index, Scalar)
                ? this
                : new LinVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<T> ToVectorStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            var index = gradeIndexToIndexMapping(Grade, Index);
                
            return index == 0
                ? new LinVectorSingleScalarDenseStorage<T>(Scalar)
                : new LinVectorSingleScalarSparseStorage<T>(index, Scalar);
        }
    }
}