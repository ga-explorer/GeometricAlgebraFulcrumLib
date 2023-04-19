using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public sealed record LinVectorSingleGradeStorage<T> :
        LinVectorSingleGradeStorageBase<T>
    {
        public override ILinVectorStorage<T> VectorStorage { get; }


        internal LinVectorSingleGradeStorage(uint grade)
            : base(grade)
        {
            VectorStorage = LinVectorEmptyStorage<T>.EmptyStorage;
        }

        internal LinVectorSingleGradeStorage(uint grade, ILinVectorStorage<T> vectorStorage)
            : base(grade)
        {
            VectorStorage = vectorStorage.IsNullOrEmpty() 
                ? LinVectorEmptyStorage<T>.EmptyStorage
                : vectorStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return VectorStorage.IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, ulong index)
        {
            return grade == Grade
                ? VectorStorage.GetScalar(index)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(RGaGradeKvIndexRecord gradeKey)
        {
            return gradeKey.Grade == Grade
                ? VectorStorage.GetScalar(gradeKey.KvIndex)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index)
        {
            return grade == Grade
                ? VectorStorage.ContainsIndex(index)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index, out T value)
        {
            if (grade == Grade)
                return VectorStorage.TryGetScalar(index, out value);

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords()
        {
            return VectorStorage.GetIndices().Select(
                index => new RGaGradeKvIndexRecord(Grade, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return VectorStorage.GetIndexScalarRecords().Select(
                indexValueRecord =>
                {
                    var (index, value) = indexValueRecord;

                    return new RGaGradeKvIndexScalarRecord<T>(Grade, index, value);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> GetCopy()
        {
            return new LinVectorSingleGradeStorage<T>(
                Grade,
                VectorStorage.GetCopy()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinVectorSingleGradeStorage<T2>(
                Grade,
                VectorStorage.MapScalars(valueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return new LinVectorSingleGradeStorage<T2>(
                Grade,
                VectorStorage.MapScalars(indexValueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LinVectorSingleGradeStorage<T2>(
                Grade,
                VectorStorage.MapScalars((index, value) => 
                    gradeKeyValueMapping(Grade, index, value)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var vectorStorage = VectorStorage.FilterByIndex(indexFilter);

            return vectorStorage.IsEmpty()
                ? LinVectorEmptyGradedStorage<T>.EmptyStorage
                : new LinVectorSingleGradeStorage<T>(Grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter)
        {
            var vectorStorage = VectorStorage.FilterByIndex(
                index => gradeKeyFilter(Grade, index)
            );
            
            return vectorStorage.IsEmpty()
                ? LinVectorEmptyGradedStorage<T>.EmptyStorage
                : new LinVectorSingleGradeStorage<T>(Grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            var vectorStorage = VectorStorage.FilterByScalar(
                value => gradeValueFilter(Grade, value)
            );
            
            return vectorStorage.IsEmpty()
                ? LinVectorEmptyGradedStorage<T>.EmptyStorage
                : new LinVectorSingleGradeStorage<T>(Grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            var vectorStorage = VectorStorage.FilterByIndexScalar(indexValueFilter);
            
            return vectorStorage.IsEmpty()
                ? LinVectorEmptyGradedStorage<T>.EmptyStorage
                : new LinVectorSingleGradeStorage<T>(Grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            var vectorStorage = VectorStorage.FilterByIndexScalar(
                (index, value) => gradeKeyValueFilter(Grade, index, value)
            );
            
            return vectorStorage.IsEmpty()
                ? LinVectorEmptyGradedStorage<T>.EmptyStorage
                : new LinVectorSingleGradeStorage<T>(Grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var vectorStorage = VectorStorage.FilterByScalar(valueFilter);
            
            return vectorStorage.IsEmpty()
                ? LinVectorEmptyGradedStorage<T>.EmptyStorage
                : new LinVectorSingleGradeStorage<T>(Grade, vectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<T> ToVectorStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
        {
            return VectorStorage.GetPermutation(index => 
                gradeIndexToIndexMapping(Grade, index)
            );
        }
    }
}