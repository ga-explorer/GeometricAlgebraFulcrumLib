using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded
{
    public sealed record LinMatrixSingleScalarGradedStorage<T> :
        LinMatrixSingleGradeStorageBase<T>
    {
        public ulong Index1 
            => SingleScalarMatrixStorage.Index1;

        public ulong Index2 
            => SingleScalarMatrixStorage.Index2;

        public T ScalarValue 
            => SingleScalarMatrixStorage.Scalar;

        public ILinMatrixSingleScalarStorage<T> SingleScalarMatrixStorage { get; }

        public override ILinMatrixStorage<T> MatrixStorage 
            => SingleScalarMatrixStorage;


        internal LinMatrixSingleScalarGradedStorage(uint grade, RGaKvIndexPairRecord index, T value) 
            : base(grade)
        {
            SingleScalarMatrixStorage = new LinMatrixSingleScalarSparseStorage<T>(index, value);
        }

        internal LinMatrixSingleScalarGradedStorage(uint grade, ulong index1, ulong index2, T value) 
            : base(grade)
        {
            SingleScalarMatrixStorage = new LinMatrixSingleScalarSparseStorage<T>(index1, index2, value);
        }
        
        internal LinMatrixSingleScalarGradedStorage(uint grade, ILinMatrixSingleScalarStorage<T> singleKeyMatrixStorage) 
            : base(grade)
        {
            SingleScalarMatrixStorage = singleKeyMatrixStorage;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, ulong index1, ulong index2)
        {
            return grade == Grade && index1 == Index1 && index2 == Index2
                ? ScalarValue
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(RGaGradeKvIndexPairRecord gradeKey)
        {
            var (grade, index1, index2) = gradeKey;

            return grade == Grade && index1 == Index1 && index2 == Index2
                ? ScalarValue
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, RGaKvIndexPairRecord index)
        {
            var (index1, index2) = index;

            return grade == Grade && index1 == Index1 && index2 == Index2
                ? ScalarValue
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index1, ulong index2)
        {
            return grade == Grade && index1 == Index1 && index2 == Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, RGaKvIndexPairRecord index)
        {
            return grade == Grade && index.KvIndex1 == Index1 && index.KvIndex2 == Index2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, RGaKvIndexPairRecord index, out T value)
        {
            if (grade == Grade && index.KvIndex1 == Index1 && index.KvIndex2 == Index2)
            {
                value = ScalarValue;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
        {
            if (grade == Grade && index1 == Index1 && index2 == Index2)
            {
                value = ScalarValue;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaGradeKvIndexPairRecord> GetGradeIndexRecords()
        {
            yield return new RGaGradeKvIndexPairRecord(Grade, Index1, Index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            yield return new RGaGradeKvIndexPairScalarRecord<T>(Grade, Index1, Index2, ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> GetCopy()
        {
            return new LinMatrixSingleScalarGradedStorage<T>(Grade, Index1, Index2, ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinMatrixSingleScalarGradedStorage<T2>(
                Grade, 
                Index1, 
                Index2,
                valueMapping(ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LinMatrixSingleScalarGradedStorage<T2>(
                Grade, 
                Index1, 
                Index2,
                indexValueMapping(Index1, Index2, ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LinMatrixSingleScalarGradedStorage<T2>(
                Grade, 
                Index1, 
                Index2,
                gradeKeyValueMapping(Grade, Index1, Index2, ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            return indexFilter(Index1, Index2)
                ? this
                : new LinMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            return gradeKeyFilter(Grade, Index1, Index2)
                ? this
                : new LinMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(ScalarValue)
                ? this
                : new LinMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return gradeValueFilter(Grade, ScalarValue)
                ? this
                : new LinMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(Index1, Index2, ScalarValue)
                ? this
                : new LinMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            return gradeKeyValueFilter(Grade, Index1, Index2, ScalarValue)
                ? this
                : new LinMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            var index1 = gradeIndexToIndexMapping(Grade, Index1);
            var index2 = gradeIndexToIndexMapping(Grade, Index2);
                
            return index1 == 0 && index2 == 0
                ? new LinMatrixSingleScalarDenseStorage<T>(ScalarValue)
                : new LinMatrixSingleScalarSparseStorage<T>(index1, index2, ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong, RGaKvIndexPairRecord> gradeIndexToIndexMapping)
        {
            var (index1, index2) = gradeIndexToIndexMapping(Grade, Index1, Index2);
                
            return index1 == 0 && index2 == 0
                ? new LinMatrixSingleScalarDenseStorage<T>(ScalarValue)
                : new LinMatrixSingleScalarSparseStorage<T>(index1, index2, ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixGradedStorage<T> GetTranspose()
        {
            return new LinMatrixSingleScalarGradedStorage<T>(
                Grade, 
                ScalarValue.CreateLinMatrixSingleScalarStorage(Index2, Index1)
            );
        }
    }
}