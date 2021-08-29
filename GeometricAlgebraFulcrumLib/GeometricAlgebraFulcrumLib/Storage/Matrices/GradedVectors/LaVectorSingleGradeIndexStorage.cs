using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors
{
    public sealed record LaVectorSingleGradeIndexStorage<T> :
        LaVectorSingleGradeStorageBase<T>
    {
        public ulong Key 
            => SingleKeyList.Index;

        public T Value 
            => SingleKeyList.Scalar;

        public ILaVectorSingleIndexEvenStorage<T> SingleKeyList { get; }

        public override ILaVectorEvenStorage<T> EvenStorage 
            => SingleKeyList;


        internal LaVectorSingleGradeIndexStorage(uint grade, [NotNull] T value) 
            : base(grade)
        {
            SingleKeyList = new LaVectorZeroIndexStorage<T>(value);
        }
        
        internal LaVectorSingleGradeIndexStorage(uint grade, ulong index, [NotNull] T value) 
            : base(grade)
        {
            SingleKeyList = index > 0
                ? new LaVectorSingleIndexStorage<T>(index, value)
                : new LaVectorZeroIndexStorage<T>(value);
        }
        
        internal LaVectorSingleGradeIndexStorage(uint grade, [NotNull] ILaVectorSingleIndexEvenStorage<T> singleKeyList) 
            : base(grade)
        {
            SingleKeyList = singleKeyList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, ulong index)
        {
            return grade == Grade && index == Key
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(GradeIndexRecord gradeKey)
        {
            var (grade, index) = gradeKey;

            return grade == Grade && index == Key
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index)
        {
            return grade == Grade && index == Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index, out T value)
        {
            if (grade == Grade && index == Key)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            yield return new GradeIndexRecord(Grade, Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            yield return new GradeIndexScalarRecord<T>(Grade, Key, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> GetCopy()
        {
            return new LaVectorSingleGradeIndexStorage<T>(Grade, Key, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaVectorSingleGradeIndexStorage<T2>(
                Grade, 
                Key, 
                valueMapping(Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return new LaVectorSingleGradeIndexStorage<T2>(
                Grade, 
                Key, 
                indexValueMapping(Key, Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LaVectorSingleGradeIndexStorage<T2>(
                Grade, 
                Key, 
                gradeKeyValueMapping(Grade, Key, Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            return indexFilter(Key)
                ? this
                : new LaVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter)
        {
            return gradeKeyFilter(Grade, Key)
                ? this
                : new LaVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this
                : new LaVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            return gradeValueFilter(Grade, Value)
                ? this
                : new LaVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            return indexValueFilter(Key, Value)
                ? this
                : new LaVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            return gradeKeyValueFilter(Grade, Key, Value)
                ? this
                : new LaVectorEmptySingleGradeStorage<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var index = gradeKeyToEvenKeyMapping(Grade, Key);
                
            return index == 0
                ? new LaVectorZeroIndexStorage<T>(Value)
                : new LaVectorSingleIndexStorage<T>(index, Value);
        }
    }
}