using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices
{
    public sealed record LaMatrixSingleGradeIndexStorage<T> :
        LaMatrixSingleGradeStorageBase<T>
    {
        public ulong Key1 
            => SingleKeyGrid.Index1;

        public ulong Key2 
            => SingleKeyGrid.Index2;

        public T Value 
            => SingleKeyGrid.Scalar;

        public ILaMatrixSingleIndexEvenStorage<T> SingleKeyGrid { get; }

        public override ILaMatrixEvenStorage<T> EvenStorage 
            => SingleKeyGrid;


        internal LaMatrixSingleGradeIndexStorage(uint grade, IndexPairRecord index, [NotNull] T value) 
            : base(grade)
        {
            SingleKeyGrid = new LaMatrixSingleIndexStorage<T>(index, value);
        }

        internal LaMatrixSingleGradeIndexStorage(uint grade, ulong index1, ulong index2, [NotNull] T value) 
            : base(grade)
        {
            SingleKeyGrid = new LaMatrixSingleIndexStorage<T>(index1, index2, value);
        }
        
        internal LaMatrixSingleGradeIndexStorage(uint grade, [NotNull] ILaMatrixSingleIndexEvenStorage<T> singleKeyGrid) 
            : base(grade)
        {
            SingleKeyGrid = singleKeyGrid;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, ulong index1, ulong index2)
        {
            return grade == Grade && index1 == Key1 && index2 == Key2
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(GradeIndexPairRecord gradeKey)
        {
            var (grade, index1, index2) = gradeKey;

            return grade == Grade && index1 == Key1 && index2 == Key2
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, IndexPairRecord index)
        {
            var (index1, index2) = index;

            return grade == Grade && index1 == Key1 && index2 == Key2
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index1, ulong index2)
        {
            return grade == Grade && index1 == Key1 && index2 == Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, IndexPairRecord index)
        {
            return grade == Grade && index.Index1 == Key1 && index.Index2 == Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, IndexPairRecord index, out T value)
        {
            if (grade == Grade && index.Index1 == Key1 && index.Index2 == Key2)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
        {
            if (grade == Grade && index1 == Key1 && index2 == Key2)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexPairRecord> GetGradeIndexRecords()
        {
            yield return new GradeIndexPairRecord(Grade, Key1, Key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            yield return new GradeIndexPairScalarRecord<T>(Grade, Key1, Key2, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> GetCopy()
        {
            return new LaMatrixSingleGradeIndexStorage<T>(Grade, Key1, Key2, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaMatrixSingleGradeIndexStorage<T2>(
                Grade, 
                Key1, 
                Key2,
                valueMapping(Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
        {
            return new LaMatrixSingleGradeIndexStorage<T2>(
                Grade, 
                Key1, 
                Key2,
                indexValueMapping(Key1, Key2, Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LaMatrixSingleGradeIndexStorage<T2>(
                Grade, 
                Key1, 
                Key2,
                gradeKeyValueMapping(Grade, Key1, Key2, Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
        {
            return indexFilter(Key1, Key2)
                ? this
                : new LaMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            return gradeKeyFilter(Grade, Key1, Key2)
                ? this
                : new LaMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this
                : new LaMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return gradeValueFilter(Grade, Value)
                ? this
                : new LaMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(Key1, Key2, Value)
                ? this
                : new LaMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            return gradeKeyValueFilter(Grade, Key1, Key2, Value)
                ? this
                : new LaMatrixEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var index1 = gradeKeyToEvenKeyMapping(Grade, Key1);
            var index2 = gradeKeyToEvenKeyMapping(Grade, Key2);
                
            return index1 == 0 && index2 == 0
                ? new LaMatrixZeroIndexStorage<T>(Value)
                : new LaMatrixSingleIndexStorage<T>(index1, index2, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong, IndexPairRecord> gradeKeyToEvenKeyMapping)
        {
            var (index1, index2) = gradeKeyToEvenKeyMapping(Grade, Key1, Key2);
                
            return index1 == 0 && index2 == 0
                ? new LaMatrixZeroIndexStorage<T>(Value)
                : new LaMatrixSingleIndexStorage<T>(index1, index2, Value);
        }
    }
}