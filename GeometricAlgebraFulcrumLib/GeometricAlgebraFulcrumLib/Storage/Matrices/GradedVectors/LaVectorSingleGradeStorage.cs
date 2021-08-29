using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors
{
    public sealed record LaVectorSingleGradeStorage<T> :
        LaVectorSingleGradeStorageBase<T>
    {
        public override ILaVectorEvenStorage<T> EvenStorage { get; }


        internal LaVectorSingleGradeStorage(uint grade)
            : base(grade)
        {
            EvenStorage = LaVectorEmptyStorage<T>.ZeroStorage;
        }

        internal LaVectorSingleGradeStorage(uint grade, [NotNull] ILaVectorEvenStorage<T> evenList)
            : base(grade)
        {
            EvenStorage = evenList.IsNullOrEmpty() 
                ? LaVectorEmptyStorage<T>.ZeroStorage
                : evenList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return EvenStorage.IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(uint grade, ulong index)
        {
            return grade == Grade
                ? EvenStorage.GetScalar(index)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(GradeIndexRecord gradeKey)
        {
            return gradeKey.Grade == Grade
                ? EvenStorage.GetScalar(gradeKey.Index)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsIndex(uint grade, ulong index)
        {
            return grade == Grade
                ? EvenStorage.ContainsIndex(index)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetScalar(uint grade, ulong index, out T value)
        {
            if (grade == Grade)
                return EvenStorage.TryGetScalar(index, out value);

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexRecord> GetGradeIndexRecords()
        {
            return EvenStorage.GetIndices().Select(
                index => new GradeIndexRecord(Grade, index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GradeIndexScalarRecord<T>> GetGradeIndexScalarRecords()
        {
            return EvenStorage.GetIndexScalarRecords().Select(
                indexValueRecord =>
                {
                    var (index, value) = indexValueRecord;

                    return new GradeIndexScalarRecord<T>(Grade, index, value);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> GetCopy()
        {
            return new LaVectorSingleGradeStorage<T>(
                Grade,
                EvenStorage.GetCopy()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LaVectorSingleGradeStorage<T2>(
                Grade,
                EvenStorage.MapScalars(valueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
        {
            return new LaVectorSingleGradeStorage<T2>(
                Grade,
                EvenStorage.MapScalars(indexValueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return new LaVectorSingleGradeStorage<T2>(
                Grade,
                EvenStorage.MapScalars((index, value) => 
                    gradeKeyValueMapping(Grade, index, value)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var evenList = EvenStorage.FilterByIndex(indexFilter);

            return evenList.IsEmpty()
                ? LaVectorEmptyGradedStorage<T>.EmptyList
                : new LaVectorSingleGradeStorage<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter)
        {
            var evenList = EvenStorage.FilterByIndex(
                index => gradeKeyFilter(Grade, index)
            );
            
            return evenList.IsEmpty()
                ? LaVectorEmptyGradedStorage<T>.EmptyList
                : new LaVectorSingleGradeStorage<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
        {
            var evenList = EvenStorage.FilterByScalar(
                value => gradeValueFilter(Grade, value)
            );
            
            return evenList.IsEmpty()
                ? LaVectorEmptyGradedStorage<T>.EmptyList
                : new LaVectorSingleGradeStorage<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            var evenList = EvenStorage.FilterByIndexScalar(indexValueFilter);
            
            return evenList.IsEmpty()
                ? LaVectorEmptyGradedStorage<T>.EmptyList
                : new LaVectorSingleGradeStorage<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            var evenList = EvenStorage.FilterByIndexScalar(
                (index, value) => gradeKeyValueFilter(Grade, index, value)
            );
            
            return evenList.IsEmpty()
                ? LaVectorEmptyGradedStorage<T>.EmptyList
                : new LaVectorSingleGradeStorage<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var evenList = EvenStorage.FilterByScalar(valueFilter);
            
            return evenList.IsEmpty()
                ? LaVectorEmptyGradedStorage<T>.EmptyList
                : new LaVectorSingleGradeStorage<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<T> ToEvenStorage(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return EvenStorage.MapIndices(index => 
                gradeKeyToEvenKeyMapping(Grade, index)
            );
        }
    }
}